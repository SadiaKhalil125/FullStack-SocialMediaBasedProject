using Bonded.Application.Interfaces;
using Bonded.Domain;
using Bonded.Infrastructure;
using Bonded.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;
     
        public UserRepository(ApplicationDbContext context)
        {
            
            _context = context;
        }
        public User GetUserById(string id)
        {
            var user = _context.Users.Where(u => u.Id == id).First();
            return user;
        }
        public void UpdateProfileAsync(string userId, string Bio, string ProfilePicture )
        {
            try
            {
               
                var user = GetUserById(userId);
                if(!string.IsNullOrEmpty(Bio))
                {
                    user.Bio = Bio;
                  
                }      
                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    user.ProfilePicture = ProfilePicture;
                }

                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void DeleteProfile(string userId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            _context.SaveChanges();
        }
        public User DetailedProfile(string userId, int FollowerCount, int FollowingCount)
        {
            var user = GetUserById(userId);

            User user1 = new User
            {
                Id = userId,
                FollowerCount = FollowerCount,
                FollowingCount = FollowingCount,
                UserName = user.UserName,
                Email = user.Email,
                Bio = user.Bio,
                ProfilePicture = user.ProfilePicture
            };
            return user1;
        }
        public User FindByEmail(string email)
        {
            var user = _context.Users.Where(u=>u.Email == email).First();
            return user;
        }
    }
}
