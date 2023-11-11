using BP.Infrastructure.Domain;

namespace BP.Domain.ValueObjects
{
    public static class ValueObjectBusinessRule
    {
        public static readonly BusinessRule DescriptionInRecipeRequired = new("An recipe must have a description.");
    }
}
