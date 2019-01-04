using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    [Authorize] //this attribute ensures that the user is authenticated to access any portion of this class
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }
        [AllowAnonymous] //this allows a non authenticaed user to view the index, but not any other portions of this controller
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Restaurant = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);
            if(model == null)
            {
                return Redirect(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        //attribute will check and ensure that the form the user is submitting is a form that we served to them
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel model)
        {
            //this property is false if the user did not fill out all the required properties on the model. In our
            //case, the user has to enter in a restaurant name because of the Required attribute on the model.
            if (ModelState.IsValid)
            {
                var newRes = new Restaurant();
                newRes.Name = model.Name;
                newRes.Cuisine = model.Cuisine;

                newRes = _restaurantData.Add(newRes);

                return RedirectToAction(nameof(Details), new { id = newRes.Id });
            }
            //the model state is invalid. Return the view again and make them retry
            else
            {
                return View();
            }
        }
    }
}
