using Application.Interfaces;
using Bonded.Application.Interfaces;
using Bonded.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFollowRepository _followRepository;
        private readonly IChatRepository _chatRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminrepository,IUserRepository userRepository, IFollowRepository followRepository, IChatRepository chatRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _followRepository = followRepository;
            _chatRepository = chatRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _adminRepository = adminrepository;

        }
        public List<int> GetCount()
        {
            return _adminRepository.GetAnalytics();
        }
        public List<CarousalImage> GetCarousalImages()
        {
            return _adminRepository.GetCarousalImages();
        }
        public void addCarousalImage(string fileName)
        {
            _adminRepository.addCarousalImage(fileName);
        }
        public void RemoveCarousals(List<CarousalImage> images)
        {
            _adminRepository.RemoveCarousals(images);
        }

    }
}
