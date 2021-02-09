using Xunit;
using Moq;
using SBSCTechnicalAssessmentServices;
using SBSCTechnicalAssessmentData;

namespace SBSCTechnicalAssessmentTest
{
    public class StudentServiceTest
    {
        [Fact]
        public void Test_InvalidStudent_ID()
        {
            var dataContext = new Mock<SBSCDataContext>();
            var studentService = new StudentService(dataContext.Object);

            Assert.Null(studentService.GetStudentByID(0, out string msg));
        }
    }
}
