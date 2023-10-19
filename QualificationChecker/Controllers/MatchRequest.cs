using QualificationChecker.Models;

namespace QualificationChecker.Controllers
{
    public partial class CandidatesController
    {
        public class MatchRequest
        {
            public Organization organization { get; set; }
            public List<Candidate> candidates { get; set; }
        }
    }

   
}
