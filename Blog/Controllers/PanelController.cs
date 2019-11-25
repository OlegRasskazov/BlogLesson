using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Blog.Models;
using Blog.ViewModels;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repository;
        private IFileManager _fileManager;

        public PanelController(
            IRepository repository,
            IFileManager fileManager)
        {
            _fileManager = fileManager;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var posts = _repository.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repository.GetPost((int)id);
                return View(new PostViewModel()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image,
                    Description= post.Description,
                    Tags = post.Tags,
                    Category = post.Category
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel postViewModel)
        {
            var post = new Post()
            {
                Id = postViewModel.Id,
                Title = postViewModel.Title,
                Body = postViewModel.Body,
                Description = postViewModel.Description,
                Category = postViewModel.Category,
                Tags = postViewModel.Tags
            };

            if (postViewModel.Image == null)
                post.Image = postViewModel.CurrentImage;
            else
                post.Image = await _fileManager.SaveImage(postViewModel.Image);

            if (post.Id > 0)
                _repository.UpdatePost(post);
            else
                _repository.AddPost(post);

            if (await _repository.SaveChangesAsync())
                return RedirectToAction("Index");
            return View(postViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repository.RemovePost(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
