using System.ComponentModel.DataAnnotations;

namespace practica01.Models
{
    public class StaffCategoryModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<StaffModel> StaffMembers { get; set; } = new List<StaffModel>();
    }
}
