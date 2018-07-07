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

    public async Task<DeductionEntry[]> GetDeductionEntriesAsync()
    {
      return await _context.Deductions.ToArrayAsync();
    }

    public async Task<bool> AddDeductionEntryAsync(DeductionEntry newEntry)
    {
      newEntry.Id = Guid.NewGuid();
      newEntry.YearlyDeduction = CalculateEmployeeDeduction(newEntry.EmployeeName, newEntry.Dependents);

      _context.Deductions.Add(newEntry);

      var saveResult = await _context.SaveChangesAsync();
      return saveResult == 1;
    }

    private int CalculateEmployeeDeduction(string employeeName, string dependents)
    {
      int employeeDeduction = (employeeName.Trim()[0] == 'A') ? 900 : 1000;

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