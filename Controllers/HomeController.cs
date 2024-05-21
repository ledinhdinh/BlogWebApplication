using BlogWebApplication.Models;
using BlogWebApplication.Services;
using BlogWebApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace BlogWebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			this._context = context;
		}

		/// <summary>
		///		Get data for drop down category.
		/// </summary>
		/// <returns></returns>
		/// <history>
		///		[13/05/2024] - Updated - Lấy dữ liệu CategoryID lên đổ vào combobox add modal.
		/// </history>
		public IActionResult ViewIndex()
		{
			var category = new BlogViewModel();
			category.listCategory = (from objCategory in _context.Categorys
									 select new SelectListItem()
									 {
										 Text = objCategory.CategoryName,
										 Value = objCategory.CategoryID.ToString()
									 }).ToList();
			return View(model: category);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
