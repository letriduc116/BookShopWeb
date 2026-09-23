using BanSach.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; //DbContextOptions

namespace BanSach.DataAcess.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {

        // chuỗi kết nối đến cơ sở dữ liệu, cấu hình bảo mật, và các tùy chọn khác liên quan đến việc làm việc với cơ sở dữ liệu. 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        //cho phép bạn thao tác với dữ liệu trong bảng thông qua Entity Framework.
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
    }


}
