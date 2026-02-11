using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wuchmiITHome.Data;

namespace wuchmiITHome.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new wuchmiITHomeContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<wuchmiITHomeContext>>()))
            {
                // Check if both Article and TeachAppoEmployee have been seeded
                if (context.Article.Any() && context.TeachAppoEmployees.Any())
                {
                    return;   // DB has been seeded
                }

                // Seed Articles if empty
                if (!context.Article.Any())
                {
                    context.Article.AddRange(
                        new Article
                        {
                            Title = "Day01 Azure 的自我修煉",
                            ReleaseDate = DateTime.Parse("2020-09-01"),
                            Link = "https://ithelp.ithome.com.tw/articles/10233277",
                            Count = 0,
                            Category = "鐵人賽"
                        },

                        new Article
                        {
                            Title = "Day02 申請Azure帳號",
                            ReleaseDate = DateTime.Parse("2020-09-02"),
                            Link = "https://ithelp.ithome.com.tw/articles/10233285",
                            Count = 0,
                            Category = "鐵人賽"
                        },

                        new Article
                        {
                            Title = "Day03 Resource Group 資源群組",
                            ReleaseDate = DateTime.Parse("2020-09-03"),
                            Link = "https://ithelp.ithome.com.tw/articles/10233371",
                            Count = 0,
                            Category = "鐵人賽"

                        },

                        new Article
                        {
                            Title = "Day04 Dotnet Core 專案",
                            ReleaseDate = DateTime.Parse("2020-09-04"),
                            Link = "https://ithelp.ithome.com.tw/articles/10233562",
                            Count = 0,
                            Category = "鐵人賽"
                        }
                    );
                }

                // Seed TeachAppoEmployees if empty
                if (!context.TeachAppoEmployees.Any())
                {
                    context.TeachAppoEmployees.AddRange(
                        new TeachAppoEmployee
                        {
                            Yr = 2024,
                            IdNo = "A123456789",
                            Birthday = DateTime.Parse("1990-05-15"),
                            EmplNo = "000001",
                            ChName = "王小明",
                            EnName = "Wang Xiaoming",
                            Email = "wang.xiaoming@example.com",
                            RefreshToken = "token_abc123",
                            RefreshTokenExpired = DateTime.Parse("2025-12-31")
                        },
                        new TeachAppoEmployee
                        {
                            Yr = 2024,
                            IdNo = "B987654321",
                            Birthday = DateTime.Parse("1988-08-20"),
                            EmplNo = "000002",
                            ChName = "李麗花",
                            EnName = "Lee Lihua",
                            Email = "lee.lihua@example.com",
                            RefreshToken = "token_def456",
                            RefreshTokenExpired = DateTime.Parse("2025-12-31")
                        },
                        new TeachAppoEmployee
                        {
                            Yr = 2025,
                            IdNo = "C111222333",
                            Birthday = DateTime.Parse("1992-03-10"),
                            EmplNo = "000003",
                            ChName = "張偉明",
                            EnName = "Zhang Weiming",
                            Email = "zhang.weiming@example.com",
                            RefreshToken = "token_ghi789",
                            RefreshTokenExpired = DateTime.Parse("2026-12-31")
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}