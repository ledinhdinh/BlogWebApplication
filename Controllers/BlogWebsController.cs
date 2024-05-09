using BlogWebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.Controllers
{
    public class BlogWebsController : Controller
    {
        private readonly ApplicationDbContext context;

        public BlogWebsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var blogs = context.BlogWebs.ToList();
            return View(blogs);
        }
    }
}
