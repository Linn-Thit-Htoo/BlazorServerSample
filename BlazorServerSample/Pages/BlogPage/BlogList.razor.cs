using BlazorServerSample.Models;
using Newtonsoft.Json;
using MudBlazor;
using BlazorServerSample.Shared;

namespace BlazorServerSample.Pages.BlogPage
{
    public partial class BlogList
    {
        List<BlogDataModel>? lst;
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await FetchData();
            }
        }

        private async Task FetchData()
        {
            try
            {
                HttpResponseMessage response = await HttpClient.GetAsync("/api/Blog");
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Edit(long id)
        {
            Nav.NavigateTo($"/blog/edit/{id}");
        }

        private async Task Delete(long id)
        {
            try
            {
                var parameters = new DialogParameters<ConfirmDialog>
                {
                    {
                        x => x.Message,
                        "Are you sure want to delete?"
                    }
                };

                var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

                var dialog = await DialogService.ShowAsync<ConfirmDialog>("Confirm", parameters, options);
                var result = await dialog.Result;
                if (result.Canceled)
                    return;

                HttpResponseMessage response = await HttpClient.DeleteAsync($"/api/Blog/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    Snackbar.Add(message, Severity.Success);
                    await FetchData();
                }
                else
                {
                    Snackbar.Add("Deleting Fail!", Severity.Error);
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
