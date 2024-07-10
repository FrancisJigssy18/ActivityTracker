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
    public class UserProfileService : IUserProfileService
    {
        private readonly ActivityTrackerContext _context;

        public UserProfileService(ActivityTrackerContext context) 
        {
            _context = context;
        }

        /// <summary>
        /// This C# function asynchronously retrieves user profiles from the database without tracking changes.
        /// </summary>
        /// <returns>
        /// An asynchronous task that returns a collection of UserProfiles.
        /// </returns>
        public async Task<IList<UserProfile>> GetUserProfilesAsync()
        {
            return await _context.UserProfiles.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// This C# function asynchronously retrieves a user profile from a database by its ID.
        /// </summary>
        /// <param name="id">The `id` parameter in the `GetUserProfileByIdAsync` method represents the unique
        /// identifier of the user profile that you want to retrieve from the database.</param>
        /// <returns>
        /// An asynchronous task that returns a `UserProfile` object corresponding to the provided `id`.
        /// </returns>
        public async Task<UserProfile> GetUserProfileByIdAsync(int id)
        {
            return await _context.UserProfiles.FindAsync(id);
        }

        /// <summary>
        /// The function asynchronously creates a user profile in C# and saves it to the database.
        /// </summary>
        /// <param name="UserProfile">UserProfile is a class representing a user profile entity in the
        /// application. It likely contains properties such as user ID, username, email, profile picture, bio,
        /// etc. The method CreateUserProfileAsync is an asynchronous method that adds a new user profile to the
        /// database using Entity Framework Core and returns the created user</param>
        /// <returns>
        /// The method `CreateUserProfileAsync` returns a `Task<UserProfile>`, which represents an asynchronous
        /// operation that will eventually return a `UserProfile` object.
        /// </returns>
        public async Task<UserProfile> CreateUserProfileAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return userProfile;
        }

        /// <summary>
        /// This C# function updates a user profile asynchronously in a database context.
        /// </summary>
        /// <param name="UserProfile">UserProfile is a class representing a user profile entity in the
        /// application. The UpdateUserProfileAsync method is used to update the user profile information in the
        /// database asynchronously.</param>
        /// <returns>
        /// The method `UpdateUserProfileAsync` is returning a `Task<UserProfile>`, which represents an
        /// asynchronous operation that will eventually return a `UserProfile` object after updating it in the
        /// database.
        /// </returns>
        public async Task<UserProfile> UpdateUserProfileAsync(UserProfile userProfile)
        {
            _context.Entry(userProfile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return userProfile;
        }

        /// <summary>
        /// The DeleteUserProfileAsync function deletes a user profile from the database asynchronously.
        /// </summary>
        /// <param name="id">The `id` parameter in the `DeleteUserProfileAsync` method is an integer value that
        /// represents the unique identifier of the user profile that needs to be deleted from the
        /// database.</param>
        public async Task DeleteUserProfileAsync(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id);
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
        }
    }
}
