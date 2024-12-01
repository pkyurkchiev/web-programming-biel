using System;
using System.ComponentModel.DataAnnotations;

namespace GM.Models{
    public class Grocery{
        public Grocery() {
            DateOfManufactoring = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public bool IsExpire { get; set; }
        public DateTime? DateOfExpiration { get; set; }
        public DateTime DateOfManufactoring { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public void MarkAsExpire() {
            if (!IsExpire) {
                IsExpire = true;
                DateOfExpiration = DateTime.UtcNow;
            }
        }
    }
}
