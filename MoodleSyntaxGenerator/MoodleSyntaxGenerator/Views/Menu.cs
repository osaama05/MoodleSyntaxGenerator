using static System.Windows.Forms.Design.AxImporter;
using MoodleSyntaxGenerator.Logic;
using MoodleSyntaxGenerator.Controllers;

namespace MoodleSyntaxGenerator
{
	public partial class Menu : Form
	{
		private readonly List<string> searchOptions = new();
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
			searchOptions.Add("Short Answer");
			searchOptions.Add("Short Answer (Case sensitive)");
			searchOptions.Add("Dropdown menu in-line in the text");
			searchOptions.Add("Vertical column of radio buttons");
			searchOptions.Add("Horizontal row of radio-buttons");
			searchOptions.Add("Numerical");

			cmbSelect.DropDownStyle = ComboBoxStyle.DropDownList;

			cmbSelect.DataSource = searchOptions;
		}

		private void BtnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txtBoxOutput.Text);
		}

		private void BtnGenerate_Click(object sender, EventArgs e)
		{
			txtBoxOutput.Text = _controller.GenerateNumeric("What is 2+2?", 4, 0);
		}

		private void CmbSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedQuestion = cmbSelect.SelectedIndex;
		}
	}
}