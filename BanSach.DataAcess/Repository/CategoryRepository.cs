using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository
{
    //là lớp cụ thể sử dụng thực thể Category,
    //và triển khai các phương thức của ICategoryRepository (kế thừa từ IRepository<Category>).
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //triển khai các phương thức CRUD được kế thừa từ IRepository
        //và phương thức cập nhật từ ICategoryRepository. 
        private readonly ApplicationDbContext _db;
        
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // phương thức Save để lưu các thay đổi trong cơ sở dữ liệu bằng cách gọi SaveChanges() từ DbContext.
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
