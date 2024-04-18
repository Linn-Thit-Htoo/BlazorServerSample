using BlazorServerSample.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorServerSample.Pages.BlogPage
{
    public partial class EditBlog
    {
        [Parameter]
        public long id { get; set; }
        BlogDataModel? blog;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                HttpResponseMessage response = await HttpClient.GetAsync($"/api/Blog/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    blog = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task Update()
        {
            try
            {
                string jsonStr = JsonConvert.SerializeObject(blog);
                HttpContent content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
                HttpResponseMessage response = await HttpClient.PutAsync($"/api/Blog/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    Snackbar.Add(message, Severity.Success);
                }
                else
                {
                    Snackbar.Add("Updating Fail!", Severity.Error);
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
