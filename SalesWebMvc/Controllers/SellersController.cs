using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; // Dependência

        public SellersController(SellerService sellerService) // Construtor recebendo SellerService
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()   // GET action to the button "Create New" created at Sellers page
        {
            return View();
        }

        [HttpPost]  // Indicates this action like POST
        [ValidateAntiForgeryToken] // Avoid CSRF attack 
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);              // Call method to insert a new Seller at database
            return RedirectToAction(nameof(Index));     // After inserted the register return to the Index page
        }
    }
}