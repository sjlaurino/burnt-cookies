using System.Collections.Generic;
using burntCookies.Project.Interfaces;

namespace burntCookies.Project.Models
{
  public class Item : IItem
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public Room Room { get; set; }

    public Item(string name, string description, Room room)
    {
      Name = name;
      Description = description;
      Room = room;
    }
  }
}