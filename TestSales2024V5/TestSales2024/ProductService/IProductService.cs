using Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ProductService
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        Products CreateProduct(Products product);

        [OperationContract]
        Products RetrieveProductById(int id);

        [OperationContract]
        bool UpdateProduct(Products product);

        [OperationContract]
        bool DeleteProduct(int id);

        [OperationContract]
        List<Products> RetrieveAllProducts();

        [OperationContract]
        List<Products> FilterProductsByName(string name);
    }
}
