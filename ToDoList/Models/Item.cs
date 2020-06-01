using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {

    public string Description { get; set; }
    public int ItemId { get; set; }

    public int CategoryId { get; set; }
    public ICollection<CategoryItem> Categories { get; set; }



  }
}