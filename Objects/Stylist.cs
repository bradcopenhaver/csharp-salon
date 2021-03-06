using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Stylist
  {
    string _name;
    int _id;

    public Stylist(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public override bool Equals(Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        return (idEquality && nameEquality);
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Stylist newStylist =  new Stylist(name, id);
        allStylists.Add(newStylist);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allStylists;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@name);", conn);

      SqlParameter nameParameter = new SqlParameter("@name", this.GetName());

      cmd.Parameters.Add(nameParameter);

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
    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
      SqlParameter StylistIdParameter = new SqlParameter("@StylistId", id.ToString());

      cmd.Parameters.Add(StylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStylistId = 0;
      string foundStylistName = null;

      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundStylist;
    }
    public List<Client> FindClients()
    {
      List<Client> foundClients = new List<Client> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @StylistId;", conn);
      SqlParameter StylistIdParameter = new SqlParameter("@StylistId", this.GetId());
      cmd.Parameters.Add(StylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int foundClientId = rdr.GetInt32(0);
        string foundClientName = rdr.GetString(1);
        int foundClientStylistId = rdr.GetInt32(2);

        Client newClient = new Client(foundClientName, foundClientStylistId, foundClientId);
        foundClients.Add(newClient);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundClients;
    }
    public void Edit(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @StylistId;", conn);

      SqlParameter nameParameter = new SqlParameter("@NewName", newName);
      SqlParameter StylistIdParameter = new SqlParameter("@StylistId", this.GetId());
      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(StylistIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._name = rdr.GetString(0);
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

      SqlCommand cmd1 = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId;", conn);
      SqlCommand cmd2 = new SqlCommand("DELETE FROM clients WHERE stylist_id = @StylistId;", conn);
      SqlParameter StylistIdParameter1 = new SqlParameter("@StylistId", this.GetId());
      SqlParameter StylistIdParameter2 = new SqlParameter("@StylistId", this.GetId());

      cmd1.Parameters.Add(StylistIdParameter1);
      cmd2.Parameters.Add(StylistIdParameter2);
      cmd1.ExecuteNonQuery();
      cmd2.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
