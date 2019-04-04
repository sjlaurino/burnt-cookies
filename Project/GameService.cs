using System;
using System.Collections.Generic;
using burntCookies.Project.Interfaces;
using burntCookies.Project.Models;

namespace burntCookies.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get; set; }
    Room IGameService.CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Playing { get; set; }

    Player Player = new Player();

    private void Initialize()
    {
      Room Kitchen = new Room("Kitchen", "You are in the Kitchen");
      Room Pantry = new Room("Pantry", "You are in the Pantry");
      Room Fridge = new Room("Fridge", "You are in the Refrigerator");
      Room DiningRoom = new Room("Dining Room", "You are in the Dining Room");

      Item Eggs = new Item("Eggs", "Farm Raised Eggs", Fridge);
      Item Butter = new Item("Butter", "1 stick of unsalted butter", Fridge);
      Item KitchenAidMixer = new Item("KitchenAidMixer", "The big homie", Pantry);
      Item CookieMix = new Item("Cookie Mix", "Betty Crocker: Oatmeal Chocolate Chip Cookie Mix", Pantry);

      Kitchen.addRoomPath(Direction.pantry, Pantry);
      Kitchen.addRoomPath(Direction.fridge, Fridge);
      Kitchen.addRoomPath(Direction.diningRoom, DiningRoom);
      Pantry.addRoomPath(Direction.kitchen, Kitchen);
      Fridge.addRoomPath(Direction.kitchen, Kitchen);
      DiningRoom.addRoomPath(Direction.kitchen, Kitchen);

      CurrentRoom = Kitchen;
      Playing = false;

    }
    public void Run()
    {
      Setup();
    }

    public void Setup()
    {
      Initialize();
      System.Console.WriteLine("Welcome to BurntCookies! \n Press any key to continue");
      Console.ReadKey();
      Console.Clear();
      System.Console.WriteLine("BurntCookies is a real life simulation that will test not only your wits, but also your will... \n You have been tasked with making cookies before your spouse gets home.");
      System.Console.WriteLine("Press any key to continue");
      Console.ReadKey();
      Console.Clear();

      System.Console.WriteLine("Pay close attention to the recipe, any mistakes and you won't make the cookies in time.");
      System.Console.WriteLine("Press (I) to view the Game Instructions.");
      string input = Console.ReadLine().ToLower();
      while (input != "i")
      {
        System.Console.WriteLine("Invalid Key, try again");
        input = Console.ReadLine().ToLower();
      }
      Console.Clear();
      System.Console.WriteLine("You are in the Kitchen, you must go between the Pantry and the Fridge to retrieve the necessary ingredients.\nOnce you have the ingredients you need to mix them and put them in the Oven...\nPro-tip: Don't forget to take them out before they burn!");
      System.Console.WriteLine("Press any key to see the Recipe");
      Console.ReadKey();
      Console.Clear();
      System.Console.WriteLine("Get:\n1. KitchenAid Mixer \n2. 1 Stick of Butter \n3. 1 Package of Cookie Mix\n4. 1 Egg ");
      System.Console.WriteLine("Directions: Place the Stick of Butter with Egg and Cookie Mix\nThen use the KitchenAid to mix the ingredients.");
      System.Console.WriteLine("At any point in the game you can enter (Help) to view a list of your Commands or (I) to view your Inventory.");
      System.Console.WriteLine("Press any key to begin");
      Console.ReadKey();
      StartGame();
    }

    public void Reset()
    {
      Setup();
    }

    public void StartGame()
    {
      Playing = true;
      Console.Clear();
      System.Console.WriteLine("You are in the Kitchen... Go get the ingredients to make some cookies!");
      while (Playing)
      {
        GetUserInput();
      }
    }

    public void GetUserInput()
    {
      string userInput = Console.ReadLine().ToLower();
      string[] inputs = userInput.Split(' ');
      string command = inputs[0];
      string option = "";
      if (inputs.Length > 1)
      {
        option = inputs[1];
      }
      Console.Clear();
      switch (command)
      {
        case "go":
          Go(option);
          break;
        case "use":
          UseItem(option);
          break;
        case "take":
          TakeItem(option);
          break;
        case "map":
          ShowMap();
          break;
        case "quit":
          Quit();
          break;
        case "reset":
          Reset();
          break;
        case "look":
          Look();
          break;
        case "i":
          Inventory();
          break;
        case "help":
          Help();
          break;
        default:
          System.Console.WriteLine("Invalid input");
          GetUserInput();
          break;
      }
    }


    public void Quit()
    {
      Playing = false;
      return;
    }

    public void Help()
    {
      System.Console.WriteLine(@"- `Go <Room>` Moves the player from room to room
    Options:
      Pantry
      Dining Room
      Fridge
      Kitchen
        
- `Use < ItemName >` Uses an item in a room or from your inventory
  
- `Take < ItemName >` Places an item into the player inventory and removes it from the room
    
- `Look` Prints the description of the room again

- `Map` Shows the game map
  
- `Inventory` Prints the players inventory
  
- `Help` Shows a list of commands and actions

- `Reset` Restarts the game
  
- `Quit` Quits the Game");

      GetUserInput();
    }

    public void Go(string roomName)
    {
      Direction dir = Direction.none;
      switch (roomName)
      {
        case "pantry":
          dir = Direction.pantry;
          break;
        case "diningroom":
          dir = Direction.diningRoom;
          break;
        case "fridge":
          dir = Direction.fridge;
          break;
        case "kitchen":
          dir = Direction.kitchen;
          break;
        default:
          Console.WriteLine("Try again");
          break;
      }
      if (CurrentRoom.RoomPaths.ContainsKey(dir))
      {
        CurrentRoom = CurrentRoom.RoomPaths[dir];
        System.Console.WriteLine($"You are in the {CurrentRoom.Name}, what would you like to do?");
        GetUserInput();
      }
      else
      {
        System.Console.WriteLine("Cannot go there from this room... try a different room");
        GetUserInput();
      }
    }

    public void TakeItem(string itemName)
    {
      Item item = CurrentRoom.Items.Find(i =>
      itemName.ToLower() == i.Name.ToLower()
      );
      if (item == null)
      {
        System.Console.WriteLine("cannot take that");
        return;
      }
      CurrentRoom.Items.Remove(item);
      Player.Inventory.Add(item);
      switch (itemName)
      {
        case "eggs":
          System.Console.WriteLine("You have added Eggs to your inventory");
          GetUserInput();
          break;
        case "butter":
          System.Console.WriteLine("You have added Butter to your inventory");
          GetUserInput();
          break;
        case "cookiemix":
          System.Console.WriteLine("You have added the Cookie Mix to your inventory");
          GetUserInput();
          break;
        case "kitchenaidmixer":
          System.Console.WriteLine("You have added the KitchenAid Mixer to your inventory");
          GetUserInput();
          break;
        default:
          System.Console.WriteLine("Not a valid item, try again...");
          GetUserInput();
          break;

      }
    }

    public void UseItem(string itemName)
    {

    }

    public void Inventory()
    {
      if (Player.Inventory.Count > 0)
      {
        foreach (Item item in Player.Inventory)
        {
          System.Console.WriteLine($"{item.Name}");
          GetUserInput();
        }
      }
      System.Console.WriteLine("No Items in your inventory!");
      System.Console.WriteLine("Reminder... you are in the " + CurrentRoom.Name);
      GetUserInput();
    }

    public void Look()
    {
      System.Console.WriteLine("You are in the " + CurrentRoom.Name + ", Description: " + CurrentRoom.Description);
      GetUserInput();

    }

    public void ShowMap()
    {
      System.Console.WriteLine(@"
                 +------+
                 |Fridge|
                 |      |
         +----------  ----+
         |                |
+--------+                |
|        |                |
| Dining       Kitchen    |
|  Room  |                |
|        +                |
+--------+-----+--   -+---+
               |        |
               | Pantry |
               +--------+
      ");
      GetUserInput();
    }
  }
}
