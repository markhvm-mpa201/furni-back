using furni.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace furni.Areas.Admin.Controllers;

[Area("Admin")]
public class EmployeeController(AppDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var employees = _context.Employees.ToList();
        return View(employees);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        if (!ModelState.IsValid) return View(employee);
        employee.CreatedDate = DateTime.UtcNow.AddHours(4);
        employee.IsDeleted = false;
        _context.Add(employee);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var employee = _context.Employees.FirstOrDefault(p => p.Id == id);
        if (employee == null) return NotFound("Employee isvnot found!");
        _context.Remove(employee);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var employee = _context.Employees.FirstOrDefault(p => p.Id == id);
        if (employee == null) return NotFound();

        return View(employee);
    }


    [HttpPost]
    public IActionResult Update(Employee employee)
    {
        if (!ModelState.IsValid)
            return View(employee);

        var existemployee = _context.Employees.FirstOrDefault(p => p.Id == employee.Id);
        if (existemployee == null) return NotFound();

        existemployee.FirstName = employee.FirstName;
        existemployee.LastName = employee.LastName;
        existemployee.Position = employee.Position;
        existemployee.Description = employee.Description;
        existemployee.ImageName = employee.ImageName;
        existemployee.ImageUrl = employee.ImageUrl;

        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public IActionResult Toggle(int id)
    {

        var existemployee = _context.Employees.FirstOrDefault(s => s.Id == id);
        if (existemployee == null) return NotFound();

        existemployee.IsDeleted = !existemployee.IsDeleted;

        _context.Employees.Update(existemployee);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
