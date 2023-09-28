using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            Addinclude(x=>x.ProductType);
            Addinclude(x=>x.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id==id)
        {
            Addinclude(x=>x.ProductType);
            Addinclude(x=>x.ProductBrand);
        }
    }
}