using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnitaire
{
    public class InfoData
    {
        public string documentNumber { get; set; }
        public string documentType { get; set; }
    }

    public class IdentityData
    {
        public string lastName { get; set; }
        public List<string> firstNames { get; set; }
    }
    public class Lines
    {
        public string label { get; set; }
        public List<string> values { get; set; }
    }

    public class ZipCode 
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class City
    {
        public string label { get; set; }
        public string value { get; set; }
    }
    public class AddressData
    {
        public Lines lines { get; set; }
        public ZipCode zipCode { get; set; }
        public City city { get; set; }
    }
    public class Person
    {
        public IdentityData identityData { get; set; }
        public AddressData addressData { get; set; }
    }

    public class InputData
    {
        public InfoData infoData { get; set; }
        public List<Person> persons { get; set; }
    }

    //public class Image
    //{
    //    public string data { get; set; }
    //    public string documentPart { get; set; }
    //    public string type { get; set; }
    //    public string uid { get; set; }
    //    public string source { get; set; }
    //}
    public class Image
    {
        public string data { get; set; }
        public DocumentPart documentPart { get; set; }
        public string type { get; set; }
        public string uid { get; set; }
        public string source { get; set; }
    }

    public enum typeImage
    {
        DL,IR,UV
    }
    public enum SourceImage
    {
        ORIGINAL,CROPPED
    }
    public enum DocumentPart
    {
       RECTO,VERSO,OTHER
    }
    public class RootObject
    {
        public string location { get; set; }
        public string type { get; set; }
        public InputData inputData { get; set; }
        public List<Image> images { get; set; }
    }
    public class AuthentificationResultat
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string id_token { get; set; }
        public int not_before_policy { get; set; }
        public string session_state { get; set; }
    }

    // Object Create Document 
    public class ResponseCreateDocument
    {
        public string uid { get; set; }
        public string status { get; set; }
        public string issuerUid { get; set; }
        public string issuerType { get; set; }
    }

    // Object Get Document By uid
    public class ResponseEachDocument
    {
        public string uid { get; set; }
        public string owner { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public List<Image> images { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public InputData inputData { get; set; }
    }

    // Searching Document 
    public class Document
    {
        public string uid { get; set; }
        public string type { get; set; }
        public string lastReportStatus { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string owner { get; set; }
    }

    public class SearchAllDocument
    {
        public List<Document> rows { get; set; }
        public int total { get; set; }
    }


}
