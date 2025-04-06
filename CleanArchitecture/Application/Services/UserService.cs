using Bonded.Application.Interfaces;
using Bonded.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly IFollowRepository _followrepo;
        public UserService(IUserRepository userRepository, IFollowRepository followrepo)
        {
            _repository = userRepository;
            _followrepo = followrepo;
        }
        public User GetUserById(string id)
        {
            var user = _repository.GetUserById(id);
            return user;
        }
        public void UpdateProfile(string userId, string Bio, string ProfilePicture)
        {
            _repository.UpdateProfileAsync(userId, Bio, ProfilePicture);
        }
        public void DeleteProfile(string userId)
        {
            _repository.DeleteProfile(userId);
        }

        public User DetailedProfile(string userId)
        {
           int followers = _followrepo.GetFollowersCountAsync(userId);
           int followings = _followrepo.GetFollowingCountAsync(userId);
           return _repository.DetailedProfile(userId, followers, followings);
           
        }
        public User FindByEmail(string email)
        {
            return _repository.FindByEmail(email);
        }
    }
}
