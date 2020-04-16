using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SignJSONFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            app.Start();
        }
        class App
        {
            List<Sign> signList = new List<Sign>();
            public void Start()
            {
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
                else if (answer == "write" || answer == "w" || answer == "print")
                {
                    Write();
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
                    Add();
                    goto start;
                }
                Console.Write(JsonConvert.SerializeObject(signList));
            }

            public void Add()
            {
                Console.Write("Id: ");
                string id = Console.ReadLine();
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Image: ");
                string image = Console.ReadLine();
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Category: , ");
                List<string> categories = new List<string>(Console.ReadLine().Split(','));
                Sign sign = new Sign(id, name, image, description, categories);
                signList.Add(sign);
            }
            
            public void Write()
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fileName = "sign.json";

                File.WriteAllText(Path.Combine(filePath, fileName), JsonConvert.SerializeObject(signList));

                Console.WriteLine($"File saved as {fileName} in your Documents folder");
            }
        }

        [System.Serializable]
        public class Sign
        {
            public string id;
            public string name;
            public string imageName;
            public string description;
            public List<string> categories = new List<string>();

            public Sign(string id, string name, string imageName, string description, List<string> categories)
            {
                this.id = id;
                this.name = name;
                this.imageName = imageName;
                this.description = description;
                this.categories = categories;
            }

            public Sign(string id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }
    }
}
