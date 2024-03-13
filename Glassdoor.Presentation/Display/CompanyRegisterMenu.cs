using Glassdoor.Domain.Entities;
using Glassdoor.Presentation.Helpers;
using Glassdoor.Service.Interfaces;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace Glassdoor.Presentation.Display;

public class CompanyRegisterMenu
{
    private readonly ICompanyService companyService;

    public CompanyRegisterMenu(ICompanyService companyService)
    {
        this.companyService = companyService;
    }

    public async Task Display()
    {
        string name = AnsiConsole.Ask<string>("[cyan2]Name: [/]");
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

        var company = new Company()
        {
            Name = name,
            Phone = phone,
            Password = password,
            Address = location.Formatted
        };

        try
        {
            var addedCompany = await companyService.RegisterAsync(company);
            AnsiConsole.MarkupLine("[green]Successfully registered...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }
}
