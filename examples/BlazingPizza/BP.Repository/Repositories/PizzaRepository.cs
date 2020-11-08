using BP.Domain.Pizza;
using BP.Domain.ValueObjects;
using BP.Repository.DataBase;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;

namespace BP.Repository.Repositories
{
    public class PizzaRepository : Repository<Pizza, Guid, DatabasePizza>, IPizzaRepository
    {
        public override Pizza FindBy(Guid id)
        {
            DatabasePizza databasePizza = (from du in _collection.AsQueryable<DatabasePizza>()
                                         where du.Id == id
                                         select du).FirstOrDefault();
            if (databasePizza != null)
            {
                return ConvertToDomain(databasePizza);
            }
            return null;
        }

        public Pizza FindByName(string name)
        {
            DatabasePizza databasePizza = (from du in _collection.AsQueryable<DatabasePizza>()
                                         where du.Name == name
                                         select du).FirstOrDefault();
            if (databasePizza != null)
            {
                return ConvertToDomain(databasePizza);
            }
            return null;
        }

        public override DatabasePizza ConvertToDatabaseType(Pizza domainType)
        {
            return new DatabasePizza()
            {
                Description = domainType.PizzaRecipe.Description,
                CookingTime = domainType.PizzaRecipe.CookingTime,
                Ingredients = domainType.PizzaRecipe.Ingredients,
                LaunchDate = domainType.LaunchDate,
                Price = domainType.Price,
                Name = domainType.Name,
                Id = domainType.Id
            };
        }

        public IEnumerable<Pizza> FindAll()
        {
            List<Pizza> allPizzas = new List<Pizza>();
            List<DatabasePizza> allDatabasePizzas = (from e in _collection.AsQueryable<DatabasePizza>()
                                                   select e).ToList();
            foreach (DatabasePizza dc in allDatabasePizzas)
            {
                allPizzas.Add(ConvertToDomain(dc));
            }

            return allPizzas;
        }

        public override Pizza ConvertToDomain(DatabasePizza databasePizza)
        {
            Pizza Pizza = new Pizza()
            {
                Id = databasePizza.Id,
                Name = databasePizza.Name,
                Price = databasePizza.Price,
                LaunchDate = databasePizza.LaunchDate,
                PizzaRecipe = new Recipe()
                {
                    Ingredients = databasePizza.Ingredients,
                    CookingTime = databasePizza.CookingTime,
                    Description = databasePizza.Description
                }
            };
            return Pizza;
        }
    }
}
