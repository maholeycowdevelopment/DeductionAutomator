using System;
using System.Threading.Tasks;
using DeductionAutomator.Data;
using DeductionAutomator.Models;
using DeductionAutomator.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DeductionAutomator.UnitTests
{
  public class DeductionEntryServiceUnitTest
  {
    [Fact]
    public async Task AddNewDeductionEntry()
    {
      var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Test_AddNewEntry").Options;

      // Set up a context (connection to the "DB") for writing
      using (var context = new ApplicationDbContext(options))
      {
        var service = new DeductionEntryService(context);

        var fakeUser = new ApplicationUser
        {
          Id = "fakeId",
          UserName = "lebronJames@gmail.com"
        };

        await service.AddDeductionEntryAsync(new DeductionEntry
        {
          EmployeeName = "Steph Curry",
          Dependents = "John, Joe, Anna",
        }, fakeUser);
      }

      // Use a separate context to read data back from the "DB"
      using (var context = new ApplicationDbContext(options))
      {
        var databaseItems = await context.Deductions.CountAsync();
        Assert.Equal(1, databaseItems);

        var entry = await context.Deductions.FirstAsync();
        Assert.Equal("Steph Curry", entry.EmployeeName);
        Assert.Equal("John, Joe, Anna", entry.Dependents);
        Assert.Equal(2450, entry.YearlyDeduction);
        Assert.Equal("94.23", entry.PaycheckDeduction.ToString("0.00"));
      }
    }
  }
}