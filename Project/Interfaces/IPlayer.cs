using System.Collections.Generic;
using burntCookies.Project.Models;

namespace burntCookies.Project.Interfaces
{
  public interface IPlayer
  {
    List<Item> Inventory { get; set; }
  }
}
