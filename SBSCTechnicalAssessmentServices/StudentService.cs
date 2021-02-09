using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using SBSCTechnicalAssessmentData.Models;
using SBSCTechnicalAssessmentData.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using SBSCTechnicalAssessmentData;
using RestSharp;

namespace SBSCTechnicalAssessmentServices
{
    public class StudentService : IStudentService
    {
        private readonly SBSCDataContext _context;

        public StudentService(SBSCDataContext context)
        {
            _context = context;
        }

        public bool CreateStudent(string name, string familyName, string address, string countryOfOrigin, string emailAddress, int age, bool isApproved, out string message)
        {
            bool result = false;
            message = String.Empty;

            try
            {
                if (String.IsNullOrWhiteSpace(name))
                {
                    message = "Student Name is required.";
                    return result;
                }

                if (name.Length < 5)
                {
                    message = "Student Name must be at least 5 characters.";
                    return result;
                }

                if (String.IsNullOrWhiteSpace(familyName))
                {
                    message = "Student Family Name is required.";
                    return result;
                }

                if (familyName.Length < 5)
                {
                    message = "Student Family Name must be at least 5 characters.";
                    return result;
                }

                if (String.IsNullOrWhiteSpace(address))
                {
                    message = "Student Address is required.";
                    return result;
                }

                if (address.Length < 10)
                {
                    message = "Student Address must be at least 10 characters.";
                    return result;
                }

                if (String.IsNullOrWhiteSpace(emailAddress))
                {
                    message = "Student Email Address Is Required.";
                    return result;
                }

                if (CheckIfEmailAlreadyExists(emailAddress))
                {
                    message = "Apologies, an account with this email already exists.";
                    return result;
                }

                if (String.IsNullOrWhiteSpace(countryOfOrigin))
                {
                    message = "Student Country of origin is required";
                    return result;
                }

                if (!ValidateCountryOfOriginExists(countryOfOrigin, out string innerErr))
                {
                    message = "Kindly Input a valid country. " + countryOfOrigin + " is not a country. [" + innerErr + "]";
                    return result;
                }

                if (age < 18 || age > 25)
                {
                    message = "Student Age must be between 18 and 25.";
                    return result;
                }

                Student student = new Student()
                {
                    Name = name,
                    FamilyName = familyName,
                    Address = address,
                    Age = age,
                    Approved = isApproved,
                    EmailAddress = emailAddress,
                    CountryOfOrigin = countryOfOrigin,
                };

                if (!student.IsValidEmail)
                {
                    message = "Email Address is not valid";
                    return result;
                }

                _context.Students.Add(student);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception error)
            {
                message = error.Message;
                return result;
            }

            return result;
        }
        public Student GetStudentByEmail(string emailAddress, out string message)
        {
            Student result = null;
            message = String.Empty;

            try
            {
                if (String.IsNullOrWhiteSpace(emailAddress))
                {
                    message = "Invalid Student Email Address";
                    return result;
                }

                Student student = _context.Students.FirstOrDefault(x => x.EmailAddress == emailAddress);
                if (student == null)
                {
                    message = "Error, No Student with this ID (" + student + ") exists.";
                    return result;
                }

                result = student;
            }
            catch (Exception error)
            {
                message = error.Message;
                result = null;
            }

            return result;
        }

        public Student GetStudentByID(long studentId, out string message)
        {
            Student result = null;
            message = String.Empty;

            try
            {
                if(studentId <= 0)
                {
                    message = "Invalid Student ID";
                    return result;
                }

                Student student = _context.Students.FirstOrDefault(x => x.Id == studentId);
                if(student == null)
                {
                    message = "Error, No Student with this ID (" + student + ") exists.";
                    return result;
                }

                result = student;
            }
            catch(Exception error)
            {
                message = error.Message;
                result = null;
            }

            return result;
        }

