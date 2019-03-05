using System;
using Developpez.Dotnet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using iTextSharp.text.pdf;
using Microsoft.VisualStudio.Modeling.Diagrams;
using DocumentFormat.OpenXml.Drawing;

namespace TestUnitaire
{
    [TestClass]
    public class UnitTest1
    {
        private string urlParameters = "?key=sYXLWU2FAYdrGqQPBEYjbRrk3vMu3eLd";

        [TestMethod]
        public void TestMethod1()
        {

            new PdfHelper().GetMap();
            string annee = NumberConverter.Spell(2528.87, 2);
            Console.Write(annee);
            var url = @"http://ws.perial.info/salesforce/preprod/souscription/read.php";
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetStringAsync(urlParameters).Result;
                //response = "{'DossiersUpsideo':[{'Name':'DS-2018-10-00172','Code_Client_UNICIA__c':null,'Date_souscription__c':'2018-10-29','Reference_dossier_Upsideo__c':'1001','Statut_Dossier__c':'En cours de v\u00e9rification'},{'Name':'DS-2017-11-00429','Code_Client_UNICIA__c':'389243','Date_souscription__c':'2017-11-15','Reference_dossier_Upsideo__c':'1019','Statut_Dossier__c':'En cours de v\u00e9rification'},{'Name':'DS-2017-11-00429','Code_Client_UNICIA__c':'389243','Date_souscription__c':'2017-11-15','Reference_dossier_Upsideo__c':'1019','Statut_Dossier__c':'En cours de v\u00e9rification'}]}";
                //response = @"[{'Name':'DS-2017-10-00193','Code_Client_UNICIA__c':'357193','Date_souscription__c':'2017-10-15','Reference_dossier_Upsideo__c':'RENAUD2UPSIDEO','Statut_Dossier__c':'Nouveau'},{'Name':'DS-2018-07-00151','Code_Client_UNICIA__c':null,'Date_souscription__c':'2018-07-09','Reference_dossier_Upsideo__c':'RENAUDCODEUPSIDEO','Statut_Dossier__c':'En attente documents'}]";
                var ManyReponsePerialAPI = JsonConvert.DeserializeObject<ManyDossierUpsideo/*List<DossiersUpsideo>*/>(response);
                // ---------- conversion image to pdf --------------///
                /*var x =*/
                new PdfHelper()./*Instance.*/SaveImageAsPdf(@"C:\Users\j.saidane\Downloads\Capture.JPG", @"C:\Users\j.saidane\Downloads\testConversion.pdf", 1000, false);

            }
            catch (Exception e)
            {

            }
            try
            {
                var colors = new List<Tuple<int, int, int>>();
                colors.Add(Tuple.Create(5, 193, 250));
                colors.Add(Tuple.Create(5, 174, 225));
                colors.Add(Tuple.Create(4, 169, 218));

                foreach(var t in colors)
                {
                    double h, s, l; int r,g,b;
                    ColorHelpers.RgbToHls(t.Item1, t.Item2, t.Item3,out  h, out l, out s);

                    ColorHelpers.HlsToRgb(h, l, s, out r, out g, out b);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class DossiersUpsideo
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Code_Client_UNICIA__c")]
        public string CodeClientUNICIAc { get; set; }
        [JsonProperty("Date_souscription__c")]
        public string Datesouscriptionc { get; set; }
        [JsonProperty("Reference_dossier_Upsideo__c")]
        public string ReferencedossierUpsideoc { get; set; }
        [JsonProperty("Statut_Dossier__c")]
        public string StatutDossierc { get; set; }
    }

    public class ManyDossierUpsideo
    {
        public List<DossiersUpsideo> DossiersUpsideo { get; set; }
    }

    public static class ColorHelpers
    {

        public static void RgbToHls(int r, int g, int b, out double h, out double l, out double s)
        {
            // Convert RGB to a 0.0 to 1.0 range.
            double double_r = r / 255.0;
            double double_g = g / 255.0;
            double double_b = b / 255.0;

            // Get the maximum and minimum RGB components.
            double max = double_r;
            if (max < double_g) max = double_g;
            if (max < double_b) max = double_b;

            double min = double_r;
            if (min > double_g) min = double_g;
            if (min > double_b) min = double_b;

            double diff = max - min;
            l = (max + min) / 2;
            if (Math.Abs(diff) < 0.00001)
            {
                s = 0;
                h = 0;  // H is really undefined.
            }
            else
            {
                if (l <= 0.5) s = diff / (max + min);
                else s = diff / (2 - max - min);

                double r_dist = (max - double_r) / diff;
                double g_dist = (max - double_g) / diff;
                double b_dist = (max - double_b) / diff;

                if (double_r == max) h = b_dist - g_dist;
                else if (double_g == max) h = 2 + r_dist - b_dist;
                else h = 4 + g_dist - r_dist;

                h = h * 60;
                if (h < 0) h += 360;
            }
        }

        // Convert an HLS value into an RGB value.
        public static void HlsToRgb(double h, double l, double s,
            out int r, out int g, out int b)
        {
            double p2;
            if (l <= 0.5) p2 = l * (1 + s);
            else p2 = l + s - l * s;

            double p1 = 2 * l - p2;
            double double_r, double_g, double_b;
            if (s == 0)
            {
                double_r = l;
                double_g = l;
                double_b = l;
            }
            else
            {
                double_r = QqhToRgb(p1, p2, h + 120);
                double_g = QqhToRgb(p1, p2, h);
                double_b = QqhToRgb(p1, p2, h - 120);
            }

            // Convert RGB to the 0 to 255 range.
            r = (int)(double_r * 255.0);
            g = (int)(double_g * 255.0);
            b = (int)(double_b * 255.0);
        }

        private static double QqhToRgb(double q1, double q2, double hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }
    }
}
