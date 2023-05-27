using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }
        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("Products/GetProductsWithCategory");
            return response.Data;
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"Products/{id}");
            return response.Data;
        }
        public async Task<ProductDto>SaveAsync(ProductDto newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync("Products", newProduct);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            return responseBody.Data;
        }
        public async Task<bool> UpdateAsync(ProductDto newProduct)
        {
            var response = await _httpClient.PutAsJsonAsync("Products", newProduct);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Removeasync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Products/{id}");

            return response.IsSuccessStatusCode;
        }
   
    }
}
