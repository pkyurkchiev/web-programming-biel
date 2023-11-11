using BP.Domain.ValueObjects;
using BP.Infrastructure.Domain;
using System;

namespace BP.Domain.Pizza
{
    public class Pizza : EntityBase<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? LaunchDate { get; set; }
        public Recipe PizzaRecipe { get; set; }
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenRule(PizzaBusinessRule.NameRequired);
            }
            PizzaRecipe.ThrowExceptionIfInvalid();
        }
    }
}
