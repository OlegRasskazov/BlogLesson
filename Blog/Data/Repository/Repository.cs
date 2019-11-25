using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
            
        } 

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        }

        public List<Post> GetAllPosts(string category)
        {
            Func<Post, bool> InCategory = (post) => post.Category.ToLower().Equals(category.ToLower());

            return _ctx.Posts.Where(post => InCategory(post)).ToList();
        }

        public Post GetPost(int Id)
        {
            return _ctx.Posts.FirstOrDefault(m => m.Id == Id);
        }

        public void RemovePost(int Id)
        {
            _ctx.Posts.Remove(GetPost(Id));
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() >0)
            {
                return true;
            }

            return false;
        }
    }
}
