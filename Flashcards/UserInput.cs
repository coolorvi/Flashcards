using Spectre.Console;

namespace Flashcards
{
    internal class UserInput
    {
        public static void Main()
        {
            var isRunningApp = true;
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
                                var nameStack = new TextPrompt<string>("Enter the name of the new stack:");
                                // Insert request
                                break;
                            case "Delete a stack":
                                // Delete request
                                break;
                            case "Update the stack name":
                                // Update request
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
