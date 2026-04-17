using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practica01.Models
{
    public class StaffModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public int StaffCategoryId { get; set; }

        [ValidateNever]
        public StaffCategoryModel StaffCategory { get; set; } = null!;

        public int? SpecialtyId { get; set; }

        [ValidateNever]
        public SpecialtyModel? Specialty { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; } = true;

        public string? PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
    }
}
