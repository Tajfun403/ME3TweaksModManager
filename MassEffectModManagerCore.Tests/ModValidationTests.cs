using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MassEffectModManagerCore.modmanager;
using MassEffectModManagerCore.modmanager.me3tweaks;
using MassEffectModManagerCore.modmanager.usercontrols;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;

namespace MassEffectModManagerCore.Tests
{
    [TestClass]
    public class ModValidationTests
    {

        [TestMethod]
        public void ValidateModLoading()
        {
            GlobalTest.Init();
            Console.WriteLine("Executing path: " + GlobalTest.GetTestModsDirectory());
            var testingDataPath = GlobalTest.GetTestModsDirectory();
            Assert.IsTrue(Directory.Exists(testingDataPath), "Directory for testing doesn't exist: " + testingDataPath);

            //Force log startup on.


            //Test cases
            Mod aceSlammer = new Mod(Path.Combine(testingDataPath, "Ace Slammer", "moddesc.ini"), Mod.MEGame.Unknown);
            Assert.IsTrue(aceSlammer.ValidMod, "Ace slammer didn't parse into a valid mod! Reason: " + aceSlammer.LoadFailedReason);

            Mod diamondDifficulty = new Mod(Path.Combine(testingDataPath, "Diamond Difficulty", "moddesc.ini"), Mod.MEGame.Unknown);
            Assert.IsTrue(diamondDifficulty.ValidMod, "Diamond Difficulty didn't parse into a valid mod! Reason: " + diamondDifficulty.LoadFailedReason);

            Mod egm = new Mod(Path.Combine(testingDataPath, "Expanded Galaxy Mod", "moddesc.ini"), Mod.MEGame.Unknown);
            Assert.IsTrue(egm.ValidMod, "EGM didn't parse into a valid mod! Reason: " + egm.LoadFailedReason);
            var egmCustDLCJob = egm.GetJob(ModJob.JobHeader.CUSTOMDLC);
            Assert.IsNotNull(egmCustDLCJob, "Could not find EGM Custom DLC job!");
            Assert.AreEqual(2, egm.InstallationJobs.Count, $"EGM: Wrong number of installation jobs for custom dlc! Should be 2, got: {egm.InstallationJobs.Count}");
            Assert.AreEqual(6, egmCustDLCJob.AlternateDLCs.Count, $"EGM: Wrong number of alternate DLC on CustomDLC job! Should be 6, got: {egmCustDLCJob.AlternateDLCs.Count}");
            Assert.AreEqual(1, egmCustDLCJob.AlternateFiles.Count, $"EGM: Wrong number of alternate files on CustomDLC job! Should be 1, got: {egmCustDLCJob.AlternateFiles.Count}");
            var egmBasegameJob = egm.GetJob(ModJob.JobHeader.BASEGAME);
            Assert.IsNotNull(egmBasegameJob, "EGM Basegame job is null when it should exist!");
            Assert.AreEqual(23, egmBasegameJob.FilesToInstall.Count, $"EGM basegame job should install 23 files, but we see {egmBasegameJob.FilesToInstall.Count}");

            Assert.IsNull(egm.GetJob(ModJob.JobHeader.CITADEL), "EGM somehow returned a job for non-existent header!");

            Mod firefight = new Mod(Path.Combine(testingDataPath, "Firefight mod", "moddesc.ini"), Mod.MEGame.Unknown);
            Assert.IsTrue(firefight.ValidMod, "Firefight mod didn't parse into a valid mod! Reason: " + firefight.LoadFailedReason);

            //SP Controller Support 2.31 - invalid cause missing ) on moddesc.ini altfiles in customdlc.
            Mod spcontroller = new Mod(Path.Combine(testingDataPath, "SP Controller Support", "moddesc.ini"), Mod.MEGame.Unknown);
            Assert.IsFalse(spcontroller.ValidMod, "SP Controller support (wrong parenthesis on customdlc alts!) loaded as a valid mod when it shouldn't!");

            Mod zombiesupercoal = new Mod(Path.Combine(testingDataPath, "Zombie [SuperCoal]", "moddesc.ini"), Mod.MEGame.Unknown);
            Assert.IsTrue(zombiesupercoal.ValidMod, "zombie mod supercoal failed to load as a valid mod! Reason: " + zombiesupercoal.LoadFailedReason);

            // Can't find moddesc.ini
            Mod badPath = new Mod(Path.Combine(testingDataPath, "Not A Path", "moddesc.ini"), Mod.MEGame.Unknown);
            Assert.IsFalse(badPath.ValidMod, "An invalid/non-existing moddesc.ini somehow loaded as a valid mod!");

            //Same Gender Romances
            Mod sameGender = new Mod(Path.Combine(testingDataPath, "Same-Gender Romances for ME3", "moddesc.ini"), Mod.MEGame.Unknown);
            Assert.IsTrue(sameGender.ValidMod, "Same-Gender Romances failed to load as a valid mod!  Reason: " + sameGender.LoadFailedReason);
            var sgCustDLCJob = sameGender.InstallationJobs.FirstOrDefault(x => x.Header == ModJob.JobHeader.CUSTOMDLC);
            Assert.IsNotNull(sgCustDLCJob, "Could not find Same-Gender Romances Custom DLC job!");
            Assert.AreEqual(1, sameGender.InstallationJobs.Count, $"Same-gender romances: Wrong number of installation jobs! Should be 1, got: {sameGender.InstallationJobs.Count}");

        }

        [TestMethod]
        public void ValidateArchiveModLoading()
        {
            GlobalTest.Init();

            Console.WriteLine("Fetching third party services");
            App.ThirdPartyImportingService = OnlineContent.FetchThirdPartyImportingService();
            App.ThirdPartyIdentificationService = OnlineContent.FetchThirdPartyIdentificationManifest();

            var compressedModsDirectory = Path.Combine(GlobalTest.GetTestDataDirectory(), "compressedmods");
            List<Mod> modsFoundInArchive = new List<Mod>();

            void addModCallback(Mod m)
            {
                Console.WriteLine($"Found mod in archive: {m.ModName}");
                modsFoundInArchive.Add(m);
            }

            void failedModCallback(Mod m)
            {
                Console.WriteLine($"A mod failed to load. This may be expected: {m.ModName}");
            }

            void logMessageCallback(string m)
            {
                Console.WriteLine(m);
            }
            foreach (var archive in Directory.GetFiles(compressedModsDirectory))
            {
                modsFoundInArchive.Clear();
                var realArchiveInfo = GlobalTest.ParseRealArchiveAttributes(archive);
                Console.WriteLine($"Inspecting archive: { archive}");
                ModArchiveImporter.InspectArchive(archive, addModCallback, failedModCallback, logMessageCallback, forcedMD5: realArchiveInfo.md5, forcedSize: realArchiveInfo.size);
                Assert.AreEqual(realArchiveInfo.nummodsexpected, modsFoundInArchive.Count, $"{archive} did not parse correct amount of mods.");
            }
        }


    }
}
