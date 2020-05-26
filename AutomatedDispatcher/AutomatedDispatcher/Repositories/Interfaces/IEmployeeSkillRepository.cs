using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Repositories.Interfaces
{
    public interface IEmployeeSkillRepository
    {

        Task<Data.EmployeeSkill> AddAsync(Data.EmployeeSkill employeeskill);

    }
}
