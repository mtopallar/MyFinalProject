using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Update(Product product);

        IResult AddTransectionalTest(Product product); //100tl den 10 lira aktarırken aktarım yapandan 10 tl düşer diğer hesaba 10 tl eklenir. 2 adet veri tabanı işi var. ilk hesaptan 10tl düşüp iinciye ekleyemezse işlemi geri alması gerekir.

    }
}
