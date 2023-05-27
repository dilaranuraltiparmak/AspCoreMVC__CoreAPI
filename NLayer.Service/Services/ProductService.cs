using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
	public class ProductService : Service<Product>, IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork,IMapper mapper,IProductRepository productRepository) : base(repository, unitOfWork)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}


	public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
		{
			var products = await _productRepository.GetProductWithCategory();
			var productsDto=_mapper.Map<List<ProductWithCategoryDto>>(products);	
			return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,productsDto);
		}

        public async Task<List<ProductWithCategoryDto>> GetProductWithCategoryy()
        {
            var products = await _productRepository.GetProductWithCategoryy();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return  productsDto;
        }
    }
}
