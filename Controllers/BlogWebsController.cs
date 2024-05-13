using BlogWebApplication.Models;
using BlogWebApplication.Services;
using BlogWebApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.Controllers
{
	public class BlogWebsController : Controller
	{
		private readonly ApplicationDbContext context;
		private readonly IWebHostEnvironment environment;

		public BlogWebsController(ApplicationDbContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}
		public IActionResult Index()
		{
			var blogs = context.BlogWebs.ToList();
			return View(blogs);
		}

		/// <summary>
		///		Function save file.
		/// </summary>
		/// <param name="blogViewModel"></param>
		/// <returns></returns>
		/// <history>
		///		[13/05/2024] - Create.
		/// </history>
		[HttpPost]
		public ActionResult AddNewBlog(BlogViewModel blogViewModel)
		{
			if (blogViewModel.Image == null)
			{
				ModelState.AddModelError("ImageFile", "Thiếu file ảnh");
			}
			//if (!ModelState.IsValid)
			//{
			//	return View(blogViewModel);
			//}

			// save image file.
			string newFileName = DateTime.Now.ToString("yyyyMMddmmmssfff");
			newFileName += Path.GetExtension(blogViewModel.Image!.FileName);

			string imgFullPath = environment.WebRootPath + "/images/" + newFileName;
			using (var stream = System.IO.File.Create(imgFullPath))
			{
				blogViewModel.Image.CopyTo(stream);
			}

			Blog objectBlog = new Blog()
			{
				BlogName = blogViewModel.BlogName,
				BlogDescription = blogViewModel.BlogDescription,
				ReadingTime = blogViewModel.ReadingTime,
				Author = blogViewModel.Author,
				CategoryID = blogViewModel.CategoryID,
				Link = blogViewModel.Link,
				CreateDate = DateTime.Now,
				LastDateModified = DateTime.Now,
				Image = newFileName,
			};

			context.BlogWebs.Add(objectBlog);
			context.SaveChanges();

			//return Json(objectBlog);
			return RedirectToAction("Index", "Home");
		}
	}
}
