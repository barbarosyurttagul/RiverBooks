using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.BookEndpoints;
using Xunit.Abstractions;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookGetById(Fixture fixture, ITestOutputHelper testOutputHelper)
  : TestClass<Fixture>(fixture, testOutputHelper)
{
  [Theory]
  [InlineData("0ab9b3de-a34e-4114-9825-3704f9b716b3", "The Two Towers")]
  public async Task ReturnsExpectedBookGivenIdAsync(string validId, string expectedTitle)
  {
    Guid id = Guid.Parse(validId);
    var request = new GetBookByIdRequest { Id = id };
    var testResult = await Fixture.Client.GETAsync<GetById, GetBookByIdRequest, BookDto>(request);

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result!.Title.Should().Be(expectedTitle);
  }
}
