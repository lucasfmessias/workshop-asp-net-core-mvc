﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; // Dependência

        private readonly DepartmentService _departmentService; // Dependência

        public SellersController(SellerService sellerService, DepartmentService departmentService) // Construtor recebendo SellerService
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        // GET action to the button "Create New" created at Sellers page that will call View Create Form
        public IActionResult Create()   
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]  // Indicates this action like POST
        [ValidateAntiForgeryToken] // Avoid CSRF attack 
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);              // Call method to insert a new Seller at database
            return RedirectToAction(nameof(Index));     // After inserted the register return to the Index page
        }

        // GET action to the link "Delete" created at Sellers page that will call View Delete Form
        public IActionResult Delete(int? id) // int? id, meaning that enter of ID is optionally
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value); // syntax: FindById(id.value) - Because in the beginning of the method the passages of ID was set as optionally.
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]  // Indicates this action like POST
        [ValidateAntiForgeryToken] // Avoid CSRF attack 
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);              // Call method to remove to delete a seller
            return RedirectToAction(nameof(Index));     // After deleted the register return to the Index page
        }

        // GET action to the link "Details" created at Sellers page that will call View Details Form
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value); // syntax: FindById(id.value) - Because in the beginning of the method the passages of ID was set as optionally.
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // GET action to the link "Edit" created at Sellers page that will call View Edit Form
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value); // syntax: FindById(id.value) - Because in the beginning of the method the passages of ID was set as optionally.
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]  // Indicates this action like POST
        [ValidateAntiForgeryToken] // Avoid CSRF attack 
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);              // Call method to update a Seller at database
                return RedirectToAction(nameof(Index));     // After deleted the register return to the Index page
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}