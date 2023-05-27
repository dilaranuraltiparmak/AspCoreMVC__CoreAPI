using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<List<Product>> GetProductWithCategory()
		{
			//Eager Loading
			return await _context.Products.Include(x => x.Category).ToListAsync();
		}

        public async Task<List<Product>> GetProductWithCategoryy()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
