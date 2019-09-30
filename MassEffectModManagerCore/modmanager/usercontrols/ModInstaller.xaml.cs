﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using MassEffectModManager;
using MassEffectModManagerCore.GameDirectories;
using MassEffectModManagerCore.gamefileformats.sfar;
using MassEffectModManagerCore.modmanager.helpers;
using MassEffectModManagerCore.modmanager.objects;
using MassEffectModManagerCore.ui;
using Serilog;

namespace MassEffectModManagerCore.modmanager.usercontrols
{
    /// <summary>
    /// Interaction logic for ModInstaller.xaml
    /// </summary>
    public partial class ModInstaller : UserControl, INotifyPropertyChanged
    {
        public ObservableCollectionExtended<object> AlternateOptions { get; } = new ObservableCollectionExtended<object>();

        public bool InstallationSucceeded { get; private set; }
        private const int PERCENT_REFRESH_COOLDOWN = 125;
        public bool ModIsInstalling { get; set; }
        public ModInstaller(Mod modBeingInstalled, GameTarget gameTarget)
        {
            DataContext = this;
            lastPercentUpdateTime = DateTime.Now;
            this.ModBeingInstalled = modBeingInstalled;
            this.gameTarget = gameTarget;
            Action = $"Preparing to install";
            InitializeComponent();
        }


        public Mod ModBeingInstalled { get; }
        private GameTarget gameTarget;
        private DateTime lastPercentUpdateTime;
        public bool InstallationCancelled;
        private const int INSTALL_SUCCESSFUL = 1;
        private const int INSTALL_FAILED_USER_CANCELED_MISSING_MODULES = 2;

        public string Action { get; set; }
        public int Percent { get; set; }
        public Visibility PercentVisibility { get; set; } = Visibility.Collapsed;


        public event EventHandler Close;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnClosing(EventArgs e)
        {
            EventHandler handler = Close;
            handler?.Invoke(this, e);
        }

        public void PrepareToInstallMod()
        {
            //Detect incompatible DLC


            //Detect outdated DLC


            //See if any alternate options are available and display them even if they are all autos
            foreach (var job in ModBeingInstalled.InstallationJobs)
            {
                AlternateOptions.AddRange(job.AlternateDLCs);
                AlternateOptions.AddRange(job.AlternateFiles);
            }

            foreach (object o in AlternateOptions)
            {
                if (o is AlternateDLC altdlc)
                {
                    altdlc.SetupInitialSelection(gameTarget);
                }
                else if (o is AlternateFile altfile)
                {
                    altfile.SetupInitialSelection(gameTarget);
                }
            }

            if (AlternateOptions.Count == 0)
            {
                //Just start installing mod
                BeginInstallingMod();
            }
        }

        private void BeginInstallingMod()
        {
            ModIsInstalling = true;
            Log.Information($"BeginInstallingMod(): {ModBeingInstalled.ModName}");
            NamedBackgroundWorker bw = new NamedBackgroundWorker($"ModInstaller-{ModBeingInstalled.ModName}");
            bw.WorkerReportsProgress = true;
            bw.DoWork += InstallModBackgroundThread;
            bw.RunWorkerCompleted += ModInstallationCompleted;
            //bw.ProgressChanged += ModProgressChanged;
            bw.RunWorkerAsync();
        }


