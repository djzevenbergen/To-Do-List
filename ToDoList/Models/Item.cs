using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Priority { get; set; }
    public int Id { get; set; }
    public Item(string des)
    {
      Description = des;

    }
    public Item(string des, int priority)
      : this(des)
    {
      Priority = priority;
    }

    public Item(string des, int priority, int id)
    {
      Description = des;
      Id = id;
      Priority = priority;
    }


    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item)otherItem;
        bool idEquality = (this.Id == newItem.Id);
        bool descriptionEquality = (this.Description == newItem.Description);
        return (idEquality && descriptionEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this.Description;
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();

      var cm = conn.CreateCommand() as MySqlCommand;

      cm.CommandText = @"INSERT INTO items (priority) VALUES (@ItemPriority);";
      MySqlParameter priority = new MySqlParameter();
      priority.ParameterName = "@ItemPriority";
      priority.Value = this.Priority;
      cm.Parameters.Add(priority);
      cm.ExecuteNonQuery();


      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();

      }
    }

    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        int itemPriority = rdr.GetInt32(2);
        Item newItem = new Item(itemDescription, itemPriority, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Item Find(int searchId)
    {
      Item placeHolderItem = new Item("placeholder item");
      return placeHolderItem;
    }
  }
}