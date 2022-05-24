using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Frexco.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Frexco.Models;
using Frexco.Services;
using Frexco.Services.Exceptions;

namespace Frexco.Controllers
{
    public class StocksController : Controller
    {
        private readonly StockService _stockService;
        private readonly ProductService _prodcutService;

        public StocksController(StockService sellerService, ProductService departmentService)
        {
            _stockService = sellerService;
            _prodcutService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _stockService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var products = await _prodcutService.FindAllAsync();
            var viewModel = new StockFormViewModel { Products = products };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                var products = await _prodcutService.FindAllAsync();
                var viewModel = new StockFormViewModel { Stock = stock, Products = products };
                return View(viewModel);
            }
            await _stockService.InsertAsync(stock);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _stockService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _stockService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _stockService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _stockService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Product> products = await _prodcutService.FindAllAsync();
            StockFormViewModel viewModel = new StockFormViewModel { Stock = obj, Products = products };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Stock stock)
        {
            if (!ModelState.IsValid)
            {
                var products = await _prodcutService.FindAllAsync();
                var viewModel = new StockFormViewModel { Stock = stock, Products = products };
                return View(viewModel);
            }
            if (id != stock.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _stockService.UpdateAsync(stock);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}