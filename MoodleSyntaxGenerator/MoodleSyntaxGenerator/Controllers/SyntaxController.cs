namespace MoodleSyntaxGenerator.Controllers
{
	public class SyntaxController
	{
		private SyntaxGenerator.SyntaxGenerator _generator;
		public SyntaxController(SyntaxGenerator.SyntaxGenerator gen)
		{
			_generator = gen;
		}

		public static string GenerateNumeric(TextBox question, TextBox answer, TextBox tolerance)
		{
			var questionText = question.Text;
			var answerText = answer.Text;
			var toleranceText = tolerance.Text;
			
			// If question box is empty
			if (string.IsNullOrWhiteSpace(questionText))
			{
				throw new Exception("Kysymyskenttä on tyhjä");
			}

			// If tolerance is not given
			if (string.IsNullOrWhiteSpace(toleranceText))
			{
				if (decimal.TryParse(answerText, out _))
				{
					return SyntaxGenerator.SyntaxGenerator.GenerateSyntaxForNumeric(questionText, Convert.ToDecimal(answerText));
				}

				else
				{
					throw new Exception("Vastauskenttä ei ole numeroina");
				}
			}

			// If answer is not given
			if (string.IsNullOrWhiteSpace(answerText))
			{
				throw new Exception("Vastauskenttä on tyhjä");
			}

			
			// If Answer is not decimal
			if (!decimal.TryParse(answerText, out _))
			{
				throw new Exception("Vastauskenttä ei ole numeroina");
			}

			// If tolerance is not decimal
			if (!decimal.TryParse(toleranceText, out _))
			{
				throw new Exception("Toleranssi ei ole numeroina");
			}

			// Everything passed
			else
			{
				return SyntaxGenerator.SyntaxGenerator.GenerateSyntaxForNumeric(questionText, Convert.ToDecimal(answerText), Convert.ToDecimal(toleranceText));
			}

		}

		public static string GenerateShortAnswers(TextBox question, TextBox answer, bool isCaseSensitive = false)
		{
			var questionText = question.Text;
			var answerText = answer.Text;
			
			if (string.IsNullOrWhiteSpace(questionText))
			{
				throw new Exception("Kysymyskenttä on tyhjä");
			}

			if (string.IsNullOrWhiteSpace(answerText))
			{
				throw new Exception("Vastauskenttä on tyhjä");
			}

			else
			{
				return SyntaxGenerator.SyntaxGenerator.GenerateSyntaxForShortAnswer(questionText, answerText, isCaseSensitive);	
			}
		}

		public static string GenerateMultipleChoiceDown(TextBox text, TextBox question, List<(TextBox option, CheckBox isCorrect)> options)
		{
			var textString = text.Text;
			var questionText = question.Text;
			var answerChoices = new List<(string text, bool isCorrect)>();

			foreach (var option in options)
			{
				var optionText = option.option.Text;
				var isCorrect = option.isCorrect.Checked;
				var answer = (optionText, isCorrect);
				answerChoices.Add(answer);
			}
			
			// If no text was given
			if (string.IsNullOrWhiteSpace(textString))
			{
				throw new Exception("Tekstikenttä on tyhjä");
			}

			// If no question was given
			if (string.IsNullOrWhiteSpace(questionText))
			{
				throw new Exception("Kysymyskenttä on tyhjä");
			}
			
			// If no answers are given
			if (answerChoices == null || !answerChoices.Any())
			{
				throw new Exception("Et ole antanut vastausvaihtoehtoja");
			}

			// Everything passed
			else
			{
				return SyntaxGenerator.SyntaxGenerator.GenerateSyntaxForMultipleChoice(textString, questionText, answerChoices);
			}
		}

		public static string GenerateRadioButtons(TextBox question, List<TextBox> answers, bool isHorizontal = false)
		{
			var questionText = question.Text;
			var answersText = new List<string>();
			
			foreach (var ans in answers)
			{
				answersText.Add(ans.Text);
			}
			
			// If question is empty
			if (string.IsNullOrWhiteSpace(questionText))
			{
				throw new Exception("Kysymyskenttä on tyhjä");
			}

			// If no answers were given
			if (answersText == null || !answersText.Any())
			{
				throw new Exception("Et ole antanut vastausvaihtoehtoja");
			}

			// Everything passed
			else 
			{ 
				return SyntaxGenerator.SyntaxGenerator.GenerateyntaxForRadioButtons(questionText, answersText, isHorizontal);
			}
		}
	}
}
