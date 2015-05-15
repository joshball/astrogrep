Changelog for AstroGrep v4.4.0
===================================================================
Bugs
-68: Selecting a file in the search results causes an exception
-61: Application freezes during preview pane population with a large amount of matched lines.

Feature Requests
-94: Add ability to download .net 4 framework if not installed from installer
-81: Word Wrap (View -> Word Wrap)
-54: Display all search results from in all found files after each search (Search Options -> Show all results after search, View -> All Results)
-21: Integrate syntax highlighters (only available when viewing entire file)
-20: full-document display (View -> Entire File)

Other
- To support newer features, updating the preview area from a RichTextBox to AvalonEdit control
- Added Context Fore Color for the context line/line number color displayed in preview area
- Move Line Numbers and Remove Leading White Space options to View menu
- Removed double click action in preview area to open file at current line/match, made it a right-click menu option instead at current mouse position 
  (making it better to discover than double clicking)
- Zoom support for preview area (View -> Zoom)
- Add NLog logging to application (Help -> View Log, log saved in %appdata%\AstroGrep or current directory if /local used + \Log\AstroGrep.log)
- Rearrange internal processing of export settings to get ready for export option selections
- libAstroGrep updated to use MatchResults collection instead of Greps, rework HitObject into MatchResult, MatchResultLine, and MatchResultLineMatch classes
- Line numbers are always saved for each MatchResult in libAstoGrep in order for display to toggle instead of performing a new search
- Update look of search and search options headers, removing toggle to show/hide options
- Use F1 key to open Help -> View Help menu item