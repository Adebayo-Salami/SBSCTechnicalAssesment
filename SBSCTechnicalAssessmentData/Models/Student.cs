using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SBSCTechnicalAssessmentData.Models
{
    public class Student
    {
        public int Id { get; set; }
        //[MinLength(5)]
        public string Name { get; set; }
        //[MinLength(5)]
        public string FamilyName { get; set; }
        //[MinLength(10)]
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Approved { get; set; }
        public bool IsValidEmail
        {
            get
            {
                return new EmailAddressAttribute().IsValid(this.EmailAddress);
            }
        }
    }
}
