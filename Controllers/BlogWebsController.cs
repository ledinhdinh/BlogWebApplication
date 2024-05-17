using BlogWebApplication.Models;
using BlogWebApplication.Services;
using BlogWebApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogWebApplication.Controllers
{
	public class BlogWebsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _environment;

		public BlogWebsController(ApplicationDbContext context, IWebHostEnvironment environment)
		{
			this._context = context;
			this._environment = environment;
		}

		/// <summary>
		///		Get detail data and return partial view.
		/// </summary>
		/// <returns></returns>
		/// <history>
		///		[14/05/2024] - Create  - Get data for view detail.
		///		[15/05/2024] - Updated - Change way get data to get image null.
		/// </history>
		[HttpGet]
		public PartialViewResult GetBlog(BlogViewModel blogViewModel)
		{
			IEnumerable<Blog> listObjectBlog = (from objectBlog in _context.BlogWebs
												select new Blog()
												{
													BlogName = objectBlog.BlogName,
													BlogDescription = objectBlog.BlogDescription,
													ReadingTime = objectBlog.ReadingTime,
													Image = string.IsNullOrEmpty(objectBlog.Image) ? null : objectBlog.Image,
													CategoryID = objectBlog.CategoryID,
													Link = string.IsNullOrEmpty(objectBlog.Link) ? null : objectBlog.Link,
													Author = string.IsNullOrEmpty(objectBlog.Author) ? null : objectBlog.Author,
													CreateDate = objectBlog.CreateDate,
												}).ToList();
			return PartialView("_PartialDisplayBlog", listObjectBlog);
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
			// save image file.
			string newFileName = DateTime.Now.ToString("yyyyMMddmmmssfff");
			if (blogViewModel.Image != null)
			{
				newFileName += Path.GetExtension(blogViewModel.Image.FileName);
				string imgFullPath = _environment.WebRootPath + "/images/" + newFileName;
				using (var stream = System.IO.File.Create(imgFullPath))
				{
					blogViewModel.Image.CopyTo(stream);
				}
			}
			else
			{
				newFileName = null;
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

			_context.BlogWebs.Add(objectBlog);
			_context.SaveChanges();

			//return Json(objectBlog);
			return RedirectToAction("ViewHome", "Home");
		}

		public IActionResult ViewIndex()
		{
			return View();
		}
	}
}
