using System.ComponentModel.DataAnnotations;

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

        public StaffCategoryModel StaffCategory { get; set; } = null!;

        public int? SpecialtyId { get; set; }

        public SpecialtyModel? Specialty { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
