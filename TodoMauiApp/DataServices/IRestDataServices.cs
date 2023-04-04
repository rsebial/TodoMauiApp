using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoMauiApp.Models;

namespace TodoMauiApp.DataServices
{
    public interface IRestDataServices
    {
        Task<List<ToDo>> GetAllTodosAsync();
        Task AddTodoAsync(ToDo toDo);
        Task DeleteTodoAsync(int id);
        Task UpdateTodoAsync(ToDo toDo);
    }
}
