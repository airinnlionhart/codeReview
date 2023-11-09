using System.Linq;
using System.Security.Cryptography;
using QualificationChecker.Models;

namespace QualificationChecker.Controllers
{
    public abstract class QualificationManager
    {
        public Candidate Candidate { get; private set; }
        public Organization Organization { get; private set; }
        protected List<Candidate> QualifiedCandidates = new List<Candidate>();
        public List<Candidate> Candidates { get; private set; }

        protected QualificationManager(List<Candidate> candidates, Organization organization)
        {
            Candidates = candidates;
            Organization = organization;

        }

        // Method to tell if a Candidate is interested in any of the position
        public bool IsCandidateInterestedInPosition(Candidate candidate)
        {
            return candidate.InterestedPositionIds.Any(positionId => Organization.OrgAnswersDictionary().ContainsKey(positionId));
        }

        // Method to make sure they meet the age requirement
        public bool MeetsAgeRequirement(Candidate candidate)
        {
            return candidate.Age >= Organization.MinAgeRequirement;
        }

        // Method to check if the answers match
        private bool MatchAnswers(bool candidateAnswer, bool organizationAnswer)
        {
            // Compare each answer
            return candidateAnswer == organizationAnswer;
        }

        public bool OrganizationFit(Candidate candidate)
        {
            return candidate.OrganizationId == Organization.OrgId;
        }

        public virtual bool IsQualified(Candidate candidate)
        {
            return IsCandidateInterestedInPosition(candidate) && MeetsAgeRequirement(candidate) && OrganizationFit(candidate);
        }

        // Method to find qualified candidates
        public void FindQualifiedCandidates(Candidate candidate)
        {
            {
                if (CandidateMatches(candidate) && IsQualified(candidate))
                {
                    QualifiedCandidates.Add(candidate);
                }
            }

        }

        public bool CandidateMatches(Candidate candidate)
        {
            bool matches = true;

            foreach (var position in candidate.InterestedPositionIds)
            {
                bool allAnswersMatch = true;
                foreach (var candidateAnswer in candidate.CandidateQuestions.Where(positions => positions.PositionId == position))
                {
                    var lookupKey = position;

                    if (!Organization.OrgAnswersDictionary().TryGetValue(lookupKey, out var orgAnswers)
                        || orgAnswers.Count != candidate.CandidateQuestions.Count(poistion => poistion.PositionId == lookupKey)
                        || !orgAnswers.All(orgAnswer => MatchAnswers(candidateAnswer.Answer, orgAnswer.Answer))
                        )
                    {
                        allAnswersMatch = false;
                        break;
                    }
                }
                if (allAnswersMatch)
                {
                    candidate.QualifiedPositions.Add(position);
                }

            }

            matches = candidate.QualifiedPositions != null && candidate.QualifiedPositions.Any();
            return matches;
        }

        public List<Candidate> ReturnQualifiedCandidates()
        {
            foreach (var candidate in Candidates)
            {
                FindQualifiedCandidates(candidate);
            }

            return QualifiedCandidates;
        }
    }

    public class CandidateQualificationEvaluator : QualificationManager
    {

        public CandidateQualificationEvaluator(List<Candidate> candidates, Organization organization) : base(candidates, organization)
        {

        }

        public bool CustomCriteriaMet(Candidate candidate)
        {
            return candidate.Age < 68; // Implement your custom criteria logic here.
        }

        // Override the IsQualified method for specific qualification checks
        public override bool IsQualified(Candidate candidate)
        {
            return CustomCriteriaMet(candidate) && base.IsQualified(candidate);
        }
    }
}


