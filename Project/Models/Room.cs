using System.Collections.Generic;
using burntCookies.Project.Interfaces;

namespace burntCookies.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
  }
}