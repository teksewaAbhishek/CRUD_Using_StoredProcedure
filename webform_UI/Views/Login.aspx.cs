

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace webform_UI.Views
{
    
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected async void Button_Click(object sender, EventArgs e)
        {
            // API endpoint URL
            string apiURL = "https://localhost:7018/api/Login";
            // Get values from textboxes
            string username = txtUsername.Value;
            string password = txtPassword.Value;

            // JSON data to send
            var requestData = new
            {
                username,
                password 
                /*username = txtUsername.ToString(),
                password = txtPassword.ToString(),*/
            };

            using (var httpClient = new HttpClient())
            {
                // Set the content type to JSON
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // Serialize the JSON data
                    string jsonData = JsonConvert.SerializeObject(requestData);

                    // Create a StringContent object with JSON data
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    // Send POST request
                    var response = await httpClient.PostAsync(apiURL, content);

                    // Read and handle the response
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        
                        
                        TokenResponse responseData = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                        string token = responseData.Token;
                        // Display the token on the label
                        // tokenLabel.Text = "Token: " + token;

                        // Store the token in a session variable
                        Session["AccessToken"] = token;

                        // Redirect to the Index page with the token as a query parameter
                        Response.Redirect($"Index.aspx?token={token}");
                    }
                    else
                    {
                        // Handle unsuccessful response
                        // You can display an error message or take appropriate action
                        string errorMessage = "Request failed with status: " + response.StatusCode;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    string errorMessage = "An error occurred: " + ex.Message;
                }
            }
        }
    }
}