        private void InstallModBackgroundThread(object sender, DoWorkEventArgs e)
        {
            Log.Information($"Mod Installer Background thread starting");
            var installationJobs = ModBeingInstalled.InstallationJobs;
            var gamePath = gameTarget.TargetPath;
            var gameDLCPath = MEDirectories.DLCPath(gameTarget);

            if (!PrecheckOfficialHeaders(gameDLCPath, installationJobs))
            {
                e.Result = INSTALL_FAILED_USER_CANCELED_MISSING_MODULES;
                return;
            }
            //todo: If statment on this
            Utilities.InstallBinkBypass(gameTarget); //Always install binkw32, don't bother checking if it is already ASI version.

            //Prepare queues
            (Dictionary<ModJob, Dictionary<string, string>> unpackedJobMappings, List<(ModJob job, string sfarPath)> sfarJobs) installationQueues = ModBeingInstalled.GetInstallationQueues(gameTarget);

            Action = $"Installing";
            PercentVisibility = Visibility.Visible;
            Percent = 0;

            int numdone = 0;



            //Calculate number of installation tasks beforehand - we won't know SFAR cou
            int numFilesToInstall = installationQueues.unpackedJobMappings.Select(x => x.Value.Count).Sum();
            numFilesToInstall += installationQueues.sfarJobs.Select(x => x.job.FilesToInstall.Count).Sum();

            void FileInstalledCallback()
            {
                numdone++;
                var now = DateTime.Now;
                if ((now - lastPercentUpdateTime).Milliseconds > PERCENT_REFRESH_COOLDOWN)
                {
                    //Don't update UI too often. Once per second is enough.
                    Percent = (int)(numdone * 100.0 / numFilesToInstall);
                    lastPercentUpdateTime = now;
                }
            }

            //Stage: Unpacked file installation
            foreach (var unpackedQueue in installationQueues.unpackedJobMappings)
            {
                //Todo: Implement unpacked copy queue
                //CopyDir.CopyFiles_ProgressBar(unpackedQueue.)
                Dictionary<string, string> fullPathMappingDisk = new Dictionary<string, string>();
                Dictionary<int, string> fullPathMappingArchive = new Dictionary<int, string>();
                foreach (var originalMapping in unpackedQueue.Value)
                {
                    //always unpacked
                    //if (unpackedQueue.Key == ModJob.JobHeader.CUSTOMDLC || unpackedQueue.Key == ModJob.JobHeader.BALANCE_CHANGES || unpackedQueue.Key == ModJob.JobHeader.BASEGAME)
                    //{
                    string sourceFile;
                    //todo: maybe handle null parameters as nothing
                    if (unpackedQueue.Key.JobDirectory == null)
                    {
                        sourceFile = FilesystemInterposer.PathCombine(ModBeingInstalled.IsInArchive, ModBeingInstalled.ModPath, originalMapping.Value);
                    }
                    else
                    {
                        sourceFile = FilesystemInterposer.PathCombine(ModBeingInstalled.IsInArchive, ModBeingInstalled.ModPath, unpackedQueue.Key.JobDirectory, originalMapping.Value);
                    }

                    var destFile = Path.Combine(unpackedQueue.Key.Header == ModJob.JobHeader.CUSTOMDLC ? MEDirectories.DLCPath(gameTarget) : gameTarget.TargetPath, originalMapping.Key); //official
                    if (ModBeingInstalled.IsInArchive)
                    {
                        int archiveIndex = ModBeingInstalled.Archive.ArchiveFileNames.IndexOf(sourceFile);
                        fullPathMappingArchive[archiveIndex] = destFile; //used for extraction indexing
                        fullPathMappingDisk[sourceFile] = destFile; //used for redirection
                    }
                    else
                    {
                        fullPathMappingDisk[sourceFile] = destFile;
                    }
                    //}
                }

                if (!ModBeingInstalled.IsInArchive)
                {
                    //Direct copy
                    CopyDir.CopyFiles_ProgressBar(fullPathMappingDisk, FileInstalledCallback);
                }
                else
                {
                    //Extraction to destination
                    string installationRedirectCallback(string inArchivePath)
                    {
                        var redirectedPath = fullPathMappingDisk[inArchivePath];
                        //Debug.WriteLine($"Redirecting {inArchivePath} to {redirectedPath}");
                        return redirectedPath;
                    }

                    ModBeingInstalled.Archive.FileExtractionStarted += (sender, args) =>
                    {
                        CLog.Information("Extracting mod file for installation: " + args.FileInfo.FileName, Settings.LogModInstallation);
                    };
                    ModBeingInstalled.Archive.FileExtractionFinished += (sender, args) =>
                    {
                        FileInstalledCallback();
                        //Debug.WriteLine(numdone);
                    };
                    ModBeingInstalled.Archive.ExtractFiles(gameTarget.TargetPath, installationRedirectCallback, fullPathMappingArchive.Keys.ToArray()); //directory parameter shouldn't be used here as we will be redirecting everything
                }
            }
            //Stage: SFAR Installation
            foreach (var sfarJob in installationQueues.sfarJobs)
            {
                InstallIntoSFAR(sfarJob, ModBeingInstalled, FileInstalledCallback);
            }


            //Main installation step has completed
            CLog.Information("Main stage of mod installation has completed", Settings.LogModInstallation);
            Percent = (int)(numdone * 100.0 / numFilesToInstall);

            //Install supporting ASI files if necessary
            //Todo: Upgrade to version detection code from ME3EXP to prevent conflicts

            Action = "Installing support files";
            CLog.Information("Installing supporting ASI files", Settings.LogModInstallation);
            if (ModBeingInstalled.Game == Mod.MEGame.ME1)
            {
                Utilities.InstallEmbeddedASI("ME1-DLC-ModEnabler-v1.0", 1.0, gameTarget);
            }
            else if (ModBeingInstalled.Game == Mod.MEGame.ME2)
            {
                //None right now
            }
            else
            {
                Utilities.InstallEmbeddedASI("ME3Logger_truncating-v1.0", 1.0, gameTarget);
                if (ModBeingInstalled.GetJob(ModJob.JobHeader.BALANCE_CHANGES) != null)
                {
                    Utilities.InstallEmbeddedASI("BalanceChangesReplacer-v2.0", 2.0, gameTarget);
                }
            }


            if (numFilesToInstall == numdone)
            {
                e.Result = INSTALL_SUCCESSFUL;
                Action = "Installed";
            }
        }

