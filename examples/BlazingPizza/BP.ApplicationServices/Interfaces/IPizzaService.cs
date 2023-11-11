using BP.ApplicationServices.Messaging.Pizzas;

namespace BP.ApplicationServices.Interfaces
{
    public interface IPizzaService
    {
        GetPizzasResponse GetAll();
        GetPizzaResponse GetById(GetPizzaRequest getUserRequest);
        GetPizzaResponse GetByName(GetPizzaByNameRequest getPizzaByNameRequest);

        InsertPizzaResponse InsertPizza(InsertPizzaRequest insertUserRequest);
        UpdatePizzaResponse UpdatePizza(UpdatePizzaRequest updateUserRequest);
        DeletePizzaResponse DeletePizza(DeletePizzaRequest deleteUserRequest);
    }
}
