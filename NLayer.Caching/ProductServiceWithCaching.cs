using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Caching
{
	public class ProductServiceWithCaching : IProductService
	{
		private const string CacheProductKey = "productsCache";
		private readonly IMapper _mapper;
		private readonly IMemoryCache _memoryCache;
		private readonly IProductRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public ProductServiceWithCaching(IUnitOfWork unitOfWork, IMemoryCache memoryCache, IMapper mapper, IProductRepository repository)
		{
			_unitOfWork=unitOfWork;
			_memoryCache=memoryCache;
			_mapper=mapper;
			_repository=repository;


			if (!_memoryCache.TryGetValue(CacheProductKey, out _))
			{
				_memoryCache.Set(CacheProductKey, _repository.GetProductWithCategory().Result);
			}
		}

		public async Task<Product> AddAsync(Product entity)
		{
			await _repository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			await CahceAllProductsAsync();
			return entity;
		}

		public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
		{
			await _repository.AddRangeAsync(entities);
			await _unitOfWork.CommitAsync();
			await CahceAllProductsAsync();
			return entities;
		}

		public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Product>> GetAllAsync()
		{
			var products=_memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
			return Task.FromResult(products);
		}

		public Task<Product> GetByIdAsync(int id)
		{
			var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id==id);
			if (product==null)
			{
				throw new DirectoryNotFoundException($"{typeof(Product).Name}({id}) bulunamadi");
			}
			return Task.FromResult(product);
		}

		public  Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
		{
			var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
			var productsWithCategoryDto=_mapper.Map<List<ProductWithCategoryDto>>(products);
			return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto));
		}

		public async Task RemoveAsync(Product entity)
		{
			_repository.Remove(entity);
			await _unitOfWork.CommitAsync();
			await CahceAllProductsAsync();
		}

		public async Task RemoveRangeAsync(IEnumerable<Product> entities)
		{
			_repository.RemoveRange(entities);
			await _unitOfWork.CommitAsync();
			await CahceAllProductsAsync();
		}

		public async Task UpdateAsync(Product entity)
		{
			_repository.Update(entity);
			await _unitOfWork.CommitAsync();
			await CahceAllProductsAsync();
		}

		public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
		{
			return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
		}

		public async Task CahceAllProductsAsync()
		{
			//her datayı çağırdığımızda sıfırdan cahce'liyo
			_memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
		}

        public Task<List<ProductWithCategoryDto>> GetProductWithCategoryy()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return Task.FromResult( productsWithCategoryDto);
        }
    }
}
