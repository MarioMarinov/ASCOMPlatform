; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "ASCOM Camera Hub"
#define MyAppPublisher "ASCOM Initiative (Peter Simpson)"
#define MyAppPublisherURL "https://ascom-standards.org"
#define MyAppSupportURL "URL=https://ascomtalk.groups.io/g/Developer/topics"
#define MyAppUpdatesURL "https://github.com/ASCOMInitiative"
#define MyAppExeName "ASCOM.CameraHub.exe"
#define MyAppAuthor "Peter Simpson"
#define MyAppCopyright "Copyright � 2023 " + MyAppAuthor
#define MyAppVersion GetVersionNumbersString("..\bin\Release\ASCOM.CameraHub.exe")  ; Create version number variable

[Setup]
AppId={{CAF02964-DDB2-458B-9009-01B08C9BBC0D}
AppCopyright={#MyAppCopyright}
AppName={#MyAppName}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppPublisherURL}
AppSupportURL={#MyAppSupportURL}
AppUpdatesURL={#MyAppUpdatesURL}
AppVerName={#MyAppName}
AppVersion={#MyAppVersion}
; ArchitecturesInstallIn64BitMode=x64
Compression=zip 
;lzma
DefaultDirName={autocf}\ASCOM\Camera\CameraHub
DefaultGroupName=ASCOMConformUniversal
MinVersion=6.1SP1
DisableProgramGroupPage=yes
OutputBaseFilename=CameraHub({#MyAppVersion})Setup
OutputDir=.\Builds
PrivilegesRequired=admin
SetupIconFile=ASCOM.ico
SetupLogging=true
ShowLanguageDialog=auto
SolidCompression=yes
UninstallDisplayName=
UninstallDisplayIcon={app}\{#MyAppExeName}
VersionInfoCompany=ASCOM Initiative
VersionInfoCopyright={#MyAppAuthor}
VersionInfoDescription= {#MyAppName}
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion= {#MyAppVersion}
VersionInfoVersion={#MyAppVersion}
WizardImageFile=NewWizardImage.bmp
WizardSmallImageFile=ASCOMLogo.bmp
WizardStyle=modern
SignToolRunMinimized=yes
SignTool = SignConformU

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "armenian"; MessagesFile: "compiler:Languages\Armenian.isl"
Name: "brazilianportuguese"; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"
Name: "bulgarian"; MessagesFile: "compiler:Languages\Bulgarian.isl"
Name: "catalan"; MessagesFile: "compiler:Languages\Catalan.isl"
Name: "corsican"; MessagesFile: "compiler:Languages\Corsican.isl"
Name: "czech"; MessagesFile: "compiler:Languages\Czech.isl"
Name: "danish"; MessagesFile: "compiler:Languages\Danish.isl"
Name: "dutch"; MessagesFile: "compiler:Languages\Dutch.isl"
Name: "finnish"; MessagesFile: "compiler:Languages\Finnish.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"
Name: "hebrew"; MessagesFile: "compiler:Languages\Hebrew.isl"
Name: "icelandic"; MessagesFile: "compiler:Languages\Icelandic.isl"
Name: "italian"; MessagesFile: "compiler:Languages\Italian.isl"
Name: "japanese"; MessagesFile: "compiler:Languages\Japanese.isl"
Name: "norwegian"; MessagesFile: "compiler:Languages\Norwegian.isl"
Name: "polish"; MessagesFile: "compiler:Languages\Polish.isl"
Name: "portuguese"; MessagesFile: "compiler:Languages\Portuguese.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"
Name: "slovak"; MessagesFile: "compiler:Languages\Slovak.isl"
Name: "slovenian"; MessagesFile: "compiler:Languages\Slovenian.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"
Name: "turkish"; MessagesFile: "compiler:Languages\Turkish.isl"
Name: "ukrainian"; MessagesFile: "compiler:Languages\Ukrainian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; 64bit OS - Install the 64bit app
Source: "J:\ASCOMPlatform\Drivers and Simulators\CameraHub\bin\Release\ASCOM.CameraHub.exe"; DestDir: "{app}"; Flags: ignoreversion signonce
Source: "J:\ASCOMPlatform\Drivers and Simulators\CameraHub\bin\Release\ASCOM.CameraHub.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "J:\ASCOMPlatform\Drivers and Simulators\CameraHub\bin\Release\ASCOM.CameraHub.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "J:\ASCOMPlatform\Drivers and Simulators\CameraHub\ASCOM.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
; Name: "{autoprograms}\ASCOM Camera Hub"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\ASCOM.ico"
; Name: "{autodesktop}\Camera Hub"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; IconFilename: "{app}\ASCOM.ico"

[Run] 
Filename: "{app}\{#MyAppExeName}"; Parameters: "/regserver"

[UninstallRun]
Filename: "{app}\{#MyAppExeName}"; Parameters: "/unregserver"

[UninstallDelete]
Name: {app}; Type: dirifempty

[Code]
procedure CurPageChanged(CurPageID: Integer);
begin
  if CurPageID = wpSelectTasks then
  begin
    WizardSelectTasks('windotnet');
  end;
end;

// Code to enable the installer to uninstall previous versions of itself when a new version is installed
procedure CurStepChanged(CurStep: TSetupStep);
var
  ResultCode: Integer;
  UninstallExe: String;
  UninstallRegistry: String;
begin
  if (CurStep = ssInstall) then
	begin
      UninstallRegistry := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#SetupSetting("AppId")}' + '_is1');
      if RegQueryStringValue(HKLM, UninstallRegistry, 'UninstallString', UninstallExe) then
        begin
          Exec(RemoveQuotes(UninstallExe), ' /SILENT', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);
          sleep(1000);    //Give enough time for the install screen to be repainted before continuing
        end
  end;
end;