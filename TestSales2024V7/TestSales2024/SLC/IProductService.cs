using System;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface IProductService
    {
        Products CreateProduct(Products product);
        bool Delete(int id);
    }
}
