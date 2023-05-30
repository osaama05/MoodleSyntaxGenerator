using MoodleSyntaxGenerator.Controllers;
using System.ComponentModel;
using System.Windows.Forms;

namespace MoodleSyntaxGenerator
{
	public partial class Menu : Form
	{
		private int _selectedQuestion;
		private readonly GroupBox[] _views = new GroupBox[6];
		private readonly Controller _controller;


		public Menu(Controller controller)
		{
			_controller = controller;
			AutoSize = true;
			InitializeComponent();
			InitializeSearch();
			
			CreateShortAnswerInputs();
			CreateDropdownInputs();
			CreateRadioButtonsInputs();
			CreateNumericInputs();
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
			if (!string.IsNullOrWhiteSpace(txtBoxOutput.Text))
			{
				Clipboard.SetText(txtBoxOutput.Text);
			}
		}

		private void BtnGenerate_Click(object sender, EventArgs e)
		{
			string output;
			var groupBox = _views[_selectedQuestion];
			
			switch (_selectedQuestion)
			{
				case 0:
					TextBox? shortAnswerQuestion = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxShortAnswerQuestion");
					TextBox? shortAnswerAnswer = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxShortAnswerAnswer");
					
					output = _controller.GenerateShortAnswers(shortAnswerQuestion.Text, shortAnswerAnswer.Text);
					break;
				case 1:
					TextBox? shortAnswerQuestionCS = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxShortAnswerQuestion");
					TextBox? shortAnswerAnswerCS = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxShortAnswerAnswer");

					output = _controller.GenerateShortAnswers(shortAnswerQuestionCS.Text, shortAnswerAnswerCS.Text, true);
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
					TextBox? numericQuestion = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericQuestion");
					TextBox? numericAnswer = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericAnswer");
					TextBox? numericTolerance = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericTolerance");

					// If tolerance is not given
					if (string.IsNullOrWhiteSpace(numericTolerance.Text) || numericAnswer == null)
					{
						output = _controller.GenerateNumeric(numericQuestion.Text, Convert.ToDouble(numericAnswer.Text));
					}
					
					else
					{
						output = _controller.GenerateNumeric(numericQuestion.Text, Convert.ToDouble(numericAnswer.Text), Convert.ToDecimal(numericTolerance.Text)); 
					}
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
			// Hide the previous groupbox
			if (_views[_selectedQuestion] != null)
			{
				_views[_selectedQuestion].Hide();
			}

			// Get the index of the new groupbox
			_selectedQuestion = cmbSelect.SelectedIndex;

			// Show the new groupbox
			if (_views[_selectedQuestion] != null)
			{
				_views[_selectedQuestion].Show(); 
			}
		}

		/// <summary>
		/// Creates the input fields for a numeric question
		/// </summary>
		private void CreateNumericInputs()
		{
			string groupBoxName = "groupBoxNumeric";
			var groupBoxToFind = Controls.OfType<GroupBox>().FirstOrDefault(c => c.Name == groupBoxName);

			if (!Controls.Contains(groupBoxToFind))
			{
				var location = cmbSelect.Location;

				GroupBox groupBox = new()
				{
					Name = groupBoxName,
					Location = new Point(location.X, location.Y + 30),
					AutoSize = true
				};


				Label labelQuestion = new()
				{
					Location = new Point(5, 15),
					Name = "lblNumericQuestion",
					Text = "Syötä kysymys"
				};

				TextBox textBoxQuestion = new()
				{
					Location = new Point(labelQuestion.Location.X, labelQuestion.Location.Y + 20),
					Name = "txtBoxNumericQuestion",
					Multiline = true
				};


				Label labelAnswer = new()
				{
					Location = new Point(textBoxQuestion.Location.X + textBoxQuestion.Width + 5, labelQuestion.Location.Y),
					Name = "lblNumericAnswer",
					Text = "Syötä vastaus"
				};

				TextBox textBoxAnswer = new()
				{
					Location = new Point(labelAnswer.Location.X, labelAnswer.Location.Y + 20),
					Name = "txtBoxNumericAnswer",
					Multiline = true
				};


				Label labelTolerance = new()
				{
					Location = new Point(textBoxAnswer.Location.X + textBoxAnswer.Width + 5, labelAnswer.Location.Y),
					Name = "lblNumericTolerance",
					Text = "Syötä toleranssi (ei pakollinen)"
				};

				TextBox textBoxTolerance = new()
				{
					Location = new Point(labelTolerance.Location.X, labelTolerance.Location.Y + 20),
					Name = "txtBoxNumericTolerance",
					Multiline = true
				};

				groupBox.Controls.Add(textBoxQuestion);
				groupBox.Controls.Add(labelQuestion);
				groupBox.Controls.Add(textBoxAnswer);
				groupBox.Controls.Add(labelAnswer);
				groupBox.Controls.Add(textBoxTolerance);
				groupBox.Controls.Add(labelTolerance);
				
				groupBox.Hide();
				Controls.Add(groupBox);
				_views[5] = groupBox;
			}
		}


		private void CreateDropdownInputs()
		{

		}

		private void CreateRadioButtonsInputs()
		{

		}

		/// <summary>
		/// Creates the input fields for a short answer question
		/// </summary>
		private void CreateShortAnswerInputs()
		{
			string groupBoxName = "groupBoxShortAnswer";
			GroupBox? groupBoxToFind = Controls.OfType<GroupBox>().FirstOrDefault(c => c.Name == groupBoxName);

			if (!Controls.Contains(groupBoxToFind))
			{
				var location = cmbSelect.Location;

				GroupBox groupBox = new()
				{
					Name = groupBoxName,
					Location = new Point(location.X, location.Y + 30),
					AutoSize = true
				};


				Label labelQuestion = new()
				{
					Location = new Point(5, 15),
					Name = "lblShortAnswerQuestion",
					Text = "Syötä kysymys"
				};

				TextBox textBoxQuestion = new()
				{
					Location = new Point(labelQuestion.Location.X, labelQuestion.Location.Y + 20),
					Name = "txtBoxShortAnswerQuestion",
					Multiline = true
				};


				Label labelAnswer = new()
				{
					Location = new Point(textBoxQuestion.Location.X + textBoxQuestion.Width + 5, labelQuestion.Location.Y),
					Name = "lblShortAnswerAnswer",
					Text = "Syötä vastaus"
				};

				TextBox textBoxAnswer = new()
				{
					Location = new Point(labelAnswer.Location.X, labelAnswer.Location.Y + 20),
					Name = "txtBoxShortAnswerAnswer",
					Multiline = true
				};

				Label labelIsCaseSensitive = new()
				{
					Location = new Point(labelAnswer.Location.X + textBoxAnswer.Width + 5, labelAnswer.Location.Y),
					Name = "lblShortAnswerIsCaseSensitive",
					Text = "Onko vastaus kirjainkoosta riippuvainen"
				};

				CheckBox checkBoxIsCaseSensitive = new()
				{
					Location = new Point(labelIsCaseSensitive.Location.X, labelIsCaseSensitive.Location.Y + 20),
					Name = "checkBoxShortAnswerIsCaseSensitive"
				};

				groupBox.Controls.Add(textBoxQuestion);
				groupBox.Controls.Add(labelQuestion);
				groupBox.Controls.Add(textBoxAnswer);
				groupBox.Controls.Add(labelAnswer);
				groupBox.Controls.Add(checkBoxIsCaseSensitive);
				groupBox.Controls.Add(labelIsCaseSensitive);

				groupBox.Hide();
				Controls.Add(groupBox);
				_views[0] = groupBox;
				_views[1] = groupBox;
			}
		}

		private void ClearAllInputs(object sender, EventArgs e)
		{

		}
	}
}