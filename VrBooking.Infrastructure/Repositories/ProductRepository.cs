using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VrBooking.Core;
using VrBooking.Core.ApplicationServices;

namespace VrBooking.Infrastructure.Repositories
{
    public class ProductRepository: IRepository<Product>
    {
        private readonly VrBookingContext _contex;
        private ProductRepository(VrBookingContext contex)
        {
            _contex = contex;
        }
        
        public Product Create(Product entity)
        {
            Product createdProduct = _contex.products.Add(entity).Entity;
            _contex.SaveChanges();
            return createdProduct;
        }

        public Product Read(long id)
        {
            return _contex.products.FirstOrDefault(prod => prod.Id == id);
        }

        public IEnumerable<Product> ReadAll()
        {
            return _contex.products;
        }

        public Product Update(Product entity)
        {
            Product oldProduct = Read(entity.Id);

            oldProduct.Description = entity.Description;
            oldProduct.Name = entity.Name;

            _contex.SaveChanges();
            return entity;
        }

        public Product Delete(Product entity)
        {
            var entityRemoved = _contex.Remove(Read(entity.Id)).Entity;
            _contex.SaveChanges();
            return entityRemoved;
        }
    }
}