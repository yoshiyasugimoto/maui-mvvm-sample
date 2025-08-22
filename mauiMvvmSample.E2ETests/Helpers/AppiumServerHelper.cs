using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace mauiMvvmSample.E2ETests.Helpers
{
    public static class AppiumServerHelper
    {
        private static AppiumLocalService? _appiumLocalService;
        private const string DefaultHost = "127.0.0.1";
        private const int DefaultPort = 4723;

        public static void StartAppiumLocalServer(string host = DefaultHost, int port = DefaultPort)
        {
            if (_appiumLocalService != null)
            {
                return;
            }

            var builder = new AppiumServiceBuilder()
                .WithIPAddress(host)
                .UsingPort(port)
                .WithStartUpTimeOut(TimeSpan.FromSeconds(60));

            _appiumLocalService = builder.Build();
            _appiumLocalService.Start();
        }

        public static void DisposeAppiumLocalServer()
        {
            _appiumLocalService?.Dispose();
            _appiumLocalService = null;
        }

        public static bool IsRunning => _appiumLocalService?.IsRunning ?? false;
    }
}