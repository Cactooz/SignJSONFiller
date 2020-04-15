using System;
using System.IO;
using System.Collections.Generic;

namespace SignJSONFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> signItems = new List<string>();

            start:

            Console.Write("\nAction (Add/Write/Exit/Error): ");
            string answer = Console.ReadLine().ToLower();

            if (answer == "exit" || answer == "e" || answer == "esc")
            {
                Console.Write("Are you sure you want to exit? (Yes/No): ");
                answer = Console.ReadLine().ToLower();
                if (answer == "yes" || answer == "y")
                    Environment.Exit(1);
                else
                    goto start;
            }
            else if (answer == "write" || answer == "w")
            {
                Write(signItems);
                Console.Write("Do you want to exit? (Yes/No): ");
                answer = Console.ReadLine().ToLower();
                if (answer == "yes" || answer == "y")
                    Environment.Exit(2);
                else
                    goto start;
            }
            else if (answer == "error" || answer == "err")
            {
                Console.WriteLine("Error codes:");
                Console.WriteLine("0: Normal program end");
                Console.WriteLine("1: Exit command");
                Console.WriteLine("2: Conent written to file & ended");
            }
            else
            {
                Add(signItems);
                goto start;
            }
        }

        static void Add(List<string> signsList)
        {
            Console.Write("\nID: ");
            signsList.Add(Console.ReadLine());
            Console.Write("Name: ");
            signsList.Add(Console.ReadLine());
            Console.Write("Image: ");
            signsList.Add(Console.ReadLine());
            Console.Write("Description: ");
            signsList.Add(Console.ReadLine());
            Console.Write("Categories (For multiple, do \", \" (including the quotation marks) bewteen them): ");
            signsList.Add(Console.ReadLine());
        }
        static void Write(List<string> signsList)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = "signJSON.txt";

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, fileName), true))
            {
                outputFile.WriteLine("============");
                outputFile.WriteLine($"Written: {DateTime.Now}");
                outputFile.WriteLine("============");

                for (int i = 0; i < signsList.Count; i+=5)
                {
                    outputFile.WriteLine("{");
                    outputFile.WriteLine($"\"id\": \"{signsList[i]}\",");
                    outputFile.WriteLine($"\"name\": \"{signsList[i+1]}\",");
                    outputFile.WriteLine($"\"imageName\": \"{signsList[i+2]}\",");
                    outputFile.WriteLine($"\"description\": \"{signsList[i+3]}\",");
                    outputFile.WriteLine($"\"categories\": [ \"{signsList[i+4]}\" ]");
                    outputFile.WriteLine("},");
                }
            }
            Console.WriteLine($"File saved as {fileName} in your Documents folder");
        }
    }
}
