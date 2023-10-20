using Microsoft.AspNetCore.Mvc;
using QualificationChecker.Controllers;
using QualificationChecker.Models;
using static QualificationChecker.Controllers.CandidatesController;

namespace QualificationChecker.Tests
{
    [TestClass]
    public class CandidatesControllerTests
    {

        [TestMethod]
        public void ShowQualifiedCandidates_ReturnsQualifiedCandidates()
        {
            // Arrange

            CandidatesController controller = new();

            var request = new MatchRequest
            {
                Organization = new Organization
                {
                    OrgId = 1,
                    AgeRequirement = 25,
                    OrgQuestions = new List<OrgQuestion>
                    {
                        new OrgQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                        new OrgQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                    }
                },
                Candidates = new List<Candidate>
                {
                    new Candidate
                    {
                        Id = 1,
                        FullName = "John Doe",
                        Age = 30,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1, 2 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                        }
                    },
                    new Candidate
                    {
                        Id = 2,
                        FullName = "Jane Smith",
                        Age = 27,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                        }
                    }
                }
            };

            // Act
            var result = controller.ShowQualifiedCandidates(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var qualifiedCandidates = result.Value as List<Candidate>;
            Assert.IsNotNull(qualifiedCandidates);
            Assert.AreEqual(2, qualifiedCandidates.Count); // Expect only the first candidate to be qualified
        }

        [TestMethod]
        public void ShowQualifiedCandidates_ReturnsNoQualifiedCandidatesAge()
        {
            // Arrange

            CandidatesController controller = new();

            var request = new MatchRequest
            {
                Organization = new Organization
                {
                    OrgId = 1,
                    AgeRequirement = 18,
                    OrgQuestions = new List<OrgQuestion>
                    {
                        new OrgQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                        new OrgQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                    }
                },
                Candidates = new List<Candidate>
                {
                    new Candidate
                    {
                        Id = 1,
                        FullName = "John Doe",
                        Age = 16,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1, 2 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                        }
                    },
                    new Candidate
                    {
                        Id = 2,
                        FullName = "Jane Smith",
                        Age = 27,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = false },
                        }
                    }
                }
            };

            // Act
            ObjectResult? result = controller.ShowQualifiedCandidates(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var qualifiedCandidates = result.Value as List<Candidate>;
            Assert.AreEqual(null, qualifiedCandidates?.Count); // Expect only the first candidate to be qualified
        }

        [TestMethod]
        public void ShowQualifiedCandidates_ReturnsCandidateJane()
        {
            // Arrange

            CandidatesController controller = new();

            var request = new MatchRequest
            {
                Organization = new Organization
                {
                    OrgId = 1,
                    AgeRequirement = 25,
                    OrgQuestions = new List<OrgQuestion>
                    {
                        new OrgQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                        new OrgQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                    }
                },
                Candidates = new List<Candidate>
                {
                    new Candidate
                    {
                        Id = 1,
                        FullName = "John Doe",
                        Age = 30,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1, 2 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = false },
                        }
                    },
                    new Candidate
                    {
                        Id = 2,
                        FullName = "Jane Smith",
                        Age = 27,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                        }
                    }
                }
            };

            // Act
            var result = controller.ShowQualifiedCandidates(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var qualifiedCandidates = result.Value as List<Candidate>;
            Assert.AreEqual(1, qualifiedCandidates?.Count);// Expects Jance
            Assert.AreEqual("Jane Smith", qualifiedCandidates?[0].FullName);
        }

        [TestMethod]
        public void ShowQualifiedCandidates_ReturnsNoQualifiedCandidatesListDoNotMatch()
        {
            // Arrange

            CandidatesController controller = new();

            var request = new MatchRequest
            {
                Organization = new Organization
                {
                    OrgId = 1,
                    AgeRequirement = 25,
                    OrgQuestions = new List<OrgQuestion>
                    {
                        new OrgQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                        new OrgQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                    }
                },
                Candidates = new List<Candidate>
                {
                    new Candidate
                    {
                        Id = 1,
                        FullName = "John Doe",
                        Age = 30,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1, 2 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                        }
                    },
                    new Candidate
                    {
                        Id = 2,
                        FullName = "Jane Smith",
                        Age = 27,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                        }
                    }
                }
            };

            // Act
            var result = controller.ShowQualifiedCandidates(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var qualifiedCandidates = result.Value as List<Candidate>;
            Assert.IsNotNull(qualifiedCandidates);
            Assert.AreEqual(1, qualifiedCandidates.Count); // Expect only the first candidate to be qualified
        }
    }
}
