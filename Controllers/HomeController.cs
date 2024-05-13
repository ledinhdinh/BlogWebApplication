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

		public IActionResult Index()
		{
			//var categoryList = _context.Categorys.OrderBy(x => x.Type).ToList();
			// [13/05/2024] - Update - Lấy dữ liệu CategoryID lên đổ vào combobox.
			var category = new BlogViewModel();
			category.listCategory = (from obj in _context.Categorys
									 select new SelectListItem()
									 {
										 Text = obj.CategoryName,
										 Value = obj.CategoryID.ToString()
									 }).ToList();

			return View(category);
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
