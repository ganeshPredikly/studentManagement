using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;

namespace StudentManagementApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Get students from StudentController's static list
        var students = StudentManagementApp.Controllers.StudentController.GetStudents();
        return View(students);
    }

    [HttpPost]
    public IActionResult PurchaseFromCanteen(int studentId, decimal amount)
    {
        var student = StudentManagementApp.Controllers.StudentController.GetStudents().Find(s => s.Id == studentId);
        if (student == null)
        {
            return NotFound();
        }
        if (amount > 0 && amount <= student.MealCardBalance)
        {
            student.MealCardBalance -= amount;
            TempData["Message"] = $"Purchase successful! ₹{amount} deducted.";
        }
        else
        {
            TempData["Message"] = "Invalid amount.";
        }
        return RedirectToAction("Index");
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

    [HttpPost]
    public IActionResult GenerateCertificate(int studentId)
    {
        var student = StudentManagementApp.Controllers.StudentController.GetStudents().Find(s => s.Id == studentId);
        if (student == null)
        {
            return NotFound();
        }
        // Pass student to certificate view
        return View("Certificate", student);
    }
}
