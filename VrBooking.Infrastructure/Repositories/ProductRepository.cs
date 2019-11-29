using System.Collections.Generic;
using System.Linq;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.DomainServices;

namespace VrBooking.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly VrBookingContext _contex;
        public ProductRepository(VrBookingContext contex)
        {
            _contex = contex;
        }

        public Product Create(Product entity)
        {
            Product createdProduct = _contex.Add<Product>(entity).Entity;
            _contex.SaveChanges();
            return createdProduct;
        }

        public Product Read(long id)
        {
            return _contex.Products.FirstOrDefault(prod => prod.Id == id);
        }

        public IEnumerable<Product> ReadAll()
        {
            return _contex.Products;
        }

        public Product Update(Product entity)
        {
            _contex.Update<Product>(entity);
            _contex.SaveChanges();
            return entity;
        }

        public Product Delete(Product entity)
        {
            Product entityRemoved = _contex.Remove<Product>(entity).Entity;
            _contex.SaveChanges();
            return entityRemoved;
        }
    }
}