; KeyInsert Installer NSIS Script
; get NSIS at http://nsis.sourceforge.net/Download
; As a program that all Power PC users should have, Notepad++ is recommended to edit this file

Icon "Properties\key_presser_5.ico"
Caption "KeyInsert Installer"
Name "KeyInsert"
AutoCloseWindow true
ShowInstDetails show

LicenseBkColor /windows
LicenseData "LICENSE.md"
LicenseForceSelection checkbox "I have read and understand this notice"
LicenseText "Please read the notice below before installing KeyInsert. If you understand the notice, click the checkbox below and click Next."

InstallDir $PROGRAMFILES\WalkmanOSS

OutFile "bin\Release\KeyInsert-Installer.exe"

; Pages

Page license
Page components
Page directory
Page instfiles
UninstPage uninstConfirm
UninstPage instfiles

; Sections

Section "Executable & Uninstaller"
  SectionIn RO
  SetOutPath $INSTDIR
  File "bin\Release\KeyInsert.exe"
  WriteUninstaller "KeyInsert-Uninst.exe"
SectionEnd

Section "Start Menu Shortcuts"
  CreateDirectory "$SMPROGRAMS\WalkmanOSS"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\KeyInsert.lnk" "$INSTDIR\KeyInsert.exe" "" "$INSTDIR\KeyInsert.exe" "" "" "" "KeyInsert"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\Uninstall KeyInsert.lnk" "$INSTDIR\KeyInsert-Uninst.exe" "" "" "" "" "" "Uninstall KeyInsert"
  ;Syntax for CreateShortCut: link.lnk target.file [parameters [icon.file [icon_index_number [start_options [keyboard_shortcut [description]]]]]]
SectionEnd

Section "Desktop Shortcut"
  CreateShortCut "$DESKTOP\KeyInsert.lnk" "$INSTDIR\KeyInsert.exe" "" "$INSTDIR\KeyInsert.exe" "" "" "" "KeyInsert"
SectionEnd

Section "Quick Launch Shortcut"
  CreateShortCut "$QUICKLAUNCH\KeyInsert.lnk" "$INSTDIR\KeyInsert.exe" "" "$INSTDIR\KeyInsert.exe" "" "" "" "KeyInsert"
SectionEnd

Section "Associate with *.KeyInsert files"
    WriteRegStr HKCR "Applications\KeyInsert.exe\shell\open\command" "" "$\"$INSTDIR\KeyInsert.exe$\" $\"%1$\""
    WriteRegStr HKCR ".KeyInsert\OpenWithList\KeyInsert.exe" "" ""
    WriteRegStr HKCR ".KeyInsert" "" "KeyInsert_auto_file"
    WriteRegStr HKCR "KeyInsert_auto_file\shell\open\command" "" "$\"$INSTDIR\KeyInsert.exe$\" $\"%1$\""
    WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.KeyInsert\OpenWithList" "j" "KeyInsert.exe"
    WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.KeyInsert\UserChoice" "Progid" "Applications\KeyInsert.exe"
SectionEnd

; Functions

Function .onInit
  MessageBox MB_YESNO "This will install KeyInsert. Do you wish to continue?" IDYES gogogo
    Abort
  gogogo:
  SetShellVarContext all
  SetAutoClose true
FunctionEnd

Function .onInstSuccess
    MessageBox MB_YESNO "Install Succeeded! Open ReadMe?" IDNO NoReadme
      ExecShell "open" "https://github.com/Walkman100/KeyInsert/blob/master/README.md#keyinsert-"
    NoReadme:
FunctionEnd

; Uninstaller

!include LogicLib.nsh ; For ${IF} logic
Section "Uninstall"
  Delete "$INSTDIR\KeyInsert-Uninst.exe" ; Remove Application Files
  Delete "$INSTDIR\KeyInsert.exe"
  RMDir "$INSTDIR"
  
  Delete "$SMPROGRAMS\WalkmanOSS\KeyInsert.lnk" ; Remove Start Menu Shortcuts & Folder
  Delete "$SMPROGRAMS\WalkmanOSS\Uninstall KeyInsert.lnk"
  RMDir "$SMPROGRAMS\WalkmanOSS"
  
  Delete "$DESKTOP\KeyInsert.lnk"     ; Remove Desktop      Shortcut
  Delete "$QUICKLAUNCH\KeyInsert.lnk" ; Remove Quick Launch Shortcut
SectionEnd

; Uninstaller Functions

Function un.onInit
    MessageBox MB_YESNO "This will uninstall KeyInsert. Continue?" IDYES NoAbort
      Abort ; causes uninstaller to quit.
    NoAbort:
    SetShellVarContext all
    SetAutoClose true
FunctionEnd

Function un.onUninstFailed
    MessageBox MB_OK "Uninstall Cancelled"
FunctionEnd

Function un.onUninstSuccess
    MessageBox MB_OK "Uninstall Completed"
FunctionEnd
