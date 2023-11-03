using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QualificationChecker.Models;
using QualificationChecker.Controllers;

namespace QualificationChecker.Controllers
{
    [ApiController]
    [Route("api/qualifiedcandidates")]
    public partial class CandidatesController : ControllerBase
    {

        [HttpPost("showQualified/")]
        public IActionResult ShowQualifiedCandidates([FromBody] MatchRequest request)
        {
            var QualifiedCandidates = new List<Candidate>();
            var stopwatch = Stopwatch.StartNew();
            try
            {
                if (request.Candidates == null)
                {
                    return Ok("No Candidate at this time you can try adding more budget");
                }
                else if (request.Organization?.OrgQuestions == null)
                {
                    return Ok("Your organization does not have any Qualification questions please add them");
                }

                else
                {
                    //Loop through each candaite and compare answers
                    foreach (var candidate in request.Candidates)

                    {   // Initialize you Candidate to work with utilities
                        var myCandidate = new CandidateUtilities(candidate, request.Organization);


                        // only run this if they pass the knockout questions
                        if (myCandidate.Candidate.OrganizationId == myCandidate.Organization.OrgId && myCandidate.IsInterested() && myCandidate.MeetsAgeRequirement())
                        {

                            // Check to make sure postionId makes a dictionary 
                            if (myCandidate.OrgAnswersDictionary != null)
                            {
                                QualifiedCandidates.AddRange(myCandidate.CheckQuestions());
                            }
                        }
                    }
                    if (QualifiedCandidates.Count >= 1)
                    { // Return the list of qualified candidates
                        return Ok(QualifiedCandidates);
                    }
                    else
                    {
                        return Ok("No Matching Canidates");
                    }
                }
            }
            finally
            {
                stopwatch.Stop();

                // Log the execution time
                var executionTimeMs = stopwatch.ElapsedMilliseconds;
                // Capture a stack trace
                var stackTrace = new StackTrace();

                Console.Write(executionTimeMs);
                Console.Write(stackTrace);
            }
        }


    }
}
