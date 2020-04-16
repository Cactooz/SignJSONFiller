using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
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
				Actions();
			}

			private void Actions()
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
				else if (answer == "load" || answer == "l")
				{
					Console.Write("Are you sure you want to load file? (Yes/No): ");
					answer = Console.ReadLine().ToLower();
					if (answer == "yes" || answer == "y")
					{
						Read();
						goto start;
					}
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
			}

			private void Add()
			{
				string id = null, name = null, image = null, description = null;
				List<string> categories = new List<string>();

				while (string.IsNullOrEmpty(id))
				{
					Console.Write("ID: ");
					id = Console.ReadLine().ToUpper();
					if (string.IsNullOrEmpty(id))
						Console.WriteLine("ID can't be empty");
				}
				while (string.IsNullOrEmpty(name))
				{
					Console.Write("Name: ");
					name = Console.ReadLine();
					if (string.IsNullOrEmpty(name))
						Console.WriteLine("Name can't be empty");
				}
				while (string.IsNullOrEmpty(image))
				{
					Console.Write("Image name: ");
					image = Console.ReadLine().ToLower();
					if (string.IsNullOrEmpty(image))
						Console.WriteLine("Image name can't be empty");
				}
				while (string.IsNullOrEmpty(description))
				{
					Console.Write("Description: ");
					description = Console.ReadLine();
					if (string.IsNullOrEmpty(description))
						Console.WriteLine("Description can't be empty");
				}
				while (!categories.Any())
				{
					Console.Write("Category/Categories (Split with ,): ");
					string[] categoriesArray = Console.ReadLine().Trim(' ').Split(',');
					if (categoriesArray == null || categoriesArray.Length == 0 || categoriesArray[0] == "")
						Console.WriteLine("Category/Categories can't be empty");
					else
						categories.AddRange(categoriesArray);
				}
				Sign sign = new Sign(id, name, image, description, categories);
				signList.Add(sign);
			}

			private void Write()
			{
				File.WriteAllText(Path.Combine(filePath, fileName), JsonConvert.SerializeObject(signList));

				Console.WriteLine($"File saved as {fileName} in your Documents folder");
			}

			public void Read()
			{
				try
				{
					List<Sign> tempList = JsonConvert.DeserializeObject<List<Sign>>(File.ReadAllText(Path.Combine(filePath, fileName)));
					signList.AddRange(tempList);
				}
				catch(Exception error)
				{
					Console.WriteLine(error.Message);
				}
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
