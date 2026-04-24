using Flashcards.Database;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace Flashcards
{
    internal class UserInput
    {
        public static void Main()
        {
            var isRunningApp = true;

            var db = new Initialize();
            db.InitializeDb();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Oops! No connection string!");

            var Stacks = new Stacks(connectionString);

            while (isRunningApp)
            {
                AnsiConsole.MarkupLine("[bold green]MAIN MENU[/]");

                var choiceMainAction = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[yellow]What do you want to do?[/]")
                    .AddChoices(new[] {
                        "Exit", "Manage Stacks", "Manage FlashCards", "Start Study Session", "View Results of Study Sessions"
                    }));

                switch (choiceMainAction)
                {
                    case "Exit":
                        isRunningApp = false;
                        AnsiConsole.Clear();
                        break;
                    case "Manage Stacks":
                        AnsiConsole.MarkupLine("[bold green]STACKS MENU[/]");
                        AnsiConsole.WriteLine("Your created stacks:");
                        
                        // Here list of stack names

                        var listStacks = Stacks.ReadAllStacks(configuration);

                        if (listStacks is null)
                        {
                            AnsiConsole.MarkupLine("[bold yellow]You don't have any stacks created yet.[/]");
                        } else
                        {
                            foreach (Flashcards.Models.Stack stack in listStacks)
                            {
                                AnsiConsole.WriteLine(stack.Name);
                            }
                        }

                        var choiceStacksAction = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("[yellow]What do you want to do?[/]")
                            .AddChoices(new[] {
                                "Exit", "Add a stack", "Delete a stack", "Update the stack name"
                            }));

                        switch (choiceStacksAction)
                        {
                            case "Exit":
                                isRunningApp = false;
                                AnsiConsole.Clear();
                                break;
                            case "Add a stack":
                                var nameStack = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the new stack:"));
                                // Insert request
                                Stacks.AddStack(configuration, nameStack.ToString());
                                break;
                            case "Delete a stack":
                                var deleteStack = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Stack>()
                                    .Title("[yellow]Which stack do you want to delete?[/]")
                                    .AddChoices(listStacks));
                                // Delete request
                                Stacks.DeleteStack(configuration, deleteStack.Id);
                                break;
                            case "Update the stack name":
                                // Update request
                                var updateStack = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Stack>()
                                    .Title("[yellow]Which stack do you want to update?[/]")
                                    .AddChoices(listStacks));
                                var newNameStack = AnsiConsole.Prompt(new TextPrompt<string>("Enter the new name of the stack:"));
                                Stacks.UpdateNameStack(configuration, newNameStack.ToString(), updateStack.Id);
                                break;
                        }

                        break;
                    case "Manage FlashCards":
                        AnsiConsole.MarkupLine("[bold green]FLASHCARDS MENU[/]");
                        AnsiConsole.WriteLine("Your created flashcards:");
                        // Here list of flashcard names and their belonging to stacks

                        var choiceCardsAction = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("[yellow]What do you want to do?[/]")
                            .AddChoices(new[] {
                                "Exit", "Add a flashcard", "Delete a flashcard", "Update the flashcard", "Move the flashcard to another stack"
                            }));

                        switch (choiceCardsAction)
                        {
                            case "Exit":
                                isRunningApp = false;
                                AnsiConsole.Clear();
                                break;
                            case "Add a flashcard":
                                // Insert request
                                break;
                            case "Delete a flashcard":
                                // Delete request
                                break;
                            case "Update the flashcard":
                                // Update request
                                break;
                            case "Move the flashcard to another stack":
                                // Update request
                                break;
                        }
                        break;
                    case "Start Study Session":
                        // Call function StartStudySession()
                        break;
                    case "View Results of Study Sessions":
                        // Read request
                        break;
                }
            }
        }
    }
}
