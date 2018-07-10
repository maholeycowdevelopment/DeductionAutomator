using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DeductionAutomator.IntegrationTests
{
  public class DeductionRouteIntegrationTest : IClassFixture<TestFixture>
  {
    private readonly HttpClient _client;

    public DeductionRouteIntegrationTest(TestFixture fixture)
    {
      _client = fixture.Client;
    }

    [Fact]
    public async Task ChallengeAnonymousUser()
    {
      // Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, "/deduction");

      // Act
      var response = await _client.SendAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
      Assert.Equal("http://localhost:8888/Deduction" + "Login?ReturnUrl=%2Fdeduction", response.Headers.Location.ToString());
    }
  }
}