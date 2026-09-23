using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository.IRepository
{   //interface định nghĩa các thuộc tính truy cập các repository cụ thể
    //Ngoài ra, nó có phương thức Save() để lưu tất cả các thay đổi vào cơ sở dữ liệu.
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository Covertype { get; }

        IProductRepository Product { get; }

        ICompanyRepository Company { get; }
        void Save();
    }
}
