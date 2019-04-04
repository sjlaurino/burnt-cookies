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

      Item Eggs = new Item("Eggs", "Farm Raised Eggs");
      Item Butter = new Item("Butter", "1 stick of unsalted butter");
      Item Mixer = new Item("Mixer", "The big homie");
      Item CookieMix = new Item("Cookie Mix", "Betty Crocker: Oatmeal Chocolate Chip Cookie Mix");

      Kitchen.addRoomPath(Direction.pantry, Pantry);
      Kitchen.addRoomPath(Direction.fridge, Fridge);
      Kitchen.addRoomPath(Direction.diningRoom, DiningRoom);
      Pantry.addRoomPath(Direction.kitchen, Kitchen);
      Fridge.addRoomPath(Direction.kitchen, Kitchen);
      DiningRoom.addRoomPath(Direction.kitchen, Kitchen);

      Fridge.Items.Add(Eggs);
      Fridge.Items.Add(Butter);
      Pantry.Items.Add(CookieMix);
      Pantry.Items.Add(Mixer);






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
      System.Console.WriteLine("Press any key to continue to the Game Instructions");
      Console.ReadKey();
      Console.Clear();
      System.Console.WriteLine("You are in the Kitchen, you must go between the Pantry and the Fridge to retrieve the necessary ingredients.\nOnce you have the ingredients you need to mix them and put them in the Oven...\nPro-tip: Don't forget to take them out before they burn!\nLastly, when the cookies are finished put them in the Dining Room");
      System.Console.WriteLine("Press any key to see the Recipe");
      Console.ReadKey();
      Console.Clear();
      System.Console.WriteLine("Get:\n1. Mixer \n2. 1 Stick of Butter \n3. 1 Package of Cookie Mix\n4. 1 Egg ");
      System.Console.WriteLine("Directions: Use the Stick of Butter with Egg and Cookie Mix\nThen Use the Mixer to blend the ingredients.");
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
    Go Options:
      Pantry
      DiningRoom
      Fridge
      Kitchen
        
- `Use < ItemName >` Uses an item in a room or from your inventory
  
- `Take < ItemName >` Places an item into the player inventory and removes it from the room
    Item Options:
      Eggs
      Butter
      CookieMix
      Mixer
    
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
      {
        return itemName.ToLower() == i.Name.ToLower();
      });
      if (item == null)
      {
        System.Console.WriteLine("That item doesn't exist in this room");
        return;
      }
      CurrentRoom.Items.Remove(item);
      Player.Inventory.Add(item);
      switch (itemName)
      {
        case "eggs":
          System.Console.WriteLine(@"
                       .-~-.
                     .'     '.
                    /         \
            .-~-.  :           ;
          .'     '.|           |
         /         \           :
        :           ; .-~""~-,/
        |           /`        `'.
        :          |             \
         \         |             /
          `.     .' \          .'
            `~~~`    '-.____.-'
                       
  ");
          System.Console.WriteLine("You added Eggs to your inventory");
          GetUserInput();
          break;
        case "butter":
          System.Console.WriteLine("You added Butter to your inventory");
          GetUserInput();
          break;
        case "cookiemix":
          System.Console.WriteLine(@"
        +---------------+
        |               |
        |  Cookie Mix   |
        |               |
        |  XXXX         |
        | X    X        |
        | X    XXXXX    |
        | X   XX    X   |
        |  XXX X    X   |
        |       XXXX    |
        |               |
        +---------------+

        ");
          System.Console.WriteLine("You added the Cookie Mix to your inventory");
          GetUserInput();
          break;
        case "mixer":
          System.Console.WriteLine("You added the Mixer to your inventory");
          System.Console.WriteLine(@"
             ___
          , | l | 
         (( | l | ))
            | l | '
             \_/
            /...\--.   _  
            =====  `--(_=
          ");
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
      Item item = CurrentRoom.Items.Find(i =>
      {
        return itemName.ToLower() == i.Name.ToLower();
      });
      if (item == null)
      {
        System.Console.WriteLine("That item doesn't exist in your inventory");
        return;
      }
      Player.Inventory.Remove(item);
      CurrentRoom.Items.Add(item);
      switch (itemName)
      {
        case "eggs":
          System.Console.WriteLine(@"
                       .-~-.
                     .'     '.
                    /         \
            .-~-.  :           ;
          .'     '.|           |
         /         \           :
        :           ; .-~""~-,/
        |           /`        `'.
        :          |             \
         \         |             /
          `.     .' \          .'
            `~~~`    '-.____.-'
                       
  ");
          System.Console.WriteLine("You added Eggs to the Mixer");
          GetUserInput();
          break;
        case "butter":
          System.Console.WriteLine("You added Butter to the Mixer");
          GetUserInput();
          break;
        case "cookiemix":
          System.Console.WriteLine(@"
        +---------------+
        |               |
        |  Cookie Mix   |
        |               |
        |  XXXX         |
        | X    X        |
        | X    XXXXX    |
        | X   XX    X   |
        |  XXX X    X   |
        |       XXXX    |
        |               |
        +---------------+

        ");
          System.Console.WriteLine("You added the Cookie Mix to the Mixer");
          GetUserInput();
          break;
        case "mixer":
          System.Console.WriteLine("You blended the ingredients");
          System.Console.WriteLine(@"
             ___
          , | l | 
         (( | l | ))
            | l | '
             \_/
            /...\--.   _  
            =====  `--(_=
          ");
          GetUserInput();
          break;
        default:
          System.Console.WriteLine("Not a valid item, try again...");
          GetUserInput();
          break;

      }

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
