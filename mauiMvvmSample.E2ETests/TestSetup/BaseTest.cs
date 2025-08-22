using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium.Mac;
using OpenQA.Selenium.Support.UI;

namespace mauiMvvmSample.E2ETests.TestSetup
{
    public abstract class BaseTest
    {
        protected AppiumDriver App => AppiumSetup.App!;
        private string _platform;
        private string _appPath;

        protected BaseTest()
        {
            // Try TestContext parameters first
            _platform = TestContext.Parameters.Get("platform", string.Empty);
            _appPath = TestContext.Parameters.Get("appPath", string.Empty);
            
            // Fallback to environment variables if parameters are not set
            if (string.IsNullOrEmpty(_platform))
            {
                _platform = Environment.GetEnvironmentVariable("platform") ?? "Windows";
            }
            
            if (string.IsNullOrEmpty(_appPath))
            {
                _appPath = Environment.GetEnvironmentVariable("appPath") ?? string.Empty;
            }
            
            if (string.IsNullOrEmpty(_appPath))
            {
                _appPath = GetDefaultAppPath();
            }
        }

        [SetUp]
        public void Setup()
        {
            AppiumSetup.StartApp(_platform, _appPath);
        }

        [TearDown]
        public void TearDown()
        {
            AppiumSetup.StopApp();
        }
        
        protected AppiumElement FindUIElement(string id)
        {
            if (App is WindowsDriver)
            {
                return App.FindElement(MobileBy.AccessibilityId(id));
            }
            else
            {
                return App.FindElement(MobileBy.Id(id));
            }
        }
        
        protected void WaitForElement(string id, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(App, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(driver =>
            {
                try
                {
                    var element = FindUIElement(id);
                    return element != null && element.Displayed;
                }
                catch
                {
                    return false;
                }
            });
        }

        private string GetDefaultAppPath()
        {
            if (_platform.Equals("Windows", StringComparison.OrdinalIgnoreCase))
            {
                return Path.Combine(TestContext.CurrentContext.TestDirectory, 
                    "..", "..", "..", "..", "mauiMvvmSample", "bin", "Debug", 
                    "net9.0-windows10.0.19041.0", "win10-x64", "mauiMvvmSample.exe");
            }
            else
            {
                return Path.Combine(TestContext.CurrentContext.TestDirectory, 
                    "..", "..", "..", "..", "mauiMvvmSample", "bin", "Debug", 
                    "net9.0-maccatalyst", "mauiMvvmSample.app");
            }
        }
    }
}