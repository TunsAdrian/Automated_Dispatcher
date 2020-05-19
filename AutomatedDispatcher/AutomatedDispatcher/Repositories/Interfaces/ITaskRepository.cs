using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Data.Task>> GetTaskListAsync();
        Task<Data.Task> GetTaskByIdAsync(int id);
        Task<IEnumerable<Data.Task>> GetTaskByNameAsync(string name);
        Task<Data.Task> AddAsync(Data.Task task);
        System.Threading.Tasks.Task UpdateAsync(Data.Task task);
        System.Threading.Tasks.Task DeleteAsync(Data.Task task);
        Task<IEnumerable<Data.Task>> GetTaskByStatus(int id);
    }
}
