using System;
using System.ComponentModel.DataAnnotations;

namespace WOM.Models
{
    public class WorkOut
    {
        public WorkOut()
        {
            Created = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public bool Complete { get; set; }
        public DateTime Created { get; set; }
        public DateTime? MarkedComplete { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public void MarkComplete()
        {
            if (!Complete)
            {
                Complete = true;
                MarkedComplete = DateTime.UtcNow;
            }
        }
    }
}
