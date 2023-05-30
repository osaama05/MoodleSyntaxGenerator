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
		private readonly SyntaxGenerator _generator;
		public Controller(SyntaxGenerator generator)
		{
			_generator = generator;
		}

		public string GenerateNumeric(string question, double answer, decimal tolerance = 0)
		{
			return _generator.GenerateNumeric(question, answer, tolerance);
		}

		public string GenerateShortAnswers(string question, string answer, bool isCaseSensitive = false)
		{
			return _generator.GenerateShortAnswer(question, answer, isCaseSensitive);
		}

		public string GenerateDropDown(string text, string question, List<(string option, bool isCorrect)> options)
		{
			return _generator.GenerateDropdown(text, question, options);
		}

		public string GenerateRadioButtons(string question, List<string> options, bool isHorizontal = false)
		{
			return _generator.GenerateRadioButtons(question, options, isHorizontal);
		}
	}
}