        private bool InstallIntoSFAR((ModJob job, string sfarPath) sfarJob, Mod mod, Action FileInstalledCallback = null)
        {

            int numfiles = sfarJob.job.FilesToInstall.Count;
            //Todo: Check all newfiles exist
            //foreach (string str in diskFiles)
            //{
            //    if (!File.Exists(str))
            //    {
            //        Console.WriteLine("Source file on disk doesn't exist: " + str);
            //        EndProgram(1);
            //    }
            //}

            //Open SFAR
            DLCPackage dlc = new DLCPackage(sfarJob.sfarPath);

            //precheck
            bool allfilesfound = true;
            //if (options.ReplaceFiles)
            //{
            //    for (int i = 0; i < numfiles; i++)
            //    {
            //        int idx = dlc.FindFileEntry(sfarFiles[i]);
            //        if (idx == -1)
            //        {
            //            Console.WriteLine("Specified file does not exist in the SFAR Archive: " + sfarFiles[i]);
            //            allfilesfound = false;
            //        }
            //    }
            //}

            //Add or Replace
            if (allfilesfound)
            {
                foreach (var entry in sfarJob.job.FilesToInstall)
                {
                    string entryPath = entry.Key.Replace('\\', '/');
                    if (!entryPath.StartsWith('/')) entryPath = '/' + entryPath; //Ensure path starts with /
                    int index = dlc.FindFileEntry(entryPath);
                    //Todo: For archive to immediate installation we will need to modify this ModPath value to point to some temporary directory
                    //where we have extracted files destined for SFAR files as we cannot unpack solid archives to streams.
                    var sourcePath = Path.Combine(mod.ModPath, sfarJob.job.JobDirectory, entry.Value);
                    if (index >= 0)
                    {
                        dlc.ReplaceEntry(sourcePath, index);
                        CLog.Information("Replaced file within SFAR: " + entry.Key, Settings.LogModInstallation);
                    }
                    else
                    {
                        dlc.AddFileQuick(sourcePath, entryPath);
                        CLog.Information("Added new file to SFAR: " + entry.Key, Settings.LogModInstallation);
                    }
                    FileInstalledCallback?.Invoke();
                }
            }

            //Todo: Support deleting files from sfar (I am unsure if this is actually ever used and I might remove this feature)
            //if (options.DeleteFiles)
            //{
            //    DLCPackage dlc = new DLCPackage(options.SFARPath);
            //    List<int> indexesToDelete = new List<int>();
            //    foreach (string file in options.Files)
            //    {
            //        int idx = dlc.FindFileEntry(file);
            //        if (idx == -1)
            //        {
            //            if (options.IgnoreDeletionErrors)
            //            {
            //                continue;
            //            }
            //            else
            //            {
            //                Console.WriteLine("File doesn't exist in archive: " + file);
            //                EndProgram(1);
            //            }
            //        }
            //        else
            //        {
            //            indexesToDelete.Add(idx);
            //        }
            //    }
            //    if (indexesToDelete.Count > 0)
            //    {
            //        dlc.DeleteEntries(indexesToDelete);
            //    }
            //    else
            //    {
            //        Console.WriteLine("No files were found in the archive that matched the input list for --files.");
            //    }
            //}

            return true;
        }

