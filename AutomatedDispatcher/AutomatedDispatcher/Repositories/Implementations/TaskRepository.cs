using AutomatedDispatcher.Data;
using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        protected readonly webappContext _dbContext;
        public TaskRepository(webappContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Data.Task> AddAsync(Data.Task task)
        {
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async System.Threading.Tasks.Task DeleteAsync(Data.Task task)
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Data.Task> GetTaskByIdAsync(int id)
        {
            return await _dbContext.Tasks
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Data.Task>> GetTaskByNameAsync(string name)
        {
            return await _dbContext.Tasks
                   .Where(p => string.IsNullOrEmpty(name) || p.Name.ToLower().Contains(name.ToLower()))
                   .OrderBy(p => p.Name)
                   .ToListAsync();
        }

        public async Task<IEnumerable<Data.Task>> GetTaskListAsync()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Data.Task task)
        {
            _dbContext.Entry(task).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
