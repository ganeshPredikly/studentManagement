using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;
using System.Collections.Generic;

namespace StudentManagementApp.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> students = new List<Student>();

        public static List<Student> GetStudents()
        {
            return students;
        }

        public IActionResult Index()
        {
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            // Get photo data from form
            var photoData = Request.Form["PhotoData"];
            if (!string.IsNullOrEmpty(photoData))
            {
                student.PhotoData = photoData;
            }
            if (ModelState.IsValid)
            {
                students.Add(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Edit(int id)
        {
            var student = students.Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var existingStudent = students.Find(s => s.Id == student.Id);
                if (existingStudent != null)
                {
                    existingStudent.Name = student.Name;
                    existingStudent.Age = student.Age;
                    existingStudent.Grade = student.Grade;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            var student = students.Find(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}