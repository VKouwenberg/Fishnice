using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using DAL_DataAccessLayer.DTO.Models;
using System;
using System.Linq;
using Fishnice.Models;
using Fishnice.Data;
using DAL_DataAccessLayer.DTO.Models;

namespace Fishnice.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new FishniceContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<FishniceContext>>()))
        {
            // Look for any movies.
            if (context.Fish.Any())
            {
                return;   // DB has been seeded
            }
            context.Fish.AddRange(
                new Fish
                {
                    Title = "Bluefin Trout",
                    CatchDate = DateTime.Parse("1989-2-12"),
                    Genre = "Trout",
                    Price = 7.99M
                },
                new Fish
                {
                    Title = "Speckled Bass ",
                    CatchDate = DateTime.Parse("1984-3-13"),
                    Genre = "Bass",
                    Price = 8.99M
                },
                new Fish
                {
                    Title = "Redbelly Bass",
                    CatchDate = DateTime.Parse("1986-2-23"),
                    Genre = "Bass",
                    Price = 9.99M
                },
                new Fish
                {
                    Title = "Hammerhead Shark",
                    CatchDate = DateTime.Parse("1959-4-15"),
                    Genre = "Shark",
                    Price = 3.99M
                }
            );
            context.SaveChanges();
        }
    }
}