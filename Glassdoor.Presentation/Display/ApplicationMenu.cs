using Glassdoor.Domain.Entities;
using Glassdoor.Service.Interfaces;
using Spectre.Console;

namespace Glassdoor.Presentation.Display;

public class ApplicationMenu
{

    private readonly User user;
    private readonly IJobService jobService;
    private readonly SelectionMenu selectionMenu;
    private readonly IApplicationService applicationService;

    public ApplicationMenu(IJobService jobService, IApplicationService applicationService, User user)
    {
        this.user = user;
        this.jobService = jobService;
        this.selectionMenu = new SelectionMenu();
        this.applicationService = applicationService;
    }

    private async Task Add()
    {
        var jobs = await jobService.GetAllAsync();
        var selection = selectionMenu.ShowSelectionMenu("Jobs", jobs.Select(j => $"{j.Id} {j.Name}, {j.SalaryRange}").ToArray());
        var jobId = Convert.ToInt64(selection.Split()[0]);

        var application = new Application()
        {
            JobId = jobId,
            UserId = user.Id,
            Time = DateTime.UtcNow,
        };

        try
        {
            var addedApplication = await applicationService.CreateAsync(application);
            AnsiConsole.MarkupLine("[blue]Application...[/]");
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task Delete()
    {
        var applications = (await applicationService.GetAllAsync()).Where(t => t.UserId == user.Id);
        var selection = selectionMenu.ShowSelectionMenu("Applications", applications.Select(a => $"{a.Id} {a.Job.Name}, {a.Time}, {a.Status}").ToArray());
        var applicationId = Convert.ToInt64(selection.Split()[0]);

        try
        {
            bool isDeleted = await applicationService.DeleteAsync(applicationId);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task Get()
    {
        var applications = (await applicationService.GetAllAsync()).Where(t => t.UserId == user.Id);
        var selection = selectionMenu.ShowSelectionMenu("Applications", applications.Select(a => $"{a.Id} {a.Job.Name}, {a.Time}, {a.Status}").ToArray());
        var applicationId = Convert.ToInt64(selection.Split()[0]);

        try
        {
            var application = await applicationService.GetByIdAsyncAsync(applicationId);
            var table = new SelectionMenu().DataTable("Applications", application);
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

    private async Task GetAll()
    {

        try
        {
            var applications = (await applicationService.GetAllAsync()).Where(t => t.UserId == user.Id);
            var table = new SelectionMenu().DataTable("Applications", applications.ToArray());
            await Task.FromResult(table);
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

    public async Task Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "Get", "Delete", "GetAll", "Back" });

            switch (selection)
            {
                case "Add":
                    await Add();
                    break;
                case "Get":
                    await Get();
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
