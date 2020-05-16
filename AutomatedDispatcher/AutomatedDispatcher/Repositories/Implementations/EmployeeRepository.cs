using AutomatedDispatcher.Data;
using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly webappContext _dbContext;
        public EmployeeRepository(webappContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Employee> AddAsync(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async System.Threading.Tasks.Task DeleteAsync(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _dbContext.Employees
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeListAsync()
        {
            return await _dbContext.Employees.ToListAsync(); ;
        }

        public async Task<IEnumerable<Employee>> GetProgrammersListAsync()
        {
            // Where 0 is Manager and 1 is Programmer
            return await _dbContext.Employees
                .Where(s => s.Role == 1)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
