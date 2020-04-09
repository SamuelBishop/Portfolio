using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternalForcesGUI.Models;
using InternalForcesGUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace InternalForcesGUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileUserInputService UserInputService;
        // Need to retrieve out user inputs now
        public IEnumerable<userInput> userInput { get; private set; } 

        // This is a constructor that has a logger as an argument
        // Logging is a service that is made available to consumers through ASP.NET
        // Want to be able to log stuff to a file or Azure? Can ask for a logger (don't have to make one).

        // Declare all of the extra services below... "I need some stuff. Go get it."
        // Anyone can ask for other people's services here. Code is entirely reusable.
        public IndexModel(ILogger<IndexModel> logger, JsonFileUserInputService userInputService)
        {
            _logger = logger;

            // Set our user input equal to the service passed in from calling the IndexModel function
            UserInputService = userInputService;
        }

        // Razor pages have an idea of an OnGet, OnPut, OnPost, etc.
        // This is where we call our product service to get the products
        // This is how our json database communicates back and forth with the HTML
        public void OnGet()
        {
            userInput = UserInputService.GetuserInput();
        }
    }
}
