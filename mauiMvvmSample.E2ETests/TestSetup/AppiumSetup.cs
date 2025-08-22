using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium.Mac;
using mauiMvvmSample.E2ETests.Helpers;

namespace mauiMvvmSample.E2ETests.TestSetup
{
    public static class AppiumSetup
    {
        public static AppiumDriver? App { get; private set; }
        private static bool _useLocalServer = false; // Set to true to use local Appium server
        
        public static void StartApp(string platform, string appPath)
        {
            if (App != null)
            {
                return;
            }
            
            // Optionally start local Appium server
            if (_useLocalServer)
            {
                AppiumServerHelper.StartAppiumLocalServer();
            }
            
            var serverUri = GetAppiumServerUri();
            
            if (platform.Equals("Windows", StringComparison.OrdinalIgnoreCase))
            {
                var options = GetWindowsOptions(appPath);
                App = new WindowsDriver(serverUri, options);
            }
            else if (platform.Equals("Mac", StringComparison.OrdinalIgnoreCase))
            {
                var options = GetMacOptions(appPath);
                App = new MacDriver(serverUri, options);
            }
            else
            {
                throw new NotSupportedException($"Platform '{platform}' is not supported");
            }
            
            App.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        
        public static void StopApp()
        {
            App?.Quit();
            App?.Dispose();
            App = null;
            
            // Stop local Appium server if it was started
            if (_useLocalServer)
            {
                AppiumServerHelper.DisposeAppiumLocalServer();
            }
        }
        
        private static AppiumOptions GetWindowsOptions(string appPath)
        {
            var options = new AppiumOptions
            {
                PlatformName = "Windows",
                AutomationName = "Windows",
                App = appPath
            };
            options.AddAdditionalAppiumOption("ms:experimental-webdriver", true);
            options.AddAdditionalAppiumOption("ms:waitForAppLaunch", "25");
            return options;
        }

        private static AppiumOptions GetMacOptions(string appPath)
        {
            var options = new AppiumOptions
            {
                PlatformName = "Mac",
                AutomationName = "Mac2",
                App = appPath
            };
            options.AddAdditionalAppiumOption("showServerLogs", true);
            return options;
        }

        private static Uri GetAppiumServerUri()
        {
            return new Uri("http://127.0.0.1:4723");
        }
    }
}