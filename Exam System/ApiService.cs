using CommunityToolkit.Maui.Views;
using System.Text;
using System.Text.Json;

namespace Exam_System
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://192.168.1.41:7076/api/") // استبدل بعنوان الـ API الخاص بك
            };
        }

        // Get Method
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var popup = new WaitingPopup();
            Application.Current.MainPage.ShowPopup(popup);
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("خطاء", ex.Message, "OK");
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                popup.Close();
            }
        }

        // Post Method
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var popup = new WaitingPopup();
            Application.Current.MainPage.ShowPopup(popup);
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(endpoint, content);
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("خطاء", ex.Message, "OK");
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                popup.Close();
            }
        }

        // Post Method
        public async Task<T> DeleteAsync<T>(string url)
        {
            var popup = new WaitingPopup();
            Application.Current.MainPage.ShowPopup(popup);
            try
            {
                var response = await _httpClient.DeleteAsync(url);
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("خطاء", ex.Message, "OK");
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                popup.Close();
            }
        }
        // Post Method
        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var popup = new WaitingPopup();
            Application.Current.MainPage.ShowPopup(popup);
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(endpoint, content);
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("خطاء", ex.Message, "OK");
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                popup.Close();
            }
        }
    }
}
