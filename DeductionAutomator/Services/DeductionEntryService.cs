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
  }
}