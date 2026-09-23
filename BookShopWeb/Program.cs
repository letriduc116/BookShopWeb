using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository;
using BanSach.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Đăng kí DbContext cho ứng dụng để kết nối với database
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
    );

//// Đăng kí Identity cho tài khoản người dùng để xác thực đăng nhập
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Đăng ký dịch vụ vào DI container
// Add IUnitOfWork and UnitOfWork to the services (Dependency Injection)
// Scoped: Tạo một đối tượng mới cho mỗi request
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Xác thực đăng nhập
app.UseAuthentication();
// Uỷ quyền
app.UseAuthorization();

// Đăng ký các route
app.MapRazorPages(); 


app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


app.Run();
