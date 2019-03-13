using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Linq;
using System.Diagnostics;

namespace TestUnitaire
{
    /// <summary>
    /// Description résumée pour TestAriadNext
    /// </summary>
    [TestClass]
    public class TestAriadNext
    {
        public string url = @"C:\Projects\Test\docTest\JUSTIFDOM_14E_HCR061_Titulaire1.pdf";
        public string urlJsonToAnalyse = @"C:\Projects\Test\docTest\JsonTest.json";
        [TestMethod]
        public async Task CallAriadNext()
        {
            try
            {
                DateTime? dateTime = new DateTime(2015,02,19);
                string d = dateTime.Value.ToString("dd/MM/yyy");

                //---------------------- All files : 

                var AllFiles = Directory.GetFiles(@"C:\Projects\Test\docTest\", "*.pdf");


                //-------------------- init object



                //-------------- authentification :
                IEnumerable<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>()
                 {
                new KeyValuePair<string, string>("broker", "demo"),
                new KeyValuePair<string, string>("client_id", "sdk-web"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("password", "28oy0S9aNJ"),  // add password from this JIRA https://upsideo.atlassian.net/browse/FIN-442
                new KeyValuePair<string, string>("username", "upsideo@ariadnext.com")  // add usernam from this JIRA https://upsideo.atlassian.net/browse/FIN-442
                 };
                var AccesToken = ""; // key authentification
                AuthentificationResultat authentificationResultat = new AuthentificationResultat();
                using (var httpClientAuthentification = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(postData))
                    {
                        content.Headers.Clear();
                        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                        var response = httpClientAuthentification.PostAsync(new Uri("https://api-test.ariadnext.com/auth/realms/customer-identity/protocol/openid-connect/token"), content);
                        var returned = response.Result.Content.ReadAsStringAsync().Result;
                        returned = returned.Replace("not-before-policy", "not_before_policy"); // fix bug 
                        authentificationResultat = JsonConvert.DeserializeObject<AuthentificationResultat>(returned);
                        AccesToken = authentificationResultat.access_token;
                    }
                }
                #region Other method ariadnext
                //   var jsonDocument = GetJsonInitObject(url);
                //Creation des documents :
                //var ListResponseCreationDocument = new List<ResponseCreateDocument>();

                //using (var Client = new HttpClient() { BaseAddress = new Uri("https://api-test.ariadnext.com/gw/cis/") })
                //{
                //    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccesToken);
                //    var content = new StringContent(jsonDocument, Encoding.UTF8, "application/json");
                //    var result = Client.PostAsync("rest/v1/demo/document", content).Result;
                //    var responseString = await result.Content.ReadAsStringAsync();
                //    ResponseCreateDocument responseCreateDocument = JsonConvert.DeserializeObject<ResponseCreateDocument>(responseString);
                //    ListResponseCreationDocument.Add(responseCreateDocument);

                //}

                //Search all Document :
                SearchAllDocument searchAllDocument = new SearchAllDocument();
                //using (var Client = new HttpClient() { BaseAddress = new Uri("https://api-test.ariadnext.com/gw/cis/") })
                //{
                //    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccesToken);
                //    var content = new StringContent(jsonDocument, Encoding.UTF8, "application/json");
                //    var result = Client.GetAsync("rest/v1/demo/document/search").Result;
                //    var responseString = await result.Content.ReadAsStringAsync();
                //    searchAllDocument = JsonConvert.DeserializeObject<SearchAllDocument>(responseString);
                //}

                //Get Document :
                var listresponseGetDocument = new List<ResponseEachDocument>();

                //foreach (var doc in searchAllDocument.rows/*ListResponseCreationDocument*/)
                //{
                //    using (var Client = new HttpClient() { BaseAddress = new Uri("https://api-test.ariadnext.com/gw/cis/") })
                //    {
                //        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccesToken);
                //        var content = new StringContent(jsonDocument, Encoding.UTF8, "application/json");
                //        var result = Client.GetAsync("rest/v1/demo/document/" + doc.uid).Result;
                //        var responseString = await result.Content.ReadAsStringAsync();
                //        ResponseEachDocument responseEachDocument = JsonConvert.DeserializeObject<ResponseEachDocument>(responseString);
                //        listresponseGetDocument.Add(responseEachDocument);
                //    }
                //}


                #endregion

                //----------------- Create and Check : 
                ///*
                var ListResponseCreationAndCheckDocument = new List<Tuple<string, string>>();
                IEnumerable<string> AllFilleWithoutCNVI = AllFiles;
                List<string> pathsFilles = new List<string>();
                //var FilleWithoutVERSO = AllFiles.SkipWhile(x => x.Contains("VERSO"));
                //var FilleVERSO = AllFiles.Where(x => x.Contains("VERSO"));
                //foreach (var f in FilleWithoutVERSO) 
                //{
                //        pathsFilles.Add(f);
                //}

                //foreach (var f in FilleVERSO) // integrer les verso qui ont pas de recto 
                //{
                //    var IfExistRecto = f.Replace("VERSO", "RECTO").Replace("CNIV", "CNI");
                //    if (!pathsFilles.Contains(IfExistRecto))
                //    {
                //        pathsFilles.Add(f);
                //    }
                //}


                // CHECK DOC : 

                foreach (var doc in AllFiles)
                {
                    var fileName = Path.GetFileName(doc);
                    var SecondeDoc = HelperAllFile(AllFiles, doc);
                    var jsonDocument = "";

                    jsonDocument = GetJsonInitObject(doc, "ADDRESS_PROOF"); // test ADDRESS_PROOF
                    jsonDocument = GetJsonInitObject(doc, "LEGAL_ENTITY"); // test LEGAL_ENTITY
                    jsonDocument = GetJsonInitObject(doc); // test LEGAL_ENTITY

                    var watch = new Stopwatch();

                    using (var Client = new HttpClient() { BaseAddress = new Uri("https://api-test.ariadnext.com/gw/cis/") })
                    {
                        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccesToken);
                        var content = new StringContent(jsonDocument, Encoding.UTF8, "application/json");
                        var resultX = Client.PostAsync("rest/v1/demo/document/check", content).Result;
                        watch.Start();
                        var result = Client.PostAsync("rest/v1/demo/document/check?synchronous=true", content).Result;
                        watch.Stop();
                        var responseString = await result.Content.ReadAsStringAsync();
                        ListResponseCreationAndCheckDocument.Add(Tuple.Create(responseString, fileName));
                    }

                    var TempEcouler = watch.Elapsed;
                }
                foreach (var item in ListResponseCreationAndCheckDocument)
                {
                    var BeutyJson = JsonHelper.FormatJson(item.Item1);
                    File.WriteAllText(@"C:\Projects\Test\docTest\" + item.Item2.Replace(".pdf", ".json"), BeutyJson);

                }


                DocToAnalyse docToAnalyse = new DocToAnalyse();
                var jsonToAnalyse = File.ReadAllText(urlJsonToAnalyse);
                docToAnalyse = JsonConvert.DeserializeObject<DocToAnalyse>(jsonToAnalyse);

            }
            catch (Exception e)
            {

                throw;
            }

        }
        //public class IdDocumentToVerify
        //{
        //    public string nom { get}
        //}
        public bool AnalyseJson(DocToAnalyse docToAnalyse , string TypeDocument)
        {
            var result = false;
            switch (TypeDocument)
            {
                case "ID":

                    break;

            }


            return result;
        }



        public string HelperAllFile(string[] AllFile, string url)
        {
            var result = "";
            if (AllFile.Any(url.Contains))
            {
                switch (Path.GetFileName(url).Substring(0, 4)) // premier mots exp : CNIV
                {
                    case "CNI_":
                        result = url.Replace(Path.GetFileName(url), Path.GetFileName(url).Replace("CNI", "CNIV").Replace("RECTO","VERSO"));
                        break;
                    case "CNIV":
                        result = url.Replace(Path.GetFileName(url), Path.GetFileName(url).Replace("CNIV", "CNI").Replace("VERSO", "RECTO"));
                        break;
                }
            }
            return result;
        }
        public string PdfToBase64(string urlPdf)
        {
            var PdfByte = File.ReadAllBytes(urlPdf);
            return Convert.ToBase64String(PdfByte);
        }
        private string GetJsonInitObject(string url ,string TypeDoc = "ID" , string SecondeDoc = "" )
        {
            var RectoBase64 = PdfToBase64(url);
            var VersoBase64 = "";
            if (SecondeDoc != "")
            {
                VersoBase64 = PdfToBase64(url);
            }
            City city = new City() { label = "", value = "" };
            ZipCode zipCode = new ZipCode() { label = "", value = "" };
            Lines lines = new Lines() { label = "", values = new List<string>() };
            AddressData addressData = new AddressData() { lines = lines, city = city, zipCode = zipCode };
            List<string> FirstNameTest = new List<string>() { "" };
            IdentityData IdentitydataTest = new IdentityData() { firstNames = FirstNameTest, lastName = "" };
            List<Person> ListPersones = new List<Person>() { new Person() { identityData = IdentitydataTest /*, addressData=addressData*/ } };
            InfoData inforDataTest = new InfoData() { documentNumber = "2", documentType = "" };
            InputData dataTest = new InputData() { infoData = inforDataTest, persons = ListPersones };
            List<Image> img = new List<Image>() { new Image() { type = "DL", documentPart = DocumentPart.RECTO, data = RectoBase64 } };
            if (VersoBase64 != "")
            {
                img.Add(new Image() { type = "DL", documentPart = DocumentPart.VERSO, data = VersoBase64 });
            }
            RootObject rootObject = new RootObject() { location = "", type = TypeDoc, images = img/*, inputData = dataTest */};
            var jsonDocument = JsonConvert.SerializeObject(rootObject);
            return jsonDocument;
        }

    }

}
