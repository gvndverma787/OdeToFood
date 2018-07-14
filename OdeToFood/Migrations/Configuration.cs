namespace OdeToFood.Migrations
{
    using OdeToFood.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Restaurants.AddOrUpdate(r => r.Name,
                new Restaurant { Name = "Sabatino", City = "Baltimore", Country = "USA" },
                new Restaurant { Name = "Greate Lake", City = "Chicago", Country = "USA" },
                new Restaurant
                {
                    Name = "Smaka",
                    City = "Gothenburg",
                    Country = "Sweden",
                    Reviews = new List<RestaurantReview> { new RestaurantReview { Rating = 9, Body = "Great food!", ReviewerName = "Shubhojit" } }
                });
            
            SeedMembership();
        }

        private void SeedMembership()
        {
            //Initialize WebSecurity instance to the database connection
            MembershipConfig.RegisterMembership();
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
            if(!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if(membership.GetUser("Sallen", false) == default(MembershipUser))
            {
                membership.CreateUserAndAccount("Sallen", "Password-1");
            }
            if(!roles.GetRolesForUser("Sallen").Contains("Admin"))
            {
                roles.AddUsersToRoles(new string[] { "Sallen" }, new string[] { "Admin" });
            }


        }
    }
}
