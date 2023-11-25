using Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

ChromeOptions options = new ChromeOptions();
options.PageLoadStrategy = PageLoadStrategy.Eager;

IWebDriver driver = new ChromeDriver(options);
driver.Url = "https://www.drive2.ru/l/288230376152315713";

try
{
    IList<IWebElement> comments = driver.FindElements(By.ClassName("c-comment"));

    foreach (var comment in comments)
    {
        Console.WriteLine("************************************************");
        var nick = comment.FindElement(By.TagName("span"));
        var text = comment.FindElement(By.TagName("p"));

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(nick.Text);
        Console.ForegroundColor = ConsoleColor.White;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text.Text);
        Console.ForegroundColor = ConsoleColor.White;

    }
}
catch(NoSuchElementException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    driver.Quit();
}
