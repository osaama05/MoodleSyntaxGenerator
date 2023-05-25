using MoodleSyntaxGenerator.Controllers;
using MoodleSyntaxGenerator.Logic;

namespace MoodleSyntaxGenerator
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			SyntaxGenerator gen = new();
			Controller controller = new(gen);
			Application.Run(new Menu(controller));
		}
	}
}