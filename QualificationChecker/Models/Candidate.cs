using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // For validation attributes

namespace QualificationChecker.Models
{
    public class Candidate
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name must be between 1 and 100 characters.", MinimumLength = 1)]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(14, 105, ErrorMessage = "Age must be between 14 and 70.")]
        public int age { get; set; }

        public int organizationId { get; set; }

        public List<int> interestedPositionIds { get; set; }

        public List<candidateQuestion> candidateQuestions { get; set; }
    }

    public class candidateQuestion
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Position ID is required.")]
        public int positionId { get; set; }

        [Required(ErrorMessage = "Question text is required.")]
        [StringLength(500, ErrorMessage = "Question text must be between 1 and 500 characters.", MinimumLength = 1)]
        public string questionText { get; set; }

        public bool answer { get; set; }
    }
}

