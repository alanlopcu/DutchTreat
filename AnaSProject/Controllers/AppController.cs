﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnaSProject.ViewModels;
using AnaSProject.Services;
using AnaSProject.Data;

namespace AnaSProject.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IAnaSRepository _repository;

        public AppController(IMailService mailService, IAnaSRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();
            //var results = _context.Products
            //    .OrderBy(p => p.Category)
            //    .ToList();

            //var results = from p in _context.Products
            //              orderby p.Category
            //              select p;

            //return View(results.ToList());
            return View(results);
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send the email
                _mailService.SendMessage("alanlopcu@gmail.com", model.Subject, $"From: {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent!*";
                ModelState.Clear();
            }
            else
            {
                //Show the errors
            }
            return View();
        }

        public IActionResult About()
        {
            //ViewBag.Title = "About Me";
            return View();
        }
    }
}
