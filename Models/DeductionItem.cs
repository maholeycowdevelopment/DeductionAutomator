using System;
using System.ComponentModel.DataAnnotations;

namespace DeductionAutomator.Models
{
  public class DeductionItem
  {
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public int DependentsCount { get; set; }

    public int YearlyDeduction { get; set; }
  }
}