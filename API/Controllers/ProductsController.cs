using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {

        private readonly IRepository<Product> _repoProduct;
        private readonly IRepository<ProductBrand> _repoBrand;
        private readonly IRepository<ProductType> _repoType;
        private readonly IMapper _mapper;

        public ProductsController(IRepository<Product> repoProduct, IRepository<ProductBrand> repoBrand, IRepository<ProductType> repoType,
            IMapper mapper
        )
        {
            _repoProduct = repoProduct;
            _repoType = repoType;
            _repoBrand = repoBrand;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductReturnDto>>> GetAllProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _repoProduct.CountAsync(countSpec);

            var products = await _repoProduct.ListAsync(spec);

            
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnDto>>(products);

            return Ok(
                new Pagination<ProductReturnDto>(
                    productParams.pageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductReturnDto>> GetProduct(int Id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(Id);
            var product = await _repoProduct.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _repoBrand.GetAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _repoType.GetAllAsync());
        }
    }
}