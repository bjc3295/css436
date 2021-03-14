using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Program5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "THREE JOKES and a fact";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page";

            return View();
        }


        [HttpPost]
        public ActionResult ProcessForm(string ButtonName)
        {
            switch (ButtonName)
            {
                case "Chuck Norris Joke":
                    Norris();
                    break;
                case "Two-part Joke":
                    Second();
                    break;
                case "Random Joke":
                    Third();
                    break;
                case "Random Fact":
                    Fourth();
                    break;
            }

            return View("Index");
        }

        private async Task Norris()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.icndb.com/jokes/random?exclude=[explicit]");

                HttpResponseMessage response = client.GetAsync("").Result;

                bool status = response.IsSuccessStatusCode;

                if (!status)
                    TempData["ResultP1"] = "Failed to connect";
                else
                {
                    var result = await response.Content.ReadAsStringAsync();

                    Norris joke = JsonConvert.DeserializeObject<Norris>(result);

                    TempData["ResultP1"] = joke.value.joke;
                }
            }
        }

        private async Task Second()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://official-joke-api.appspot.com/random_joke");

                HttpResponseMessage response = client.GetAsync("").Result;

                bool status = response.IsSuccessStatusCode;

                if (!status)
                    TempData["ResultP1"] = "Failed to connect";
                else
                {
                    var result = await response.Content.ReadAsStringAsync();

                    Second joke = JsonConvert.DeserializeObject<Second>(result);

                    TempData["ResultP1"] = joke.setup;
                    TempData["ResultP2"] = joke.punchline;
                }
            }
        }

        private void Third()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://v2.jokeapi.dev/joke/Programming,Miscellaneous,Pun?blacklistFlags=nsfw,racist,sexist,explicit&type=single");

                HttpResponseMessage response = client.GetAsync("").Result;

                bool status = response.IsSuccessStatusCode;

                if (!status)
                    TempData["ResultP1"] = "Failed to connect";
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    Third joke = JsonConvert.DeserializeObject<Third>(result);

                    TempData["ResultP1"] = joke.joke;
                }
            }
        }

        private async Task Fourth()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://useless-facts.sameerkumar.website/api");

                HttpResponseMessage response = client.GetAsync("").Result;

                bool status = response.IsSuccessStatusCode;

                if (!status)
                    TempData["ResultP1"] = "Failed to connect";
                else
                {
                    var result = await response.Content.ReadAsStringAsync();

                    Fourth fact = JsonConvert.DeserializeObject<Fourth>(result);

                    TempData["ResultP1"] = fact.data;
                }
            }
        }
    }


    public class Norris
    {
        public string type { get; set; }
        public Value value { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public string joke { get; set; }
        public object[] categories { get; set; }
    }



    public class Second
    {
        public int id { get; set; }
        public string type { get; set; }
        public string setup { get; set; }
        public string punchline { get; set; }
    }



    public class Third
    {
        public bool error { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string joke { get; set; }
        public Flags flags { get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string lang { get; set; }
    }

    public class Flags
    {
        public bool nsfw { get; set; }
        public bool religious { get; set; }
        public bool political { get; set; }
        public bool racist { get; set; }
        public bool sexist { get; set; }
        public bool _explicit { get; set; }
    }


    public class Fourth
    {
        public string data { get; set; }
    }

}