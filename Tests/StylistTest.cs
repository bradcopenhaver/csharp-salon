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
      //Arrange and Act
      Stylist firstStylist = new Stylist("Grace");
      Stylist secondStylist = new Stylist("Grace");
      //Assert
      Assert.Equal(firstStylist,secondStylist);
    }
    [Fact]
    public void Save_SavesStylisToDatabase_true()
    {
      //Arrange
      Stylist newStylist = new Stylist("Francesca");
      //Act
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      //Assert
      Assert.Equal(newStylist, allStylists[0]);
    }
    [Fact]
    public void Find_FindsStylistById_true()
    {
      //Arrange
      Stylist newStylist = new Stylist("Harmony");
      newStylist.Save();
      //Act
      Stylist foundStylist = Stylist.Find(newStylist.GetId());
      //Assert
      Assert.Equal(newStylist, foundStylist);
    }
  }
}
