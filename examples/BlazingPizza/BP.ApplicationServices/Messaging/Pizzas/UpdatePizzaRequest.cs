using System;

namespace BP.ApplicationServices.Messaging.Pizzas
{
    public class UpdatePizzaRequest : GuidIdRequest
    {
        public UpdatePizzaRequest(Guid pizzaId) : base(pizzaId)
		{ }

        public PizzaPropertiesVM PizzaProperties { get; set; }
    }
}
