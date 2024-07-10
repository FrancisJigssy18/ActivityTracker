using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfilesController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        /// <summary>
        /// This C# function uses an HTTP GET request to retrieve user profiles asynchronously and returns them
        /// as an ActionResult.
        /// </summary>
        /// <returns>
        /// The `GetUserProfiles` method is returning an `ActionResult` containing a collection of `UserProfile`
        /// objects. The collection is retrieved asynchronously from the `_userProfileService` using the
        /// `GetUserProfilesAsync` method. The method returns an HTTP 200 OK response with the user profiles
        /// data.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IList<UserProfile>>> GetUserProfiles()
        {
            var userProfiles = await _userProfileService.GetUserProfilesAsync();
            return Ok(userProfiles);
        }

        /// <summary>
        /// This C# function retrieves a user profile by ID and returns it as an ActionResult.
        /// </summary>
        /// <param name="id">The `id` parameter in the `GetUserProfile` method is used to specify the unique
        /// identifier of the user profile that the method is trying to retrieve. This identifier is typically
        /// used to look up the user profile in the data source, such as a database, and return the
        /// corresponding user profile information.</param>
        /// <returns>
        /// The `GetUserProfile` method is returning an `ActionResult<UserProfile>`. If the `userProfile` is
        /// found, it will return an `Ok` response with the `userProfile` data. If the `userProfile` is not
        /// found (null), it will return a `NotFound` response.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
        {
            var userProfile = await _userProfileService.GetUserProfileByIdAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }

        /// <summary>
        /// This C# function creates a user profile and returns the created user profile with a HTTP 201 Created
        /// status.
        /// </summary>
        /// <param name="UserProfile">The `CreateUserProfile` method is an HTTP POST endpoint that receives a
        /// `UserProfile` object as a parameter. The method is asynchronous and returns a
        /// `Task<ActionResult<UserProfile>>`.</param>
        /// <returns>
        /// The CreateUserProfile method is returning an ActionResult of type UserProfile. It is creating a new
        /// user profile by calling the CreateUserProfileAsync method from the _userProfileService and then
        /// returning the created user profile along with a CreatedAtAction result, which includes the route to
        /// retrieve the newly created user profile.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<UserProfile>> CreateUserProfile(UserProfile userProfile)
        {
            var createdUserProfile = await _userProfileService.CreateUserProfileAsync(userProfile);
            return CreatedAtAction(nameof(GetUserProfile), new { id = createdUserProfile.Id }, createdUserProfile);
        }

        /// <summary>
        /// This C# function updates a user profile with the specified ID.
        /// </summary>
        /// <param name="id">The `id` parameter in the `UpdateUserProfile` method represents the unique
        /// identifier of the user profile that you want to update. It is used to identify the specific user
        /// profile that needs to be updated in the system.</param>
        /// <param name="UserProfile">UserProfile is a model class that represents the user profile data, such
        /// as user's name, email, address, etc. It is used to pass the updated user profile information to the
        /// UpdateUserProfile method for processing and updating in the database.</param>
        /// <returns>
        /// The method is returning an `IActionResult`. If the `id` does not match the `userProfile.Id`, it will
        /// return a `BadRequest` response. If the update is successful, it will return a `NoContent` response.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }
            await _userProfileService.UpdateUserProfileAsync(userProfile);
            return NoContent();
        }

        /// <summary>
        /// This C# function deletes a user profile by ID and returns a No Content response.
        /// </summary>
        /// <param name="id">The `id` parameter in the `DeleteUserProfile` method represents the unique
        /// identifier of the user profile that you want to delete. This identifier is typically used to locate
        /// and delete the specific user profile from the database or any other data source.</param>
        /// <returns>
        /// NoContent() is being returned.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            await _userProfileService.DeleteUserProfileAsync(id);
            return NoContent();
        }
    }
}