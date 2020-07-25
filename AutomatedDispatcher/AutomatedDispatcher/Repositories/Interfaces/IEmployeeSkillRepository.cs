using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories.Interfaces
{
    public interface IEmployeeSkillRepository
    {

        Task<Data.EmployeeSkill> AddAsync(Data.EmployeeSkill employeeskill);

    }
}
