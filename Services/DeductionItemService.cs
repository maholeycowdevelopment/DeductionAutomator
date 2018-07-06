using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeductionAutomator.Models;
using DeductionAutomator.Data;

namespace DeductionAutomator.Services
{
  public class DeductionItemService : IDeductionItemService
  {
    private readonly ApplicationDbContext _context;

    public DeductionItemService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<DeductionItem[]> GetDeductionItemsAsync()
    {
      return await _context.Deductions.ToArrayAsync();
    }
  }
}