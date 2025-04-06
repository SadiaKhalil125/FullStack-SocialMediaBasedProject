using Bonded.Domain;
using Microsoft.AspNetCore.Http;

namespace Bonded.Application.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(string id);
        void UpdateProfileAsync(string userId, string Bio, string ProfilePicture);
        public void DeleteProfile(string userId);
        public User DetailedProfile(string userId, int FollowerCount, int FollowingCount);
        public User FindByEmail(string email);


        }
}
