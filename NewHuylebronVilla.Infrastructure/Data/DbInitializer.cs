using Microsoft . AspNetCore . Identity ;
using Microsoft . EntityFrameworkCore ;
using NewHuylebronVilla . Application . Common . Division ;
using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Infrastructure.Data ;

public class DbInitializer  : IDbInitializer
{
    
    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public DbInitializer(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext db)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
    }

    public void Initialize()
    {
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }

            if (!_roleManager.RoleExistsAsync(Claim.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Claim.Role_Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(Claim.Role_Customer)).Wait();
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "doquochuy27102003@gmail.com",
                    Email = "doquochuy27102003@gmail.com",
                    Name = "huylebron",
                    NormalizedUserName = "ADMIN@HUYLEBRON.COM",
                    NormalizedEmail = "ADMIN@HUYLEBRON.COM.COM",
                    PhoneNumber = "0398658222",
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "doquochuy27102003@gmail.com");
                _userManager.AddToRoleAsync(user, Claim.Role_Admin).GetAwaiter().GetResult();
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }
}