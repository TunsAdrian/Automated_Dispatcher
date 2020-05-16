using AutomatedDispatcher.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        protected readonly webappContext _dbContext;

        public SkillRepository(webappContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Skill> AddAsync(Skill skill)
        {
            _dbContext.Skills.Add(skill);
            await _dbContext.SaveChangesAsync();
            return skill;
        }

        public async System.Threading.Tasks.Task DeleteAsync(Skill skill)
        {
            _dbContext.Skills.Remove(skill);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Skill> GetSkillByIdAsync(int id)
        {
            return await _dbContext.Skills
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Skill>> GetSkillByNameAsync(string name)
        {
            return await _dbContext.Skills
                   .Where(p => string.IsNullOrEmpty(name) || p.SkillName.ToLower().Contains(name.ToLower()))
                   .OrderBy(p => p.SkillName)
                   .ToListAsync();
        }

        public async Task<IEnumerable<Skill>> GetSkillsListAsync()
        {
            return await _dbContext.Skills.ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Skill skill)
        {
            _dbContext.Entry(skill).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
