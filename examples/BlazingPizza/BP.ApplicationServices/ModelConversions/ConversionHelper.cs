using BP.ApplicationServices.ViewModels;
using BP.Domain.Pizza;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.ApplicationServices.ModelConversions
{
    public static class ConversionHelper
    {
        public static PizzaViewModel ConvertToViewModel(this Pizza pizza)
        {
            return new PizzaViewModel()
            {
                Name = pizza.Name,
                Price = pizza.Price,
                LaunchDate = pizza.LaunchDate,
                Ingredients = pizza.PizzaRecipe.Ingredients,
                CookingTime = pizza.PizzaRecipe.CookingTime,
                Description = pizza.PizzaRecipe.Description
            };
        }

        public static IEnumerable<PizzaViewModel> ConvertToViewModels(this IEnumerable<Pizza> pizzas)
        {
            List<PizzaViewModel> customerViewModels = new List<PizzaViewModel>();
            foreach (Pizza pizza in pizzas)
            {
                customerViewModels.Add(pizza.ConvertToViewModel());
            }
            return customerViewModels;
        }
    }
}
