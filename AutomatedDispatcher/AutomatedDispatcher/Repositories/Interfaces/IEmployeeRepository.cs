using AutomatedDispatcher.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Data.Employee>> GetEmployeeListAsync();
        Task<IEnumerable<Data.Employee>> GetProgrammersListAsync();
        Task<Data.Employee> GetEmployeeByIdAsync(int id);
        Task<Data.Employee> AddAsync(Data.Employee employee);
        System.Threading.Tasks.Task UpdateAsync(Data.Employee employee);
        System.Threading.Tasks.Task DeleteAsync(Data.Employee employee);

        Task<IEnumerable<Employee>> GetEmployeesMinWorkload();
    }
}
