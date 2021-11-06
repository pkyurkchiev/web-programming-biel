using System;
using System.Collections.Generic;

namespace BP.Repository.DataBase
{
    public class DatabasePizza : Entity<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? LaunchDate { get; set; }
        public List<string> Ingredients { get; set; }
        public double CookingTime { get; set; }
        public string Description { get; set; }
    }
}
