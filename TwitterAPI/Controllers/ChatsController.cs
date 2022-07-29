using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAPI.Authentication;
using TwitterAPI.DTOs.MessageDTOs;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatServices _chatServices;
        public ChatsController(IChatServices chatServices)
        {
            _chatServices = chatServices;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost("SendMessage")]
        public async Task<ActionResult> SendMessage(string receiver, CreateMessageDTO createMessageDTO )
        {
            var result = await _chatServices.SendMessage(User.Identity.Name, receiver,createMessageDTO);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("GetChatHistory")]
        public async Task<ActionResult<IEnumerable<ViewMessageDTO>>> GetChatHistory(string receiver)
        {
            var result = await _chatServices.GetChatHistory(User.Identity.Name, receiver);
            if(result == null)
            {
                return BadRequest("No existing chat history with this user");
            }
            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ViewAllChats")]
        public async Task<ActionResult<IEnumerable<ViewMessageDTO>>> ViewAllChats()
        {
            var result = await _chatServices.ViewAllChats(User.Identity.Name);
            return Ok(result);
        }

    }
}
