using System.Collections.Generic;
using burntCookies.Project.Models;

namespace burntCookies.Project.Interfaces
{
  public interface IPlayer
  {
    string PlayerName { get; set; }
    List<Item> Inventory { get; set; }
  }
}
