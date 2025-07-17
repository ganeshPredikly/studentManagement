using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using StudentManagementApp.Helpers;

namespace StudentManagementApp.Controllers
{
    public class CanteenController : Controller
    {
        // Static list of canteen items
        private static List<CanteenItem> items = new List<CanteenItem>
        {
            new CanteenItem { Id = 1, Name = "Sandwich", Price = 50 },
            new CanteenItem { Id = 2, Name = "Juice", Price = 30 },
            new CanteenItem { Id = 3, Name = "Coffee", Price = 40 },
            new CanteenItem { Id = 4, Name = "Samosa", Price = 20 },
            new CanteenItem { Id = 5, Name = "Fruit Bowl", Price = 60 },
            new CanteenItem { Id = 6, Name = "Lemon Tea", Price = 35 }
        };

        // Simple cart per session
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.Items = items;
            ViewBag.Cart = cart;
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int itemId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = items.Find(i => i.Id == itemId);
            if (item != null && quantity > 0)
            {
                var cartItem = cart.Find(ci => ci.ItemId == itemId);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartItem { ItemId = itemId, Name = item.Name, Price = item.Price, Quantity = quantity });
                }
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout(int studentId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var student = StudentManagementApp.Controllers.StudentController.GetStudents().Find(s => s.Id == studentId);
            if (student == null)
            {
                TempData["Message"] = "Student not found.";
                return RedirectToAction("Index");
            }
            decimal total = 0;
            foreach (var ci in cart)
            {
                total += ci.Price * ci.Quantity;
            }
            if (total > 0 && total <= student.MealCardBalance)
            {
                student.MealCardBalance -= total;
                TempData["Message"] = $"Checkout successful! ₹{total} deducted.";
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                TempData["Message"] = "Insufficient balance or empty cart.";
            }
            return RedirectToAction("Index");
        }
    }

    public class CartItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CanteenItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
