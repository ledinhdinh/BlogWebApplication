using BlogWebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApplication.Controllers
{

	public class CategorysController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CategorysController(ApplicationDbContext context)
		{
			this._context = context;
		}
		public IActionResult Index()
		{
			var categoryList = _context.Categorys.OrderBy(x => x.Type).ToList();
			return View(categoryList);
		}
	}
}
