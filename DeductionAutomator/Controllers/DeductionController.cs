using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeductionAutomator.Models;
using DeductionAutomator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace DeductionAutomator.Controllers
{
  [Authorize]
  public class DeductionController : Controller
  {
    private readonly IDeductionEntryService _deductionEntryService;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeductionController(IDeductionEntryService deductionEntryService, UserManager<ApplicationUser> userManager)
    {
      _deductionEntryService = deductionEntryService;
      _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
      var currentUser = await _userManager.GetUserAsync(User);
      if (currentUser == null) return Challenge();

      // Get deduction entries from database
      var deductions = await _deductionEntryService.GetDeductionEntriesAsync(currentUser);

      // Put entries into a model
      var model = new DeductionViewModel()
      {
        Deductions = deductions
      };

      // Pass the view to a model and render
      return View(model);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddDeductionEntry(DeductionEntry newEntry)
    {
      if (!ModelState.IsValid)
      {
        return RedirectToAction("Index");
      }

      var successful = await _deductionEntryService.AddDeductionEntryAsync(newEntry);
      if (!successful)
      {
        return BadRequest("Could not add item.");
      }

      return RedirectToAction("Index");
    }
  }
}