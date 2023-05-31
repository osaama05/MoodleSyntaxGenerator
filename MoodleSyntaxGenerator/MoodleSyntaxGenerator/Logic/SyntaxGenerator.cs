namespace MoodleSyntaxGenerator.Logic
{
	public class SyntaxGenerator
	{
		public SyntaxGenerator()
		{
			
		}

		public static string GenerateSyntaxForShortAnswer(string question, string answer, bool isCaseSensitive = false)
		{
			if (isCaseSensitive == true)
			{
				return "{" + $"1:{question}" + $" =#CASE_SENSITIVE={answer}" + "}";
			}

			else
			{
				return "{" + $"1:{question}" + $" ={answer}" + "}";
			}
		}

		public static string GenerateyntaxForRadioButtons(string question, List<string> options, bool isHorizontal = false)
		{
			string output;

			if (isHorizontal == true)
			{
				output = "{" + $"1:{question}: presentation=horizontal" + "}";
			}

			else
			{
				output = "{" + $"1:{question}:" + "}";
			}

			foreach (var option in options)
			{
				output = output + $" - [] {option}";
			}

			return output;
		}

		public static string GenerateSyntaxForDropdown(string text, string question, List<(string option, bool isCorrect)> options)
		{
			var output = "{" + $"1:{text}:" + "}" + $" {question} " + "{1:";
			var amountOfOptions = 0;

			foreach (var option in options)
			{
				amountOfOptions++;

				if (amountOfOptions == 1)
				{
					if (option.isCorrect == true)
					{
						output = output + $"{option.option} ={option.option}";
					}

					else
					{
						output = output + $"{option.option}";
					} 
				}

				else
				{
					if (option.isCorrect == true)
					{
						output = output + $" ~ {option.option} ={option.option}";
					}

					else
					{
						output = output + $" ~ {option.option}";
					}
				}
			}
			output = output + "}.";

			return output;
		}

		public static string GenerateSyntaxForNumeric(string question, double answer, decimal tolerance = 0)
		{
			return "{" + $"1:{question}: ={answer}:{tolerance}" + "}";
		}
	}
}
