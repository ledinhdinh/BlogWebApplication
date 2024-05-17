using BlogWebApplication.Models;
using BlogWebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.ViewComponents
{
	public class MenuViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;
		public MenuViewComponent(ApplicationDbContext context)
		{
			this._context = context;
		}

		/// <summary>
		///		Load menu = view component.
		/// </summary>
		/// <returns></returns>
		/// <history>
		///		[16/05/2024] - Created - Load data from menu.
		/// </history>
		public IViewComponentResult Invoke()
		{
			var listMenu = _context.Menus.ToList().OrderBy(m => m.MenuParentIndex);
			return View(listMenu);
		}

		//public async Task<IViewComponentResult> InvokeAsync()
		//{
		//	return View();
		//}
	}
}
