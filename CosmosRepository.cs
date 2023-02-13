using Azure;
using Microsoft.Azure.Cosmos;

namespace TodoList
{
    public class CosmosRepository
    {
        Database todoDatabase;
        Container todoContainer;
        public CosmosRepository()
        {
            CosmosClient client = new CosmosClient("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            todoDatabase = client.GetDatabase("TodoApp");
            todoContainer = todoDatabase.GetContainer("Todo");
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            var response = todoContainer.GetItemLinqQueryable<TodoItem>(true).ToList();
            return response;

        }

        public async Task Add(TodoItem item)
        {
            await todoContainer.CreateItemAsync(item);
        }

        public async Task Retrieve(TodoItem item)
        {
            try
            {
                ItemResponse<dynamic> itemResponse = await todoContainer.ReadItemAsync<dynamic>(item.Id.ToString(), new PartitionKey(item.Id.ToString()));
                dynamic retrievedItem = itemResponse.Resource;
                System.Diagnostics.Debug.WriteLine($"retrieved item with id {retrievedItem.id}");
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async Task Update(string id, TodoItem item)
        {
            //System.Diagnostics.Debug.WriteLine("connected to cosmosrepo");

            try
            {
                var originalItem = await todoContainer.ReadItemAsync<dynamic>(item.Id.ToString(), new PartitionKey(item.Id.ToString()));

                //dynamic gottenItem = originalItem.Resource;
                System.Diagnostics.Debug.WriteLine($"{originalItem.Resource}");
                originalItem.Resource.Title = item.Title;
                originalItem.Resource.IsDone = item.IsDone;
                //id = Guid.NewGuid().ToString(); 
                System.Diagnostics.Debug.WriteLine($"{originalItem.Resource}");

                await todoContainer.ReplaceItemAsync<dynamic>(originalItem, item.Id.ToString(), new PartitionKey(item.Id.ToString()));
                System.Diagnostics.Debug.WriteLine($"item with id {item.Id} updated");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async Task Delete(TodoItem item)
        {
            System.Diagnostics.Debug.WriteLine("Deleting item with id: " + item.Id);
            try
            {
                await todoContainer.DeleteItemAsync<dynamic>(item.Id.ToString(), new PartitionKey(item.Id.ToString()));
                System.Diagnostics.Debug.WriteLine("Item with id " + item.Id + " deleted blach");
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine("blah blah");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

    }
}
