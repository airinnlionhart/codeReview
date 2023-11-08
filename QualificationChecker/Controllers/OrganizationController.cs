using Microsoft.AspNetCore.Mvc;
using QualificationChecker.Models;

namespace QualificationChecker.Controllers
{
    [ApiController]
    [Route("api/qualifiedcandidates")]
    public partial class OrganizationController : ControllerBase
    {
        [HttpPost("showQualified/")]
        public IActionResult ShowQualifiedCandidates([FromBody] MatchRequest request)
        {
            if (request.Candidates == null)
            {
                return Ok("No Candidate at this time");
            }

            if (request.Organization?.OrgQuestions == null)
            {
                return Ok("Your organization does not have any Qualification questions; please add them.");
            }


            List<Candidate> qualifiedCandidates = new List<Candidate>();

            foreach (var candidate in request.Candidates)
            {
                var myqualifiedCandidates = new CandidateQualificationEvaluator(candidate, request.Organization);
                myqualifiedCandidates.FindQualifiedCandidates(candidate);
                qualifiedCandidates.AddRange(myqualifiedCandidates.ReturnListOfCandidate());
            }

            if (qualifiedCandidates.Count >= 1)
            {
                return Ok(qualifiedCandidates);
            }
            else
            {
                return Ok("No Matching Candidates");
            }


        }

    }
}

