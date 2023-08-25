
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace webform_UI.Views
{
    public partial class RegisterUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7018/api/Auth/Register";
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Create a registration model with the provided username and password
            var registrationModel = new
            {
                Username = username,
                Password = password
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonPayload = JsonConvert.SerializeObject(registrationModel);
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Registration successful, you can redirect to a success page
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        // Registration failed, display an error message
                        errorMessage.Text = "Registration failed: " + response.ReasonPhrase;
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
