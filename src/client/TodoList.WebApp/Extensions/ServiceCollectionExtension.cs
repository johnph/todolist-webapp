namespace TodoList.WebApp.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using TodoList.WebApp.Services;

    public static class ServiceCollectionExtension
    {
        public static void AddTodoListService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ITodoListService, TodoListService>();
        }
    }
}
