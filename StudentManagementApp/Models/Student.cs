using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Range(1, 120)]
        public int Age { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Grade { get; set; }

        [Required]
        public string Status { get; set; } // Passed or Failed

        public string PhotoData { get; set; } // Base64 image data

        public decimal MealCardBalance { get; set; } = 2000; // Monthly balance in INR
    }
}