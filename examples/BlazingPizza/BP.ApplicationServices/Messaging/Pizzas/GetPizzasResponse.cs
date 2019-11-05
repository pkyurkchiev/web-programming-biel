using BP.ApplicationServices.ViewModels;
using System.Collections.Generic;

namespace BP.ApplicationServices.Messaging.Pizzas
{
    public class GetPizzasResponse : ServiceResponseBase
    {
        public IEnumerable<PizzaViewModel> Pizzas { get; set; }
    }
}
