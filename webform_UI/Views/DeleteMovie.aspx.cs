using System;
using System.Web.UI;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Web.UI.WebControls;

namespace webform_UI.Views
{
    public partial class DeleteMovie : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["movieId"]))
                {
                    string movieId = Request.QueryString["movieId"];
                    string token = Request.QueryString["token"];
                    await PopulateMovieDetailsAsync(movieId, token);
                }
            }
        }

        private async Task PopulateMovieDetailsAsync(string movieId, string token)
        {
            ApiDataModel movie = await FetchMovieDetailsAsync(movieId, token);
            if (movie != null)
            {
                txtTitle.Text = movie.Title;
                txtGenre.Text = movie.Genre;
                txtReleaseDate.Text = movie.ReleaseDate.ToString("yyyy-MM-dd");
            }
        }

        private async Task<ApiDataModel> FetchMovieDetailsAsync(string movieId, string token)
        {
            string apiUrl = $"https://localhost:7018/api/Movies/{movieId}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        ApiDataModel movie = JsonConvert.DeserializeObject<ApiDataModel>(jsonResponse);
                        return movie;
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

            return null; // Movie details not found or error occurred
        }


        // ...

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            string movieId = Request.QueryString["movieId"];
            string token = Request.QueryString["token"];

            string apiUrl = $"https://localhost:7018/api/Movies/{movieId}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Add the token to the request headers
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to Index.aspx after successful delete
                        //Response.Redirect("Index.aspx");
                        //Response.Redirect($"Index.aspx?token={token}");
                        Response.Redirect($"Checking.aspx?token={token}");
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
