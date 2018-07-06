using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeductionAutomator.Models;

namespace DeductionAutomator.Services
{
  public interface IDeductionItemService
  {
    Task<DeductionItem[]> GetDeductionItemsAsync();
  }
}