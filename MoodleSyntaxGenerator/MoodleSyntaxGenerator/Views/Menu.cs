using static System.Windows.Forms.Design.AxImporter;
using MoodleSyntaxGenerator.Logic;
using MoodleSyntaxGenerator.Controllers;

namespace MoodleSyntaxGenerator
{
	public partial class Menu : Form
	{
		private int selectedQuestion;
		Controller _controller;


		public Menu(Controller controller)
		{
			_controller = controller;
			InitializeComponent();
			InitializeSearch();
		}

		private void InitializeSearch()
		{
			List<string> searchOptions = new()
			{
				"Short Answer",
				"Short Answer (Case sensitive)",
				"Dropdown menu in-line in the text",
				"Vertical column of radio buttons",
				"Horizontal row of radio-buttons",
				"Numerical"
			};

			cmbSelect.DropDownStyle = ComboBoxStyle.DropDownList;

			cmbSelect.DataSource = searchOptions;
		}

		private void BtnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txtBoxOutput.Text);
		}

		private void BtnGenerate_Click(object sender, EventArgs e)
		{
			switch (selectedQuestion)
			{
				case 0:
					_controller.GenerateShortAnswers("", "");
					break;
				case 1:
					_controller.GenerateShortAnswers("", "", true);
					break;
				case 2:
					_controller.();
					break;
				case 3:
					_controller.GenerateVerticalRadioButtons();
					break;
				case 4:
					_controller.GenerateHorizontalRadioButtons();
					break;
				case 5:
					_controller.GenerateNumerical();
					break;
				default:
					break;
			}
		}

		private void CmbSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedQuestion = cmbSelect.SelectedIndex;
		}

		public void SetOutput(string output)
		{
			txtBoxOutput.Text = output;
		}
	}
}