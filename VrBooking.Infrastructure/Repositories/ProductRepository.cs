using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly VrBookingContext _ctx;
        public ProductRepository(VrBookingContext contex)
        {
            _ctx = contex;
        }

        public Product Create(Product entity)
        {
            Product createdProduct = _ctx.Add<Product>(entity).Entity;
            _ctx.SaveChanges();
            return createdProduct;
        }

        public Product Read(long id)
        {
            return _ctx.Products
                .Include( p => p.Category)
                .FirstOrDefault(prod => prod.Id == id);
        }

        public IEnumerable<Product> ReadAll()
        {
            return _ctx.Products
                .Include(p => p.Category);
        }

        public Product Update(Product entity)
        {
            Product oldProduct = _ctx.Products.First(a => a.Id == entity.Id);
            oldProduct.Name = entity.Name;
            oldProduct.Description = entity.Description;
            oldProduct.Category = entity.Category;
            _ctx.SaveChanges();
            return entity;
        }

        public Product Delete(Product entity)
        {
            Product entityRemoved = _ctx.Remove<Product>(entity).Entity;
            _ctx.SaveChanges();
            return entityRemoved;
        }
    }
}
