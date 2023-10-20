using System.ComponentModel.DataAnnotations; // For validation attributes

namespace QualificationChecker.Models
{
    public class OrgQuestion
    {
        [Required(ErrorMessage = "Org Question ID is required.")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Position ID is required.")]
        public required int PositionId { get; set; }

        [Required(ErrorMessage = "Question text is required.")]
        [StringLength(500, ErrorMessage = "Question text must be between 1 and 500 characters.", MinimumLength = 1)]
        public required string QuestionText { get; set; }

        public bool Answer { get; set; }
    }
}


