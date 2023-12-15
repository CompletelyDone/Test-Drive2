using Lib.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Parser
    {
        private IDatabaseFunc db;
        private IWebDriver driver;
        public Parser(IWebDriver driver, IDatabaseFunc db, string url)
        {
            this.db = db;
            this.driver = driver;
            driver.Url = url;
        }
        public void Parse()
        {
            try
            {
                IList<IWebElement> comments = driver.FindElements(By.ClassName("c-comment"));

                foreach (var comment in comments)
                {
                    var nick = comment.FindElement(By.TagName("span"));
                    var text = comment.FindElement(By.TagName("p"));

                    db.AddComment(nick.Text, text.Text);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Успешно добавлены в бд");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
