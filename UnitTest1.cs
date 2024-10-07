using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace curious4everautomation
{
    public class Program
    {

        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void registration_fails_with_empty_name()
        {
            driver.Navigate().GoToUrl("https://app-linux-test-curious4ever-user.azurewebsites.net/registration");
            System.Threading.Thread.Sleep(2000);
            IWebElement fullnameField = driver.FindElement(By.Id("txtFullName"));
            IWebElement emailField = driver.FindElement(By.Id("txtEmailId"));
            
            IWebElement registerButton = driver.FindElement(By.Id("btnRegister"));

            fullnameField.SendKeys("abc");
            
            registerButton.Click();
            string answer = null;

            Console.WriteLine(emailField.Text);
           
            if (emailField.Text == "")
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(drv => drv.FindElement(By.CssSelector("span.error-message.alert.alert-danger.d-block")).Displayed);
                IWebElement errorMessage = driver.FindElement(By.CssSelector("span.error-message.alert.alert-danger.d-block"));
                Console.WriteLine("Error Message: " + errorMessage.Text);
                answer = errorMessage.Text;
            }
            else
            {
                answer = null;
            }

            Assert.That(answer, Is.EqualTo("Please mention your email id."));
        }

        [Test]
        public void registration_fails_with_incorrect_email()
        {
            driver.Navigate().GoToUrl("https://app-linux-test-curious4ever-user.azurewebsites.net/registration");
            System.Threading.Thread.Sleep(2000);
            IWebElement fullnameField = driver.FindElement(By.Id("txtFullName"));
            IWebElement emailField = driver.FindElement(By.Id("txtEmailId"));
            IWebElement registerButton = driver.FindElement(By.Id("btnRegister"));

            registerButton.Click();
            string answer = null;

            if (emailField.Text == "")
            {
               
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(drv => drv.FindElement(By.CssSelector("span.error-message.alert.alert-danger.d-block")).Displayed);
                IWebElement errorMessage = driver.FindElement(By.CssSelector("span.error-message.alert.alert-danger.d-block"));
                Console.WriteLine("Error Message: " + errorMessage.Text);
                answer = errorMessage.Text;
            }
            else
            {
                answer = null;


            }


            Assert.That(answer, Is.EqualTo("Please mention your name."));
        }

        [TearDown]
        public void TearDown()
        {
            // Check if driver is not null before quitting
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); // Dispose of the driver
            }
        }


    }
}