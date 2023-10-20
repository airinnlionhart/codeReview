using Microsoft.AspNetCore.Mvc;
using QualificationChecker.Models;

namespace QualificationChecker.Controllers
{
    [ApiController]
    [Route("api/qualifiedcandidates")]
    public partial class CandidatesController : ControllerBase
    {

        [HttpPost("showQualified/")]
        public IActionResult ShowQualifiedCandidates([FromBody] MatchRequest request)
        {
            // Initialize a list to store qualified candidates
            List<Candidate> qualifiedCandidates = new List<Candidate>();

            //Loop through each candaite and compare answers
            foreach (var candidate in request.Candidates)
            {
                // knock out questions
#pragma warning disable CS8604 // Possible null reference argument.
                bool isInterested = candidate.InterestedPositionIds.Any(candidatePositionId =>
                    request.Organization.OrgQuestions.Any(OrgQuestion => OrgQuestion.PositionId == candidatePositionId));
#pragma warning restore CS8604 // Possible null reference argument.

                //knock out questions
                bool meetsAgeRequirements = candidate.Age >= request.Organization.AgeRequirement;



                // only run this if they pass the knockout questions
                if (candidate.OrganizationId == request.Organization.OrgId && isInterested && meetsAgeRequirements)
                {
                    // Create a dictionary to store organization answers by PositionId and Id
                    var orgAnswersDictionary = request.Organization.OrgQuestions
                        .GroupBy(q => q.PositionId)
                        .ToDictionary(group => group.Key, group => group.ToList());

                    // Initialize a flag to track if all answers match
                    bool allAnswersMatch = true;

                    // Loop through each candidate question
                    foreach (var candidateAnswer in candidate.CandidateQuestions)
                    {
                        // Create a lookup key to match the PositionId
                        var lookupKey = candidateAnswer.PositionId;

                        if (orgAnswersDictionary.TryGetValue(lookupKey, out var OrgAnswers))
                        {
                            // Loop through the orgAnswers (in case there are multiple with the same PositionId)
                            bool answersMatch = false;


                            foreach (var OrgAnswer in OrgAnswers)
                            {
                                // make sure they have the same number first before checking answers
                                if (OrgAnswers.Count != candidate.CandidateQuestions.Count)
                                {
                                    answersMatch = false;
                                    break;
                                }

                                if (!MatchAnswers(candidateAnswer.Answer, OrgAnswer.Answer))
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
            if (qualifiedCandidates.Count >= 1)
            { // Return the list of qualified candidates
                return Ok(qualifiedCandidates);
            }
            else
            {
                return Ok("No Matching Canidates");
            }
        }

        private bool MatchAnswers(bool candidateAnswers, bool organizationAnswers)
        {
            // Compare each answer
            return candidateAnswers == organizationAnswers;
        }
    }
}
