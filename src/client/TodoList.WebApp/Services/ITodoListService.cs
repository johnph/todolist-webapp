namespace TodoList.WebApp.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoList.WebApp.Models;

    public interface ITodoListService
    {
        Task<IEnumerable<TodoModel>> GetAsync();
        Task<TodoModel> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<TodoModel> AddAsync(TodoModel todo);
        Task<TodoModel> EditAsync(TodoModel todo);
    }
}
