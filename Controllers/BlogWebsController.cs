using BlogWebApplication.Models;
using BlogWebApplication.Services;
using BlogWebApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
													BlogID = objectBlog.BlogID,
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
		///		[19/05/2024] - Updated - LastModifyDate null if create new.
		///		[21/05/2024] - Updated - Add case edit.
		/// </history>
		[HttpPost]
		public IActionResult AddNewBlog(BlogViewModel blogViewModel)
		{
			// 1. Add new.
			if (blogViewModel.BlogID == 0)
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
					Image = newFileName,
				};
				_context.BlogWebs.Add(objectBlog);
			}
			// 2. Edit.
			else
			{
				Blog objectBlogEdit = _context.BlogWebs.Single(model => model.BlogID == blogViewModel.BlogID);
				string newFileName = DateTime.Now.ToString("yyyyMMddmmmssfff");
				if (blogViewModel.Image != null)
				{
					newFileName += Path.GetExtension(blogViewModel.Image.FileName);
					string imgFullPath = _environment.WebRootPath + "/images/" + newFileName;
					using (var stream = System.IO.File.Create(imgFullPath))
					{
						blogViewModel.Image.CopyTo(stream);
					}
					objectBlogEdit.Image = newFileName;
				}
				else
				{
					newFileName = null;
				}

				objectBlogEdit.BlogName = blogViewModel.BlogName;
				objectBlogEdit.BlogDescription = blogViewModel.BlogDescription;
				objectBlogEdit.ReadingTime = blogViewModel.ReadingTime;
				objectBlogEdit.Author = blogViewModel.Author;
				objectBlogEdit.CategoryID = blogViewModel.CategoryID;
				objectBlogEdit.Link = blogViewModel.Link;
				objectBlogEdit.LastDateModified = DateTime.Now;

			}
			_context.SaveChanges();
			return RedirectToAction("ViewIndex", "Home");
		}

		/// <summary>
		///		Load detail data blog by BlogID.
		/// </summary>
		/// <returns></returns>
		/// <history>
		///		[17/05/2024] - Created
		/// </history>
		public IActionResult ViewIndex(int? id)
		{
			if (id != null)
			{
				Blog blog = _context.BlogWebs.Find(id);
				return View(model: blog);
			}
			return View();
		}

		/// <summary>
		///		Load data update, return modal add new.
		/// </summary>
		/// <returns></returns>
		/// <history>
		///		[19/05/2024] - Create.
		/// </history>
		[HttpGet]
		public JsonResult UpdateBlog(int? blogID)
		{
			var result = _context.BlogWebs.Single(model => model.BlogID == blogID);
			return Json(result);
		}
	}
}
