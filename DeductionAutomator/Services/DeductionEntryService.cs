using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeductionAutomator.Data;
using DeductionAutomator.Models;
using Microsoft.EntityFrameworkCore;

namespace DeductionAutomator.Services
{
  public class DeductionEntryService : IDeductionEntryService
  {
    private readonly ApplicationDbContext _context;

    public DeductionEntryService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<DeductionEntry[]> GetDeductionEntriesAsync(ApplicationUser user)
    {
      return await _context.Deductions.Where(x => x.UserId == user.Id).ToArrayAsync();
    }

    public async Task<bool> AddDeductionEntryAsync(DeductionEntry newEntry, ApplicationUser user)
    {
      newEntry.Id = Guid.NewGuid();
      newEntry.YearlyDeduction = CalculateEmployeeDeduction(newEntry.EmployeeName, newEntry.Dependents);
      newEntry.PaycheckDeduction = newEntry.YearlyDeduction / 26;
      newEntry.UserId = user.Id;

      _context.Deductions.Add(newEntry);

      var saveResult = await _context.SaveChangesAsync();
      return saveResult == 1;
    }

    public async Task<bool> DeleteDeductionEntryAsync(DeductionEntry existingEntry, ApplicationUser user)
    {
      _context.Deductions.Remove(_context.Deductions.FirstOrDefault(x => (x.UserId == user.Id && x.Id == existingEntry.Id)));
      var saveResult = await _context.SaveChangesAsync();
      return saveResult == 1;
    }

    public async Task<bool> UpdateDeductionEntryAsync(DeductionEntry updatedEntry, ApplicationUser user)
    {
      _context.Deductions.Remove(_context.Deductions.FirstOrDefault(x => (x.EmployeeName == updatedEntry.EmployeeName)));

      _context.Deductions.Add(updatedEntry);

      var saveResult = await _context.SaveChangesAsync();
      return saveResult == 1;
    }

    public DeductionEntry GetStudentToUpdate(Guid id)
    {
      var entry = _context.Deductions.FirstOrDefault(x => x.Id == id);
      return entry;
    }

    private float CalculateEmployeeDeduction(string employeeName, string dependents)
    {
      float employeeDeduction = (employeeName.Trim()[0] == 'A') ? 900 : 1000;

      if (!dependents.Equals(""))
      {
        string[] dependentsList = dependents.Split(",");
        foreach (string dependentName in dependentsList)
        {
          employeeDeduction += (dependentName.Trim()[0] == 'A') ? 450 : 500;
        }
      }

      return employeeDeduction;
    }
  }
}