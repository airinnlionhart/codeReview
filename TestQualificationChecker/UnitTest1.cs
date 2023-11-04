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
                    MinAgeRequirement = 18,
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
                        FullName = "Should ShowMoe",
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
                        FullName = "WillShow Smith",
                        Age = 27,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = true },
                        }
                    },
                    new Candidate
                    {
                        Id = 3,
                        FullName = "To YoungChris",
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
                        Id = 4,
                        FullName = "Miss QuestionSam",
                        Age = 27,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 1, QuestionText = "Question 2", Answer = false },
                        }
                    },
                     new Candidate
                    {
                        Id = 5,
                        FullName = "One QuestionGeorge",
                        Age = 27,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 1 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 1, QuestionText = "Question 1", Answer = true },
                        }
                    },
                      new Candidate
                    {
                        Id = 6,
                        FullName = "Not IntrestedPaul",
                        Age = 27,
                        OrganizationId = 1,
                        InterestedPositionIds = new List<int> { 2 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 2, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 2, QuestionText = "Question 2", Answer = true },
                        }
                    },
                        new Candidate
                    {
                        Id = 7,
                        FullName = "Wrong OrgJane",
                        Age = 27,
                        OrganizationId = 2,
                        InterestedPositionIds = new List<int> { 1 },
                        CandidateQuestions = new List<CandidateQuestion>
                        {
                            new CandidateQuestion { Id = 1, PositionId = 2, QuestionText = "Question 1", Answer = true },
                            new CandidateQuestion { Id = 2, PositionId = 2, QuestionText = "Question 2", Answer = true },
                        }
                    },
                }
            };

            // Act
            var result = controller.ShowQualifiedCandidates(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var qualifiedCandidates = result.Value as List<Candidate>;
            Assert.IsNotNull(qualifiedCandidates);
            Assert.AreEqual(2, qualifiedCandidates.Count); // Expects only 2 Candidates
            
        }

    }

}
