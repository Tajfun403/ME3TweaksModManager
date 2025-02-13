using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using Serilog;

namespace MassEffectModManagerCore.modmanager.localizations
{
	//DO NOT MODIFY THIS FILE - IT IS AUTO GENERATED BY M3L_Template.txt and LocalizationHelper!

    /// <summary>
    /// ME3 - M3 Localizer. Call GetString() with one of it's listed static keys.
    /// </summary>
    [Localizable(false)]
    public static class M3L
    {
        internal static string GetString(string resourceKey, params string[] interpolationItems)
        {
            try
            {
                if (!resourceKey.StartsWith(@"string_")) throw new Exception(@"Localization keys must start with a string_ identifier!");
                var str = (string)Application.Current.FindResource(resourceKey);
                return string.Format(str, interpolationItems);
            }
            catch (Exception e)
            {
                Log.Error($@"Error fetching string with key {resourceKey}: {e.ToString()}. Returning value of ERROR.");
                return "ERROR!";
            }
        }

		public static readonly string string_AvailableMods = "string_AvailableMods";
		public static readonly string string_Language = "string_Language";
		public static readonly string string_Actions = "string_Actions";
		public static readonly string string_CreateaME3modonModMaker = "string_CreateaME3modonModMaker";
		public static readonly string string_OpenME3Tweaks = "string_OpenME3Tweaks";
		public static readonly string string_OpensME3Tweaks = "string_OpensME3Tweaks";
		public static readonly string string_OpenMemoryAnalyzer = "string_OpenMemoryAnalyzer";
		public static readonly string string_Options = "string_Options";
		public static readonly string string_ModManager = "string_ModManager";
		public static readonly string string_Setmodlibrarypath = "string_Setmodlibrarypath";
		public static readonly string string_Developermode = "string_Developermode";
		public static readonly string string_EnableDeveloperOrientedFeatures = "string_EnableDeveloperOrientedFeatures";
		public static readonly string string_Darktheme = "string_Darktheme";
		public static readonly string string_SwitchUItodarktheme = "string_SwitchUItodarktheme";
		public static readonly string string_Logging = "string_Logging";
		public static readonly string string_Enabletelemetry = "string_Enabletelemetry";
		public static readonly string string_LogModStartup = "string_LogModStartup";
		public static readonly string string_LogMixinStartup = "string_LogMixinStartup";
		public static readonly string string_LogModInstallation = "string_LogModInstallation";
		public static readonly string string_Checkforcontentupdates = "string_Checkforcontentupdates";
		public static readonly string string_ChecksME3Tweaksforupdatestovariousservices = "string_ChecksME3Tweaksforupdatestovariousservices";
		public static readonly string string_ReloadsMods = "string_ReloadsMods";
		public static readonly string string_Reloadsmodsfromthemodlibrary = "string_Reloadsmodsfromthemodlibrary";
		public static readonly string string_Exit = "string_Exit";
		public static readonly string string_ModManagement = "string_ModManagement";
		public static readonly string string_ImportMod = "string_ImportMod";
		public static readonly string string_HeaderDragDropFaster = "string_HeaderDragDropFaster";
		public static readonly string string_ImportmodfromArchive = "string_ImportmodfromArchive";
		public static readonly string string_ImportModFromArchiveTooltip = "string_ImportModFromArchiveTooltip";
		public static readonly string string_ImportalreadyinstalledCustomDLCmod = "string_ImportalreadyinstalledCustomDLCmod";
		public static readonly string string_DownloadModMakermod = "string_DownloadModMakermod";
		public static readonly string string_ASIModManager = "string_ASIModManager";
		public static readonly string string_ASIManagerTooltip = "string_ASIManagerTooltip";
		public static readonly string string_CustomDLCConflictDetector = "string_CustomDLCConflictDetector";
		public static readonly string string_GUICompact = "string_GUICompact";
		public static readonly string string_GUICompactTooltip = "string_GUICompactTooltip";
		public static readonly string string_BatchModInstaller = "string_BatchModInstaller";
		public static readonly string string_Openmodslibrarydirectory = "string_Openmodslibrarydirectory";
		public static readonly string string_Opensthemodlibraryfolder = "string_Opensthemodlibraryfolder";
		public static readonly string string_MixinsForME3 = "string_MixinsForME3";
		public static readonly string string_MixInLibrary = "string_MixInLibrary";
		public static readonly string string_ClearMixinCache = "string_ClearMixinCache";
		public static readonly string string_ModUtils = "string_ModUtils";
		public static readonly string string_Checkforupdates = "string_Checkforupdates";
		public static readonly string string_RestoremodfromME3Tweaks = "string_RestoremodfromME3Tweaks";
		public static readonly string string_Installcustomkeybindsintothismod = "string_Installcustomkeybindsintothismod";
		public static readonly string string_Developeroptions = "string_Developeroptions";
		public static readonly string string_Metadataeditor = "string_Metadataeditor";
		public static readonly string string_Deploymodto7zfilefordistribution = "string_Deploymodto7zfilefordistribution";
		public static readonly string string_Prepareforupdaterservice = "string_Prepareforupdaterservice";
		public static readonly string string_SubmitmodinformationtoME3Tweaks = "string_SubmitmodinformationtoME3Tweaks";
		public static readonly string string_Openmodfolder = "string_Openmodfolder";
		public static readonly string string_Deletemodfromlibrary = "string_Deletemodfromlibrary";
		public static readonly string string_Tools = "string_Tools";
		public static readonly string string_Runofficialgameconfigurationtool = "string_Runofficialgameconfigurationtool";
		public static readonly string string_MassEffect = "string_MassEffect";
		public static readonly string string_MassEffect2 = "string_MassEffect2";
		public static readonly string string_MassEffect3 = "string_MassEffect3";
		public static readonly string string_GenerateStarterKit = "string_GenerateStarterKit";
		public static readonly string string_Binkw32Bypasses = "string_Binkw32Bypasses";
		public static readonly string string_Grantwriteaccesstogamedirectories = "string_Grantwriteaccesstogamedirectories";
		public static readonly string string_Grantswritepermissionstogamedirectories = "string_Grantswritepermissionstogamedirectories";
		public static readonly string string_RunAutoTOC = "string_RunAutoTOC";
		public static readonly string string_RunsAutoTOCToolTip = "string_RunsAutoTOCToolTip";
		public static readonly string string_EnableconsoleinME1 = "string_EnableconsoleinME1";
		public static readonly string string_EnableME1ConsoleTooltip = "string_EnableME1ConsoleTooltip";
		public static readonly string string_Additionalmoddingtools = "string_Additionalmoddingtools";
		public static readonly string string_ALOTInstaller = "string_ALOTInstaller";
		public static readonly string string_ALOTInstallerTooltip = "string_ALOTInstallerTooltip";
		public static readonly string string_MassEffectINIModder = "string_MassEffectINIModder";
		public static readonly string string_MEIMTooltip = "string_MEIMTooltip";
		public static readonly string string_MassEffectModder = "string_MassEffectModder";
		public static readonly string string_Texturebrowserandinstaller = "string_Texturebrowserandinstaller";
		public static readonly string string_MassEffectRandomizer = "string_MassEffectRandomizer";
		public static readonly string string_RandomizerforthefirstMassEffectgame = "string_RandomizerforthefirstMassEffectgame";
		public static readonly string string_ME3ExplorerME3TweaksFork = "string_ME3ExplorerME3TweaksFork";
		public static readonly string string_ME3ExplorerME3TweaksForkTooltip = "string_ME3ExplorerME3TweaksForkTooltip";
		public static readonly string string_BackupRestore = "string_BackupRestore";
		public static readonly string string_Backup = "string_Backup";
		public static readonly string string_Createentiregamebackups = "string_Createentiregamebackups";
		public static readonly string string_Restore = "string_Restore";
		public static readonly string string_Restoreagamefrombackup = "string_Restoreagamefrombackup";
		public static readonly string string_Help = "string_Help";
		public static readonly string string_DynamicHelp = "string_DynamicHelp";
		public static readonly string string_Diagnostics = "string_Diagnostics";
		public static readonly string string_ModManagerLogsDiagnostics = "string_ModManagerLogsDiagnostics";
		public static readonly string string_TroubleshootingtoolsforME3Tweaks = "string_TroubleshootingtoolsforME3Tweaks";
		public static readonly string string_About = "string_About";
		public static readonly string string_moddescfileformatspecification = "string_moddescfileformatspecification";
		public static readonly string string_ReferenceinformationModdesc = "string_ReferenceinformationModdesc";
		public static readonly string string_AboutME3TweaksModManager = "string_AboutME3TweaksModManager";
		public static readonly string string_Aboutthisprogram = "string_Aboutthisprogram";
		public static readonly string string_CreateTestingArchive = "string_CreateTestingArchive";
		public static readonly string string_CreateTestingArchiveTooltip = "string_CreateTestingArchiveTooltip";
		public static readonly string string_startingUp = "string_startingUp";
		public static readonly string string_selectModOnLeftToGetStarted = "string_selectModOnLeftToGetStarted";
		public static readonly string string_applyMod = "string_applyMod";
		public static readonly string string_addTarget = "string_addTarget";
		public static readonly string string_startGame = "string_startGame";
		public static readonly string string_installationTarget = "string_installationTarget";
		public static readonly string string_binkAsiBypassNotInstalled = "string_binkAsiBypassNotInstalled";
		public static readonly string string_binkAsiBypassInstalled = "string_binkAsiBypassInstalled";
		public static readonly string string_binkAsiLoaderNotInstalled = "string_binkAsiLoaderNotInstalled";
		public static readonly string string_binkAsiLoaderInstalled = "string_binkAsiLoaderInstalled";
		public static readonly string string_gameNotInstalled = "string_gameNotInstalled";
		public static readonly string string_otherSavedTargets = "string_otherSavedTargets";
		public static readonly string string_cannotEndorseMod = "string_cannotEndorseMod";
		public static readonly string string_cannotEndorseOwnMod = "string_cannotEndorseOwnMod";
		public static readonly string string_notLinkedToNexusMods = "string_notLinkedToNexusMods";
		public static readonly string string_notAuthenticated = "string_notAuthenticated";
		public static readonly string string_gettingEndorsementStatus = "string_gettingEndorsementStatus";
		public static readonly string string_endorseMod = "string_endorseMod";
		public static readonly string string_modEndorsed = "string_modEndorsed";
		public static readonly string string_loginToNexusModsToEnableEndorsements = "string_loginToNexusModsToEnableEndorsements";
		public static readonly string string_endorsementsEnabledAuthenicatedToNexusMods = "string_endorsementsEnabledAuthenicatedToNexusMods";
		public static readonly string string_endorsing = "string_endorsing";
		public static readonly string string_unendorsing = "string_unendorsing";
		public static readonly string string_dialogConsoleEnabled = "string_dialogConsoleEnabled";
		public static readonly string string_consoleEnabled = "string_consoleEnabled";
		public static readonly string string_couldNotEnableConsole = "string_couldNotEnableConsole";
		public static readonly string string_confirmDeletion = "string_confirmDeletion";
		public static readonly string string_updateCompleted = "string_updateCompleted";
		public static readonly string string_gameRunning = "string_gameRunning";
		public static readonly string string_errorAddingTarget = "string_errorAddingTarget";
		public static readonly string string_dialogCannotAddTargetCmmVanilla = "string_dialogCannotAddTargetCmmVanilla";
		public static readonly string string_installationAborted = "string_installationAborted";
		public static readonly string string_cannotInstallMod = "string_cannotInstallMod";
		public static readonly string string_dialogUACPreConsent = "string_dialogUACPreConsent";
		public static readonly string string_someTargetsKeysWriteProtected = "string_someTargetsKeysWriteProtected";
		public static readonly string string_targetsWritable = "string_targetsWritable";
		public static readonly string string_allTargetsWritable = "string_allTargetsWritable";
		public static readonly string string_errorCreatingModLibrary = "string_errorCreatingModLibrary";
		public static readonly string string_unableToCreateModLibrary = "string_unableToCreateModLibrary";
		public static readonly string string_loadingMods = "string_loadingMods";
		public static readonly string string_loadedMods = "string_loadedMods";
		public static readonly string string_checkingModsForUpdates = "string_checkingModsForUpdates";
		public static readonly string string_modUpdateCheckCompleted = "string_modUpdateCheckCompleted";
		public static readonly string string_errorCheckingForModUpdates = "string_errorCheckingForModUpdates";
		public static readonly string string_downloadingRequiredFiles = "string_downloadingRequiredFiles";
		public static readonly string string_requiredFilesDownloaded = "string_requiredFilesDownloaded";
		public static readonly string string_failedToDownloadRequiredFiles = "string_failedToDownloadRequiredFiles";
		public static readonly string string_checkingForModManagerUpdates = "string_checkingForModManagerUpdates";
		public static readonly string string_completedModManagerUpdateCheck = "string_completedModManagerUpdateCheck";
		public static readonly string string_failedToCheckForUpdates = "string_failedToCheckForUpdates";
		public static readonly string string_dialogCouldNotDownloadStaticAssets = "string_dialogCouldNotDownloadStaticAssets";
		public static readonly string string_missingAssets = "string_missingAssets";
		public static readonly string string_failedToDownloadStaticFiles = "string_failedToDownloadStaticFiles";
		public static readonly string string_downloadingStaticFiles = "string_downloadingStaticFiles";
		public static readonly string string_staticFilesDownloaded = "string_staticFilesDownloaded";
		public static readonly string string_dialogTurningOffTelemetry = "string_dialogTurningOffTelemetry";
		public static readonly string string_turningOffTelemetry = "string_turningOffTelemetry";
		public static readonly string string_selectModLibraryFolder = "string_selectModLibraryFolder";
		public static readonly string string_runningAutoTOC = "string_runningAutoTOC";
		public static readonly string string_ranAutoTOC = "string_ranAutoTOC";
		public static readonly string string_nonModManagerModFound = "string_nonModManagerModFound";
		public static readonly string string_dialogCriticalFilesMissing = "string_dialogCriticalFilesMissing";
		public static readonly string string_requiredFilesNotDownloaded = "string_requiredFilesNotDownloaded";
		public static readonly string string_checkingWritePermissions = "string_checkingWritePermissions";
		public static readonly string string_checkedUserWritePermissions = "string_checkedUserWritePermissions";
		public static readonly string string_loadingPackageInfoDatabases = "string_loadingPackageInfoDatabases";
		public static readonly string string_loadedPackageInfoDatabases = "string_loadedPackageInfoDatabases";
		public static readonly string string_loadingTipsService = "string_loadingTipsService";
		public static readonly string string_loadedTipsService = "string_loadedTipsService";
		public static readonly string string_loadingThirdPartyServices = "string_loadingThirdPartyServices";
		public static readonly string string_loadedThirdPartyServices = "string_loadedThirdPartyServices";
		public static readonly string string_loadingDynamicHelp = "string_loadingDynamicHelp";
		public static readonly string string_confirmUnendorsement = "string_confirmUnendorsement";
		public static readonly string string_loadedDynamicHelp = "string_loadedDynamicHelp";
		public static readonly string string_selectModArchive = "string_selectModArchive";
		public static readonly string string_selectGameExecutable = "string_selectGameExecutable";
		public static readonly string string_interp_unendorseMod = "string_interp_unendorseMod";
		public static readonly string string_interp_unableToModifyBioinputIni = "string_interp_unableToModifyBioinputIni";
		public static readonly string string_interp_dialogDeleteSelectedModFromLibrary = "string_interp_dialogDeleteSelectedModFromLibrary";
		public static readonly string string_interp_modManagerHasBeenUpdatedTo = "string_interp_modManagerHasBeenUpdatedTo";
		public static readonly string string_interp_launching = "string_interp_launching";
		public static readonly string string_interp_launched = "string_interp_launched";
		public static readonly string string_interp_cannotInstallModGameNotInstalled = "string_interp_cannotInstallModGameNotInstalled";
		public static readonly string string_interp_dialog_installingTextureMod = "string_interp_dialog_installingTextureMod";
		public static readonly string string_interp_minorUpdateAvailableMessage = "string_interp_minorUpdateAvailableMessage";
		public static readonly string string_interp_dialogUnableToCreateModLibraryNoPermissions = "string_interp_dialogUnableToCreateModLibraryNoPermissions";
		public static readonly string string_interp_dialogCannotInstallModsWhileGameRunning = "string_interp_dialogCannotInstallModsWhileGameRunning";
		public static readonly string string_interp_failedToInstallMod = "string_interp_failedToInstallMod";
		public static readonly string string_interp_installingMod = "string_interp_installingMod";
		public static readonly string string_interp_installedMod = "string_interp_installedMod";
		public static readonly string string_interp_dialogUnableToAddGameTarget = "string_interp_dialogUnableToAddGameTarget";
		public static readonly string string_gameExecutable = "string_gameExecutable";
		public static readonly string string_interp_dialogCannotInstallBinkWhileGameRunning = "string_interp_dialogCannotInstallBinkWhileGameRunning";
		public static readonly string string_tooltip_launchCurrentlySelectedTarget = "string_tooltip_launchCurrentlySelectedTarget";
		public static readonly string string_tooltip_applyThisModToTheGame = "string_tooltip_applyThisModToTheGame";
		public static readonly string string_tooltip_currentTarget = "string_tooltip_currentTarget";
		public static readonly string string_tooltip_reloadModsFromLibrary = "string_tooltip_reloadModsFromLibrary";
		public static readonly string string_tooltip_filterME1 = "string_tooltip_filterME1";
		public static readonly string string_tooltip_filterME2 = "string_tooltip_filterME2";
		public static readonly string string_tooltip_filterME3 = "string_tooltip_filterME3";
		public static readonly string string_tooltip_someModsFailed = "string_tooltip_someModsFailed";
		public static readonly string string_tooltip_addAdditionalTarget = "string_tooltip_addAdditionalTarget";
		public static readonly string string_tooltip_targetInfo = "string_tooltip_targetInfo";
		public static readonly string string_targetInfo = "string_targetInfo";
		public static readonly string string_modDescription = "string_modDescription";
		public static readonly string string_preparingToInstall = "string_preparingToInstall";
		public static readonly string string_interp_devModeAlotInstalledWarning = "string_interp_devModeAlotInstalledWarning";
		public static readonly string string_brokenTexturesWarning = "string_brokenTexturesWarning";
		public static readonly string string_installing = "string_installing";
		public static readonly string string_loadingModArchive = "string_loadingModArchive";
		public static readonly string string_installingSupportFiles = "string_installingSupportFiles";
		public static readonly string string_installed = "string_installed";
		public static readonly string string_interp_dialogOfficialTargetDLCNotInstalled = "string_interp_dialogOfficialTargetDLCNotInstalled";
		public static readonly string string_dialogJobDescriptionMessageHeader = "string_dialogJobDescriptionMessageHeader";
		public static readonly string string_dialogJobDescriptionMessageFooter = "string_dialogJobDescriptionMessageFooter";
		public static readonly string string_dialogJobDescriptionMessageTitle = "string_dialogJobDescriptionMessageTitle";
		public static readonly string string_dialogOutdatedDLCHeader = "string_dialogOutdatedDLCHeader";
		public static readonly string string_dialogOutdatedDLCFooter = "string_dialogOutdatedDLCFooter";
		public static readonly string string_outdatedDLCDetected = "string_outdatedDLCDetected";
		public static readonly string string_dialogIncompatibleDLCDetectedHeader = "string_dialogIncompatibleDLCDetectedHeader";
		public static readonly string string_dialogIncompatibleDLCDetectedFooter = "string_dialogIncompatibleDLCDetectedFooter";
		public static readonly string string_incompatibleDLCDetected = "string_incompatibleDLCDetected";
		public static readonly string string_dialogNoWritePermissions = "string_dialogNoWritePermissions";
		public static readonly string string_cannotWriteToGameDirectory = "string_cannotWriteToGameDirectory";
		public static readonly string string_requiredContentMissing = "string_requiredContentMissing";
		public static readonly string string_dialogRequiredContentMissing = "string_dialogRequiredContentMissing";
		public static readonly string string_dialogInstallationSucceededFailedInstallCountCheck = "string_dialogInstallationSucceededFailedInstallCountCheck";
		public static readonly string string_dialogInstallationBlockedByALOT = "string_dialogInstallationBlockedByALOT";
		public static readonly string string_installationBlocked = "string_installationBlocked";
		public static readonly string string_installationSucceededMaybe = "string_installationSucceededMaybe";
		public static readonly string string_dialogRunGameOnceFirst = "string_dialogRunGameOnceFirst";
		public static readonly string string_gameMustBeRunAtLeastOnce = "string_gameMustBeRunAtLeastOnce";
		public static readonly string string_ui_allAlternatesAreAutomatic = "string_ui_allAlternatesAreAutomatic";
		public static readonly string string_ui_title_selectInstallationOptions = "string_ui_title_selectInstallationOptions";
		public static readonly string string_install = "string_install";
		public static readonly string string_cancel = "string_cancel";
		public static readonly string string_installMod = "string_installMod";
		public static readonly string string_abortInstallingMod = "string_abortInstallingMod";
		public static readonly string string_dialogInvalidRCWFile = "string_dialogInvalidRCWFile";
	}
}
