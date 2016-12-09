using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Client
  {
    string _name;
    int _stylist_id;
    int _id;

    public Client(string name, int stylist_id, int id = 0)
    {
      _name = name;
      _stylist_id = stylist_id;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetStylistId()
    {
      return _stylist_id;
    }
    public string GetName()
    {
      return _name;
    }
    public override bool Equals(Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        return (idEquality && nameEquality && stylistIdEquality);
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);
        Client newClient =  new Client(name, stylistId, id);
        allClients.Add(newClient);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allClients;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@name, @stylist_id);", conn);

      SqlParameter nameParameter = new SqlParameter("@name", this.GetName());
      SqlParameter stylistIdParameter = new SqlParameter("@stylist_id", this.GetStylistId());

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(stylistIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
      SqlParameter ClientIdParameter = new SqlParameter("@ClientId", id.ToString());

      cmd.Parameters.Add(ClientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundClientId = 0;
      string foundClientName = null;
      int foundStylistId = 0;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundStylistId = rdr.GetInt32(2);
      }
      Client foundClient = new Client(foundClientName, foundStylistId, foundClientId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }
    public void Edit(string newName, int newStylistId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName, stylist_id = @NewStylistId OUTPUT INSERTED.name, INSERTED.stylist_id WHERE id = @ClientId;", conn);

      SqlParameter[] insertParameters = new SqlParameter[]
      {
        new SqlParameter("@NewName", newName),
        new SqlParameter("@NewStylistId", newStylistId),
        new SqlParameter("@ClientId", this.GetId())
      };

      cmd.Parameters.AddRange(insertParameters);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._stylist_id = rdr.GetInt32(1);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @Id;", conn);
      SqlParameter idParameter = new SqlParameter("@Id", this.GetId());

      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
