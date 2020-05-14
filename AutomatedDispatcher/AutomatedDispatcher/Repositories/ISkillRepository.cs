using AutomatedDispatcher.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories
{
    interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetSkillsListAsync();
        Task<Skill> GetSkillByIdAsync(int id);
        Task<IEnumerable<Skill>> GetSkillByNameAsync(string name);
        Task<Skill> AddAsync(Skill skill);

        // Aici era conflict cu Task name
        System.Threading.Tasks.Task UpdateAsync(Skill product);
        System.Threading.Tasks.Task DeleteAsync(Skill product); 
        
        // Aici nu sunt sigur daca trebuie implementat
        //Task<IEnumerable<Skill>> GetEmployees();
    }
}
