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

    }
}
