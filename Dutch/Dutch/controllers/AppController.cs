using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dutch.Data;
using Dutch.services;
using Dutch.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dutch.controllers
{
    public class AppController : Controller
    {
       // private readonly INullMailService mailService;
        private readonly INullMailService _mailService;
        private readonly IDutchRepository _repository;
        
        private readonly DutchContext _ctx;
        public AppController(INullMailService mailService,  DutchContext ctx, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
            
            _ctx = ctx;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var results = _ctx.Products.ToList();
            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
                  // send the email
                _mailService.SendMessage("chaitu256@gmail.com", model.Subject, $"From: {model.Name}-{model.Email},Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
               return View();
        }
           
            
         public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
        [Authorize]
        public IActionResult Shop()
        {
        var results = _repository.GetAllProducts();
                
            return View(results.ToList());
        }

    }
}
