REM  This bundle script will combine all AstroGrep's dependency assemblies from mono.  This is done so that you can 
REM  build an Astrogrep executable that can run without installing .net framwork or mono framework.
REM
REM  This script requires that you have Mono2.4 installed to c:\mono-2.4\  (http://www.go-mono.com/mono-downloads/download.html)
REM  also you should have upx.exe in your path (http://upx.sourceforge.net/).  Upx is optional but nice because the bundled exe
REM  is larger then it needs to be.  Not, sure but you might also need to have cygwin configured if you have any issues try that.
REM  Script written by Ed Jakubowski - 08/01/2009  Slikktic@yahoo.com

set ExeName=AstroGrep
set BinDirectory=%CD%\..\bin\debug
set MonoBin=c:\mono-2.4\bin
set OutputDir=Bundle

echo Setting Mono Bin Path
REM call setmono
echo Prepending '%MonoBin%;C:\cygwinM\bin' to PATH
PATH=%MonoBin%;C:\cygwinM\bin;%PATH%
set PKG_CONFIG_PATH=C:\Mono-2.4\lib\pkgconfig

mkdir %OutputDir%

cd %OutputDir%
call mkbundle -o %ExeName% %BinDirectory%\%ExeName%.exe %BinDirectory%\libAstroGrep.dll --deps

echo Compressing Executable...
call upx %ExeName%.exe

copy %MonoBin%\mono.dll .
copy %MonoBin%\libglib-2.0-0.dll .
copy %MonoBin%\libgthread-2.0-0.dll .

pause