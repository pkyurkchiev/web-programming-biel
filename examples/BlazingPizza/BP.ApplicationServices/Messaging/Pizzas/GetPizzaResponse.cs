using BP.ApplicationServices.ViewModels;

namespace BP.ApplicationServices.Messaging.Pizzas
{
    public class GetPizzaResponse : ServiceResponseBase
    {
        public PizzaViewModel Pizza { get; set; }
    }
}
