REM  This bundle script will combine all AstroGrep's dependency assemblies from mono.  This is done so that you can 
REM  Build Astrogrep that can run without installing .net framwork or mono framework.
REM
REM  This script requires that you have Mono2.4 installed to c:\mono-2.4\  (http://www.go-mono.com/mono-downloads/download.html)
REM  also you should have upx.exe in your path (http://upx.sourceforge.net/).  Upx is optional but nice because the bundled exe
REM  is larger then it needs to be.
REM  Script written by Ed Jakubowski - 08/01/2009

set ExeName=AstroGrep
set BinDirectory=%CD%\..\bin\debug
set MonoBin=c:\mono-2.4\bin
set OutputDir=Bundle

echo Setting Mono Bin Path
call setmono

mkdir %OutputDir%

cd %OutputDir%
call mkbundle -o %ExeName% %BinDirectory%\%ExeName%.exe %BinDirectory%\libAstroGrep.dll --deps

echo Compressing Executable...
call upx %ExeName%.exe

copy %MonoBin%\mono.dll .
copy %MonoBin%\libglib-2.0-0.dll .
copy %MonoBin%\libgthread-2.0-0.dll .

pause