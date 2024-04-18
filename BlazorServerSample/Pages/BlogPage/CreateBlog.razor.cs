using BlazorServerSample.Models;
using MudBlazor;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorServerSample.Pages.BlogPage
{
    public partial class CreateBlog
    {
        private BlogDataModel requestModel = new();

        private async Task Save()
        {
            try
            {
                string jsonStr = JsonConvert.SerializeObject(requestModel);
                HttpContent content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
                HttpResponseMessage response = await HttpClient.PostAsync("/api/Blog", content);
                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    Snackbar.Add(message, Severity.Success);
                }
                else
                {
                    Snackbar.Add("Creating Fail.", Severity.Error);
                }
                Nav.NavigateTo("/blog");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
