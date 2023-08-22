using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telerik.Web.UI;

namespace webform_UI.Views
{
    public partial class Index : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                ViewState["SortExpression"] = radGrid1.MasterTableView.SortExpressions.GetSortString();
                ViewState["PageIndex"] = radGrid1.MasterTableView.CurrentPageIndex;

                await FetchAndBindDataAsync();
            }
        }

        protected async void radGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            await FetchAndBindDataAsync();
            radGrid1.Rebind();
        }

        private async Task FetchAndBindDataAsync()
        {
            string apiUrl = "https://localhost:7018/api/Movies";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<ApiDataModel> data = JsonConvert.DeserializeObject<List<ApiDataModel>>(jsonResponse);

                        radGrid1.DataSource = data;
                    }
                    else
                    {
                        errorMessage.Text = "API request failed: " + response.ReasonPhrase;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}
