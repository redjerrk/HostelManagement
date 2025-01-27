using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.MVC.Data;
using Web.MVC.Services.FeeRepo;
using Web.MVC.Services.Hostel;
using Web.MVC.Services.HostelRepo;
using Web.MVC.Services.MemberRepo;
using Web.MVC.Services.RoomRepo;
using Web.MVC.Services.RoomTypeRepo;
using Web.MVC.Services.StaffRepo;
using Web.MVC.Services.RentCollection;

var builder = WebApplication.CreateBuilder(args);

// Build the application
// Setting up services and dependencies
// Add MVC framework for handling web requests
// Configure Identity with default user and role management

// Get the database connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Add Identity services to handle user authentication and authorization
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add Controllers and Views for handling web requests
builder.Services.AddControllersWithViews();

// Dependency Injections
// Add transient services for Hostel, Fee, Member, Room, RoomType, Staff and RentCollection functionalities
builder.Services.AddTransient<IHostelService, HostelService>()
                .AddTransient<IFeeService, FeeService>()
                .AddTransient<IMemberService, MemberService>()
                .AddTransient<IRoomService, RoomService>()
                .AddTransient<IRoomTypeService, RoomTypeService>()
                .AddTransient<IStaffService, StaffService>()
                .AddTransient<IRentCollectionService, RentCollectionService>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable endpoint for database migrations in the development environment
    app.UseMigrationsEndPoint();
}
else
{
    // Use custom error handling page in the production environment
    app.UseExceptionHandler("/Home/Error");
    // Enable HTTP Strict Transport Security (HSTS) in the production environment
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();
// Serve static files (e.g., CSS, JavaScript, images)
app.UseStaticFiles();

// Enable routing for handling incoming requests
app.UseRouting();

// Enable authentication for user login and authorization checks
app.UseAuthentication();
// Enable authorization to access protected resources based on user roles
app.UseAuthorization();

// Configure the default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enable Razor Pages
app.MapRazorPages();

// Get the current environment and load the appropriate app settings file
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var appSettingFile = $"appsettings.{environment}.json";
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(appSettingFile, optional: true, reloadOnChange: true)
                .Build();

// Start the application
app.Run();