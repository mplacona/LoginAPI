namespace LoginAPI
{
    using System.Linq;
    using Nancy;
    using Nancy.ModelBinding;

    public class HomeModule : NancyModule
    {
        User user = new User() { Id = 1, Name = "Marcos" };
        Login login = new Login() { Email = "marcos@twilio.com", Password = "pass1" };

        public HomeModule()
        {
            string base64Login = EncodeToBase64(login.Email + ":" + login.Password);

            Post("/api/login", args =>
            {
                var model = this.Bind<Login>();
                if(model.Email.CompareTo(login.Email) == 0 && model.Password.CompareTo(login.Password) == 0){
                    return Response.AsJson(user);
                }

                var r = (Response)"user not found";
                r.StatusCode = HttpStatusCode.NotFound;
                return r; 
            });  

            Get("/api/user", _ => {
                var header = Request.Headers["Authorization"].FirstOrDefault();
                if(header.CompareTo("Basic " + base64Login) == 0){
                    return Response.AsJson(user); 
                }

                var r = (Response)"invalid authorization header";
                r.StatusCode = HttpStatusCode.Forbidden;
                return r;
            });         

            Get("/api/logout", _ => Response.AsJson(user));
        }

        private static string EncodeToBase64(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

    
}
