using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace TestUnitaire
{
    [TestClass]
    public class CTMSTest
    {
        [TestMethod]
        public async Task CallCtms()
        {
            var AccesToken = Authentificate();

            var folder = CreateFolder(AccesToken);



        }

        public FolderModel CreateFolder(string AccesToken)
        {
         
            using (var client = new HttpClient() { BaseAddress = new Uri("https://partenaires.easyconform.com") })
            {
                client.DefaultRequestHeaders.Add("accessToken", AccesToken);
                using (var content1 = new MultipartFormDataContent())
                {
                    var stringContentRef = new StringContent("refeTest");

                    content1.Add(stringContentRef, "reference");
                  

                    var response = client.PostAsync("ws22/dossier", content1);
                    var returned = response.Result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<FolderModel>(returned);
                    
                }
            }
        }

        public string Authentificate()
        {
            var AccesToken = "";
            using (var client = new HttpClient() { BaseAddress = new Uri("https://partenaires.easyconform.com") })
            {

                using (var content1 = new MultipartFormDataContent())
                {
                    var stringContentLogin = new StringContent("wsupsideo");
                    var stringContentPassword = new StringContent("Upsideo.Ctms$$2019");

                    content1.Add(stringContentLogin, "login");
                    content1.Add(stringContentPassword, "password");

                    var response = client.PostAsync("ws22/authenticate", content1);
                    var returned = response.Result.Content.ReadAsStringAsync().Result;
                    var CtmsAuthentificate = JsonConvert.DeserializeObject<AuthentificateCTMS>(returned);
                    AccesToken = CtmsAuthentificate.accessToken;
                }
            }
            return AccesToken;
        }
    }
}
