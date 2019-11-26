using System.Collections.Generic;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public interface ICategoryService
    {
        Category Create(Category category);
        Category Delete(long id);
        Category Read(long id);
        List<Category> ReadAll();
        Category Update(Category category);
    }
}