using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        // GET action to the button "Create New" created at Sellers page that will call View Create Form
        public async Task<IActionResult> Create()   
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]  // Indicates this action like POST
        [ValidateAntiForgeryToken] // Avoid CSRF attack 
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);              // Call method to insert a new Seller at database
            return RedirectToAction(nameof(Index));     // After inserted the register return to the Index page
        }

        // GET action to the link "Delete" created at Sellers page that will call View Delete Form
        public async Task<IActionResult> Delete(int? id) // int? id, meaning that enter of ID is optionally
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // syntax: FindById(id.value) - Because in the beginning of the method the passages of ID was set as optionally.
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]  // Indicates this action like POST
        [ValidateAntiForgeryToken] // Avoid CSRF attack 
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);              // Call method to remove to delete a seller
            return RedirectToAction(nameof(Index));     // After deleted the register return to the Index page
        }

        // GET action to the link "Details" created at Sellers page that will call View Details Form
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // syntax: FindById(id.value) - Because in the beginning of the method the passages of ID was set as optionally.
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // GET action to the link "Edit" created at Sellers page that will call View Edit Form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // syntax: FindById(id.value) - Because in the beginning of the method the passages of ID was set as optionally.
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]  // Indicates this action like POST
        [ValidateAntiForgeryToken] // Avoid CSRF attack 
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);              // Call method to update a Seller at database
                return RedirectToAction(nameof(Index));     // After deleted the register return to the Index page
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // Pegar o ID interno da requisição
            };
            return View(viewModel);
        }
    }
}