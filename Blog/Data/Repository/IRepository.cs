using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int Id);
        List<Post> GetAllPostsPost(int Id);
        bool AddPost(Post post);
        bool RemovePost(int Id);
        bool UpdatePost(Post post);
    }
}
