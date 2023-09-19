using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    public class Program
    {
        static void Main(string[] args)
        {
        #region Default Values
            User User = new User("mahmoud", "naseri");
            Tasks NewTask = null;
            List<Tasks> savedTasks = null;
        #endregion

        #region Login
        Login:
            Time.PrintDate();
            Console.WriteLine("\t* Login page *\n\n");
            Console.Write("Enter your username: ");
            string user = Console.ReadLine().ToLower().Trim();
            Console.Write("Enter your password: ");
            string pass = Console.ReadLine().Trim();
            string savedPass = User.LoadPass();
            if(savedPass != null)
            {
                User.Password = savedPass;
            }
            if (user == User.User_name && pass == User.Password)
            {
                Time.PrintDate();
                Console.WriteLine($"Welcome {User.User_name}!\n");
                Thread.Sleep(1000);
                Console.WriteLine("Logging you in . . .");
                Thread.Sleep(1000);
                goto Menu;
            }
            else
            {
                Time.PrintDate();
                Console.WriteLine("Invalid Username or Password!\n");
                Thread.Sleep(1000);
                Console.WriteLine("Please try again.");
                Thread.Sleep(1000);
                goto Login;
            }
        #endregion

        #region UserMainMenu
        Menu:
            Time.PrintDate();
            Console.WriteLine("1. Create a new Task\n2. display today's tasks\n3. display all tasks\n4. Display tasks sorted by category\n5. Search for a task\n6. Delete a task\n7. Settings\n8. Log out\n");
            Console.Write("Your option is: ");
            int option;
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Time.PrintDate();
                Console.WriteLine($"Error: {ex.Message}\n");
                Thread.Sleep(1000);
                Console.WriteLine("Please try again.");
                Thread.Sleep(1000);
                goto Menu;
            }
            switch (option)
            {
                case 1:
                    #region Creation of a new Task
                    Time.PrintDate();
                    Console.Write("What is your task related to(work, personal, study, sports, etc...): ");
                    string tag = Console.ReadLine().Trim();
                    Console.Write("Enter a description for your task: ");
                    string desc = Console.ReadLine().Trim();
                    Console.Write("Enter the date: ");
                    string dateString = Console.ReadLine();

                    DateTime date;
                    if (DateTime.TryParseExact(dateString, "MMM d, yyyy", null, System.Globalization.DateTimeStyles.None, out date))
                    {
                        DateTime dateOnly = date.Date;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                    }

                    Console.Write("Enter the time (optional): ");
                    string timeString = Console.ReadLine();

                    DateTime? time = null;
                    if (!string.IsNullOrWhiteSpace(timeString))
                    {
                        DateTime tempTime;
                        if (DateTime.TryParseExact(timeString, "H:mm", null, System.Globalization.DateTimeStyles.None, out tempTime))
                        {
                            time = tempTime;
                        }
                    }

                    Console.Write("Is your task completed? (yes/no): ");
                    string isDone = Console.ReadLine().Trim().ToLower();

                    bool done;
                    if (isDone == "yes")
                    {
                        done = true;
                    }
                    else if (isDone == "no")
                    {
                        done = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Task status set to 'not done' by default.");
                        done = false;
                    }

                    NewTask = new Tasks(tag, desc, date, time, done);
                    savedTasks.Add(NewTask);
                    Tasks.SaveTasks(savedTasks);
                    Console.WriteLine("\nTask added successfully!\n");
                    Thread.Sleep(1000);
                    Console.WriteLine("\tReturning to Main Menu...");
                    Thread.Sleep(1000);
                    goto Menu;
                #endregion
                case 2:
                    #region Displaying Today's Tasks
                    savedTasks = Tasks.RestoreTasks();
                    Time.PrintDate();
                    if(savedTasks != null)
                    {
                        Tasks.PrintToday(savedTasks);
                    }
                    Console.WriteLine("\nPress any key to return to Main Menu . . .");
                    Console.ReadKey();
                    goto Menu;
                #endregion
                case 3:
                    #region Displaying All Tasks
                    savedTasks = Tasks.RestoreTasks();
                    if (savedTasks.Count == 0)
                    {
                        Time.PrintDate();
                        Console.WriteLine("\nThere are currently no tasks available for you.\n\n\tPress any key to return to Main Menu . . .");
                        Console.ReadKey();
                        goto Menu;
                    }
                    else
                    {
                        Time.PrintDate();
                        foreach (var tasks in savedTasks)
                        {
                            tasks.Print();
                        }
                        Console.WriteLine("\nPress any key to return to Main Menu . . .");
                        Console.ReadKey();
                        goto Menu;
                        
                    }
                #endregion
                case 4:
                    #region Displaying Grouped Tasks
                    savedTasks = Tasks.RestoreTasks();
                    Time.PrintDate();
                    Tasks.PrintByGroup(savedTasks);
                    Console.WriteLine("Press any key to return to Main Menu . . .");
                    Console.ReadKey();
                    goto Menu;
                #endregion
                case 5:
                    #region Searching For a Task
                    Time.PrintDate();
                    Console.Write("Enter the tag, date or an ID of a task: ");
                    string search = Console.ReadLine().Trim();
                    List<Tasks> matchingTasks = Tasks.Search(savedTasks, search);

                    if (matchingTasks.Count == 0)
                    {
                        Console.WriteLine("No tasks found.");
                    }
                    else
                    {
                        foreach (var tasks in matchingTasks)
                        {
                            tasks.Print();
                        }
                    }

                    Console.WriteLine("\nPress any key to return to Main Menu . . .");
                    Console.ReadKey();
                    goto Menu;
                    #endregion
                case 6:
                    #region Deleting a Task
                    Time.PrintDate();
                    Console.Write("Enter the ID(s): ");
                    string[] searchInput = Console.ReadLine().Split();
                    Tasks.Delete(savedTasks, searchInput);
                    Console.WriteLine("\nPress any key to return to Main Menu . . .");
                    Console.ReadKey();
                    goto Menu;
                #endregion
                case 7:
                    #region UserSettings
                Settings:
                    Time.PrintDate();
                    Console.WriteLine("1. Change console background color\n2. Change password\n3. Back\n");
                    Console.Write("Your option is: ");
                    int settingsOption;
                    try
                    {
                        settingsOption = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Time.PrintDate();
                        Console.WriteLine($"Error: {ex.Message}\n");
                        Thread.Sleep(1000);
                        Console.WriteLine("Please try again.");
                        Thread.Sleep(1000);
                        goto case 7;
                    }
                    switch (settingsOption)
                    {
                        case 1:
                            #region Console Color
                        ConsoleColor:
                            Time.PrintDate();
                            Console.WriteLine("Choose one of the following colors:\n\n");
                            Console.WriteLine("1. Dark Red");
                            Console.WriteLine("2. Blue");
                            Console.WriteLine("3. Yellow");
                            Console.WriteLine("4. Magenta");
                            Console.WriteLine("5. Green");
                            Console.WriteLine("6. Black\n");
                            Console.Write("Your option is : ");
                            int colorOption;
                            while (!int.TryParse(Console.ReadLine(), out colorOption))
                            {
                                Time.PrintDate();
                                Console.WriteLine("Please only choose from the displayed options.");
                                Thread.Sleep(1000);
                                goto ConsoleColor;
                            }
                            switch (colorOption)
                            {
                                case 1:
                                    Time.PrintDate();
                                    Console.WriteLine("\n\tChanging color ...");
                                    Thread.Sleep(1500);
                                    Console.BackgroundColor = ConsoleColor.DarkRed;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto Settings;
                                case 2:
                                    Time.PrintDate();
                                    Console.WriteLine("\n\tChanging color ...");
                                    Thread.Sleep(1500);
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    goto Settings;
                                case 3:
                                    Time.PrintDate();
                                    Console.WriteLine("\n\tChanging color ...");
                                    Thread.Sleep(1500);
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    goto Settings;
                                case 4:
                                    Time.PrintDate();
                                    Console.WriteLine("\n\tChanging color ...");
                                    Thread.Sleep(1500);
                                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto Settings;
                                case 5:
                                    Time.PrintDate();
                                    Console.WriteLine("\n\tChanging color ...");
                                    Thread.Sleep(1500);
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto Settings;
                                case 6:
                                    Time.PrintDate();
                                    Console.WriteLine("\n\tChanging color ...");
                                    Thread.Sleep(1500);
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto Settings;
                                default:
                                    Time.PrintDate();
                                    Console.WriteLine("Please only choose from the options available.");
                                    Thread.Sleep(2000);
                                    goto ConsoleColor;
                            }
                        #endregion
                        case 2:
                            #region Password Change
                            Time.PrintDate();
                            Console.Write("Enter new Password: ");
                            string newPass1 = Console.ReadLine().Trim();
                            Console.Write("Re-Enter new Password: ");
                            string newPass2 = Console.ReadLine().Trim();
                            if (newPass1 == newPass2)
                            {
                                Time.PrintDate();
                                User.NewPassword(newPass1);
                                User.SavePass(newPass1);
                                Console.WriteLine("\nPassword changed successfully!");
                                Thread.Sleep(1000);
                                Console.Write("\nPlease re-Login with your new password.");
                                Thread.Sleep(1000);
                                
                                goto Login;
                            }
                            else
                            {
                                Time.PrintDate();
                                Console.WriteLine("\nPasswords dont match!");
                                Thread.Sleep(1000);
                                Console.WriteLine("\nPlease try again.");
                                Thread.Sleep(1000);
                                goto case 2;
                            }
                        #endregion
                        case 3:
                            goto Menu;
                    }
                    break;
                #endregion
                case 8:
                    #region Log out
                    Time.PrintDate();
                    Console.WriteLine($"Goodbye {User.User_name}! see you soon :)\n");
                    Thread.Sleep(1000);
                    Console.WriteLine("\tLogging you out . . .");
                    Thread.Sleep(1000);
                    goto Login;
                    #endregion
            }

            #endregion
        }
    }
}
