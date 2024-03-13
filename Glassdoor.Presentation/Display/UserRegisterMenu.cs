using Glassdoor.Domain.Entities;
using Glassdoor.Presentation.Helpers;
using Glassdoor.Service.Interfaces;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace Glassdoor.Presentation.Display;

public class UserRegisterMenu
{
    private readonly IUserService userService;

    public UserRegisterMenu(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task Display()
    {
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone: [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone: [/]");
        }
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your password:").Secret());
        while (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your password:").Secret());
        }
    key:
        var address = AnsiConsole.Ask<string>("[cyan3]Address: [/]").Trim();
        var locations = await GeoLocation.GetLocations(address);
        var names = await GeoLocation.GetLocationsNames(locations);
        names.Add("Change address");

        var selectionDisplay = new SelectionMenu();
        var selection = selectionDisplay.ShowSelectionMenu("Choose one of locations", names.ToArray());
        Location location;

        if (selection.Equals("Change address"))
            goto key;
        else
            location = await GeoLocation.GetLocationByName(locations, selection);

        var user = new User()
        {
            Phone = phone,
            Password = password,
            LastName = lastName,
            FirstName = firstName,
            Address = location.Formatted
        };

        try
        {
            var addedUser = await userService.RegisterAsync(user);
            AnsiConsole.MarkupLine("[green]Successfully registered...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }
}