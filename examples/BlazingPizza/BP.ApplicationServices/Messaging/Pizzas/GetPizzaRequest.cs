using System;

namespace BP.ApplicationServices.Messaging.Pizzas
{
    public class GetPizzaRequest : GuidIdRequest
    {
        public GetPizzaRequest(Guid pizzaId) 
            : base(pizzaId) { }
    }
}
