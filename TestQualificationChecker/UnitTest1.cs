using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QualificationChecker.Controllers;
using QualificationChecker.Models;
using System.Collections.Generic;
using static QualificationChecker.Controllers.CandidatesController;

namespace QualificationChecker.Tests
{
    [TestClass]
    public class CandidatesControllerTests
    {
        private object logger;

        [TestMethod]
        public void ShowQualifiedCandidates_ReturnsQualifiedCandidates()
        {
            // Arrange
            
            var controller = new CandidatesController((ILogger<CandidatesController>)logger);

            var request = new MatchRequest
            {
                organization = new Organization
                {
                    orgId = 1,
                    ageRequirement = 25,
                    orgQuestions = new List<orgQuestion>
                    {
                        new orgQuestion { id = 1, positionId = 1, questionText = "Question 1", answer = true },
                        new orgQuestion { id = 2, positionId = 1, questionText = "Question 2", answer = true },
                    }
                },
                candidates = new List<Candidate>
                {
                    new Candidate
                    {
                        id = 1,
                        fullName = "John Doe",
                        age = 30,
                        organizationId = 1,
                        interestedPositionIds = new List<int> { 1, 2 },
                        candidateQuestions = new List<candidateQuestion>
                        {
                            new candidateQuestion { id = 1, positionId = 1, questionText = "Question 1", answer = true },
                            new candidateQuestion { id = 2, positionId = 1, questionText = "Question 2", answer = true },
                        }
                    },
                    new Candidate
                    {
                        id = 2,
                        fullName = "Jane Smith",
                        age = 27,
                        organizationId = 1,
                        interestedPositionIds = new List<int> { 1 },
                        candidateQuestions = new List<candidateQuestion>
                        {
                            new candidateQuestion { id = 1, positionId = 1, questionText = "Question 1", answer = true },
                            new candidateQuestion { id = 2, positionId = 1, questionText = "Question 2", answer = false },
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
