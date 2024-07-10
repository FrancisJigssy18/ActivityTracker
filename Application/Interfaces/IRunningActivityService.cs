using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRunningActivityService
    {
        Task<IEnumerable<RunningActivity>> GetRunningActivitiesAsync();
        Task<RunningActivity> GetRunningActivityByIdAsync(int id);
        Task<RunningActivity> CreateRunningActivityAsync(RunningActivity runningActivity);
        Task<RunningActivity> UpdateRunningActivityAsync(RunningActivity runningActivity);
        Task DeleteRunningActivityAsync(int id);
    }
}
