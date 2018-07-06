using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeductionAutomator.Models;

namespace DeductionAutomator.Services
{
  public class FakeDeductionItemService : IDeductionItemService
  {
    public Task<DeductionItem[]> GetDeductionItemsAsync()
    {
      var deduction1 = new DeductionItem
      {
        FirstName = "Pat",
        LastName = "Clover",
        DependentsCount = 4,
        YearlyDeduction = 3000
      };

      return Task.FromResult(new[] { deduction1 });
    }
  }
}