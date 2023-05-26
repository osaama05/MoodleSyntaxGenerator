using static System.Windows.Forms.Design.AxImporter;
using MoodleSyntaxGenerator.Logic;
using MoodleSyntaxGenerator.Controllers;

namespace MoodleSyntaxGenerator
{
	public partial class Menu : Form
	{
		private int _selectedQuestion;
		private readonly Controller _controller;


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
			if (string.IsNullOrWhiteSpace(txtBoxOutput.Text))
			{
				Clipboard.SetText(txtBoxOutput.Text);
			}
		}

		private void BtnGenerate_Click(object sender, EventArgs e)
		{
			string output;
			switch (_selectedQuestion)
			{
				case 0:
					output = _controller.GenerateShortAnswers("", "");
					break;
				case 1:
					output = _controller.GenerateShortAnswers("", "", true);
					break;
				case 2:
					output = _controller.GenerateDropDown("", "", new List<(string, bool)>());
					break;
				case 3:
					output = _controller.GenerateRadioButtons("", new List<string>());
					break;
				case 4:
					output = _controller.GenerateRadioButtons("", new List<string>(), true);
					break;
				case 5:
					output = _controller.GenerateNumeric("", 0);
					break;
				default:
					output = "";
					MessageBox.Show("Et ole valinnut vastaustyyppiä");
					break;
			}
			txtBoxOutput.Text = output;
		}

		private void CmbSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			_selectedQuestion = cmbSelect.SelectedIndex;

			switch (_selectedQuestion)
			{
				case 0:
					CreateShortAnswerInputs();
					break;
				case 1:
					CreateShortAnswerInputs();
					break;
				case 2:
					CreateDropdownInputs();
					break;
				case 3:
					CreateRadioButtonsInputs();
					break;
				case 4:
					CreateRadioButtonsInputs();
					break;
				case 5:
					CreateNumericInputs();
					break;
				default:
					break;
			}
		}

		private void CreateNumericInputs()
		{
			
		}

		private void CreateDropdownInputs() 
		{ 
			
		}

		private void CreateRadioButtonsInputs()
		{

		}

		private void CreateShortAnswerInputs()
		{
			
		}
	}
}