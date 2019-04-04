using System.Collections.Generic;
using burntCookies.Project.Interfaces;

namespace burntCookies.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<Direction, IRoom> RoomPaths { get; set; }


    public void addRoomPath(Direction direction, IRoom room)
    {
      RoomPaths.Add(direction, room);
    }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      RoomPaths = new Dictionary<Direction, IRoom>();
      Items = new List<Item>();
    }
  }

  public enum Direction
  {
    pantry,
    fridge,
    diningRoom,
    kitchen,
    none

  }
}