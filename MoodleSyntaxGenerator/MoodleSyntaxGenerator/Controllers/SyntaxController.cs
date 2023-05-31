using MoodleSyntaxGenerator.Logic;

namespace MoodleSyntaxGenerator.Controllers
{
	public class SyntaxController
	{
		public SyntaxController()
		{
			
		}

		public string GenerateNumeric(string question, double answer, decimal tolerance = 0)
		{
			return SyntaxGenerator.GenerateSyntaxForNumeric(question, answer, tolerance);
		}

		public string GenerateShortAnswers(string question, string answer, bool isCaseSensitive = false)
		{
			return SyntaxGenerator.GenerateSyntaxForShortAnswer(question, answer, isCaseSensitive);
		}

		public string GenerateDropDown(string text, string question, List<(string option, bool isCorrect)> options)
		{
			return SyntaxGenerator.GenerateSyntaxForDropdown(text, question, options);
		}

		public string GenerateRadioButtons(string question, List<string> options, bool isHorizontal = false)
		{
			return SyntaxGenerator.GenerateyntaxForRadioButtons(question, options, isHorizontal);
		}
	}
}
