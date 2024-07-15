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

		[TempData]
		public string StatusMessage { get; set; }
		public static string flagAction, flagStatus = string.Empty;

		public BlogWebsController(ApplicationDbContext context, IWebHostEnvironment environment)
		{
			this._context = context;
			this._environment = environment;
		}

		// Create object.
		Blog objectBlog = new Blog();

		/// <summary>
		///		Get detail data and return partial view.
		/// </summary>
		/// <param name="blogViewModel"></param>
		/// <returns></returns>
		/// <history>
		///		[14/05/2024] - Create  - Get data for view detail.
		///		[15/05/2024] - Updated - Change way get data to get image null.
		///		[11/06/2024] - Updated - Remove column ReadingTime.
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
													CoverImage = string.IsNullOrEmpty(objectBlog.CoverImage) ? null : objectBlog.CoverImage,
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
		///		[23/05/2024] - Updated - Use TempData send notify message.
		///		[11/06/2024] - Updated - Remove column ReadingTime.
		///		[14/07/2024] - Updated - Clean code & add try catch.
		/// </history>
		[HttpPost]
		public async Task<IActionResult> AddNewBlog(BlogViewModel blogViewModel)
		{
			try
			{
				//string newFileName = DateTime.Now.ToString("yyyyMMddmmmssfff");
				string newFileName = (blogViewModel.CoverImage != null) ? blogViewModel.CoverImage.FileName : null;
				if (blogViewModel.CoverImage != null)
				{
					//newFileName += Path.GetExtension(blogViewModel.CoverImage.FileName);
					string imgFullPath = _environment.WebRootPath + "/ImagesBlog/" + newFileName;
					using (var stream = System.IO.File.Create(imgFullPath))
					{
						blogViewModel.CoverImage.CopyTo(stream);
						stream.Dispose();
					}
				}

				// 1. Add new.
				if (blogViewModel.BlogID == 0)
				{
					objectBlog.BlogName = blogViewModel.BlogName;
					objectBlog.BlogDescription = blogViewModel.BlogDescription;
					objectBlog.Author = blogViewModel.Author;
					objectBlog.CategoryID = blogViewModel.CategoryID;
					objectBlog.Link = blogViewModel.Link;
					objectBlog.CreateDate = DateTime.Now;
					objectBlog.CoverImage = newFileName;

					_context.BlogWebs.Add(objectBlog);
					flagAction = "thêm mới";
				}
				// 2. Edit.
				else
				{
					objectBlog = _context.BlogWebs.Single(model => model.BlogID == blogViewModel.BlogID);

					if (string.IsNullOrEmpty(objectBlog.CoverImage))
					{
						objectBlog.CoverImage = newFileName;
					}

					objectBlog.BlogName = blogViewModel.BlogName;
					objectBlog.BlogDescription = blogViewModel.BlogDescription;
					objectBlog.Author = blogViewModel.Author;
					objectBlog.CategoryID = blogViewModel.CategoryID;
					objectBlog.Link = blogViewModel.Link;
					objectBlog.LastDateModified = DateTime.Now;

					flagAction = "cập nhật";
				}
				flagStatus = "thành công !";
				_context.SaveChanges();
				StatusMessage = $"Đã {flagAction} bài viết {flagStatus} ";
			}
			catch (Exception error)
			{
				StatusMessage = "Đã có lỗi xảy ra !";
			}
			// Code trong khối này được thực thi ngay cả khi có phát sinh ngoại lệ hay không.
			// Khối này cơ bản để giải phóng các tài nguyên chiếm giữ.
			//finally
			//{
			//	StatusMessage = $"Đã {flagAction} bài viết {flagStatus} ";
			//}
			return RedirectToAction("ViewIndex", "Home");
		}

		/// <summary>
		///		Load detail data blog by BlogID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <history>
		///		[17/05/2024] - Created
		/// </history>

		public IActionResult ViewIndex(int? id)
		{
			if (id.HasValue)
			{
				objectBlog = _context.BlogWebs.Find(id);
				return View(model: objectBlog);
			}
			return View();
		}

		/// <summary>
		///		Load data update, return modal add new.
		/// </summary>
		/// <param name="blogID"></param>
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
