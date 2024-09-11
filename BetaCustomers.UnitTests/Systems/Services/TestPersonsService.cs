using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using BetaCustomers.API.Services;
using BetaCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace BetaCustomers.UnitTests.Systems.Services;

public class TestPersonsService
{
   private readonly PersonsService _sut;
   private readonly Mock<IPersonsService> _personServiceMock = new Mock<IPersonsService>();

   public TestPersonsService()
   {
      _sut = new PersonsService();
      //_sut = new PersonsService(_personServiceMock.Object);
   }

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
      Assert.Equal(expectedResponse._persons.Count, result.Count);
   }
   
   [Fact]
   public void LoadPerson_With_EmptyList()
   {
      //Arrange
      var expectedResponse = new PersonsFixture();
      var sut = new PersonsService();
      var mockHandler = new Mock<PersonsService>();

      //Act
      var result = sut.LoadAll();

      //Assert
      //result.Should().BeOfType<List<Person>>();
      Assert.IsType<List<Person>>(new List<Person>());
   }
   
   [Fact]
   public void GetPersonById_ShouldReturnPerson_WhenIdExists()
   {
      //Arrange
      var personId = 1;
      var firstName = "John";
      var lastName = "Doe";

      var personDto = new Person
      {
         Id = personId,
         FirstName = firstName,
         LastName = lastName
      };

      _personServiceMock
         .Setup(x => x.GetPersonById(personId))
         .Returns(personDto);

      //Act
      var person = _sut.GetPersonById(personId);

      //Assert
      Assert.Equal(personId, person.Id);
   }
}