namespace StickyNotes;
class Program
{
    static void Main(string[] args) {
        Console.WriteLine("-----------------------\nWelcome to StickyNotes!\n-----------------------");
        displayMenu();
        Console.Write("\nGoodbye! (Press any key to exit)");
        Console.ReadKey();
    }

    public static void displayMenu() {
        string input = "";

        while(!input.Equals("Exit")) {
            Console.WriteLine("\n----------------------MAIN----------------------");
            Console.WriteLine("'Manage' - View and manage all of your Stickys");
            Console.WriteLine("'Create' - Create a new Sticky");
            Console.WriteLine("'Dump' - Erase ALL of your Stickys...PERMANENTLY!");
            Console.WriteLine("'Exit' - Close the console application");
            Console.WriteLine("------------------------------------------------\n");

            Console.Write("Input the course of action: ");
            input = Console.ReadLine();

            if(input.Equals("Manage")) {
                manageStickys();
            } else if(input.Equals("Create")) {
                createSticky();
            } else if(input.Equals("Dump")) {
                Console.Write("Are you sure? This cannot be undone(Y/N): ");
                input = Console.ReadLine();

                if(input.Equals("Y")) {
                    destroy();
                } else {
                    Console.WriteLine("Stickys have not been deleted");
                }
            } else if(input.Equals("Exit")) {
                break;
            } else {
                Console.WriteLine("INVALID INPUT");
            }
        }
    }

    public static void createSticky() {
        Console.Write("Please enter the name of the stickynote without the file extension: ");
        string filename = Console.ReadLine();
        Console.Write("Write your note: ");
        string content = Console.ReadLine();
        try {
            StreamWriter sw = new StreamWriter("stickys/" + filename + ".txt");
            sw.Write(content);
            sw.Close();
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        } finally {
            Console.WriteLine("Sticky created!");
        }
    }

    public static void destroy() {
        foreach(string file in Directory.GetFiles("stickys/")) {
            File.Delete(file);
        }
        Console.WriteLine("All Stickys deleted");
    }

    //Manage Menu
    public static void manageStickys() {
        string input = "";

        while(!input.Equals("Back")) {
            Console.WriteLine("\n---------------MANAGE---------------");
            Console.WriteLine("'View' - Lists all Stickys");
            Console.WriteLine("'Read' - Read the contents of a Sticky");
            Console.WriteLine("'Create' - Create a new Sticky");
            Console.WriteLine("'Delete' - Delete a specified Sticky");
            Console.WriteLine("'Back' - Go back to the main menu");
            Console.WriteLine("------------------------------------\n");

            Console.Write("Input the course of action: ");
            input = Console.ReadLine();

            if(input.Equals("View")) {
                viewStickys();
            } else if(input.Equals("Read")) {
                Console.Write("Please enter the name of the Sticky without the file extension: ");
                string name = Console.ReadLine();
                readSticky(name);
            } else if(input.Equals("Create")) {
                createSticky();
            } else if(input.Equals("Delete")) {
                Console.Write("Please enter the name of the Sticky without the file extension: ");
                string name = Console.ReadLine();
                deleteSticky(name);
            } else if(input.Equals("Back")) {
                break;
            } else {
                Console.WriteLine("INVALID INPUT");
            }
        }
    }

    public static void viewStickys() {
        Console.WriteLine("\n-----YOUR STICKYS-----");
        foreach(string file in Directory.GetFiles("stickys/")) {
            Console.WriteLine(file.Substring(8));
        }
        Console.WriteLine("----------------------\n");
    }

    public static void readSticky(string fileName) {
        string line = "";
        try {
            StreamReader sr = new StreamReader("stickys/" + fileName + ".txt");
            line = sr.ReadLine();

            while(line != null) {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }

            sr.Close();
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public static void deleteSticky(string name) {
        try {
            File.Delete("stickys/" + name + ".txt");
        } catch (FileNotFoundException) {
            Console.WriteLine("File not found");
        } finally {
            Console.WriteLine("File deleted successfully");
        }
    }
}
