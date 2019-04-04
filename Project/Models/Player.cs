using System.Collections.Generic;
using burntCookies.Project.Interfaces;

namespace burntCookies.Project.Models
{
  public class Player : IPlayer
  {
    public List<Item> Inventory { get; set; }
    public Player()
    {
      Inventory = new List<Item>();
    }
  }
}