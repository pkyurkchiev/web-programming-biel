using BP.ApplicationServices.Exceptions;
using BP.ApplicationServices.Interfaces;
using BP.ApplicationServices.Messaging.Pizzas;
using BP.ApplicationServices.ModelConversions;
using BP.Domain.Pizza;
using BP.Domain.ValueObjects;
using BP.Infrastructure.Domain;
using BP.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BP.ApplicationServices.Implementations
{
    public class PizzaService : ApplicationServiceBase, IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository ?? throw new ArgumentNullException("Pizza repo");
        }

        public GetPizzaResponse GetById(GetPizzaRequest getPizzaRequest)
        {
            GetPizzaResponse result = new GetPizzaResponse();

            try
            {
                Pizza pizza = _pizzaRepository.FindBy(getPizzaRequest.Id);
                result.Pizza = pizza.ConvertToViewModel();
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.StatusDesciption = ex.Message;
            }

            return result;
        }

        public GetPizzaResponse GetByName(GetPizzaByNameRequest getPizzaByNameRequest)
        {
            GetPizzaResponse result = new GetPizzaResponse();

            try
            {
                Pizza pizza = _pizzaRepository.FindByName(getPizzaByNameRequest.Name);
                result.Pizza = pizza.ConvertToViewModel();
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.StatusDesciption = ex.Message;
            }

            return result;
        }

        public GetPizzasResponse GetAll()
        {
            GetPizzasResponse result = new GetPizzasResponse();
            IEnumerable<Pizza> allPizzas = null;

            try
            {
                allPizzas = _pizzaRepository.FindAll();
                result.Pizzas = allPizzas.ConvertToViewModels();
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.StatusDesciption = ex.Message;
            }

            return result;
        }

        public InsertPizzaResponse InsertPizza(InsertPizzaRequest insertPizzaRequest)
        {
            InsertPizzaResponse result = new InsertPizzaResponse();
            Pizza newPizza = AssignAvailablePropertiesToDomain(insertPizzaRequest.PizzaProperties);
            try
            {
                ThrowExceptionIfPizzaIsInvalid(newPizza);

                _pizzaRepository.Insert(newPizza);

            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.StatusDesciption = ex.Message;
            }

            return result;
        }

        public UpdatePizzaResponse UpdatePizza(UpdatePizzaRequest updatePizzaRequest)
        {
            UpdatePizzaResponse result = new UpdatePizzaResponse();

            try
            {
                Pizza existingPizza = _pizzaRepository.FindBy(updatePizzaRequest.Id);
                if (existingPizza != null)
                {
                    Pizza assignableProperties = AssignAvailablePropertiesToDomain(updatePizzaRequest.PizzaProperties);
                    existingPizza.Name = assignableProperties.Name ?? existingPizza.Name;
                    existingPizza.LaunchDate = assignableProperties.LaunchDate ?? existingPizza.LaunchDate;
                    existingPizza.Price = assignableProperties.Price;
                    existingPizza.PizzaRecipe = assignableProperties.PizzaRecipe ?? existingPizza.PizzaRecipe;
                    ThrowExceptionIfPizzaIsInvalid(existingPizza);

                    _pizzaRepository.Update(existingPizza);

                    return new UpdatePizzaResponse();
                }
                else
                {
                    GetStandardPizzaNotFoundException();
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.StatusDesciption = ex.Message;
            }

            return result;
        }

        public DeletePizzaResponse DeletePizza(DeletePizzaRequest deletePizzaRequest)
        {
            DeletePizzaResponse result = new DeletePizzaResponse();

            try
            {
                Pizza pizza = _pizzaRepository.FindBy(deletePizzaRequest.Id);
                if (pizza != null)
                {
                    _pizzaRepository.Delete(pizza);

                }
                else
                {
                    GetStandardPizzaNotFoundException();
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.StatusDesciption = ex.Message;
            }

            return result;
        }

        private ResourceNotFoundException GetStandardPizzaNotFoundException()
        {
            return new ResourceNotFoundException("The requested pizza was not found.");
        }

        private Pizza AssignAvailablePropertiesToDomain(PizzaPropertiesVM pizzaProperties)
        {
            Pizza pizza = new Pizza
            {
                Name = pizzaProperties.Name,
                Price = pizzaProperties.Price,
                LaunchDate = pizzaProperties.LaunchDate
            };

            Recipe recipe = new Recipe
            {
                Ingredients = pizzaProperties.Ingredients,
                CookingTime = pizzaProperties.CookingTime,
                Description = pizzaProperties.Description
            };
            pizza.PizzaRecipe = recipe;

            return pizza;
        }

        private void ThrowExceptionIfPizzaIsInvalid(Pizza newPizza)
        {
            IEnumerable<BusinessRule> brokenRules = newPizza.GetBrokenRules();
            if (brokenRules.Count() > 0)
            {
                StringBuilder brokenRulesBuilder = new StringBuilder();
                brokenRulesBuilder.AppendLine("There were problems saving the LoadtestPortalCustomer object:");
                foreach (BusinessRule businessRule in brokenRules)
                {
                    brokenRulesBuilder.AppendLine(businessRule.RuleDescription);
                }

                throw new Exception(brokenRulesBuilder.ToString());
            }
        }
    }
}
