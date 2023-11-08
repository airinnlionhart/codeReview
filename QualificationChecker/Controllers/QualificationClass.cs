using QualificationChecker.Models;

namespace QualificationChecker.Controllers
{
    public abstract class QualificationManager
    {
        public Candidate Candidate { get; private set; }
        public Organization Organization { get; private set; }
        protected List<Candidate> QualifiedCandidates = new List<Candidate>();
        private Dictionary<int, List<OrgQuestion>> OrgQuestionsDictionary;

        protected QualificationManager(Candidate candidate, Organization organization)
        {
            Candidate = candidate;
            Organization = organization;

        }

        // Method to tell if a Candidate is interested in the position
        public bool IsCandidateInterestedInPosition()
        {

            return Candidate.InterestedPositionIds != null ||
            Candidate.InterestedPositionIds.Any(OrgQuestionsDictionary.ContainsKey);

        }

        // Method to make sure they meet the age requirement
        public bool MeetsAgeRequirement()
        {
            return Candidate.Age >= Organization.MinAgeRequirement;
        }

        // Method to check if the answers match
        private bool MatchAnswers(bool candidateAnswer, bool organizationAnswer)
        {
            // Compare each answer
            return candidateAnswer == organizationAnswer;
        }

        public bool OrganizationFit()
        {
            return Candidate.OrganizationId == Organization.OrgId;
        }

        public virtual bool IsQualified()
        {
            return IsCandidateInterestedInPosition() && MeetsAgeRequirement() && OrganizationFit();
        }

        // Method to find qualified candidates
        public void FindQualifiedCandidates(Candidate candidate)
        {
             if (CandidateMatches(candidate) && IsQualified())
                {
                    QualifiedCandidates.Add(candidate);
                }
            
        }

        public bool CandidateMatches(Candidate candidate)
        {
            bool allAnswersMatch = true;

            foreach (var candidateAnswer in candidate.CandidateQuestions)
            {
                var lookupKey = candidateAnswer.PositionId;

                if (!Organization.OrgAnswersDictionary().TryGetValue(lookupKey, out var orgAnswers))
                {
                    allAnswersMatch = false;
                    break;
                }

                if (orgAnswers.Count != candidate.CandidateQuestions.Count)
                {
                    allAnswersMatch = false;
                    break;
                }

                if (!orgAnswers.All(orgAnswer => MatchAnswers(candidateAnswer.Answer, orgAnswer.Answer)))
                {
                    allAnswersMatch = false;
                    break;
                }
            }

            return allAnswersMatch;
        }

        public List<Candidate> ReturnListOfCandidate()
        {
            return QualifiedCandidates;
        }
    }

    public class CandidateQualificationEvaluator : QualificationManager
    {

        public CandidateQualificationEvaluator(Candidate candidate, Organization organization) : base(candidate, organization)
        {

        }

        public bool CustomCriteriaMet()
        {
            return Candidate.Age < 68; // Implement your custom criteria logic here.
        }

        // Override the IsQualified method for specific qualification checks
        public override bool IsQualified()
        {
            return CustomCriteriaMet() && base.IsQualified();
        }


        public new void FindQualifiedCandidates(Candidate candidate)
        {
            {
                if (CandidateMatches(candidate) && IsQualified())
                {
                    QualifiedCandidates.Add(candidate);
                }
            }

        }
    }
}
