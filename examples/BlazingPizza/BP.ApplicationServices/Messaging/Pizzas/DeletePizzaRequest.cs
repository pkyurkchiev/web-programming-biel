using System;

namespace BP.ApplicationServices.Messaging.Pizzas
{
    public class DeletePizzaRequest : GuidIdRequest
    {
        public DeletePizzaRequest(Guid pizzaId) : base(pizzaId)
        { }
    }
}
