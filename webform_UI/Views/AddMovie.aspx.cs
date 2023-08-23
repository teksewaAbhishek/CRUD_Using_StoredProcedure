using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Telerik.Web.UI.PdfViewer;

namespace webform_UI.Views
{
    public partial class AddMovie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnAdd_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7018/api/Movies";

           
            ApiDataModel newMovie = new ApiDataModel
            {
                Title = txtTitle.Text,
                Genre = txtGenre.Text,
                ReleaseDate = DateTime.Parse(txtReleaseDate.Text)
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(newMovie);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        
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
