using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
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

       /* protected void radGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem dataItem)
            {
                dataItem.CssClass = "grid-row"; // Apply the class to every row
            }
        }*/


        protected void btnAddMovie_Click(object sender, EventArgs e)
        {
            if (Session["GridAccessToken"] != null)
            {
                string token = Session["GridAccessToken"].ToString();
                //Response.Redirect("AddMovie.aspx");
                Response.Redirect($"AddMovie.aspx?token={token}");

            }
                
        }

        protected async void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.CommandArgument;

            string apiUrl = $"https://localhost:7018/api/Movies/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (Session["GridAccessToken"] != null)
                    {
                        string token = Session["GridAccessToken"].ToString();
                        // Add the token to the request headers
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    }

                   
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (Session["GridAccessToken"] != null)
                    {
                        string token = Session["GridAccessToken"].ToString();
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            ApiDataModel movie = JsonConvert.DeserializeObject<ApiDataModel>(jsonResponse);

                           // Response.Redirect("EditMovie.aspx?movieId=" + id);
                            Response.Redirect($"EditMovie.aspx?movieId={id}&token={token}");
                        }
                        else
                        {
                            errorMessage.Text = "API request failed: " + response.ReasonPhrase;
                        }

                    }

                   
                }
                catch (Exception ex)
                {
                    errorMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.CommandArgument;

            string apiUrl = $"https://localhost:7018/api/Movies/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (Session["GridAccessToken"] != null)
                    {
                        string token = Session["GridAccessToken"].ToString();
                        // Add the token to the request headers
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    }
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        radGrid1.Rebind();
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


