using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTLab2_Final.Models;
using System.Diagnostics;

namespace PTLab2_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationContext db;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Electronics.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Electronic electronic)
        {
            db.Electronics.Add(electronic);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Electronic? electronic = await db.Electronics.FirstOrDefaultAsync(p => p.Id == id);
                if (electronic != null)
                {
                    db.Electronics.Remove(electronic);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Electronic? electronic = await db.Electronics.FirstOrDefaultAsync(p => p.Id == id);
                if (electronic != null) return View(electronic);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Electronic electronic)
        {
            db.Electronics.Update(electronic);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}