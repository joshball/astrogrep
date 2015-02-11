Changelog for AstroGrep v4.3.2
===================================================================
Bugs
-41: Only filenames shown when searching for 'whole word'
-50: Search path cleared as vertical splitter is moved (if not searched yet)
-52: Exclusions: double clicking entry/​checkbox
-53: Whole word search returns invalid results
-55: File type filter "*" causes exception
-57: Whole Word search can report invalid results when search text has regex values
-58: Search Results only contain Line Numbers, and no Text Examples
-59: File filter induces duplicate results
-60: Encoding detection does not work correctly

Feature Requests
-43: Make modified after/​before a between checkable option
-68: Help File
-69: Help entry for regular expressions
-70: Create installer
-73: Action of 'enter' key should be dependent on what pane has focus
-74: Save results from command line
-76: Link visibility
-77: Right click "Open Directory" could have file selected.
-78: More Exclusion options (moved system/hidden/date/file size/hit count to exclusion interface)
-79: Exclusions "Select/​Unselect All"
-80: Open With Associated App
-85: Option to remove leading white space in output area (Tools->Options->Results tab->Remove Leading White Space checkbox)
-87: Add keyboard shortcut to put focus in search text field
-88: Add new column for file extension
-91: Add .class and .chm to the default file extension exclusion list
-92: Specify an encoding for a file

Other
- Change browse for folder to use the newer dialog when on Vista+
- Added File->New Window menu item to open another instance of AstroGrep
- Changed display of Exclusion|Error messages to a filtering view window
- Added View menu and sub menu items to view status|exclusion|error|all messages
- Added ability to clear each MRU list (as well as all)
- Added more exclusions, such as binary file, readonly file, date created
- Moved checking for updates to separate Help menu item
- Removed Current Hit from Print options (selected items works for all selected files)
- Display detected encoding as part of searching status message
- Exporting now exports all exclusion items
- Moved Detect file encoding to File Encoding tab of options (goes with #92)
