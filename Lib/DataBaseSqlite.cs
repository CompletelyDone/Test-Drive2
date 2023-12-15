using Lib.Repos;
using Lib.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class DataBaseSqlite : DbContext, IDatabaseFunc
    {
        private String path;
        public DataBaseSqlite(String name)
        {
            this.path = Path.GetTempPath() + name;
            Database.EnsureCreated();
        }
        public DbSet<Comment> Comments { get; set; } = null!;

        public void AddComment(string name, string message)
        {
            Comment comment = new Comment() {Name = name, Message = message };
            Comments.Add(comment);
            this.SaveChanges();
        }
        public void AddComment(int id, string name, string message)
        {
            Comment comment = new Comment() { LocalId = id, Name = name, Message = message };
            Comments.Add(comment);
            this.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            var comm = Comments.Where(x => x.LocalId == id).FirstOrDefault();
            if (comm != null) Comments.Remove(comm);
            this.SaveChanges();
        }

        public Comment GetById(int id)
        {
            var comm = Comments.Where(x => x.LocalId == id).FirstOrDefault();
            return comm;
        }

        public List<Comment> GetByName(string name)
        {
            var comm = Comments.Where(x => x.Name == name).ToList();
            return comm;
        }

        public void UpdateComment(int id, string newMessage)
        {
            var comm = Comments.Where(x => x.LocalId == id).FirstOrDefault();
            comm.Message = newMessage;
            this.SaveChanges();
        }
        public void RecreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }
    }
}
