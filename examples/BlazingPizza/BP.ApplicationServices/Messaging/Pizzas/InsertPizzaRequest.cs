namespace BP.ApplicationServices.Messaging.Pizzas
{
    public class InsertPizzaRequest : ServiceRequestBase
    {
        public PizzaPropertiesVM PizzaProperties { get; set; }
    }
}
