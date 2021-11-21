using System;
using System.ComponentModel.DataAnnotations;

namespace GM.Models
{
    public class Grocery
    {
        public Grocery()
        {
            CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public bool IsExpire { get; set; }
        public DateTime? ExpireOn { get; set; }
        public DateTime CreatedOn { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public void MarkAsExpire()
        {
            if (!IsExpire)
            {
                IsExpire = true;
                ExpireOn = DateTime.UtcNow;
            }
        }
    }
}
