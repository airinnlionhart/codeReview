using System.ComponentModel.DataAnnotations; // For validation attributes

namespace QualificationChecker.Models
{
    public class Organization
    {
        public int OrgId { get; set; }

        [Required(ErrorMessage = "Age requirement is required.")]
        [Range(14, 120, ErrorMessage = "Age requirement must be between 14 and 70.")]
        public int MinAgeRequirement { get; set; }

        public List<OrgQuestion>? OrgQuestions { get; set; }
    }

    public static class OrganizationExtensions
    {
        public static Dictionary<int, List<OrgQuestion>> OrgAnswersDictionary(this Organization organization)
        {
            return organization.OrgQuestions
                .GroupBy(q => q.PositionId)
                .ToDictionary(group => group.Key, group => group.ToList());
        }
    }
}


