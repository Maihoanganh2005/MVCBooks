using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCBooks.Data;
using System;
using System.Linq;

namespace MVCBooks.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCBooksContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MVCBooksContext>>()))
            {
                // Kiểm tra thông tin cuốn sách đã tồn tại hay chưa
                if (context.Book.Any())
                {
                    return;   // Không thêm nếu cuốn sách đã tồn tại trong DB
                }

                context.Book.AddRange(
                    new Book
                    {
                        Title = "Atomic Habits",
                        ReleaseDate = DateTime.Parse("2018-10-16"),
                        Genre = "Self-Help",
                        Price = 11.98M
                    },
                    new Book
                    {
                        Title = "Dark Roads",
                        ReleaseDate = DateTime.Parse("2021-8-3"),
                        Genre = "Novel",
                        Price = 18.59M
                    }
                );
                context.SaveChanges();//lưu dữ liệu
            }
        }
    }
}