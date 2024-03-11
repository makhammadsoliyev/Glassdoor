using Glassdoor.DataAccess.Contexts;
using Glassdoor.DataAccess.Repositories;
using Glassdoor.Domain.Entities;
using Glassdoor.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Service.Services;

public class ResumeService : IResume
{
    private GlassdoorDbContext context;
    private Repository<Resume> repository;
    private List<Resume> Resumes;
    public ResumeService(GlassdoorDbContext context)
    {
        this.context = context;
        this.repository = new Repository<Resume>(context);
        if (repository?.SelectAllAsEnumerable().ToList() is not null)
            this.Resumes = repository.SelectAllAsEnumerable().ToList();
        else
            this.Resumes = new List<Resume>();
    }
    public async Task<Resume> CreateAsync(Resume model)
    {
        var existPhone = Resumes.FirstOrDefault(Resume => Resume.Id == model.Id)
            ?? throw new Exception($"This Resume is always exists");

        await repository.InsertAsync(model);
        await repository.SaveChanges();
        return model;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exist = Resumes.FirstOrDefault(Resume => Resume.Id == id);
        if (exist is null)
            throw new Exception("This Resume is not found");

        if (exist.IsDeleted)
            throw new Exception("This Resume is not found");

        await repository.DeleteAsync(exist);
        await repository.SaveChanges();
        return true;
    }

    public Task<IEnumerable<Resume>> GetAllAsEnumerable()
    {
        return Task.FromResult(repository.SelectAllAsEnumerable());
    }

    public Task<IQueryable<Resume>> GetAllAsQueyable()
    {
        return Task.FromResult(repository.SelectAllAsQueryable());
    }

    public Task<Resume> GetByIdAsync(long id)
    {
        var exist = Resumes.FirstOrDefault(Resume => Resume.Id == id)
            ?? throw new Exception($"This Resume is with id {id} is not found");

        return Task.FromResult(exist);
    }

    public async Task<Resume> UpdateAsync(long id, Resume model)
    {
        var exist = Resumes.FirstOrDefault(Resume => Resume.Id == id);
        if (exist is null)
            throw new Exception("This Resume is not found");

        exist.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(exist);
        await repository.SaveChanges();
        return model;
    }
}
