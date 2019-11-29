using System.Collections.Generic;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    internal class CategoryRepository : IRepository<Category>
    {
        private readonly VrBookingContext _ctx;

        public CategoryRepository(VrBookingContext ctx)
        {
            _ctx = ctx;
        }

        public Category Create(Category entity)
        {
            Category createdCategory = _ctx.Add<Category>(entity).Entity;
            _ctx.SaveChanges();
            return createdCategory;
        }

        public Category Delete(Category entity)
        {
            Category deletedCategory = _ctx.Remove<Category>(entity).Entity;
            _ctx.SaveChanges();
            return deletedCategory;
        }

        public Category Read(long id)
        {
            return _ctx.Categories.FirstOrDefault(cat => cat.Id == id);
        }

        public IEnumerable<Category> ReadAll()
        {
            return _ctx.Categories;
        }

        public Category Update(Category entity)
        {
            Category updatedCategory = _ctx.Update<Category>(entity).Entity;
            _ctx.SaveChanges();
            return updatedCategory;
        }
    }
}
