namespace BP.ApplicationServices.Messaging.Pizzas
{
    public class GetPizzaByNameRequest : NameRequest
    {
        public GetPizzaByNameRequest(string name) 
            : base(name) { }
    }
}
