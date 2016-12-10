using Xunit;
using System;
using System.Collections.Generic;
using HairSalon.Objects;

namespace  HairSalon
{
  public class ClientTest : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
    }
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_DatabaseEmpty_true()
    {
      //Arrange
      //Act
      int result = Client.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_EqualOverride_True()
    {
      //Arrange and Act
      Client firstClient = new Client("Cathy", 1);
      Client secondClient = new Client("Cathy", 1);
      //Assert
      Assert.Equal(firstClient,secondClient);
    }
    [Fact]
    public void Save_SavesClientToDatabase_true()
    {
      //Arrange
      Client newClient = new Client("Cindy", 1);
      //Act
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      //Assert
      Assert.Equal(newClient, allClients[0]);
    }
    [Fact]
    public void Find_FindsClientById_true()
    {
      //Arrange
      Client newClient = new Client("Betty", 2);
      newClient.Save();
      //Act
      Client foundClient = Client.Find(newClient.GetId());
      //Assert
      Assert.Equal(newClient, foundClient);
    }
    [Fact]
    public void Edit_UpdatesValues_true()
    {
      //Arrange
      Client newClient = new Client("client1", 2);
      newClient.Save();
      //Act
      newClient.Edit("Cliff", 1);
      Client foundClient = Client.Find(newClient.GetId());
      Client expectedResult = new Client(newClient.GetName(), newClient.GetStylistId(), newClient.GetId());

      //Assert
      Assert.Equal(expectedResult, foundClient);
    }
    [Fact]
    public void Delete_DeletesClienFromDB()
    {
      //Arrange
      Client newClient1 = new Client("name1", 1);
      Client newClient2 = new Client("name2", 2);
      newClient1.Save();
      newClient2.Save();
      //Act
      newClient1.Delete();
      List<Client> result = Client.GetAll();
      List<Client> expectedResult = new List<Client> {newClient2};
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void GetStylistName_ReturnsStylistName_true()
    {
      //Arrange
      Stylist newStylist = new Stylist("Frankie");
      newStylist.Save();
      Client newClient = new Client("Frances", newStylist.GetId());
      newClient.Save();
      string expectedResult = newStylist.GetName();
      //Act
      string result = newClient.GetStylistName();
      //Assert
      Assert.Equal(expectedResult, result);
    }
  }
}
