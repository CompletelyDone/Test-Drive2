using Lib;
using Lib.Repos;
using System.Xml.Linq;

namespace TestUnit
{
    public class Tests
    {
        private DataBaseSqlite db;
        [SetUp]
        public void Setup()
        {
            db = new DataBaseSqlite("DBNew");
            db.RecreateDatabase();
        }

        [TestCase(5, "Vasya", "Hello World!")]
        public void AddComplete(int id, string name, string message)
        {
            int count = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).Count();

            db.AddComment(id, name, message);

            int newCount = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).Count();

            Assert.AreEqual(count + 1, newCount);

        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void Add2Elems(int id, string name, string message)
        {
            int count = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).Count();

            db.AddComment(id, name, message);
            db.AddComment(id, name, message);

            int newCount = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).Count();

            Assert.AreEqual(count + 2, newCount);
        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void DeleteComm(int id, string name, string message)
        {
            db.AddComment(id, name, message);

            int count = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).Count();

            db.DeleteComment(id);

            int newCount = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).Count();

            Assert.AreEqual(count - 1, newCount);
        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void DeleteDontCrushAfterDeletingNullComm(int id, string name, string message)
        {
            db.AddComment(id, name, message);

            int count = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).Count();

            db.DeleteComment(id);
            db.DeleteComment(id);

            int newCount = db.Comments.Where(x => x.LocalId == id && x.Name == name && x.Message == message).Count();

            Assert.AreEqual(count - 1, newCount);
        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void getComment(int id, string name, string message)
        {
            db.AddComment(id, name, message);

            var tmp = db.GetById(id);

            Assert.AreEqual(message, tmp.Message);
        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void getBySameLocalIdComment(int id, string name, string message)
        {
            db.AddComment(id, name, message);
            db.AddComment(id, name, "Новый текст");

            var tmp = db.GetById(id);

            Assert.AreEqual(message, tmp.Message);
        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void getCollectionComments(int id, string name, string message)
        {
            db.AddComment(id, name, message);
            db.AddComment(id, "Олег", message);
            db.AddComment(id, name, message);
            db.AddComment(id, name, message);
            db.AddComment(id, name, message);
            db.AddComment(id, name, message);

            var tmp = db.GetByName(name);

            Assert.AreEqual(5, tmp.Count);
        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void getEmptyCollectionComments(int id, string name, string message)
        {
            var tmp = db.GetByName(name);

            Assert.AreEqual(0, tmp.Count);
        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void editComment(int id, string name, string message)
        {
            db.AddComment(id, name, message);

            String newMess = "Bruce?!";

            db.UpdateComment(id, newMess);

            var tmp = db.GetById(id);

            Assert.AreEqual(newMess, tmp.Message);
        }
        [TestCase(5, "Vasya", "Hello World!")]
        public void editCommentWithNullMessageText(int id, string name, string message)
        {
            db.AddComment(id, name, message);

            String newMess = "";

            db.UpdateComment(id, newMess);

            var tmp = db.GetById(id);

            Assert.AreEqual(newMess, tmp.Message);
        }
    }
}