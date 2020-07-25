using AutomatedDispatcher.Data;
using AutomatedDispatcher.Repositories.Interfaces;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories.Implementations
{

    public class EmployeeSkillRepository : IEmployeeSkillRepository
    {

        protected readonly webappContext _dbContext;

        public async Task<EmployeeSkill> AddAsync(EmployeeSkill employeeskill)
        {
            _dbContext.EmployeeSkill.Add(employeeskill);
            await _dbContext.SaveChangesAsync();
            return employeeskill;
        }
    }
}
