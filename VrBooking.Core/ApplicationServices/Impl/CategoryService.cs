using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repo;

        public CategoryService(IRepository<Category> repo)
        {
            _repo = repo;
        }

        public Category Create(Category category)
        {
            Category createdCategory;
            try
            {
                if (category == null)
                {
                    throw new InvalidDataException("Category was Null");
                }
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    throw new InvalidDataException("Name must contain letters");
                }
                if (CategoryExist(category))
                {
                    throw new InvalidDataException("Category already exist");
                }
                createdCategory = _repo.Create(category);

                if (createdCategory == null)
                {
                    throw new InvalidOperationException("Created Category returned as Null");
                }
                if (createdCategory.Id <= 0)
                {
                    throw new InvalidOperationException("Created Category had an invalid ID");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return createdCategory;
        }

        public Category Read(long id)
        {
            Category category;
            try
            {
                category = _repo.Read(id);
                if (category == null)
                {
                    throw new InvalidDataException("Could not Find Category");
                }
                if (category.Id != id)
                {
                    throw new InvalidOperationException("returns the wrong entity");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return category;
        }
        public List<Category> ReadAll()
        {
            List<Category> categories;
            try
            {
                categories = _repo.ReadAll().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return categories;
        }
        public Category Update(Category category)
        {
            Category updatedCategory;
            try
            {
                if (category == null)
                {
                    throw new InvalidDataException("Category was Null");
                }
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    throw new InvalidDataException("Name must contain letters");
                }
                if (CategoryExist(category))
                {
                    throw new InvalidDataException("Category already exist");
                }
                updatedCategory = _repo.Update(category);
                if (updatedCategory == null)
                {
                    throw new InvalidOperationException("Updated User was return as Null");
                }
                if (!updatedCategory.Equals(Read(category.Id)))
                {
                    throw new InvalidOperationException("Category was not updated");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            return updatedCategory;
        }
        public Category Delete(long id)
        {
            Category deletedCategopry;
            try
            {
                deletedCategopry = _repo.Delete(Read(id));
                if (deletedCategopry == null || _repo.Read(id) != null)
                {
                    throw new InvalidOperationException("Category was not deleted");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return deletedCategopry;

        }

        private bool CategoryExist(Category category)
        {
            return ReadAll().Where(cat => cat.Id != category.Id).FirstOrDefault(cat => cat.Name.ToLower().Equals(category.Name.ToLower())) != null;
        }
    }
}
