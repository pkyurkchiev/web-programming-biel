using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoOperations
{
    public static class DocUpdateToDoItem
    {
        [FunctionName("DocUpdateToDoItem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "items/{id}")]HttpRequest req,
            [CosmosDB(ConnectionStringSetting = "CosmosDBConnection")]
        DocumentClient client,
            ILogger log, string id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<UpdateToDoItem>(requestBody);
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri("ToDoItems", "Items");
            var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id).AsEnumerable().FirstOrDefault();
            if (document == null)
            {
                return new NotFoundResult();
            }

            document.SetPropertyValue("isCompleted", updated.IsCompleted);
            if (!string.IsNullOrEmpty(updated.Description))
            {
                document.SetPropertyValue("description", updated.Description);
            }

            await client.ReplaceDocumentAsync(document, options: new RequestOptions { PartitionKey = new Microsoft.Azure.Documents.PartitionKey(id) });

            // an easier way to deserialize a Document
            ToDoItem todo2 = (dynamic)document;

            return new OkObjectResult(todo2);
        }
    }
}
