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

        /// <summary>
        /// The function `GetRunningActivitiesAsync` retrieves a list of running activities asynchronously from
        /// the database context.
        /// </summary>
        /// <returns>
        /// An asynchronous task that returns a collection of RunningActivity objects.
        /// </returns>
        public async Task<IList<RunningActivity>> GetRunningActivitiesAsync()
        {
            return await _context.RunningActivities.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// This C# function asynchronously retrieves a RunningActivity object by its ID from the database
        /// context.
        /// </summary>
        /// <param name="id">The `id` parameter is an integer value that represents the unique identifier of the
        /// running activity you want to retrieve from the database.</param>
        /// <returns>
        /// An asynchronous task that will return a RunningActivity object with the specified id.
        /// </returns>
        public async Task<RunningActivity> GetRunningActivityByIdAsync(int id)
        {
            return await _context.RunningActivities.FindAsync(id);
        }

        /// <summary>
        /// The function asynchronously creates a running activity in a C# application.
        /// </summary>
        /// <param name="RunningActivity">The `CreateRunningActivityAsync` method is an asynchronous method that
        /// adds a `RunningActivity` object to the `_context` and then saves the changes to the database using
        /// `_context.SaveChangesAsync()`. Finally, it returns the added `RunningActivity` object.</param>
        /// <returns>
        /// The method `CreateRunningActivityAsync` returns a `Task` that will eventually yield a
        /// `RunningActivity` object after the running activity has been added to the database and saved
        /// successfully.
        /// </returns>
        public async Task<RunningActivity> CreateRunningActivityAsync(RunningActivity runningActivity)
        {
            _context.RunningActivities.Add(runningActivity);
            await _context.SaveChangesAsync();
            return runningActivity;
        }

        /// <summary>
        /// This C# function updates a running activity in a database asynchronously.
        /// </summary>
        /// <param name="RunningActivity">The `RunningActivity` parameter represents an entity or object that
        /// contains information about a running activity. In the provided code snippet, the
        /// `UpdateRunningActivityAsync` method is responsible for updating a `RunningActivity` object in the
        /// database. The method sets the state of the `runningActivity` object to</param>
        /// <returns>
        /// The method `UpdateRunningActivityAsync` is returning a `Task` of type `RunningActivity`.
        /// </returns>
        public async Task<RunningActivity> UpdateRunningActivityAsync(RunningActivity runningActivity)
        {
            var existingActivity = await _context.RunningActivities.FindAsync(runningActivity.Id);
            if (existingActivity == null)
            {
                throw new NullReferenceException("User not found.");
            }

            _context.Entry(existingActivity).CurrentValues.SetValues(runningActivity);
            await _context.SaveChangesAsync();
            return existingActivity;
        }

        /// <summary>
        /// This C# function asynchronously deletes a running activity from the database by its ID.
        /// </summary>
        /// <param name="id">The `id` parameter in the `DeleteRunningActivityAsync` method is used to specify
        /// the unique identifier of the running activity that needs to be deleted from the database. This
        /// identifier is typically used to locate the specific running activity record in the database for
        /// deletion.</param>
        public async Task DeleteRunningActivityAsync(int id)
        {
            var runningActivity = await _context.RunningActivities.FindAsync(id);
            _context.RunningActivities.Remove(runningActivity);
            await _context.SaveChangesAsync();
        }
    }
}
