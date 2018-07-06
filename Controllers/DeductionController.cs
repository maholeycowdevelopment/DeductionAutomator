using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeductionAutomator.Models;
using DeductionAutomator.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeductionAutomator.Controllers
{
  public class DeductionController : Controller
  {
    private readonly IDeductionItemService _deductionItemService;

    public DeductionController(IDeductionItemService deductionItemService)
    {
      _deductionItemService = deductionItemService;
    }

    public async Task<IActionResult> Index()
    {
      // Get employee deductions from database
      var deductions = await _deductionItemService.GetDeductionItemsAsync();
      // Put deductions into a model
      var model = new DeductionViewModel()
      {
        Deductions = deductions
      };
      // Render view using the model
      return View(model);
    }
  }
}