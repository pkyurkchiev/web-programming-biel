using BP.Infrastructure.Domain;
using System.Collections.Generic;

namespace BP.Domain.ValueObjects
{
    public class Recipe : ValueObjectBase
    {
        public List<string> Ingredients { get; set; }
        public double CookingTime { get; set; }
        public string Description { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Description))
            {
                AddBrokenRule(ValueObjectBusinessRule.DescriptionInRecipeRequired);
            }
        }
    }
}
