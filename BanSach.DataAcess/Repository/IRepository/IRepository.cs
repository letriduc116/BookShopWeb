using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository.IRepository
{
    // định nghĩa các phương thức chung cho tất cả các repository (lớp lưu trữ dữ liệu) để thao tác với cơ sở dữ liệu
    public interface IRepository<T> where T : class
    {
        //T là một kiểu dữ liệu đại diện cho một thực thể cụ thể (entity) trong cơ sở dữ liệu
        //(ví dụ: Category, CoverType, Product, ...)
        //includeProperties: là một chuỗi các thuộc tính mà bạn muốn bao gồm trong kết quả trả về.
        T GetFirstOrDefault(Expression<Func<T,bool>> filter, string? includeProperties = null);
        IEnumerable<T> GetAll(string? includeProperties = null);

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
