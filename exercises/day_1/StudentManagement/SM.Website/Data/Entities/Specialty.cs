using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Website.Data.Entities
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
