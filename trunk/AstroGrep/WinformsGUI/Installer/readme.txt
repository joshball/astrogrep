Changelog for AstroGrep v4.4.2
===================================================================
Bugs
-75: Issues with accented characters (use Tools->Options->File Encoding->Performance to set level of file encoding performance, or specify a specific encoding for a file)
-74: Line numbers truncated on v4.4
-73: Files fail to open in system editor when Search Text field is empty
-69: Drastic search speed change over versions (allow ability to select performance level of file encoding via Tools->Options->File Encoding->Performance)

Feature Requests
-97: Create a portable version

Other
- Moved some common code to AstroGrep.Common to allow better usage of shared code and logging
- Added file encoding cache for each performance level and the ability to clear it (Tools->Options->File Encoding->Clear Cache)
- Removed zip file in favor of portable version (so regular version is only available as installer)
- Updated application icon
- Updated About dialog for a UI refresh