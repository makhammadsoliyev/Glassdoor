using Glassdoor.Domain.Entities;
using Glassdoor.Service.Interfaces;
using Spectre.Console;
using System.Data;

namespace Glassdoor.Presentation.Display;

public class JobMenu
{
    private readonly Company company;
    private readonly IJobService jobService;
    private readonly SelectionMenu selectionMenu;

    public JobMenu(IJobService jobService, Company company)
    {
        this.company = company;
        this.jobService = jobService;
        this.selectionMenu = new SelectionMenu();
    }

    private async Task Add()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        string salaryRange = AnsiConsole.Ask<string>("[cyan1]SalaryRange: [/]");

        var job = new Job()
        {
            Name = name,
            CompanyId = company.Id,
            Description = description,
            SalaryRange = salaryRange,
        };

        try
        {
            var addedJob = await jobService.CreateAsync(job);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task Get()
    {
        var jobs = (await jobService.GetAllAsync()).Where(t => t.CompanyId == company.Id);
        var selection = selectionMenu.ShowSelectionMenu("Jobs", jobs.Select(j => $"{j.Id} {j.Name}").ToArray());
        var jobId = Convert.ToInt64(selection.Split()[0]);

        try
        {
            var job = await jobService.GetByIdAsync(jobId);
            var table = new SelectionMenu().DataTable("Job", job);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task Update()
    {
        var jobs = (await jobService.GetAllAsync()).Where(t => t.CompanyId == company.Id);
        var selection = selectionMenu.ShowSelectionMenu("Jobs", jobs.Select(j => $"{j.Id} {j.Name}").ToArray());
        var jobId = Convert.ToInt64(selection.Split()[0]);
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        string salaryRange = AnsiConsole.Ask<string>("[cyan1]SalaryRange: [/]");

        var job = new Job()
        {
            Name = name,
            Description = description,
            SalaryRange = salaryRange,
        };

        try
        {
            var updatedJob = await jobService.UpdateAsync(jobId, job);
            AnsiConsole.MarkupLine("[blue]Successfully updated...[/]");
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task Delete()
    {
        var jobs = (await jobService.GetAllAsync()).Where(t => t.CompanyId == company.Id);
        var selection = selectionMenu.ShowSelectionMenu("Jobs", jobs.Select(j => $"{j.Id} {j.Name}").ToArray());
        var jobId = Convert.ToInt64(selection.Split()[0]);

        try
        {
            bool isDeleted = await jobService.DeleteAsync(jobId);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task GetAll()
    {
        var jobs = (await jobService.GetAllAsync()).Where(t => t.CompanyId == company.Id);
        var table = new SelectionMenu().DataTable("Jobs", jobs.ToArray());
        AnsiConsole.Write(table);
        await Task.FromResult(jobs);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    public async Task Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "Get", "Update", "Delete", "GetAll", "Back" });

            switch (selection)
            {
                case "Add":
                    await Add();
                    break;
                case "Get":
                    await Get();
                    break;
                case "Update":
                    await Update();
                    break;
                case "Delete":
                    await Delete();
                    break;
                case "GetAll":
                    await GetAll();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
