using Flashcards.Database;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

// TODO: ADD LOOP FOR SUBMENU

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

            var Cards = new Cards(connectionString);

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

                        var listStacks = Stacks.ReadAllStacks();

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
                                Stacks.AddStack(nameStack.ToString());
                                break;
                            case "Delete a stack":
                                var deleteStack = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Stack>()
                                    .Title("[yellow]Which stack do you want to delete?[/]")
                                    .AddChoices(listStacks));
                                // Delete request
                                Stacks.DeleteStack(deleteStack.Id);
                                break;
                            case "Update the stack name":
                                // Update request
                                var updateStack = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Stack>()
                                    .Title("[yellow]Which stack do you want to update?[/]")
                                    .AddChoices(listStacks));
                                var newNameStack = AnsiConsole.Prompt(new TextPrompt<string>("Enter the new name of the stack:"));
                                Stacks.UpdateNameStack(newNameStack.ToString(), updateStack.Id);
                                break;
                        }

                        break;
                    case "Manage FlashCards":
                        // TODO: ADD LINKED STACK FOR FLASHCARD
                        AnsiConsole.MarkupLine("[bold green]FLASHCARDS MENU[/]");
                        AnsiConsole.WriteLine("Your created flashcards:");

                        var listCards = Cards.ReadAllCards();

                        if (listCards == null)
                        {
                            AnsiConsole.MarkupLine("[bold red]You don't have any flashcards created yet.[/]");
                        }

                        foreach (Flashcards.Models.Card card in listCards)
                        {
                            AnsiConsole.WriteLine(card.Title);
                        }
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
                                listStacks = Stacks.ReadAllStacks();

                                if (listStacks is null)
                                {
                                    AnsiConsole.MarkupLine("[bold yellow]You don't have any stacks created yet. To add a flashcard, create a stack[/]");
                                }

                                var choiceStack = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Stack>()
                                    .Title("[yellow]Which stack do you want to add the flashcard to?[/]")
                                    .AddChoices(listStacks));
                                var titleCard = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the flashcard:"));
                                var descriptionCard = AnsiConsole.Prompt(new TextPrompt<string>("Enter the description of the flashcard:"));
                                Cards.AddCard(choiceStack.Id, titleCard, descriptionCard);
                                break;
                            case "Delete a flashcard":
                                // Delete request

                                if (listCards is null)
                                {
                                    AnsiConsole.MarkupLine("[bold red]You don't have any flashcards created yet.[/]");
                                    break;
                                }

                                var choiceDeleteCard = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Card>()
                                    .Title("[yellow]Which flashcard do you want to delete?[/]")
                                    .AddChoices(listCards));

                                Cards.DeleteCard(choiceDeleteCard.Id);

                                break;
                            case "Update the flashcard":
                                // Update request

                                if (listCards is null)
                                {
                                    AnsiConsole.MarkupLine("[bold red]You don't have any maps created yet.[/]");
                                    break;
                                }

                                var choiceUpdateCard = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Card>()
                                    .Title("[yellow]Which flashcard do you want to update?[/]")
                                    .AddChoices(listCards));

                                var choiceUpdateField = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("[yellow]Which field do you want to update?[/]")
                                    .AddChoices(new[] {
                                        "Title", "Description"
                                    }));

                                if (choiceUpdateField == "Title")
                                {
                                    var newTitle = AnsiConsole.Prompt(new TextPrompt<string>("Enter a new flashcard title:"));
                                    Cards.UpdateFlashcardInfo(choiceUpdateCard.Id, newTitle, choiceUpdateCard.Description);
                                } else
                                {
                                    var newDescription = AnsiConsole.Prompt(new TextPrompt<string>("Enter a new flashcard description"));
                                    Cards.UpdateFlashcardInfo(choiceUpdateCard.Id, choiceUpdateCard.Title, newDescription);
                                }

                                break;

                            case "Move the flashcard to another stack":
                                // Update request

                                if (listCards is null)
                                {
                                    AnsiConsole.MarkupLine("[bold red]You don't have any maps created yet.[/]");
                                    break;
                                }

                                listStacks = Stacks.ReadAllStacks();

                                var choiceUpdateIdStackCard = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Card>()
                                    .Title("[yellow]Which flashcard do you want to delete?[/]")
                                    .AddChoices(listCards));

                                var choiceStackForMove = AnsiConsole.Prompt(
                                    new SelectionPrompt<Flashcards.Models.Stack>()
                                    .Title("[yellow]Which stack do you want to transfer the flashcard to?[/]")
                                    .AddChoices(listStacks));

                                Cards.UpdateStackId(choiceUpdateIdStackCard.Id, choiceStackForMove.Id);

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
