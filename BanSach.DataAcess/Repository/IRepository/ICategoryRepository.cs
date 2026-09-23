using BanSach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository.IRepository
{
    //kế thừa từ IRepository<Category>. Nó mở rộng thêm một phương thức là Update, để cập nhật một Category.
    public interface ICategoryRepository:IRepository<Category>
    {
        //kế thừa tất cả các phương thức CRUD của IRepository,
        //đồng thời có thêm khả năng cập nhật đối tượng Category.
        void Update(Category category);
    }
}
