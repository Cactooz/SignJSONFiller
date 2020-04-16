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
			Console.Write($"Start time: {DateTime.Now}");
			App app = new App();
			app.Start();
		}
		class App
		{
			string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string fileName = "Data.json";
			List<Sign> signList = new List<Sign>();
			public void Start()
			{
				start:

				Console.Write("\nAction (Add/Write/Load/Exit/Error/Help): ");
				string answer = Console.ReadLine().ToLower();

				if (answer == "exit" || answer == "e" || answer == "esc" || answer == "end")
				{
					Console.Write("Are you sure you want to exit? (Yes/No): ");
					answer = Console.ReadLine().ToLower();
					if (answer == "yes" || answer == "y")
						Environment.Exit(1);
					else
						goto start;
				}
				else if (answer == "write" || answer == "w" || answer == "print" || answer == "p")
				{
					Console.Write("Are you sure you want to write (this will overwrite the existing file)? (Yes/No): ");
					answer = Console.ReadLine().ToLower();
					if (answer == "yes" || answer == "y")
						Write();
					else
						goto start;

					Console.Write("Do you want to exit? (Yes/No): ");
					answer = Console.ReadLine().ToLower();
					if (answer == "yes" || answer == "y")
						Environment.Exit(2);
					else
						goto start;
				}
				else if(answer == "load" || answer == "l")
				{
					Console.Write("Are you sure you want to load file? (Yes/No): ");
					answer = Console.ReadLine().ToLower();
					if (answer == "yes" || answer == "y")
						Read();
					else
						goto start;
				}
				else if (answer == "error" || answer == "err")
				{
					Console.WriteLine("=== Exit codes ===");
					Console.WriteLine("0: Normal program end");
					Console.WriteLine("1: Exit command");
					Console.WriteLine("2: Conent written to file & ended");

					goto start;
				}
				else if (answer == "help" || answer == "h")
				{
					Console.WriteLine("=== Help ===");
					Console.WriteLine("Add: add more signs (Alias: enter)");
					Console.WriteLine("Write: write content to file (Alias: w, print, p)");
					Console.WriteLine("Load: load content from file (Alias: l)");
					Console.WriteLine("Exit: stop the program (NOTE: file wont be saved) (Alias: e,esc,end)");
					Console.WriteLine("Error: exit codes (Alias: err)");
					Console.WriteLine("Help: this command (Alias: h)");

					goto start;
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
				Console.Write("ID: ");
				string id = Console.ReadLine().ToUpper();
				Console.Write("Name: ");
				string name = Console.ReadLine();
				Console.Write("Image: ");
				string image = Console.ReadLine().ToLower();
				Console.Write("Description: ");
				string description = Console.ReadLine();
				Console.Write("Category (Split with ,): ");
				List<string> categories = new List<string>(Console.ReadLine().Split(','));
				Sign sign = new Sign(id, name, image, description, categories);
				signList.Add(sign);
			}
			
			public void Write()
			{
				File.WriteAllText(Path.Combine(filePath, fileName), JsonConvert.SerializeObject(signList));

				Console.WriteLine($"File saved as {fileName} in your Documents folder");
			}

			public void Read()
			{
				List<Sign> tempList = JsonConvert.DeserializeObject<List<Sign>>(File.ReadAllText(Path.Combine(filePath, fileName)));
				signList.AddRange(tempList);
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
