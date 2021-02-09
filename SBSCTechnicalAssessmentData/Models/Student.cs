using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SBSCTechnicalAssessmentData.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Approved { get; set; }
    }
}
