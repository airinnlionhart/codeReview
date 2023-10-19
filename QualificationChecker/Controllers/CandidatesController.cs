using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QualificationChecker.Models;
using System.Collections.Generic;
using System.Linq;

namespace QualificationChecker.Controllers
{
    [ApiController]
    [Route("api/qualifiedcandidates")]
    public partial class CandidatesController : ControllerBase
    {
        private readonly ILogger<CandidatesController> _logger;

        public CandidatesController(ILogger<CandidatesController> logger)
        {
            // Cause I love logging
            _logger = logger;
        }


        [HttpPost("showQualified/")]
        public IActionResult ShowQualifiedCandidates([FromBody] MatchRequest request)
        {
            // Initialize a list to store qualified candidates
            List<Candidate> qualifiedCandidates = new List<Candidate>();

            //Loop through each candaite and compare answers
            foreach (var candidate in request.candidates)
            {
                // knock out questions
                bool isInterested = candidate.interestedPositionIds.Any(candidatePositionId =>
                    request.organization.orgQuestions.Any(orgQuestion => orgQuestion.positionId == candidatePositionId));

                //know out questions
                bool meetsAgeRequirements = candidate.age >= request.organization.ageRequirement;

                

                // only run this if they pass the knockout questions
                if (candidate.organizationId == request.organization.orgId && isInterested && meetsAgeRequirements)
                {
                    // Create a dictionary to store organization answers by PositionId and Id
                    var orgAnswersDictionary = request.organization.orgQuestions
                        .GroupBy(q => q.positionId)
                        .ToDictionary(group => group.Key, group => group.ToList());

                    // Initialize a flag to track if all answers match
                    bool allAnswersMatch = true;
                    
                    // Loop through each candidate question
                    foreach (var candidateAnswer in candidate.candidateQuestions)
                    {
                        // Create a lookup key to match the PositionId
                        var lookupKey = candidateAnswer.positionId;

                        if (orgAnswersDictionary.TryGetValue(lookupKey, out var orgAnswers))
                        {
                            // Loop through the orgAnswers (in case there are multiple with the same PositionId)
                            bool answersMatch = false;
                            

                            foreach (var orgAnswer in orgAnswers)
                            {
                                // make sure they have the same number first before checking answers
                                if (orgAnswers.Count != candidate.candidateQuestions.Count)
                                {
                                    answersMatch = false;
                                    break;
                                }

                                if (!MatchAnswers(candidateAnswer.answer, orgAnswer.answer))
                                {
                                    // If an answer doesn't match or orgAnswer is null, set answersMatch to false
                                    answersMatch = false;
                                    break;
                                }
                                answersMatch = true;
                            }

                            if (!answersMatch)
                            {
                                // If no orgAnswer matches, set the flag to false and break the loop
                                allAnswersMatch = false;
                                break;
                            }
                        }
                        else
                        {
                            // If no orgAnswer is found for this PositionId, set the flag to false and break the loop
                            allAnswersMatch = false;
                            break;
                        }
                    }

                    if (allAnswersMatch)
                    {
                        // Add the candidate to the list of qualified candidates
                        qualifiedCandidates.Add(candidate);
                    }
                }
            }

            // Return the list of qualified candidates
            return Ok(qualifiedCandidates);
        }

        private bool MatchAnswers(bool candidateAnswers, bool organizationAnswers)
        {
            // Compare each answer
            return candidateAnswers == organizationAnswers;
        }
    }
}
