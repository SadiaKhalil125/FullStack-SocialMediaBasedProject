using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Bonded.Models;
using Microsoft.AspNetCore.Identity;
using Bonded.Application.Services;
using Bonded.Domain;
namespace Bonded.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatRepository;
        private readonly UserManager<User> _userManager;


        public ChatHub(ChatService chatRepository, UserManager<User> userManager)
        {
            _chatRepository = chatRepository;
            _userManager = userManager;

        }
        public async Task JoinChat(string senderId, string receiverId)
        {
            var chatId = _chatRepository.GetOrCreateChat(senderId, receiverId);
            string groupName = chatId.Id.ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            
            var chatId = _chatRepository.GetOrCreateChat(senderId, receiverId);
            await _chatRepository.SendMessageAsync(chatId.Id, senderId, receiverId, message);
          
            var profile = await _userManager.FindByIdAsync(senderId);
            string groupName = chatId.Id.ToString();
            await Clients.Group(groupName).SendAsync("ReceiveMessage", senderId, message, profile.ProfilePicture);
        }

    }
}
