using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeductionAutomator.Models;

namespace DeductionAutomator.Services
{
  public interface IDeductionEntryService
  {
    Task<DeductionEntry[]> GetDeductionEntriesAsync(ApplicationUser user);

    Task<bool> AddDeductionEntryAsync(DeductionEntry newEntry, ApplicationUser user);

    Task<bool> DeleteDeductionEntryAsync(DeductionEntry existingEntry, ApplicationUser user);

    Task<bool> UpdateDeductionEntryAsync(DeductionEntry updatedEntry, ApplicationUser user);

    DeductionEntry GetStudentToUpdate(Guid id);
  }
}