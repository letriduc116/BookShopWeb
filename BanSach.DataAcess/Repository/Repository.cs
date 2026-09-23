using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //là lớp triển khai của IRepository<T>

        private readonly ApplicationDbContext _db;
        internal DbSet<T> DbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }
        //Thêm dữ liệu
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        //Lấy dữ liệu
        //includeProperties: là một chuỗi các thuộc tính mà bạn muốn bao gồm trong kết quả trả về.
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {// lặp và loại bỏ các entyti trống trong chuỗi includeProperties
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                { // tách chuỗi từ Json thành mảng các chuỗi con, mỗi chuỗi con là một thuộc tính
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        // chatGPT thêm hàm GetAll với điều kiện (tránh lỗi 'System.InvalidOperationException' )
        // video: 123 (nếu muốn có thể comment lại để xem lỗi gì)
        public IEnumerable<T> GetAll( Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
                IQueryable<T> query = DbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

            return query.ToList();
        }


        //Lấy dữ liệu đầu tiên thỏa mãn điều kiện.
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        //Xóa dữ liệu
        public void Remove(T entity)
        {
           DbSet.Remove(entity);
        }

        //Xóa nhiều dữ liệu
        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
