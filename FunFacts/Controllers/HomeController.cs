using FunFacts.Common;
using FunFacts.Models;
using FunFacts.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace FunFacts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool showMostRecent = false)
        {
            var funFactPageViewModel = new FunFactPageViewModel();
            funFactPageViewModel.IsMostRecent = showMostRecent;
            funFactPageViewModel.FunFacts = GetFunFactViewModels(funFactPageViewModel.IsMostRecent);
            return View(funFactPageViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public IEnumerable<FunFact> GetFunFactViewModels(bool showRecent = false)
        {
            IEnumerable<FunFact> funfacts = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings[Constants.ApiEndPointKey]);

                //Called Member default GET All records  
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request  
                var responseTask = client.GetAsync(ConfigurationManager.AppSettings[Constants.ApiEndPointKey] + "random?amount=500");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<FunFact>>();
                    readTask.Wait();
                    funfacts = showRecent ? readTask.Result.Where(f => f.UpdatedAt.Year == DateTime.Now.Year && f.UpdatedAt.Month == DateTime.Now.AddMonths(-1).Month) : readTask.Result.OrderByDescending(f => f.UpdatedAt).Take(100);
                }
                else
                {
                    //Error response received   
                    funfacts = Enumerable.Empty<FunFact>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return funfacts;
        }

    }
}