//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace InternalForcesGUI.Services
//{
//    public class JsonFileuserInputService
//    {
//    }
//}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using InternalForcesGUI.Models;
using Microsoft.AspNetCore.Hosting;

namespace InternalForcesGUI.Services
{
    // Main job of JsonFileUserInputService is to take text and convert it into an enumerable to use in json later
    public class JsonFileUserInputService
    {
        // Webhost environment is a chain of shared services that is being used by our product
        public JsonFileUserInputService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            // Command for combining strings into a path
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "userInput.json"); }
        }

        // IEnumerable is the "great great grandparent of list" can for each over the entire data type
        public IEnumerable<userInput> GetuserInput()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {  
                // Read the entire JSON file and deserialize it
                // Inserts it into an enumeration
                return JsonSerializer.Deserialize<userInput[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void AddRating(string userInputId, int rating)
        {
            var userInputs = GetuserInput();

            // LINQ - "SQL-Like query language"
            var query = userInputs.First(x => x.ID == userInputId);

            if(query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add( rating );
                query.Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<userInput>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    userInputs
                );
            }
        }
    }

}