using System;
using burntCookies.Project;

namespace burntCookies
{
  public class Program
  {
    public static void Main(string[] args)
    {
      GameService gs = new GameService();
      gs.Run();
    }
  }
}
