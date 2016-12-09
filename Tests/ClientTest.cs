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
  }
}
