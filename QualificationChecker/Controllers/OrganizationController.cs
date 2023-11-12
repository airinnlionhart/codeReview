using Microsoft.AspNetCore.Mvc;

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

            var getCandidates = new CandidateQualificationEvaluator(request.Candidates, request.Organization).ReturnQualifiedCandidates();
            return Ok(getCandidates);

        }

    }
}

