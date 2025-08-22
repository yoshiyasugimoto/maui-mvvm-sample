# MAUI E2E Tests with Appium

This project contains E2E tests for the MAUI MVVM Sample application using Appium.

## Prerequisites

1. **Appium Server**: Install and run Appium server
   ```bash
   npm install -g appium
   appium
   ```

2. **Appium Drivers**: Install platform-specific drivers
   ```bash
   # For Windows
   appium driver install windows
   
   # For macOS
   appium driver install mac2
   ```

3. **Platform Setup**:
   - **Windows**: Windows Application Driver (WinAppDriver) must be installed
   - **Mac**: macOS 10.15+ with Xcode Command Line Tools

## Building the App

Before running tests, build the MAUI app:

```bash
# Windows
dotnet build -f net9.0-windows10.0.19041.0 ../mauiMvvmSample/mauiMvvmSample.csproj

# macOS
dotnet build -f net9.0-maccatalyst ../mauiMvvmSample/mauiMvvmSample.csproj
```

## Running Tests

### Using .runsettings file

1. Update the `.runsettings` file with your app path
2. Run tests:
   ```bash
   dotnet test --settings .runsettings
   ```

### Using command line parameters

**Method 1: Using TestRunParameters (Recommended)**
```bash
# Windows
dotnet test -- TestRunParameters.Parameter\(name=\"platform\",value=\"Windows\"\) TestRunParameters.Parameter\(name=\"appPath\",value=\"C:\path\to\your.exe\"\)

# Mac
dotnet test -- TestRunParameters.Parameter\(name=\"platform\",value=\"Mac\"\) TestRunParameters.Parameter\(name=\"appPath\",value=\"/path/to/your.app\"\)
```

**Method 2: Using environment variables**
```bash
# Windows (PowerShell)
$env:platform="Windows"; $env:appPath="C:\path\to\your.exe"; dotnet test

# Mac (bash/zsh)
platform=Mac appPath=/path/to/your.app dotnet test
```

**Method 3: Update .runsettings file directly**
Edit the `.runsettings` file to set your specific values:
```xml
<Parameter name="platform" value="Mac" />
<Parameter name="appPath" value="/Users/username/path/to/mauiMvvmSample.app" />
```

Then run:
```bash
dotnet test --settings .runsettings
```

## Test Structure

- `TestSetup/AppiumSetup.cs`: Appium configuration for different platforms
- `TestSetup/BaseTest.cs`: Base test class with setup/teardown
- `Tests/MainPageTests.cs`: E2E tests for the main page functionality

## Troubleshooting

1. **Appium server not running**: Ensure Appium is running on port 4723
2. **App not found**: Verify the app path in .runsettings or command line
3. **Windows Driver issues**: Ensure WinAppDriver is installed and running
4. **Mac Driver issues**: Check that Mac2 driver is properly installed with Appium
5. **Element not found**: Check if element names are properly set in XAML

### Common Issues with Test Parameters

**Issue: Parameters not being passed correctly**
- The test will output debug information showing which parameters were received
- Check the console output for "Test Parameters Debug" section
- If parameters show as empty, try using environment variables method instead

**Mac-specific Requirements:**
1. Enable accessibility permissions for Terminal/IDE:
   - System Preferences → Security & Privacy → Privacy → Accessibility
   - Add Terminal.app or your IDE
2. The .app bundle must be properly signed or have entitlements
3. For unsigned apps, you may need to allow in Security settings

**Path Format Examples:**
- Windows: `C:\Users\username\source\repos\mauiMvvmSample\bin\Debug\net9.0-windows10.0.19041.0\win10-x64\mauiMvvmSample.exe`
- Mac: `/Users/username/source/repos/mauiMvvmSample/bin/Debug/net9.0-maccatalyst/mauiMvvmSample.app`