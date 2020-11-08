using BP.ApplicationServices.Interfaces;
using BP.ApplicationServices.Messaging;
using BP.ApplicationServices.Messaging.Pizzas;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BP.WebAPIServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public PizzasController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService ?? throw new ArgumentNullException("PizzaService in PizzasController");
        }

        // GET: api/users
        [HttpGet]
        [Produces(typeof(ServiceResponseBase))]
        //[SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(ServiceResponseBase))]
        public IActionResult Get()
        {
            ServiceResponseBase resp = _pizzaService.GetAll();
            return Ok(resp);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Produces(typeof(ServiceResponseBase))]
        //[SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(ServiceResponseBase))]
        public IActionResult Get([FromRoute] Guid id)
        {
            ServiceResponseBase resp = _pizzaService.GetById(new GetPizzaRequest(id));
            return Ok(resp);
        }

        // GET api/values/5
        [HttpGet("names/{name}")]
        [Produces(typeof(ServiceResponseBase))]
        //[SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(ServiceResponseBase))]
        public IActionResult Get([FromRoute] string name)
        {
            ServiceResponseBase resp = _pizzaService.GetByName(new GetPizzaByNameRequest(name));
            return Ok(resp);
        }

        // POST api/values
        [HttpPost]
        [ActionName("Post")]
        //[SwaggerOperation("Post")]
        //[Produces(typeof(InsertUserResponse))]
        //[SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(InsertUserResponse))]
        public IActionResult Post(PizzaPropertiesVM insertPizzaVM)
        {
            InsertPizzaResponse insertUserResponse = _pizzaService.InsertPizza(new InsertPizzaRequest() { PizzaProperties = insertPizzaVM });
            return Ok(insertUserResponse);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Produces(typeof(UpdatePizzaRequest))]
        //[SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(UpdateUserRequest))]
        public IActionResult Put(UpdatePizzaVM updatePizzaVM)
        {
            UpdatePizzaRequest req = new UpdatePizzaRequest(updatePizzaVM.Id)
            {
                PizzaProperties = new PizzaPropertiesVM()
                {
                    Name = updatePizzaVM.Name,
                    LaunchDate = updatePizzaVM.LaunchDate,
                    Price = updatePizzaVM.Price,
                    Ingredients = updatePizzaVM.Ingredients,
                    CookingTime = updatePizzaVM.CookingTime,
                    Description = updatePizzaVM.Description
                }
            };

            UpdatePizzaResponse updatePizzaResponse = _pizzaService.UpdatePizza(req);
            return Ok(updatePizzaResponse);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Produces(typeof(DeletePizzaResponse))]
        //[SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(DeleteUserResponse))]
        public IActionResult Delete(Guid id)
        {
            DeletePizzaResponse deletePizzaResponse = _pizzaService.DeletePizza(new DeletePizzaRequest(id));
            return Ok(deletePizzaResponse);
        }
    }
}