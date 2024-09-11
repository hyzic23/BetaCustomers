using BetaCustomers.API.Models;
using BetaCustomers.API.Services;
using BetaCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace BetaCustomers.UnitTests.Systems.Services;

public class TestPersonsService
{
   
   [Fact]
   public void LoadPerson_ValidCall()
   {
      //Arrange
      var expectedResponse = new PersonsFixture();
      var sut = new PersonsService();
      var mockHandler = new Mock<PersonsService>();

      //Act
      var result = sut.LoadAll();

      //Assert
      //result.Should().BeOfType<List<Person>>();
      //Assert.True(result != null);
      Assert.IsType<List<Person>>(result);
   }
}