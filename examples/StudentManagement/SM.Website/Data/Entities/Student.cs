using System;
using System.ComponentModel.DataAnnotations;

namespace SM.Website.Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First name")]
        [Required, StringLength(300, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [StringLength(300, MinimumLength = 3)]
        public string LastName { get; set; }
        [Display(Name = "Faculty number")]
        [Required, StringLength(10, MinimumLength = 3)]
        public string FacultyNumber { get; set; }

        [Display(Name = "Create date")]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Display(Name = "Specialty")]
        public int? SpecialtyId { get; set; }
        public virtual Specialty? Specialty { get; set; }

    }
}
