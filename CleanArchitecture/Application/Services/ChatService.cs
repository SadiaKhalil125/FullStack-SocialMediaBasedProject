using Bonded.Domain;
using Bonded.Application.Interfaces;
namespace Bonded.Application.Services
{
    public class ChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        // Get all chats for a specific user
        public async Task<Chat> GetChatsForUserAsync(string userId)
        {
            return await _chatRepository.GetChatsForUserAsync(userId);
          
        }


        //Get a specific chat with messages
        public async Task<List<Chat>> GetChatAsync(string userIdValue)
        {
            return await _chatRepository.GetChatAsync(userIdValue);
        }
        
        public List<Convo> GetConvoForAChat(int chatId)
        {
            return _chatRepository.GetConvoForAChat(chatId);
        }
        public Chat GetOrCreateChat(string senderId, string receiverId)
        {
           return _chatRepository.GetOrCreateChat(senderId, receiverId);
        }

        // Send a new message
        public async Task SendMessageAsync(int chatId, string senderId, string receiverId, string text)
        {
             await _chatRepository.SendMessageAsync(chatId, senderId, receiverId, text);   
        }
        public void DeleteMessage(int msgId)
        {
           _chatRepository.DeleteMessage(msgId);

        }
        public Convo GetMessageById(int msgId)
        {
            return _chatRepository.GetMessageById(msgId);
        }
        public void DeleteChat(int chatId)
        {
           _chatRepository.DeleteChat(chatId);
        }
        public void DeleteMessagesOfAChat(int chatId)
        {
            _chatRepository.DeleteMessagesOfAChat(chatId);
        }

    }
}
