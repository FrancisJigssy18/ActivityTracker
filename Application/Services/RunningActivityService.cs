using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RunningActivityService : IRunningActivityService
    {
        private readonly ActivityTrackerContext _context;

        public RunningActivityService(ActivityTrackerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RunningActivity>> GetRunningActivitiesAsync()
        {
            return await _context.RunningActivities.AsNoTracking().ToListAsync();
        }

        public async Task<RunningActivity> GetRunningActivityByIdAsync(int id)
        {
            return await _context.RunningActivities.FindAsync(id);
        }

        public async Task<RunningActivity> CreateRunningActivityAsync(RunningActivity runningActivity)
        {
            _context.RunningActivities.Add(runningActivity);
            await _context.SaveChangesAsync();
            return runningActivity;
        }

        public async Task<RunningActivity> UpdateRunningActivityAsync(RunningActivity runningActivity)
        {
            _context.Entry(runningActivity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return runningActivity;
        }

        public async Task DeleteRunningActivityAsync(int id)
        {
            var runningActivity = await _context.RunningActivities.FindAsync(id);
            _context.RunningActivities.Remove(runningActivity);
            await _context.SaveChangesAsync();
        }
    }
}
