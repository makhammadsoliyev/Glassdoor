using Glassdoor.Domain.Entities;
using Glassdoor.Presentation.Helpers;
using Glassdoor.Service.Interfaces;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace Glassdoor.Presentation.Display;

public class CompanyMenu
{
    private JobMenu jobMenu;
    private Company company;
    private readonly IJobService jobService;
    private readonly ICompanyService companyService;
    private readonly CompanyRegisterMenu companyRegisterMenu;

    public CompanyMenu(ICompanyService companyService, IJobService jobService)
    {
        this.jobService = jobService;
        this.companyService = companyService;
        this.companyRegisterMenu = new CompanyRegisterMenu(companyService);
    }

    private async Task Update()
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
            var addedCompany = await companyService.UpdateAsync(this.company.Id, company);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task Delete()
    {
        try
        {
            bool isDeleted = await companyService.DeleteAsync(company.Id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task GetById()
    {
        try
        {
            var table = new SelectionMenu().DataTable("Company", company);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            await Task.Delay(1500);
        }
    }

    private async Task GetJobs()
    {
        var jobs = (await jobService.GetAllAsync()).Where(t => t.CompanyId == company.Id);
        var table = new SelectionMenu().DataTable("Jobs", jobs.ToArray());
        AnsiConsole.Write(table);
        await Task.FromResult(jobs);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private async Task LogIn()
    {
        string phone = AnsiConsole.Ask<string>("[blue]Phone: [/]").Trim();
        string password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());

        try
        {
            company = await companyService.LogInAsync(phone, password);
            jobMenu = new JobMenu(jobService, company);

            var circle = true;
            var selectionDisplay = new SelectionMenu();

            while (circle)
            {
                AnsiConsole.Clear();
                var options = new string[] { "ShowProfile", "UpdateProfile", "DeleteProfile", "Jobs", "JobMenu", "[red]Back[/]" };
                var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", options);

                switch (selection)
                {
                    case "UpdateProfile":
                        await Update();
                        break;
                    case "DeleteProfile":
                        await Delete();
                        circle = false;
                        break;
                    case "ShowProfile":
                        await GetById();
                        break;
                    case "Jobs":
                        await GetJobs();
                        break;
                    case "JobMenu":
                        await jobMenu.Display();
                        break;
                    case "[red]Back[/]":
                        circle = false;
                        break;
                }
            }

        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    public async Task Display()
    {
        var circ = true;
        var selectionDisplay = new SelectionMenu();

        while (circ)
        {
            AnsiConsole.Clear();
            var options = new string[] { "Register", "LogIn", "[red]Back[/]" };
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", options);

            switch (selection)
            {
                case "Register":
                    await companyRegisterMenu.Display();
                    break;
                case "LogIn":
                    await LogIn();
                    break;
                case "[red]Back[/]":
                    circ = false;
                    break;
            }
        }
    }
}