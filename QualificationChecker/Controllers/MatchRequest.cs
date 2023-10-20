using QualificationChecker.Models;

namespace QualificationChecker.Controllers
{
    public partial class CandidatesController
    {
        public class MatchRequest
        {
            public Organization Organization { get; set; }
            public List<Candidate> Candidates { get; set; }
        }
    }


}
