using Xunit;
using Moq;
using SBSCTechnicalAssessmentServices;
using SBSCTechnicalAssessmentData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SBSCTechnicalAssessmentTest
{
    public class StudentServiceTest
    {
        [Fact]
        public void Name_Not_LessThan_5Characters()
        {
            var options = new Mock<DbContextOptions>();
            SBSCDataContext context = new SBSCDataContext(options.Object);
            var studentService = new StudentService(context);

            SBSCTechnicalAssessmentData.Models.Student studentTest = new SBSCTechnicalAssessmentData.Models.Student()
            {
                Name = "Test",
                Address = "Address SBSC",
                Age = 23,
                Approved = true,
                CountryOfOrigin = "Nigeria",
                EmailAddress = "test@test.com",
                FamilyName = "Test Family Name SBSC",
            };

            Assert.False(studentService.CreateStudent(studentTest.Name, studentTest.FamilyName, studentTest.Address, studentTest.CountryOfOrigin, studentTest.EmailAddress, studentTest.Age, studentTest.Approved, out string msg));
        }

        [Fact]
        public void FamilyName_Not_LessThan_5Characters()
        {
            var mockedDataContext = new Mock<SBSCDataContext>();
            var studentService = new StudentService(mockedDataContext.Object);

            SBSCTechnicalAssessmentData.Models.Student studentTest = new SBSCTechnicalAssessmentData.Models.Student()
            {
                Name = "Test 1",
                Address = "Address SBSC",
                Age = 23,
                Approved = true,
                CountryOfOrigin = "Nigeria",
                EmailAddress = "test@test.com",
                FamilyName = "Test",
            };

            Assert.False(studentService.CreateStudent(studentTest.Name, studentTest.FamilyName, studentTest.Address, studentTest.CountryOfOrigin, studentTest.EmailAddress, studentTest.Age, studentTest.Approved, out string msg));
        }

        [Fact]
        public void Address_Not_LessThan_10Characters()
        {
            var mockedDataContext = new Mock<SBSCDataContext>();
            var studentService = new StudentService(mockedDataContext.Object);

            SBSCTechnicalAssessmentData.Models.Student studentTest = new SBSCTechnicalAssessmentData.Models.Student()
            {
                Name = "Test 1",
                Address = "Address",
                Age = 23,
                Approved = true,
                CountryOfOrigin = "Nigeria",
                EmailAddress = "test@test.com",
                FamilyName = "Test Family Name SBSC",
            };

            Assert.False(studentService.CreateStudent(studentTest.Name, studentTest.FamilyName, studentTest.Address, studentTest.CountryOfOrigin, studentTest.EmailAddress, studentTest.Age, studentTest.Approved, out string msg));
        }

        [Fact]
        public void Country_Of_Origin_IsValid()
        {
            var mockedDataContext = new Mock<SBSCDataContext>();
            var studentService = new StudentService(mockedDataContext.Object);

            SBSCTechnicalAssessmentData.Models.Student studentTest = new SBSCTechnicalAssessmentData.Models.Student()
            {
                Name = "Test 1",
                Address = "Address SBSC",
                Age = 23,
                Approved = true,
                CountryOfOrigin = "bag",
                EmailAddress = "test@test.com",
                FamilyName = "Test Family Name SBSC",
            };

            Assert.False(studentService.CreateStudent(studentTest.Name, studentTest.FamilyName, studentTest.Address, studentTest.CountryOfOrigin, studentTest.EmailAddress, studentTest.Age, studentTest.Approved, out string msg));
        }

        [Fact]
        public void Age_Between_18_And_25()
        {
            var mockedDataContext = new Mock<SBSCDataContext>();
            var studentService = new StudentService(mockedDataContext.Object);

            SBSCTechnicalAssessmentData.Models.Student studentTest = new SBSCTechnicalAssessmentData.Models.Student()
            {
                Name = "Test 1",
                Address = "Address SBSC",
                Age = 10,
                Approved = true,
                CountryOfOrigin = "bag",
                EmailAddress = "test@test.com",
                FamilyName = "Test Family Name SBSC",
            };

            Assert.False(studentService.CreateStudent(studentTest.Name, studentTest.FamilyName, studentTest.Address, studentTest.CountryOfOrigin, studentTest.EmailAddress, studentTest.Age, studentTest.Approved, out string msg));
        }

    }
}