        public bool RemoveStudent(long studentId, out string message)
        {
            bool result = false;
            message = String.Empty;

            try
            {
                if (studentId <= 0)
                {
                    message = "Invalid Student ID";
                    return result;
                }

                Student student = _context.Students.FirstOrDefault(x => x.Id == studentId);
                if (student == null)
                {
                    message = "Error, No Student with this ID (" + student + ") exists.";
                    return result;
                }

                _context.Students.Remove(student);
                _context.SaveChanges();
                result = true;
            }
            catch(Exception error)
            {
                message = error.Message;
                return result;
            }

            return result;
        }

        public bool UpdateStudent(long studentId, Student studentInfo, out string message)
        {
            bool result = false;
            message = String.Empty;

            try
            {
                if (studentInfo == null)
                {
                    message = "Invalid Student Object Passed";
                    return result;
                }

                //Check If Student Object Exists in DB
                Student student = _context.Students.FirstOrDefault(x => x.Id == studentId);
                if(student == null)
                {
                    message = "No Student record with the specified student ID exists.";
                    return result;
                }

                if (!String.IsNullOrWhiteSpace(studentInfo.Name))
                {
                    if (studentInfo.Name.Length < 5)
                    {
                        message = "Student Name must be at least 5 characters.";
                        return result;
                    }
                    student.Name = studentInfo.Name;
                }

                if (!String.IsNullOrWhiteSpace(studentInfo.FamilyName))
                {
                    if (studentInfo.FamilyName.Length < 5)
                    {
                        message = "Student Family Name must be at least 5 characters.";
                        return result;
                    }
                    student.FamilyName = studentInfo.FamilyName;
                }

                if (!String.IsNullOrWhiteSpace(studentInfo.EmailAddress))
                {
                    if (!studentInfo.IsValidEmail)
                    {
                        message = "Email Address is not valid";
                        return result;
                    }

                    if (CheckIfEmailAlreadyExists(studentInfo.EmailAddress) && studentInfo.EmailAddress != student.EmailAddress)
                    {
                        message = "Apologies, an account with this email already exists.";
                        return result;
                    }
                    student.EmailAddress = studentInfo.EmailAddress;
                }

                if (!String.IsNullOrWhiteSpace(studentInfo.Address))
                {
                    if (studentInfo.Address.Length < 10)
                    {
                        message = "Student Address must be at least 10 characters.";
                        return result;
                    }
                    student.Address = studentInfo.Address;
                }

                if (!String.IsNullOrWhiteSpace(studentInfo.CountryOfOrigin))
                {
                    if (!ValidateCountryOfOriginExists(studentInfo.CountryOfOrigin, out string innerErr))
                    {
                        message = "Kindly Input a valid country. " + studentInfo.CountryOfOrigin + " is not a country. [" + innerErr + "]";
                        return result;
                    }
                    student.CountryOfOrigin = studentInfo.CountryOfOrigin;
                }

                if(studentInfo.Age > 0)
                {
                    if (studentInfo.Age < 18 || studentInfo.Age > 25)
                    {
                        message = "Student Age must be between 18 and 25.";
                        return result;
                    }
                    student.Age = studentInfo.Age;
                }

                _context.Students.Update(student);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception error)
            {
                message = error.Message;
                return result;
            }

            return result;
        }

        private bool ValidateCountryOfOriginExists(string countryOfOrigin, out string errMsg)
        {
            bool result = false;
            errMsg = String.Empty;

            try
            {
                string _baseUrl = "https://restcountries.eu";

                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var client = new RestClient(_baseUrl);
                var enquiryRequest = new RestRequest("rest/v2/name/" + countryOfOrigin + "?fullText=true", Method.GET);
                enquiryRequest.AddHeader("Content-Type", "application/json");
                enquiryRequest.AddHeader("Accept", "application/json");
                IRestResponse<string> response = client.Execute<string>(enquiryRequest);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = response.Content != null;
                }
                else
                {
                    result = false;
                }
            } 
            catch (Exception error) {
                errMsg = error.Message;
            }

            return result;
        }

        private bool CheckIfEmailAlreadyExists(string emailAddress)
        {
            return _context.Students.Any(x => x.EmailAddress == emailAddress);
        }
    }
}
