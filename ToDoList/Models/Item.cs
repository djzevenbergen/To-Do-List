using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Priority { get; set; }
    public int Id { get; }
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

    }
    public static Item Find(int searchId)
    {
      Item placeHolderItem = new Item("placeholder item");
      return placeHolderItem;
    }
  }
}