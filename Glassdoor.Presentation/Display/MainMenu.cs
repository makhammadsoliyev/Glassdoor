using Glassdoor.DataAccess.Contexts;
using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Service.Interfaces;
using Glassdoor.Service.Services;
using Spectre.Console;

namespace Glassdoor.Presentation.Display;

public class MainMenu
{
    private readonly GlassdoorDbContext context;

    private readonly UserMenu userMenu;
    private readonly CompanyMenu companyMenu;

    private readonly IJobService jobService;
    private readonly IUserService userService;
    private readonly ICompanyService companyService;
    private readonly IApplicationService applicationService;

    private readonly IRepository<Job> jobRepository;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Company> companyRepository;
    private readonly IRepository<Application> applicationRepository;

    public MainMenu()
    {
        context = new GlassdoorDbContext();

        jobRepository = new Repository<Job>(context);
        userRepository = new Repository<User>(context);
        companyRepository = new Repository<Company>(context);
        applicationRepository = new Repository<Application>(context);

        jobService = new JobService(jobRepository);
        userService = new UserService(userRepository);
        companyService = new CompanyService(companyRepository);
        applicationService = new ApplicationService(applicationRepository, jobService);

        companyMenu = new CompanyMenu(companyService, jobService);
        userMenu = new UserMenu(userService, applicationService, jobService);
    }

    public async Task Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var options = new string[] { "Company", "User", "[red]Exit[/]" };
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", options);

            switch (selection)
            {
                case "Company":
                    await companyMenu.Display();
                    break;
                case "User":
                    await userMenu.Display();
                    break;
                case "[red]Exit[/]":
                    circle = false;
                    break;
            }
        }
    }
}
