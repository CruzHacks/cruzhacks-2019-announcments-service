using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using cruzhacks_2019_announcments_service.Models;

namespace cruzhacks_2019_announcments_service.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageContext _databaseContext;

        public MessageController(MessageContext context)
        {
            _databaseContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> getMessages()
        {
            return await _databaseContext.StoredMessages.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> getMessageByID(int id)
        {
            var targetMessage = await _databaseContext.FindAsync<Message>(id);
            if (targetMessage == null) return NotFound();
            return targetMessage;
        }
        

        [HttpPost]
        public async Task<ActionResult<Message>> uploadMessage([FromBody] Message incomingMessage)
        {
            incomingMessage.timeStamp = DateTime.Now.ToString();
            _databaseContext.StoredMessages.Add(incomingMessage);
            await _databaseContext.SaveChangesAsync();          
            return incomingMessage;
        }

    }
}