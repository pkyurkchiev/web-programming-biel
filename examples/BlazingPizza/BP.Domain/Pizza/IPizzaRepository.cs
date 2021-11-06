using BP.Infrastructure.Domain;
using System;

namespace BP.Domain.Pizza
{
    public interface IPizzaRepository : IRepository<Pizza, Guid>
    {
        Pizza FindByName(string name);
    }
}
