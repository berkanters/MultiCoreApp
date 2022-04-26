using System.Text;
using MultiCoreApp.MVC.DTOs;
using Newtonsoft.Json;

namespace MultiCoreApp.MVC.ApiServices
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductWithCategoryDto>> GetAllAsync()
        {
            IEnumerable<ProductWithCategoryDto> productDtos;
            var response = await _httpClient.GetAsync("product");
            if (response.IsSuccessStatusCode)
            {
                productDtos =
                    JsonConvert.DeserializeObject<IEnumerable<ProductWithCategoryDto>>(
                        await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                productDtos= null;
            }

            return productDtos;
        }

        public async Task<ProductWithCategoryDto> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"product/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductWithCategoryDto>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                return null;
            }
        }

        public async Task<ProductWithCategoryDto> AddAsync(ProductWithCategoryDto proDto)
        {
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(proDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("product", stringContent);
            if (response.IsSuccessStatusCode)
            {
                proDto=JsonConvert.DeserializeObject<ProductWithCategoryDto>(await response.Content.ReadAsStringAsync())!;
                return proDto;
            }

            return null;
        }

        public async Task<bool> Update(ProductWithCategoryDto productDto)
        {
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(productDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("product", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProductWithCategoryDto>> GetAllAsyncWithCategory()
        {
            IEnumerable<ProductWithCategoryDto> productDtos;
            var response = await _httpClient.GetAsync("product/categoryall");
            if (response.IsSuccessStatusCode)
            {
                productDtos =
                    JsonConvert.DeserializeObject<IEnumerable<ProductWithCategoryDto>>(
                        await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                productDtos = null;
            }

            return productDtos;
        }
        public async Task<ProductWithCategoryDto> GetByIdWithCategryAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"product/{id}/Category");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductWithCategoryDto>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                return null;
            }
        }


    }
}
