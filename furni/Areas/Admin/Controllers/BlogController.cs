using furni.Contexts;
using furni.Models;
using Microsoft.AspNetCore.Mvc;

namespace furni.Areas.Admin.Controllers;


[Area("Admin")]
public class BlogController(AppDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var blogs = _context.Blogs.ToList();
        return View(blogs);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Blog blog)
    {
        if (!ModelState.IsValid) return View(blog);
        blog.CreatedDate = DateTime.UtcNow.AddHours(4);
        blog.IsDeleted = false;
        _context.Add(blog);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var blog = _context.Blogs.FirstOrDefault(p => p.Id == id);
        if (blog == null) return NotFound("Blog isvnot found!");
        _context.Remove(blog);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var blog = _context.Blogs.FirstOrDefault(p => p.Id == id);
        if (blog == null) return NotFound();

        return View(blog);
    }


    [HttpPost]
    public IActionResult Update(Blog blog)
    {
        if (!ModelState.IsValid)
            return View(blog);

        var existBlog = _context.Blogs.FirstOrDefault(p => p.Id == blog.Id);
        if (existBlog == null) return NotFound();

        existBlog.Title = blog.Title;
        existBlog.Text = blog.Text;
        existBlog.EmployeeId = blog.EmployeeId;
        existBlog.ImageName = blog.ImageName;
        existBlog.ImageUrl = blog.ImageUrl;

        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public IActionResult Toggle(int id)
    {

        var existBlog = _context.Blogs.FirstOrDefault(s => s.Id == id);
        if (existBlog == null) return NotFound();

        existBlog.IsDeleted = !existBlog.IsDeleted;

        _context.Blogs.Update(existBlog);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
