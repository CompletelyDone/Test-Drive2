using Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

IWebDriver driver = new ChromeDriver();
DataBaseSqlite db = new DataBaseSqlite("DatabaseNew");

Parser parser = new Parser(driver, db, "https://www.drive2.ru/l/288230376152315713");

parser.Parse();
