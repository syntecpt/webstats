using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using webstats.Models;

[assembly: OwinStartupAttribute(typeof(webstats.Startup))]
namespace webstats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createRolesandUsers();
        }
        //cheats to set admin role.
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            //Here we create a Admin super user who will maintain the website                  
            var id = "419da414-6a82-4f98-9d8f-fdeb914b9101";
            ApplicationUser user = context.Users.Find(id);

            //Add default User to Role Admin   
            UserManager.AddToRole(user.Id, "Admin");
        }
    }
}
