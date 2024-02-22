using System;
using QualificationChecker.Models;

namespace QualificationChecker.Controllers
{
	public class MatchRequest
	{
		public Organization Organization { get; set; }
		public List<Candidate> Candidates { get; set; }
		
	}
}

