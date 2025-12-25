using furni.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace furni.Controllers;

public class ProductController : Controller
{
    readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        return View(_context.Products.AsQueryable().ToList());
    }
}
