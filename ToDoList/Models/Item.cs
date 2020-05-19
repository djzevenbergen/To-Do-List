using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Priority { get; set; }
    public int Id { get; }
    private static List<Item> _instances = new List<Item> { };
    public Item(string des)
    {
      Description = des;
      _instances.Add(this);
      Id = _instances.Count;
    }
    public Item(string des, int priority)
      : this(des)
    {
      Priority = priority;
    }

    public static List<Item> GetAll()
    {
      return _instances;
    }

    public static void ClearAll()
    {
      _instances.Clear();
    }
    public static Item Find(int searchId)
    {
      return _instances[searchId - 1];
    }
  }
}