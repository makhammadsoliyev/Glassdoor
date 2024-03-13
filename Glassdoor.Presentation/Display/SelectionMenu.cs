using Glassdoor.Domain.Entities;
using Spectre.Console;

namespace Glassdoor.Presentation.Display;

public class SelectionMenu
{
    public Table DataTable(string title, params Application[] applications)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("User");
        table.AddColumn("Job");
        table.AddColumn("Status");

        foreach (var application in applications)
            table.AddRow(application.Id.ToString(), $"{application.User.FirstName} {application.User.LastName}", application.Job?.Name, application.Status.ToString());

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public Table DataTable(string title, params Company[] companies)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Phone");
        table.AddColumn("Address");

        foreach (var company in companies)
            table.AddRow(company.Id.ToString(), company.Name, company.Phone, company.Address);

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public Table DataTable(string title, params User[] users)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Phone");
        table.AddColumn("Address");

        foreach (var user in users)
            table.AddRow(user.Id.ToString(), user.FirstName, user.LastName, user.Phone, user.Address);

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public Table DataTable(string title, params Job[] jobs)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Description");
        table.AddColumn("SalaryRange");
        table.AddColumn("Status");
        table.AddColumn("Company");

        foreach (var job in jobs)
            table.AddRow(job.Id.ToString(), job.Name, job.Description, job.SalaryRange, job.Status.ToString(), job.Company.Name);

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(7) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }
}
