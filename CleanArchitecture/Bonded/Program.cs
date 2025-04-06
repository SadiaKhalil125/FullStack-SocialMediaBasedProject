
using Microsoft.AspNetCore.Identity;
using Bonded.Domain;
using Bonded.Application.Interfaces;
using Bonded.Application.Services;
using Microsoft.EntityFrameworkCore;
using Bonded.Infrastructure.Repositories;
using Bonded.Infrastructure;
using Bonded.Hubs;
using Application.Services;
using Infrastructure.Repositories;
using Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

   

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();
builder.Services.AddTransient<IFollowRepository, FollowRepository>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<IChatRepository, ChatRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();

builder.Services.AddTransient<ChatService>();
builder.Services.AddTransient<NotificationService>();
builder.Services.AddTransient<LikeService>();
builder.Services.AddTransient<CommentService>();
builder.Services.AddTransient<FollowService>();
builder.Services.AddTransient<PostService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<AdminService>();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("AdminAccess", policy =>
         policy.RequireClaim("Role","Admin"));

});
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSession();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
   // app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors();
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseDeveloperExceptionPage();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapHub<ChatHub>("/chatHub");
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
