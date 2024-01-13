using LibApp.Data;
using Microsoft.EntityFrameworkCore;

// to recreate database: Nuget Package Manager Console -> update-database 

namespace LibApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new ApplicationDbContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any MembershipTypes
                if (context.MembershipTypes.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }

                context.MembershipTypes.AddRange(
                    new MembershipType { Name = "Pay as you go", Id = 1, SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0 },
                    new MembershipType { Name = "Monthly", Id = 2, SignUpFee = 30, DurationInMonths = 1, DiscountRate = 5 },
                    new MembershipType { Name = "Quaterly", Id = 3, SignUpFee = 90, DurationInMonths = 3, DiscountRate = 10 },
                    new MembershipType { Name = "Annualy", Id = 4, SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20 }
                );

                context.SaveChanges();
            }
        }
    }
}
