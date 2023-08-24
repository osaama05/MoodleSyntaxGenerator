using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoodleSyntaxGenerator.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleSyntaxGenerator.Controllers;

namespace MoodleSyntaxGenerator.Logic.Tests
{
	//[TestClass()]
	//public class SyntaxGeneratorTests
	//{
	//	private SyntaxGenerator _generator = new();
	//	[TestMethod()]
	//	public void GenerateShortAnswer_ShouldGenerateRegularShortAnswerQuestion()
	//	{
	//		// Arrange
	//		string question = "What is the capital of France?";
	//		string answer = "Paris";
	//		string expected = "{1:What is the capital of France? =Paris}";

	//		// Act
	//		string result = _generator.GenerateShortAnswer(question, answer);

	//		// Assert
	//		Assert.AreEqual(expected, result);
	//	}

	//	[TestMethod()]
	//	public void GenerateShortAnswer_ShouldGenerateCaseSensitiveShortAnswerQuestion()
	//	{
	//		// Arrange
	//		string question = "What is the capital of France?";
	//		string answer = "Paris";
	//		string expected = "{1:What is the capital of France? =#CASE_SENSITIVE=Paris}";

	//		// Act
	//		string result = _generator.GenerateShortAnswer(question, answer, isCaseSensitive: true);

	//		// Assert
	//		Assert.AreEqual(expected, result);
	//	}

	//	[TestMethod()]
	//	public void GenerateRadioButtons_ShouldGenerateVerticalRadioButtons()
	//	{
	//		// Arrange
	//		string question = "Select your preferred color";
	//		List<string> options = new List<string> { "Red", "Blue", "Green" };
	//		string expected = "{1:Select your preferred color:} - [] Red - [] Blue - [] Green";

	//		// Act
	//		string result = _generator.GenerateRadioButtons(question, options);

	//		// Assert
	//		Assert.AreEqual(expected, result);
	//	}

	//	[TestMethod()]
	//	public void GenerateRadioButtons_ShouldGenerateHorizontalRadioButtons()
	//	{
	//		// Arrange
	//		string question = "Select your preferred color";
	//		List<string> options = new List<string> { "Red", "Blue", "Green" };
	//		string expected = "{1:Select your preferred color: presentation=horizontal} - [] Red - [] Blue - [] Green";

	//		// Act
	//		string result = _generator.GenerateRadioButtons(question, options, true);

	//		// Assert
	//		Assert.AreEqual(expected, result);
	//	}

	//	[TestMethod()]
	//	public void GenerateMultipleChoice_ShouldGenerateMultipleChoiceWithCorrectOptions()
	//	{
	//		// Arrange
	//		string text = "Select the correct answer";
	//		string question = "What is the capital of France?";
	//		List<(string option, bool isCorrect)> options = new List<(string, bool)>
	//		{
	//			("Paris", true),
	//			("London", false),
	//			("Berlin", false),
	//			("Rome", false)
	//		};
	//		string expected = "{1:Select the correct answer:} What is the capital of France? {1:Paris =Paris ~ London ~ Berlin ~ Rome}.";

	//		// Act
	//		string result = _generator.GenerateMultipleChoice(text, question, options);

	//		// Assert
	//		Assert.AreEqual(expected, result);
	//	}

	//	[TestMethod()]
	//	public void GenerateNumeric_ShouldGenerateNumericQuestion()
	//	{
	//		// Arrange
	//		string question = "What is the square root of 25?";
	//		int answer = 5;
	//		decimal tolerance = 0.1m;
	//		string expected = "{1:What is the square root of 25?: =5:0,1}";

	//		// Act
	//		string result = _generator.GenerateNumeric(question, answer, tolerance);

	//		// Assert
	//		Assert.AreEqual(expected, result);
	//	}
	//}
}