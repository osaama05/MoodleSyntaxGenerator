using MoodleSyntaxGenerator.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleSyntaxGenerator.Controllers
{
	public class Controller
	{
		SyntaxGenerator _generator;
		public Controller(SyntaxGenerator generator)
		{
			_generator = generator;
		}

		public string GenerateNumericQuestion(string question, int answer, decimal tolerance = 0)
		{
			return _generator.GenerateNumeric(question, answer, tolerance);
		}

		public string GenerateShortAnswers(string question, string answer, bool isCaseSensitive = false)
		{
			return _generator.GenerateShortAnswer(question, answer, isCaseSensitive);
		}
	}
}
