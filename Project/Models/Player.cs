using System.Collections.Generic;
using burntCookies.Project.Interfaces;

namespace burntCookies.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }
  }
}