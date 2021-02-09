using SBSCTechnicalAssessmentData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBSCTechnicalAssessmentData.Interfaces
{
    public interface IStudentService
    {
        bool CreateStudent(string name, string familyName, string address, string countryOfOrigin, string emailAddress, int age, bool isApproved, out string message);
        bool UpdateStudent(long studentId, Student studentInfo, out string message);
        Student GetStudentByID(long studentId, out string message);
        Student GetStudentByEmail(string emailAddress, out string message);
        bool RemoveStudent(long studentId, out string message);
    }
}
