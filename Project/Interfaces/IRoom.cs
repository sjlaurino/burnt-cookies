using System.Collections.Generic;
using burntCookies.Project.Models;

namespace burntCookies.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    List<Item> Items { get; set; }
    Dictionary<Direction, IRoom> RoomPaths { get; set; }
  }
}
