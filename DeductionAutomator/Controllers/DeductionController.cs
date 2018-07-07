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
    private readonly IDeductionEntryService _deductionEntryService;

    public DeductionController(IDeductionEntryService deductionEntryService)
    {
      _deductionEntryService = deductionEntryService;
    }

    public async Task<IActionResult> Index()
    {
      // Get deduction entries from database
      var deductions = await _deductionEntryService.GetDeductionEntriesAsync();

      // Put entries into a model
      var model = new DeductionViewModel()
      {
        Deductions = deductions
      };

      // Pass the view to a model and render
      return View(model);
    }
  }
}