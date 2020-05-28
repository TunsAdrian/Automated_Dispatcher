using AutomatedDispatcher.Data;
using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
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
            var excluded = new[] { "CurrentWorkload" };

            var entry = _dbContext.Entry(employee);
            entry.State = EntityState.Modified;

            foreach (var item in excluded)
            {
                entry.Property(item).IsModified = false;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetProgrammersMinWorkload(int employeeId)
        {
            // Get the minimum workload of the programmers except the one sent as param
            var minWorkload = _dbContext.Employee
                .Where(s=> s.Id != employeeId && s.Role == 1)
                .Min(s => s.CurrentWorkload);

            // Return a list with the programmers that have minimum workload, sorted descending by their working hours
            return await _dbContext.Employee
                .Where(s => s.CurrentWorkload == minWorkload && s.Role == 1)
                .OrderByDescending(s => s.WorkingHours)
                .ToListAsync();
        }
    }
}
