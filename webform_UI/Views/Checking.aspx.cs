using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Threading.Tasks;

namespace webform_UI.Views
{
    public partial class Checking : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the token from the query parameter
                string token = Request.QueryString["token"];

                if (!string.IsNullOrEmpty(token))
                {
                    // Store the token in a session variable
                    Session["AccessToken"] = token;
                    Session["GridAccessToken"] = token;

                    // Now you have the token in the session for further use
                }
                //ViewState["SortExpression"] = radGrid1.MasterTableView.SortExpressions.GetSortString();
                //ViewState["PageIndex"] = radGrid1.MasterTableView.CurrentPageIndex;
                await FetchAndBindDataAsync(token);


            }
        }

        protected async void radGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["GridAccessToken"] != null)
            {
                string token = Session["GridAccessToken"].ToString();
                await FetchAndBindDataAsync(token);
                radGrid1.Rebind();
            }
        }
        protected void btnAddMovie_Click(object sender, EventArgs e)
        {
            if (Session["GridAccessToken"] != null)
            {
                string token = Session["GridAccessToken"].ToString();
                //Response.Redirect("AddMovie.aspx");
                Response.Redirect($"AddMovie.aspx?token={token}");

            }

        }

        private async Task FetchAndBindDataAsync(string token)
        {
            string apiUrl = "https://localhost:7018/api/Movies";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<ApiDataModel> data = JsonConvert.DeserializeObject<List<ApiDataModel>>(jsonResponse);

                        radGrid1.DataSource = data;
                        radGrid1.DataBind();
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