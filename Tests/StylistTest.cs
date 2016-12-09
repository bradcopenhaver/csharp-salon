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
    [Fact]
    public void FindClients_FindsClientsByStylistId_true()
    {
      //Arrange
      Stylist newStylist1 = new Stylist("Serenity");
      Stylist newStylist2 = new Stylist("Dawn");
      newStylist1.Save();
      newStylist2.Save();
      Client client1 = new Client("client1", newStylist1.GetId());
      Client client2 = new Client("client2", newStylist2.GetId());
      Client client3 = new Client("client3", newStylist1.GetId());
      client1.Save();
      client2.Save();
      client3.Save();
      List<Client> expectedResult = new List<Client> {client1, client3};
      //Act
      List<Client> result = newStylist1.FindClients();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Edit_ChangesName_true()
    {
      //Arrange
      Stylist newStylist = new Stylist("Grace");
      newStylist.Save();
      //Act
      newStylist.Edit("Graysce");
      Stylist foundStylist = Stylist.Find(newStylist.GetId());

      //Assert
      Assert.Equal("Graysce", foundStylist.GetName());
    }
  }
}
