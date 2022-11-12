using BP.Infrastructure.Domain;

namespace BP.Domain.Pizza
{
    public static class PizzaBusinessRule
    {
        public static readonly BusinessRule NameRequired = new("A pizza must have a name.");
    }
}
