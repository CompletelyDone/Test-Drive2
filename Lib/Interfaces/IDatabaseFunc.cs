using Lib.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Interfaces
{
    public interface IDatabaseFunc
    {
        public Comment GetById(int id);
        public List<Comment> GetByName(string name);
        public void AddComment(int id, string name, string message);
        public void UpdateComment(int id, string newMessage);
        public void DeleteComment(int id);
    }
}
