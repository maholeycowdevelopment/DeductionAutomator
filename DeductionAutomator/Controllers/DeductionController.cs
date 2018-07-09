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

    public IActionResult AddDeductionForm()
    {
      return View();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddDeductionEntry(DeductionEntry newEntry)
    {
      if (!ModelState.IsValid)
      {
        return RedirectToAction("Index");
      }

      var currentUser = await _userManager.GetUserAsync(User);
      if (currentUser == null) return Challenge();

      var successful = await _deductionEntryService.AddDeductionEntryAsync(newEntry, currentUser);
      if (!successful)
      {
        return BadRequest("Could not add item.");
      }

      return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteDeductionEntry(DeductionEntry existingEntry)
    {
      var currentUser = await _userManager.GetUserAsync(User);
      if (currentUser == null) return Challenge();

      var successful = await _deductionEntryService.DeleteDeductionEntryAsync(existingEntry, currentUser);
      if (!successful)
      {
        return BadRequest("Could not delete item.");
      }

      return RedirectToAction("Index");
    }

    public IActionResult EditDeductionEntry(Guid id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var entry = _deductionEntryService.GetStudentToUpdate(id);
      if (entry == null)
      {
        return NotFound();
      }
      return View(entry);
    }
  }
}