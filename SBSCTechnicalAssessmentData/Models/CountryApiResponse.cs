using System;
using System.Collections.Generic;
using System.Text;

namespace SBSCTechnicalAssessmentData.Models
{
    public class CountryApiResponse
    {
        public CountryDetails[] CountryDetails { get; set; }
    }

    public class CountryDetails
    {
        public string name { get; set; }
        public string alpha3Code { get; set; }
        public string capital { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public string numericCode { get; set; }
        public string nativeName { get; set; }
        public string cioc { get; set; }
    }
}
