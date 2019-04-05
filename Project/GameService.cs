using System;
using System.Collections.Generic;
using burntCookies.Project.Interfaces;
using burntCookies.Project.Models;
using System.Timers;

namespace burntCookies.Project
{
  public class GameService : IGameService
  {

    public IRoom CurrentRoom { get; set; }
    Room IGameService.CurrentRoom { get; set; }
    public Player Player { get; set; }
    public Timer aTimer { get; set; }
    public bool Playing { get; set; }

    public bool OvenPreheated { get; set; }

    public GameService()
    {
      Player = new Player();
      OvenPreheated = false;
    }

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
      Item Oven = new Item("Oven", "This is the Oven");
      Item Cookies = new Item("Cookies", "Warm Oatmeal Chocolate Chip Cookies");

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
      Kitchen.Items.Add(Cookies);
      Player.Inventory.Add(Oven);






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
      System.Console.WriteLine("Welcome to BurntCookies! \n~Press any key to continue");
      Console.ReadKey();
      Console.Clear();
      System.Console.WriteLine("BurntCookies is a real life simulation that will test not only your wits, but also your will... \nYou have been tasked with making cookies without burning them.");
      System.Console.WriteLine("~Press any key to continue to the Game Instructions");
      Console.ReadKey();
      Console.Clear();
      System.Console.WriteLine("You are in the Kitchen, you must go between the Pantry and the Fridge to retrieve the necessary ingredients.\nOnce you have the ingredients you need to mix them and put them in the Oven...\n If you are able to retreive the cookies before they burn you will win the game.");
      System.Console.WriteLine("~Press any key to see the Recipe");
      Console.ReadKey();
      Console.Clear();
      System.Console.WriteLine("Get:\n1. Mixer \n2. 1 Stick of Butter \n3. 1 Package of Cookie Mix\n4. Eggs ");
      System.Console.WriteLine("Directions: Use the Stick of Butter with the Eggs and Cookie Mix\nThen Use the Mixer to blend the ingredients.\nOnce you have blended the ingredients you can Use the oven.");
      System.Console.WriteLine("At any point in the game you can enter (Help) to view a list of your Commands or (I) to view your Inventory.");
      System.Console.WriteLine("~Press any key to begin");
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
        if (inputs.Length > 2)
        {
          option += " " + inputs[2];
        }
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
      Cookie Mix
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
        :           ; .-~""~-, /
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
          System.Console.WriteLine(@"
        
       +---------------------------------+
       |                                 |
       |             Butter              |
       |                                 |
       +---------------------------------+

        ");
          GetUserInput();
          break;
        case "cookie mix":
          System.Console.WriteLine(@"
        +---------------+
        |               |
        |  Cookie Mix   |
        |               |
        |   XXXX        |
        |  X    X       |
        |  X    XXXXX   |
        |  X   XX    X  |
        |   XXX X    X  |
        |        XXXX   |
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
        case "cookies":
          if (CurrentRoom.Items.Count > 3)
          {
            aTimer.Enabled = false;
            System.Console.WriteLine("Congratulations you made perfect cookies and beat the Game!");
            System.Console.WriteLine(@"
              .---. .---. 
             :     : o   :    me want cookie!
         _..-:   o :     :-.._    /
     .-''  '  `---' `---'      ``-.
   .'   ''   '  ''  .    ''. '  ''  `.  
  :    .---.,,.,...,.,.,.,..---.     :
  `.   `.                      .'   `:
   `.  '`.                    .'    `'
    `.    `-._            _.-'     .`
      `. ''    ''--...--''   . ' .`
       '`-._'    '' .     '' _.-'
           ```--.....--''' ``    
 
      ");
            System.Console.WriteLine("Press (Q) to quit, or any other key to restart the game");
            string input = Console.ReadLine().ToLower();
            if (input == "q")
            {
              Quit();
            }
            Reset();
          }
          else
          {
            Item invalidCookies = Player.Inventory.Find(i =>
            {
              return itemName.ToLower() == i.Name.ToLower();
            });
            Player.Inventory.Remove(invalidCookies);
            System.Console.WriteLine("You haven't made the cookies yet!");
          }
          break;
        default:
          System.Console.WriteLine("Not a valid item, try again...");
          GetUserInput();
          break;

      }
    }

    public void UseItem(string itemName)
    {
      Item item = Player.Inventory.Find(i =>
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
        :           ; .-~""~-, /
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
          System.Console.WriteLine(@"
        
       +---------------------------------+
       |                                 |
       |             Butter              |
       |                                 |
       +---------------------------------+

        ");
          System.Console.WriteLine("You added Butter to the Mixer");
          GetUserInput();
          break;
        case "cookie mix":
          System.Console.WriteLine(@"
        +---------------+
        |               |
        |  Cookie Mix   |
        |               | 
        |   XXXX        |
        |  X    X       |
        |  X    XXXXX   |    
        |  X   XX    X  |
        |   XXX X    X  |
        |        XXXX   |
        |               |
        +---------------+

        ");
          System.Console.WriteLine("You added the Cookie Mix to the Mixer");
          GetUserInput();
          break;
        case "mixer":
          Item eggs = CurrentRoom.Items.Find(i => i.Name.ToLower() == "eggs");
          Item butter = CurrentRoom.Items.Find(i => i.Name.ToLower() == "butter");
          Item cookieMix = CurrentRoom.Items.Find(i => i.Name.ToLower() == "cookie mix");
          if (eggs == null || butter == null || cookieMix == null)
          {
            Item invalidMixer = CurrentRoom.Items.Find(i =>
            {
              return itemName.ToLower() == i.Name.ToLower();
            });
            Player.Inventory.Add(invalidMixer);
            CurrentRoom.Items.Remove(invalidMixer);
            System.Console.WriteLine("You can't use the Mixer until you have used all of the ingredients");
          }
          else
          {
            OvenPreheated = true;
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
          }
          GetUserInput();
          break;
        case "oven":
          if (!OvenPreheated)
          {
            Item invalidOven = CurrentRoom.Items.Find(i =>
            {
              return itemName.ToLower() == i.Name.ToLower();
            });
            Player.Inventory.Add(invalidOven);
            CurrentRoom.Items.Remove(invalidOven);
            System.Console.WriteLine("You can't use the Oven until you have finished mixing the ingredients");
          }
          else
          {
            System.Console.WriteLine("You placed the cookies in the Oven\nYour timer is set for 90 seconds... Take them out before the timer is up");
            Scramble();
          }
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
        }
        GetUserInput();
        return;
      }
      System.Console.WriteLine("No Items in your inventory!");
      System.Console.WriteLine("Reminder... you are in the " + CurrentRoom.Name);
      GetUserInput();
    }

    private void Scramble()
    {
      CurrentRoom = CurrentRoom.RoomPaths[Direction.diningRoom];
      System.Console.WriteLine($"You are in the {CurrentRoom.Name}... \nOh look! A Word Scramble... I Love Word Scrambles!");
      System.Console.WriteLine("Press any key to view the challenge");
      Console.ReadKey();
      SetTimer();
      Console.Clear();
      System.Console.WriteLine("Unscramble the word!");
      System.Console.WriteLine("rfw-otoyt");
      bool unscrambling = true;
      while (unscrambling)
      {
        string input = Console.ReadLine().ToLower();
        Console.Clear();
        if (input != "forty-two")
        {
          System.Console.WriteLine("Try again!\n---------");
          System.Console.WriteLine("rfw-otoyt");
          continue;
        }
        unscrambling = false;
        System.Console.WriteLine("Congratulations you solved the Word Scramble!");
        CurrentRoom = CurrentRoom.RoomPaths[Direction.kitchen];
        System.Console.WriteLine("You are back in the Kitchen and your Cookies are about to burn! Hurry and Take them out before the timer runs out!");
        GetUserInput();
      }


    }

    public void SetTimer()
    {
      // Create a timer with a two second interval.
      aTimer = new System.Timers.Timer(90000);
      // Hook up the Elapsed event for the timer. 
      aTimer.Elapsed += OnTimedEvent;
      aTimer.AutoReset = true;
      aTimer.Enabled = true;
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
      Console.Clear();
      System.Console.WriteLine("You got distracted in your word scramble and burned the cookies!!!");
      System.Console.WriteLine("Press (Q) to quit, or any other key to restart the game");
      string input = Console.ReadLine().ToLower();
      if (input == "q")
      {
        Quit();
      }
      Reset();
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
