using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleSyntaxGenerator.Logic
{
	public class SyntaxGenerator
	{
		public SyntaxGenerator()
		{
			
		}

		public string GenerateShortAnswer(string question, string answer, bool isCaseSensitive = false)
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

		public string GenerateRadioButtons(string question, List<string> options, bool isHorizontal = false)
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

		public string GenerateDropdown(string text, string question, List<(string option, bool isCorrect)> options)
		{
			var output = "{" + $"1:{text}:" + "}" + $" {question} " + "{1:";

			foreach (var option in options)
			{
				if (option.isCorrect == true)
				{
					output = output + $"{option.option} ={option.option}";
				}

				else
				{
					output = output + $" ~ {option.option}";
				}
			}
			output = output + "}.";

			return output;
		}

		public string GenerateNumeric(string question, double answer, decimal tolerance = 0)
		{
			return "{" + $"1:{question}: ={answer}:{tolerance}" + "}";
		}
	}
}
