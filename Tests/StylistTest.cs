using Xunit;
using System;
using System.Collections.Generic;
using HairSalon.Objects;

namespace  HairSalon
{
  public class StylistTest : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_DatabaseEmpty_true()
    {
      //Arrange
      //Act
      int result = Stylist.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_EqualOverride_True()
    {
      Stylist firstStylist = new Stylist("Grace");
      Stylist secondStylist = new Stylist("Grace");

      Assert.Equal(firstStylist,secondStylist);
    }
  }
}