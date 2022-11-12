using System;
using System.Collections.Generic;

namespace BP.ApplicationServices.Messaging.Pizzas
{
    public class PizzaPropertiesVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? LaunchDate { get; set; }
        public List<string> Ingredients { get; set; }
        public double CookingTime { get; set; }
        public string Description { get; set; }
    }
}
