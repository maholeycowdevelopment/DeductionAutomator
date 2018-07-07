using System;
using System.ComponentModel.DataAnnotations;

namespace DeductionAutomator.Models
{
  public class DeductionEntry
  {
    public Guid Id { get; set; }

    [Required]
    public string EmployeeName { get; set; }

    public float YearlyDeduction { get; set; }

    public string Dependents { get; set; }

    public string UserId { get; set; }
  }
}