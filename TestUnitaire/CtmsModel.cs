using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Newtonsoft.Json;

namespace TestUnitaire
{
    public class AuthentificateCTMS
    {
            public long expiresIn { get; set; }
            public string login { get; set; }
            public string accessToken { get; set; }
            public long expiresAfter { get; set; }
    }
    public class FolderModel
    {
        public string reference { get; set; }
        public int idDossier { get; set; }
        public List<object> documents { get; set; }
        public object statut { get; set; }
        public object libelleStatut { get; set; }
        public bool cloture { get; set; }
    }
}
