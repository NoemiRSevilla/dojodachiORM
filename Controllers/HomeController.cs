using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Happiness") == null)
            {
                // if one quality is null, all are null
                HttpContext.Session.SetInt32("Happiness", 20);
                HttpContext.Session.SetInt32("Fullness", 20);
                HttpContext.Session.SetInt32("Energy", 50);
                HttpContext.Session.SetInt32("Meals", 3);
                HttpContext.Session.SetString("State", "Happy");
            }

            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            int? Energy = HttpContext.Session.GetInt32("Energy");
            
            if (Happiness >= 100 && Fullness >= 100 && Energy >= 100)
            {
                HttpContext.Session.SetString("State", "Win");
                TempData["Message"] = "You won!";
            }

            if (Happiness <= 0){
                HttpContext.Session.SetString("State", "Lose");
                HttpContext.Session.SetInt32("Happiness", 0);
                TempData["Message"] = "Your Dojodachi died!";
            }
            if (Fullness <= 0){
                HttpContext.Session.SetString("State", "Lose");
                HttpContext.Session.SetInt32("Fullness", 0);
                TempData["Message"] = "Your Dojodachi died!";
            }
            

            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.State = HttpContext.Session.GetString("State");
            
            return View();
        }

        [HttpPost("feed")]
        public RedirectToActionResult Feed()
        {
            int? Meals = HttpContext.Session.GetInt32("Meals");
            if (Meals > 0 )
            {
                Meals -= 1;
                HttpContext.Session.SetInt32("Meals", (int)Meals);
                
                Random rand = new Random();
                if (rand.Next(1,101) % 4 != 0)
                {
                    int? Fullness = HttpContext.Session.GetInt32("Fullness");
                    int addToFull = rand.Next(5,11);
                    Fullness += addToFull;
                    HttpContext.Session.SetInt32("Fullness", (int)Fullness);
                    TempData["Message"] = $"You fed your Dojodachi! Fullness +{addToFull}, Meals -1";
                    HttpContext.Session.SetString("State", "Happy");
                }
                else
                {
                    TempData["Message"] = "You fed your Dojodachi but it didn't like it! Meals -1";
                    HttpContext.Session.SetString("State", "Sad");
                }
            }
            else
            {
                TempData["Message"] = "No more meals remaining. Go to work!";
                HttpContext.Session.SetString("State", "Sad");
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost("play")]
        public RedirectToActionResult Play()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            if (Energy >= 5)
            {
                Energy -= 5;
                HttpContext.Session.SetInt32("Energy", (int)Energy);

                Random rand = new Random();
                if (rand.Next(1, 101) % 4 != 0){
                    int? Happiness = HttpContext.Session.GetInt32("Happiness");
                    int addToHapp = rand.Next(5,11);
                    Happiness += addToHapp;
                    HttpContext.Session.SetInt32("Happiness", (int)Happiness);
                    TempData["Message"] = $"You played with your Dojodachi! Happiness +{addToHapp}, Energy -5";
                    HttpContext.Session.SetString("State", "Happy");
                }
                else
                {
                    TempData["Message"] = "You played with your Dojodachi but it didn't like it! Energy -5";
                    HttpContext.Session.SetString("State", "Sad");
                }
            }
            else
            {
                TempData["Message"] = "No more energy remaining. Get some sleep!";
                HttpContext.Session.SetString("State", "Sad");
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost("work")]
        public RedirectToActionResult Work()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            if (Energy >= 5)
            {
                Energy -= 5;
                HttpContext.Session.SetInt32("Energy", (int)Energy);

                Random rand = new Random();
                
                int? Meals = HttpContext.Session.GetInt32("Meals");
                int addToMeals = rand.Next(1,4);
                Meals += addToMeals;
                HttpContext.Session.SetInt32("Meals", (int)Meals);
                TempData["Message"] = $"You went to work and earned money to buy meals! Meals +{addToMeals}";
                HttpContext.Session.SetString("State", "Happy");
                

            }
            else
            {
                TempData["Message"] = "Not enough energy to work and buy meals! Get some sleep!";
                HttpContext.Session.SetString("State", "Sad");
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost("sleep")]
        public RedirectToActionResult Sleep()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            
            if (Fullness >= 5 && Happiness >= 5){

                Energy += 15;
                Fullness -= 5;
                Happiness -= 5;

                HttpContext.Session.SetInt32("Energy", (int)Energy);
                HttpContext.Session.SetInt32("Fullness", (int)Fullness);
                HttpContext.Session.SetInt32("Happiness", (int)Happiness);
                TempData["Message"] = "Your Dojodachi was neglected while your slept! Fullness -5, Happiness -5, Energy +15";
            }

            return RedirectToAction("Index");
        }

        [HttpGet("reset")]
        public RedirectToActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}