        /// <summary>
        /// Checks if DLC specified by the job installation headers exist and prompt user to continue or not if the DLC is not found. This is only used for ME3's headers such as CITADEL or RESURGENCE and not CUSTOMDLC or BASEGAME.'
        /// </summary>
        /// <param name="gameDLCPath">Game DLC path</param>
        /// <param name="installationJobs">List of jobs to look through and validate</param>
        /// <returns></returns>
        private bool PrecheckOfficialHeaders(string gameDLCPath, List<ModJob> installationJobs)
        {
            if (ModBeingInstalled.Game != Mod.MEGame.ME3) { return true; } //me1/me2 don't have dlc header checks like me3
            foreach (var job in installationJobs)
            {
                if (job.Header == ModJob.JobHeader.BALANCE_CHANGES) continue; //Don't check balance changes
                if (job.Header == ModJob.JobHeader.BASEGAME) continue; //Don't check basegame
                if (job.Header == ModJob.JobHeader.CUSTOMDLC) continue; //Don't check custom dlc
                string sfarPath = job.Header == ModJob.JobHeader.TESTPATCH ? Utilities.GetTestPatchPath(gameTarget) : Path.Combine(gameDLCPath, ModJob.HeadersToDLCNamesMap[job.Header], "CookedPCConsole", "Default.sfar");
                if (!File.Exists(sfarPath))
                {
                    Log.Warning($"DLC not installed that mod is marked to modify: {job.Header}, prompting user.");
                    //Prompt user
                    bool cancel = false;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string message = $"{ModBeingInstalled.ModName} installs files into the {ModJob.HeadersToDLCNamesMap[job.Header]} ({ME3Directory.OfficialDLCNames[ModJob.HeadersToDLCNamesMap[job.Header]]}) DLC, which is not installed.";
                        if (job.RequirementText != null)
                        {
                            message += $"\n{job.RequirementText}";
                        }
                        message += "\n\nThe mod might not function properly without this DLC installed first. Continue installing anyways?";
                        MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, $"{ME3Directory.OfficialDLCNames[ModJob.HeadersToDLCNamesMap[job.Header]]} DLC not installed", MessageBoxButton.YesNo, MessageBoxImage.Error);
                        if (result == MessageBoxResult.No) { cancel = true; return; }

                    }));
                    if (cancel) { Log.Error("User canceling installation"); return false; }
                    Log.Warning($"User continuing installation anyways");
                }
            }
            return true;
        }

        private void ModInstallationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            InstallationSucceeded = e.Result is int res && res == INSTALL_SUCCESSFUL;
            OnClosing(new EventArgs());
        }

        private void InstallStart_Click(object sender, RoutedEventArgs e)
        {
            BeginInstallingMod();
        }

        private void InstallCancel_Click(object sender, RoutedEventArgs e)
        {
            InstallationSucceeded = false;
            InstallationCancelled = true;
            OnClosing(new EventArgs());
        }
    }
}
