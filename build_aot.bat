@echo off
setlocal enabledelayedexpansion

echo ===================================================
echo   VPSManager - Bat Dau Native AOT Publish
echo ===================================================

:: Thu muc chua file script bat
set "ROOT_DIR=%~dp0"
set "PROJECT_PATH=%ROOT_DIR%src\VPSManager\VPSManager.csproj"
set "PUBLISH_DIR=%ROOT_DIR%src\VPSManager\bin\Release\net10.0\win-x64\publish"
set "OUTPUT_DIR=%ROOT_DIR%output"

echo [*] Dang bien dich Native AOT (Release, win-x64)...
dotnet publish "%PROJECT_PATH%" -c Release -r win-x64 /p:PublishAot=true /p:SelfContained=true

if %ERRORLEVEL% neq 0 (
    echo.
    echo [!] Loi: Bien dich Native AOT that bai!
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo [*] Dang khoi tao thu muc dau ra: %OUTPUT_DIR%
if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"

echo [*] Dang sao chep cac file ung dung da bien dich vao: %OUTPUT_DIR%
xcopy "%PUBLISH_DIR%\*.*" "%OUTPUT_DIR%\" /y /e /i

echo [*] Dang loai bo cac file .pdb khong can thiet de toi uu dung luong...
if exist "%OUTPUT_DIR%\libHarfBuzzSharp.pdb" del "%OUTPUT_DIR%\libHarfBuzzSharp.pdb"
if exist "%OUTPUT_DIR%\libSkiaSharp.pdb" del "%OUTPUT_DIR%\libSkiaSharp.pdb"
if exist "%OUTPUT_DIR%\VPSManager.pdb" del "%OUTPUT_DIR%\VPSManager.pdb"

if %ERRORLEVEL% equ 0 (
    echo.
    echo ===================================================
    echo   [THANH CONG] Bien dich va sao chep hoan tat!
    echo   File chay duoc luu tai: %OUTPUT_DIR%\VPSManager.exe
    echo ===================================================
) else (
    echo.
    echo [!] Loi: Sao chep file sang thu muc output that bai!
)

pause
