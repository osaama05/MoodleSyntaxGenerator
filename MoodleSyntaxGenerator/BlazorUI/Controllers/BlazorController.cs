using SyntaxGenerator;

namespace BlazorUI.Controllers
{
	public class BlazorController
	{
		public BlazorController()
		{
			
		}

		public static string GenerateNumeric(string questionText, string answerText, string toleranceText)
		{
			if (string.IsNullOrWhiteSpace(questionText))
			{
				throw new Exception("Question is empty");
			}

			if (string.IsNullOrWhiteSpace(toleranceText))
			{
				if (decimal.TryParse(answerText, out decimal answer))
				{
					return SyntaxGenerator.SyntaxGenerator.GenerateSyntaxForNumeric(questionText, answer);
				}
				else
				{
					throw new Exception("Answer is not numeric");
				}
			}

			if (string.IsNullOrWhiteSpace(answerText))
			{
				throw new Exception("Answer is empty");
			}

			if (!decimal.TryParse(answerText, out _))
			{
				throw new Exception("Answer is not numeric");
			}

			if (!decimal.TryParse(toleranceText, out decimal tolerance))
			{
				throw new Exception("Tolerance is not numeric");
			}

			return SyntaxGenerator.SyntaxGenerator.GenerateSyntaxForNumeric(questionText, Convert.ToDecimal(answerText), tolerance);
		}

		public static string GenerateShortAnswers(string questionText, string answerText, bool isCaseSensitive = false)
		{
			if (string.IsNullOrWhiteSpace(questionText))
			{
				throw new Exception("Question is empty");
			}

			if (string.IsNullOrWhiteSpace(answerText))
			{
				throw new Exception("Answer is empty");
			}

			return SyntaxGenerator.SyntaxGenerator.GenerateSyntaxForShortAnswer(questionText, answerText, isCaseSensitive);
		}

		public static string GenerateMultipleChoiceDown(string textString, string questionText, List<(string optionText, bool isCorrect)> answerChoices)
		{
			if (string.IsNullOrWhiteSpace(textString))
			{
				throw new Exception("Text is empty");
			}

			if (string.IsNullOrWhiteSpace(questionText))
			{
				throw new Exception("Question is empty");
			}

			if (answerChoices == null || !answerChoices.Any())
			{
				throw new Exception("No answer options provided");
			}

			return SyntaxGenerator.SyntaxGenerator.GenerateSyntaxForMultipleChoice(textString, questionText, answerChoices);
		}

		public static string GenerateRadioButtons(string questionText, List<string> answersText, bool isHorizontal = false)
		{
			if (string.IsNullOrWhiteSpace(questionText))
			{
				throw new Exception("Question is empty");
			}

			if (answersText == null || !answersText.Any())
			{
				throw new Exception("No answer options provided");
			}

			return SyntaxGenerator.SyntaxGenerator.GenerateyntaxForRadioButtons(questionText, answersText, isHorizontal);
		}
	}
}
