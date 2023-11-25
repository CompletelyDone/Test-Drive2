using Lib;
using Lib.Repos;

namespace TestUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [TestCase(5, "Vasya", "Hello World!")]
        [TestCase(5, "Ïåòÿ", "Helûâôûâàlo World!")]
        [TestCase(5, "Îëåã", "Hello âàïûâàïWorld!")]
        [TestCase(5, "VaÂàâïèsya", "Hello âûàðïûàïðWorld!")]
        [TestCase(5, "Va632456sya", "Helloôûàâàôï World!")]
        public void AddComplete(int id, string name, string message)
        {
            DataBaseSqlite db = new DataBaseSqlite("MyApp");

            db.AddComment(id, name, message);

            Comment comment = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).FirstOrDefault();

            Assert.IsNotNull(comment);

            db.DeleteDatabase();
        }
        [TestCase(5, "Helloôûàâàôï World!")]
        public void AddNotCompleteNameMoreThan256symbols(int id, string message)
        {
            DataBaseSqlite db = new DataBaseSqlite("MyApp");
            Random rnd = new Random();
            String long257SymbString = new string(Enumerable.Repeat("abc", 257).Select(s => s[rnd.Next(s.Length)]).ToArray());

            db.AddComment(id, long257SymbString, message);

            Comment comment = db.Comments.Where(x => x.LocalId == id && x.Name == long257SymbString && x.Message == message).FirstOrDefault();

            Assert.IsNull(comment);

            db.DeleteDatabase();
        }
    }
}