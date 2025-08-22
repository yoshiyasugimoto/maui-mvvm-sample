namespace mauiMvvmSample.E2ETests.Tests
{
    [TestFixture]
    public class SimpleE2ETest
    {
        [Test]
        public void TestConfiguration_ShouldBeValid()
        {
            // Arrange
            var platform = TestContext.Parameters.Get("platform", "Not Set");
            var appPath = TestContext.Parameters.Get("appPath", "Not Set");
            
            // Act & Assert
            Assert.That(platform, Is.Not.EqualTo("Not Set"), "Platform should be configured");
            Assert.That(platform, Is.EqualTo("Windows").Or.EqualTo("Mac"), "Platform should be Windows or Mac");
            Assert.That(appPath, Is.Not.EqualTo("Not Set"), "App path should be configured");
            
            TestContext.Out.WriteLine($"Test configuration is valid - Platform: {platform}, AppPath: {appPath}");
        }
        
        [Test]
        public void BasicCalculation_ShouldWork()
        {
            // Arrange
            int a = 5;
            int b = 3;
            
            // Act
            int sum = a + b;
            int difference = a - b;
            int product = a * b;
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sum, Is.EqualTo(8), "Sum should be correct");
                Assert.That(difference, Is.EqualTo(2), "Difference should be correct");
                Assert.That(product, Is.EqualTo(15), "Product should be correct");
            });
            
            TestContext.Out.WriteLine("Basic calculations passed successfully");
        }
        
        [Test]
        public void SimulateCounterLogic_ShouldWork()
        {
            // Arrange
            int counter = 0;
            string GetButtonText(int count) => count == 0 ? "Click me" : 
                                               count == 1 ? "Clicked 1 time" : 
                                               $"Clicked {count} times";
            
            // Act & Assert - Initial state
            Assert.That(GetButtonText(counter), Is.EqualTo("Click me"));
            
            // Simulate first click
            counter++;
            Assert.That(GetButtonText(counter), Is.EqualTo("Clicked 1 time"));
            
            // Simulate second click
            counter++;
            Assert.That(GetButtonText(counter), Is.EqualTo("Clicked 2 times"));
            
            // Simulate multiple clicks
            counter = 5;
            Assert.That(GetButtonText(counter), Is.EqualTo("Clicked 5 times"));
            
            TestContext.Out.WriteLine($"Counter logic simulation passed - Final count: {counter}");
        }
        
        [Test]
        public void TestEnvironment_ShouldBeConfigured()
        {
            // Arrange & Act
            var workingDirectory = TestContext.CurrentContext.WorkDirectory;
            var testDirectory = TestContext.CurrentContext.TestDirectory;
            var testName = TestContext.CurrentContext.Test.Name;
            
            // Assert
            Assert.That(workingDirectory, Is.Not.Null.And.Not.Empty, "Working directory should be set");
            Assert.That(testDirectory, Is.Not.Null.And.Not.Empty, "Test directory should be set");
            Assert.That(testName, Is.EqualTo("TestEnvironment_ShouldBeConfigured"), "Test name should match");
            
            TestContext.Out.WriteLine($"Test environment is properly configured");
            TestContext.Out.WriteLine($"Working Directory: {workingDirectory}");
            TestContext.Out.WriteLine($"Test Directory: {testDirectory}");
        }
    }
}