using System;
using System.Web.UI;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace webform_UI.Views
{
    public partial class EditMovie : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["movieId"]))
                {
                    string movieId = Request.QueryString["movieId"];
                    await PopulateMovieDetailsAsync(movieId);
                }
            }
        }

        private async Task PopulateMovieDetailsAsync(string movieId)
        {
            ApiDataModel movie = await FetchMovieDetailsAsync(movieId);
            if (movie != null)
            {
                txtTitle.Text = movie.Title;
                txtGenre.Text = movie.Genre;
                txtReleaseDate.Text = movie.ReleaseDate.ToString("yyyy-MM-dd");
            }
        }

        private async Task<ApiDataModel> FetchMovieDetailsAsync(string movieId)
        {
            string apiUrl = $"https://localhost:7018/api/Movies/{movieId}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
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

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            string movieId = Request.QueryString["movieId"];
            ApiDataModel updatedMovie = new ApiDataModel
            {
                Id = int.Parse(movieId),
                Title = txtTitle.Text,
                Genre = txtGenre.Text,
                ReleaseDate = DateTime.Parse(txtReleaseDate.Text)
            };

            string apiUrl = $"https://localhost:7018/api/Movies/{movieId}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonPayload = JsonConvert.SerializeObject(updatedMovie);
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to Index.aspx after successful update
                        Response.Redirect("Index.aspx");
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
