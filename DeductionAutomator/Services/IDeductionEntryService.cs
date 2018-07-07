using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeductionAutomator.Models;

namespace DeductionAutomator.Services
{
  public interface IDeductionEntryService
  {
    Task<DeductionEntry[]> GetDeductionEntriesAsync();

    Task<bool> AddDeductionEntryAsync(DeductionEntry newEntry);
  }
}