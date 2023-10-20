using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // For validation attributes

namespace QualificationChecker.Models
{
    public class Organization
    {
        public int orgId { get; set; }

        [Required(ErrorMessage = "Age requirement is required.")]
        [Range(14, 120, ErrorMessage = "Age requirement must be between 14 and 70.")]
        public int ageRequirement { get; set; }

        public List<orgQuestion> orgQuestions{ get; set; }
    }

    public class orgQuestion
    {
        [Required(ErrorMessage = "Org Question ID is required.")]
        public required int id { get; set; }

        [Required(ErrorMessage = "Position ID is required.")]
        public required int positionId { get; set; }

        [Required(ErrorMessage = "Question text is required.")]
        [StringLength(500, ErrorMessage = "Question text must be between 1 and 500 characters.", MinimumLength = 1)]
        public required string questionText { get; set; }

        public bool answer { get; set; }
    }
}


