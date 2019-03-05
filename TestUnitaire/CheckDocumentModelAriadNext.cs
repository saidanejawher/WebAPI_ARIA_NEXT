using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnitaire
{

    public class DataReference
    {
        public string givenValue { get; set; }
    }

    public class SubCheck2
    {
        public string identifier { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string documentUid { get; set; }
        public List<DataReference> dataReferences { get; set; }
    }

    public class DataReference2
    {
        public string givenValue { get; set; }
    }

    public class SubCheck
    {
        public string identifier { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string documentUid { get; set; }
        public List<SubCheck2> subChecks { get; set; }
        public List<DataReference2> dataReferences { get; set; }
    }

    public class Check
    {
        public string identifier { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string documentUid { get; set; }
        public List<SubCheck> subChecks { get; set; }
    }

    public class IssueDate
    {
        public string label { get; set; }
        public string value { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    public class IssueDay
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class IssueMonth
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class IssueYear
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class IssuingCountry
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class Issuance
    {
        public IssueDate issueDate { get; set; }
        public IssueDay issueDay { get; set; }
        public IssueMonth issueMonth { get; set; }
        public IssueYear issueYear { get; set; }
        public IssuingCountry issuingCountry { get; set; }
    }

    public class DocumentNumber
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class DocumentType
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class SidesIssue
    {
        public string label { get; set; }
        public string value { get; set; }
        public string valueLabel { get; set; }
    }

    public class ExpirationDate
    {
        public string label { get; set; }
        public string value { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    public class ExpirationDay
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class ExpirationMonth
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class ExpirationYear
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class Extra
    {
        public string key { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }

    public class Info
    {
        public DocumentNumber documentNumber { get; set; }
        public DocumentType documentType { get; set; }
        public SidesIssue sidesIssue { get; set; }
        public ExpirationDate expirationDate { get; set; }
        public ExpirationDay expirationDay { get; set; }
        public ExpirationMonth expirationMonth { get; set; }
        public ExpirationYear expirationYear { get; set; }
        public List<Extra> extra { get; set; }
    }

    public class Role
    {
        public string label { get; set; }
        public string value { get; set; }
        public string valueLabel { get; set; }
    }

    public class LastName
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class FirstNames
    {
        public string label { get; set; }
        public List<string> values { get; set; }
    }

    public class BirthDate
    {
        public string label { get; set; }
        public string value { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    public class BirthDay
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class BirthMonth
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class BirthYear
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class BirthPlace
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class Gender
    {
        public string label { get; set; }
        public string value { get; set; }
        public string valueLabel { get; set; }
    }

    public class Nationality
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class IdentityDataCheck
    {
        public LastName lastName { get; set; }
        public FirstNames firstNames { get; set; }
        public BirthDate birthDate { get; set; }
        public BirthDay birthDay { get; set; }
        public BirthMonth birthMonth { get; set; }
        public BirthYear birthYear { get; set; }
        public BirthPlace birthPlace { get; set; }
        public Gender gender { get; set; }
        public Nationality nationality { get; set; }
    }

    public class PersonCheck
    {
        public Role role { get; set; }
        public IdentityDataCheck identityData { get; set; }
    }

    public class LastReport
    {
        public string uid { get; set; }
        public DateTime generationDate { get; set; }
        public string globalStatus { get; set; }
        public List<Check> checks { get; set; }
        public Issuance issuance { get; set; }
        public Info info { get; set; }
        public List<PersonCheck> persons { get; set; }
        public string backendResultId { get; set; }
    }

    public class Report
    {
        public string uid { get; set; }
        public DateTime generationDate { get; set; }
        public string globalStatus { get; set; }
    }

   
    
    public class IdentityData2
    {
        public string lastName { get; set; }
        public List<string> firstNames { get; set; }
    }

    public class Person2
    {
        public IdentityData2 identityData { get; set; }
    }

  
    public class CreatAnbdCheckDocument
    {
        public string uid { get; set; }
        public string owner { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public LastReport lastReport { get; set; }
        public string lastAnalysisStatus { get; set; }
        public List<Report> reports { get; set; }
        public List<Image> images { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public InputData inputData { get; set; }
    }

}
