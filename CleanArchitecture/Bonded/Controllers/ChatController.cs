using Bonded.Models;
using Microsoft.AspNetCore.Mvc;
using Bonded.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Runtime.Intrinsics.X86;
using Bonded.Hubs;
using Microsoft.AspNetCore.SignalR;
using Bonded.Application.Interfaces;
using Bonded.Application.Services;
using Bonded.Domain;
namespace Bonded.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatService _chatService;
        private readonly UserManager<User> _userManager;
        

        public ChatController(ChatService chatService,  UserManager<User> userManager)
        {
            _chatService = chatService;
            _userManager = userManager;
           // _chatHub = chatHub;
        }

        // View all chats for the current user
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = HttpContext.Session.GetString("UserId");
            string userIdValue = userId ?? "";
            if (userIdValue == "")
            {
                TempData["ErrorMessage"] = "Signin to view more!";
                return RedirectToAction("Login", "User"); // Redirect if no user is logged in
            }

            var chats = await _chatService.GetChatAsync(userIdValue);

            List<ChatViewModel1> list = new List<ChatViewModel1>();
            foreach (var chat in chats)
            {
                var messages = _chatService.GetConvoForAChat(chat.Id);

                if (userIdValue == chat.UserOneId)
                {
                    list.Add(new ChatViewModel1 { ChatId = chat.Id, Messages = messages, UserONE = await _userManager.FindByIdAsync(userIdValue), UserTWO = await _userManager.FindByIdAsync(chat.UserTwoId) });
                }
                else 
                {
                    list.Add(new ChatViewModel1 { ChatId = chat.Id, Messages = messages, UserONE = await _userManager.FindByIdAsync(chat.UserTwoId), UserTWO =await _userManager.FindByIdAsync(chat.UserOneId) });
                }
            }
            return View(list);
        }
        [HttpGet]
        public IActionResult GetMessages(int chatId)
        {
            string? userId = HttpContext.Session.GetString("UserId");
            string currentUserId = userId ?? "";

            List<Convo> messages = _chatService.GetConvoForAChat(chatId);

            var cvm = messages.Select(message => new ConvoViewModel
            {
                Message = message,
                Receiver = _userManager.FindByIdAsync(message.ReceiverId).Result,
                Sender = _userManager.FindByIdAsync(message.SenderId).Result
            }).ToList();

            ViewBag.CurrentUserId = currentUserId;

            return PartialView("_ChatMessages", cvm);
        }

        [HttpGet]
        public async Task<IActionResult> ChatView(string receiverId)
        {
            string? userId = HttpContext.Session.GetString("UserId");
            string userIdValue = userId ?? "";

            if (userIdValue == "")

            {
                TempData["ErrorMessage"] = "Signin to view more!";
                return RedirectToAction("Login", "User");
            }
            //make a viewmodel


            Chat chat = _chatService.GetOrCreateChat(userIdValue, receiverId);
            List<Convo> messages = _chatService.GetConvoForAChat(chat.Id);

            List<ConvoViewModel> cvm = new List<ConvoViewModel>();
            if (messages != null)
            {
                foreach (Convo message in messages)
                {
                    if (message != null)
                    {
                        cvm.Add(new ConvoViewModel { Message = message, Receiver = await _userManager.FindByIdAsync(message.ReceiverId), Sender = await _userManager.FindByIdAsync(message.SenderId) });
                    }
                }
            }

            ChatViewModel chatViewModel = new ChatViewModel
            {
                ChatId = chat.Id,
                UserONE = await _userManager.FindByIdAsync(chat.UserOneId),
                UserTWO = await _userManager.FindByIdAsync(chat.UserTwoId),
                Messages = cvm
            };


            if (chatViewModel == null)
            {
                return NotFound("Chat not found");
            }

            ViewBag.CurrentUserId = userIdValue;
            ViewBag.ReceiverId = receiverId;
            var user1 = await _userManager.FindByIdAsync(receiverId);
            ViewBag.ProfilePicture = user1.ProfilePicture;
            ViewBag.Username = user1.UserName;
            return View(chatViewModel);
        }


        //[HttpPost]
        //public async Task<IActionResult> SendMessage(int chatId, string receiverId, string text)
        //{
        //    string? userId = HttpContext.Session.GetString("UserId");
        //    string senderId = userId ?? "";
        //    if (string.IsNullOrWhiteSpace(senderId) || string.IsNullOrWhiteSpace(text))
        //    {
        //        return BadRequest("Invalid request.");
        //    }

        //    await _chatService.SendMessageAsync(chatId, senderId, receiverId, text);

        //    return Ok();
        //}



        //Send a new message
        [HttpPost]
          public async Task<IActionResult> SendMessage(int chatId, string receiverId, string text)
        {

            string? userId = HttpContext.Session.GetString("UserId");
            string senderId = userId ?? "";
            if (senderId == "")
            {
                TempData["ErrorMessage"] = "Signin to view more!";
                return RedirectToAction("Login", "User");
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                ModelState.AddModelError("Text", "Message cannot be empty.");
                return RedirectToAction("ChatView", new { receiverId });
            }

            await _chatService.SendMessageAsync(chatId, senderId, receiverId, text);
            return RedirectToAction("ChatView", new { receiverId });
        }
        [HttpPost]
        public IActionResult DeleteMessage(int msgId)
        {
            try
            {
                _chatService.DeleteMessage(msgId); // Adjust to match your repository method
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            try
            {
                // Delete all messages and the chat
                _chatService.DeleteMessagesOfAChat(chatId);
                _chatService.DeleteChat(chatId);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        //[HttpGet]
        //public IActionResult DeleteChat(int chatId)
        //{
        //    _chatService.DeleteMessagesOfAChat(chatId);
        //    _chatService.DeleteChat(chatId);
        //    return RedirectToAction("Index","Chat");
        //    // Ensure the user is authorized to delete the message
        //     // Redirect to the chat page
        //}

    }
}

