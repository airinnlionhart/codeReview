using QualificationChecker.Models;

namespace QualificationChecker.Controllers
{
    public abstract class UtilityClass
    {
        protected UtilityClass(Candidate candidate, Organization organization)
        {
            Candidate = candidate;
            Organization = organization;
        }

        public Candidate Candidate { get; private set; }

        public Organization Organization { get; private set; }

        public Dictionary<int, List<OrgQuestion>> OrgAnswersDictionary
        {
            get
            {
                return Organization.OrgQuestions
                                    .GroupBy(q => q.PositionId)
                                    .ToDictionary(group => group.Key, group => group.ToList());
            }
        }

    }

    public class MatchRequest
    {
        public Organization Organization { get; set; }
        public List<Candidate> Candidates { get; set; }
    }

    public class CandidateUtilities : UtilityClass
    {
        public CandidateUtilities(Candidate candidate, Organization organization) : base(candidate, organization)
        {
        }

        // Method to tell if a Canidate is intrested in the position
        public bool IsInterested()
        {
            if (Candidate.InterestedPositionIds == null || !Candidate.InterestedPositionIds.Any())
            {
                return false;
            }

            return Candidate.InterestedPositionIds.Any(candidatePositionId =>
                Organization.OrgQuestions.Any(OrgQuestion => OrgQuestion.PositionId == candidatePositionId));
        }
        // Method to make sure they meet the age requirement
        public bool MeetsAgeRequirement()
        {
            return Candidate.Age >= Organization.AgeRequirement;
        }
        // Method to return if the answers match
        private bool MatchAnswers(bool candidateAnswers, bool organizationAnswers)
        {
            // Compare each answer
            return candidateAnswers == organizationAnswers;
        }
        // method too loop through to and append if answers match
        public List<Candidate> CheckQuestions()
        {
            bool allAnswersMatch = true;
            var QualifiedCandidates = new List<Candidate>();

            foreach (var candidateAnswer in Candidate.CandidateQuestions)
            {
                // Create a lookup key to match the PositionId
                var lookupKey = candidateAnswer.PositionId;

                if (OrgAnswersDictionary.TryGetValue(lookupKey, out var OrgAnswers))
                {
                    // Loop through the orgAnswers (in case there are multiple with the same PositionId)
                    bool answersMatch = false;

                    foreach (var OrgAnswer in OrgAnswers)
                    {
                        // make sure they have the same number first before checking answers
                        if (OrgAnswers.Count != Candidate.CandidateQuestions.Count)
                        {
                            answersMatch = false;
                            break;
                        }

                        // If an answer doesn't match or orgAnswer is null, set answersMatch to false
                        if (!MatchAnswers(candidateAnswer.Answer, OrgAnswer.Answer))
                        {
                            answersMatch = false;
                            break;
                        }
                        answersMatch = true;
                    }
                    // If no orgAnswer matches, set the flag to false and break the loop
                    if (!answersMatch)
                    {
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
                QualifiedCandidates.Add(Candidate);
            }
            // Return the list of qualified candidates
            return QualifiedCandidates;
        }
    }
}


