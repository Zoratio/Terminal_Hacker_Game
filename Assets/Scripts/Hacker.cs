using UnityEngine;

public class Hacker : MonoBehaviour
{
    //current level that the player is on
    int level;

    //different screens of the game which have different typing actions
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    //random password that the player is currently on
    int curPasswordIndex;

    //3 difficulties, each with 5 passwords
    string[,] passwords = new string[3,5] { 
                                            { "book", "page", "font", "title", "paper" }, 
                                            { "police", "crime", "uniform", "holster", "arrest" }, 
                                            { "ammunition", "commander", "camouflage", "fortification", "headquarters" }
                                          };



    //Game setup
    void Start()
    {        
        ShowMainMenu(); //calling the method that will display the starting screen
    }


    //Game setup
    void ShowMainMenu()
    {
        //Various options of what difficulty the player wants to select
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine(@"What would you like to hack into?

Type 1 for the local library system
Type 2 for the police station system
Type 3 for the military base system

Type the command 'menu' to return to   the menu or 'quit' to close the game

Enter your selection:");
    }


    //Manages what will happen with the users input
    void OnUserInput (string input) 
    {   //I have put the menu command check only in the main menu, I will also need to do it in the other 2 screens too.
        if (currentScreen.Equals(Screen.MainMenu))
        {
            RunMainMenu(input);
        }   
        else if (currentScreen.Equals(Screen.Password))
        {
            CheckPassword(input);
        }
        else
        {
            if (input.Equals("menu"))
            {
                ShowMainMenu();
            }
            else if (input.Equals("quit"))
            {
                print("Quit");
                Application.Quit();
            }
            else
            {
                Terminal.WriteLine("Unknown Command");
            }
        }
    }

    
    //Input action for the Menu screen
    void RunMainMenu(string input)
    {
        if (int.TryParse(input, out int type))
        {
            switch (input)
            {
                case "1":
                case "2":
                case "3":
                    level = int.Parse(input);
                    AskForPassword();
                    break;
                case "007": //easter egg
                    Terminal.WriteLine("Greetings Mr.Bond, please select a     valid level!");
                    break;
                default:
                    Terminal.WriteLine("Unknown Level");
                    break;
            }          
        }
        else
        {
            switch (input)
            { 
                case "menu":
                    ShowMainMenu();
                    break;
                case "quit":
                    print("Quit");
                    Application.Quit();
                    break;
                default:
                    Terminal.WriteLine("Unknown Command");
                    break;
            }
        }        
    }


    //Password guess information
    void AskForPassword()
    {
        currentScreen = Screen.Password;
        RandomPasswordSelection();
        Terminal.ClearScreen();
        Terminal.WriteLine("Level " + level + " has been chosen");
        Terminal.WriteLine("Commands available - menu & quit");        
        Terminal.WriteLine("Enter your password, hint: " + passwords[(level - 1), curPasswordIndex].Anagram());
    }


    //Picks a password at random from that level's options
    void RandomPasswordSelection()
    {
        curPasswordIndex = Random.Range(0, passwords.GetLength(1));
        print("Dev note - the password is " + passwords[(level - 1), curPasswordIndex]);
    }


    //Checks to see if the string matches the password's
    void CheckPassword(string input)
    {
        if (input.Equals("menu"))
        {
            ShowMainMenu();
        }
        else if (input.Equals("quit"))
        {
            print("Quit");
            Application.Quit();
        }
        else if (input.Equals(passwords[(level -1), curPasswordIndex]))
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }


    //Changes to the win screen
    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }


    //Displays the correct ascii art
    void ShowLevelReward()
    {        
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
     _________
    (\        \
    \ \        \
     \ \________\
      \(_________| 

"
                );
                Terminal.WriteLine("Commands available - menu & quit:");
                break;
            case 2:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine(@"
     __
    /O \________
    \__/-='-='-='

"
                );
                Terminal.WriteLine("Commands available - menu & quit:");
                break;
            case 3:
                Terminal.WriteLine("You got the launch button!");
                Terminal.WriteLine(@"
       ______    
      /      \___
      \______/| /|
     / |____|/ //
    /_________//
    |________|/

"
                );
                Terminal.WriteLine("Commands available - menu & quit:");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }        
    }
}