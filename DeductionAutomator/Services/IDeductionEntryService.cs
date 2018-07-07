using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeductionAutomator.Models;

namespace DeductionAutomator.Services
{
  public interface IDeductionEntryService
  {
    Task<DeductionEntry[]> GetDeductionEntriesAsync(ApplicationUser user);

    Task<bool> AddDeductionEntryAsync(DeductionEntry newEntry);
  }
}