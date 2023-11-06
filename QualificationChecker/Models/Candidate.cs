using System.ComponentModel.DataAnnotations; // For validation attributes

namespace QualificationChecker.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name must be between 1 and 100 characters.", MinimumLength = 1)]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(14, 70, ErrorMessage = "Age must be between 14 and 70.")]
        public int Age { get; set; }

        public int OrganizationId { get; set; }

        public List<int>? InterestedPositionIds { get; set; }

        public List<CandidateQuestion>? CandidateQuestions { get; set; }
    }

    public class CandidateQuestion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Position ID is required.")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Question text is required.")]
        [StringLength(500, ErrorMessage = "Question text must be between 1 and 500 characters.", MinimumLength = 1)]
        public required string QuestionText { get; set; }

        public required bool Answer { get; set; }
    }

}

