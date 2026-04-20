using Microsoft.EntityFrameworkCore;
using YTSchool.Api.Cms.Database;

namespace YTSchool.Api.Cms.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication webApplication)
    {
        using IServiceScope scope = webApplication.Services.CreateScope();
        await using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await dbContext.Database.MigrateAsync();
            webApplication.Logger.LogInformation("Database Migrations applied successfully.");
        }
        catch (Exception e)
        {
            webApplication.Logger.LogError(e, "An error occured while applying database migrations");
            throw;
        }
        
    }
}
