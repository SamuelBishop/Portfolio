using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternalForcesGUI.Models;
using InternalForcesGUI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternalForcesGUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class userInputController : ControllerBase
    {
        public userInputController(JsonFileUserInputService UserInputService)
        {
            this.userInputService = UserInputService;
        }

        public JsonFileUserInputService userInputService { get;  }

        [HttpGet]
        public IEnumerable<userInput> Get()
        {
            return userInputService.GetuserInput();
        }
        
        //[HttpPatch] "[FromBody]" These could do a lot of things for the database
        [Route("Rate")]
        [HttpGet]
        public ActionResult Get( [FromQuery] string userInputId, [FromQuery] int Rating)
        {
            userInputService.AddRating(userInputId, Rating);
            return Ok(); // Hides http from us! Better than 404 or whatever
        }
    }
}