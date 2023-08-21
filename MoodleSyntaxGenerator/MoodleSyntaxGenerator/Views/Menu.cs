using MoodleSyntaxGenerator.Controllers;

namespace MoodleSyntaxGenerator
{
	public partial class Menu : Form
	{
		private int _selectedQuestion;
		private int _lineSpacing = 25;
		private Point _questionStartLocation = new Point();
		private readonly GroupBox[] _views = new GroupBox[6];

		public Menu()
		{
			AutoSize = true;
			InitializeComponent();
			InitializeSearch();

			CreateShortAnswerInputs();
			CreateDropdownInputs();
			CreateRadioButtonInputs();
			CreateNumericInputs();

			_views[_selectedQuestion].Show();
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

			_questionStartLocation = new Point(cmbSelect.Location.X, cmbSelect.Location.Y + 40);
		}

		private void BtnCopy_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtBoxOutput.Text))
			{
				Clipboard.SetText(txtBoxOutput.Text);
			}
		}
		private void CmbSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowChosenQuestion();
		}
		private void ShowChosenQuestion()
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
		private void BtnGenerate_Click(object sender, EventArgs e)
		{
			GenerateSyntax();
		}
		
		
		private void GenerateSyntax()
		{
			string moodleSyntax;
			var currentGroupBox = _views[_selectedQuestion];

			switch (_selectedQuestion)
			{
				case 0:
					moodleSyntax = GenerateShortAnswerSyntax(currentGroupBox, false);
					break;
				case 1:
					moodleSyntax = GenerateShortAnswerSyntax(currentGroupBox, true);
					break;
				case 2:
					moodleSyntax = GenerateDropdownSyntax(currentGroupBox);
					break;
				case 3:
					moodleSyntax = GenerateRadioSyntax(currentGroupBox, false);
					break;
				case 4:
					moodleSyntax = GenerateRadioSyntax(currentGroupBox, true);
					break;
				case 5:
					moodleSyntax = GenerateNumericSyntax(currentGroupBox);
					break;
				default:
					moodleSyntax = "";
					MessageBox.Show("Et ole valinnut vastaustyyppiä");
					break;
			}
			txtBoxOutput.Text = moodleSyntax;
		}

		private string GenerateNumericSyntax(GroupBox groupBoxToSearchFrom)
		{
			groupBoxToSearchFrom.AutoSize = true;

			var question = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericQuestion").Text;
			var answer = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericAnswer").Text;
			var tolerance = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericTolerance").Text;

			// If tolerance is not given
			if (string.IsNullOrWhiteSpace(tolerance))
			{
				return SyntaxController.GenerateNumeric(question, Convert.ToDouble(answer));
			}

			else
			{
				return SyntaxController.GenerateNumeric(question, Convert.ToDouble(answer), Convert.ToDecimal(tolerance));
			}
		}

		private string GenerateDropdownSyntax(GroupBox groupBoxToSearchFrom)
		{
			List<(string, bool)> answers = new();
			groupBoxToSearchFrom.AutoSize = true;
			int amountOfAnswers = 0;
			var text = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxDropdownText").Text;
			var question = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxDropdownQuestion").Text;

			foreach (CheckBox cBox in groupBoxToSearchFrom.Controls.OfType<CheckBox>())
			{
				amountOfAnswers++;
				bool isCorrect = cBox.Checked;
				var answer = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxDropdownAnswer" + amountOfAnswers).Text;
				// Only add the answer if there is something written in the textbox
				if (!string.IsNullOrWhiteSpace(answer))
				{
					answers.Add((answer, isCorrect));
				}
			}

			return SyntaxController.GenerateDropDown(text, question, answers);
		}

		private string GenerateShortAnswerSyntax(GroupBox groupBoxToSearchFrom, bool isCaseSensitive)
		{
			groupBoxToSearchFrom.AutoSize = true;

			var question = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxShortAnswerQuestion").Text;
			var answer = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxShortAnswerAnswer").Text;

			return SyntaxController.GenerateShortAnswers(question, answer, isCaseSensitive);
		}

		private string GenerateRadioSyntax(GroupBox groupBoxToSearchFrom, bool isHorizontal)
		{
			List<string> answers = new();
			groupBoxToSearchFrom.AutoSize = true;
			int amountOfAnswers = -1; // Start from -1, because the question is a textbox
			var question = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxRadioQuestion").Text;

			foreach (TextBox cBox in groupBoxToSearchFrom.Controls.OfType<TextBox>())
			{
				amountOfAnswers++;
				if (amountOfAnswers > 0)
				{
					var answer = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxRadioAnswer" + amountOfAnswers).Text;
					// Only add the answer if there is something written in the textbox
					if (!string.IsNullOrWhiteSpace(answer))
					{
						answers.Add(answer);
					}
				}
			}

			return SyntaxController.GenerateRadioButtons(question, answers, isHorizontal);
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
					Location = _questionStartLocation,
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
		

		/// <summary>
		/// Creates the input fields for a dropdown question
		/// </summary>
		private void CreateDropdownInputs()
		{
			string groupBoxName = "groupBoxDropdown";
			GroupBox? groupBoxToFind = Controls.OfType<GroupBox>().FirstOrDefault(c => c.Name == groupBoxName);

			if (!Controls.Contains(groupBoxToFind))
			{
				var location = cmbSelect.Location;

				GroupBox groupBox = new()
				{
					Name = groupBoxName,
					Location = _questionStartLocation,
					AutoSize = true
				};

				Label labelText = new()
				{
					Location = new Point(5, 15),
					Name = "lblDropdownText",
					Text = "Syötä teksti"
				};

				TextBox textBoxText = new()
				{
					Location = new Point(labelText.Location.X, labelText.Location.Y + _lineSpacing),
					Name = "txtBoxDropdownText",
					Multiline = true
				};

				Label labelQuestion = new()
				{
					Location = new Point(labelText.Location.X + textBoxText.Width + 5, labelText.Location.Y),
					Name = "lblDropdownQuestion",
					Text = "Syötä kysymys"
				};

				TextBox textBoxQuestion = new()
				{
					Location = new Point(labelQuestion.Location.X, labelQuestion.Location.Y + _lineSpacing),
					Name = "txtBoxDropdownQuestion",
					Multiline = true
				};


				Button btnAddAnswer = new()
				{
					Location = new Point(textBoxText.Location.X, textBoxText.Location.Y + _lineSpacing),
					Name = "btnAddDropdownAnswer",
					Text = "Lisää vastaus",
					AutoSize = true
				};
				btnAddAnswer.Click += AddDropdownAnswer;

				Button btnDeleteAnswer = new()
				{
					AutoSize = true,
					Location = new Point(btnAddAnswer.Location.X + btnAddAnswer.Width + 10, btnAddAnswer.Location.Y),
					Name = "btnDeleteDropdownAnswer",
					Text = "Poista vastaus"
				};
				btnDeleteAnswer.Click += DeleteDropdownAnswer;

				
				groupBox.Controls.Add(textBoxText);
				groupBox.Controls.Add(labelText);
				groupBox.Controls.Add(textBoxQuestion);
				groupBox.Controls.Add(labelQuestion);
				groupBox.Controls.Add(btnAddAnswer);
				groupBox.Controls.Add(btnDeleteAnswer);

				groupBox.Hide();
				Controls.Add(groupBox);
				_views[2] = groupBox;
			}
		}

		/// <summary>
		/// Adds an answer to a dropdown question
		/// </summary>
		private void AddDropdownAnswer(object sender, EventArgs e)
		{
			// Get the groupbox for a dropdown question
			var groupBox = _views[2];
			int amountOfAnswers = 0;

			var startLocation = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxDropdownText").Location;

			foreach (CheckBox cBox in groupBox.Controls.OfType<CheckBox>())
			{
				amountOfAnswers += 1;
			}

			if (amountOfAnswers > 0)
			{
				startLocation = groupBox.Controls.OfType<CheckBox>().Last().Location;
			}

			CheckBox checkBoxIsCorrect = new()
			{
				Location = new Point(startLocation.X, startLocation.Y + _lineSpacing),
				Name = "checkBoxIsCorrect" + (amountOfAnswers + 1)
			};

			TextBox textBoxAnswer = new()
			{
				Location = new Point(checkBoxIsCorrect.Location.X + 15, checkBoxIsCorrect.Location.Y),
				Name = "txtBoxDropdownAnswer" + (amountOfAnswers + 1)
			};

			var btnAddAnswer = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnAddDropdownAnswer");
			var btnDeleteAnswer = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnDeleteDropdownAnswer");

			// Move the button down
			btnAddAnswer.Location = new Point(btnAddAnswer.Location.X, btnAddAnswer.Location.Y + _lineSpacing);
			btnDeleteAnswer.Location = new Point(btnDeleteAnswer.Location.X, btnDeleteAnswer.Location.Y + _lineSpacing);

			groupBox.Controls.Add(textBoxAnswer);
			groupBox.Controls.Add(checkBoxIsCorrect);
		}

		/// <summary>
		/// Delete the latest dropdown answer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteDropdownAnswer(object sender, EventArgs e)
		{
			// Get the groupbox for a dropdown question
			var groupBox = _views[2];

			var textBoxes = groupBox.Controls.OfType<TextBox>().ToList();
			var checkBoxes = groupBox.Controls.OfType<CheckBox>().ToList();

			if (textBoxes.Count > 1)
			{
				groupBox.Controls.Remove(textBoxes[textBoxes.Count - 1]);
				groupBox.Controls.Remove(checkBoxes[checkBoxes.Count - 1]);
				

				var btnAdd = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnAddDropdownAnswer");
				var btnDelete = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnDeleteDropdownAnswer");

				// Move the buttons up
				btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y - _lineSpacing);
				btnDelete.Location = new Point(btnDelete.Location.X, btnDelete.Location.Y - _lineSpacing);
			}
		}
		

		/// <summary>
		/// Creates the input fields for a row of radio buttond
		/// </summary>
		private void CreateRadioButtonInputs()
		{
			string groupBoxName = "groupBoxRadio";
			GroupBox? groupBoxToFind = Controls.OfType<GroupBox>().FirstOrDefault(c => c.Name == groupBoxName);

			if (!Controls.Contains(groupBoxToFind))
			{
				var location = cmbSelect.Location;

				GroupBox groupBox = new()
				{
					Name = groupBoxName,
					Location = _questionStartLocation,
					AutoSize = true
				};

				Label labelText = new()
				{
					Location = new Point(5, 15),
					Name = "lblRadioQuestion",
					Text = "Syötä kysymys"
				};

				TextBox textBoxQuestion = new()
				{
					Location = new Point(labelText.Location.X, labelText.Location.Y + 20),
					Name = "txtBoxRadioQuestion"
				};

				Button btnAddAnswer = new()
				{
					Location = new Point(textBoxQuestion.Location.X, textBoxQuestion.Location.Y + _lineSpacing),
					AutoSize = true,
					Name = "btnAddRadioAnswer",
					Text = "Lisää vastaus"
				};
				btnAddAnswer.Click += AddRadioAnswer;

				Button btnDeleteAnswer = new()
				{
					AutoSize = true,
					Location = new Point(btnAddAnswer.Location.X + btnAddAnswer.Width + 10, btnAddAnswer.Location.Y),
					Name = "btnDeleteRadioAnswer",
					Text = "Poista vastaus"
				};
				btnDeleteAnswer.Click += DeleteRadioAnswer;


				groupBox.Controls.Add(textBoxQuestion);
				groupBox.Controls.Add(labelText);
				groupBox.Controls.Add(btnAddAnswer);
				groupBox.Controls.Add(btnDeleteAnswer);

				groupBox.Hide();
				Controls.Add(groupBox);
				_views[3] = groupBox;
				_views[4] = groupBox;
			}
		}

		/// <summary>
		/// Adds an answer choice to the row of radio buttons
		/// </summary>
		private void AddRadioAnswer(object sender, EventArgs e)
		{
			var groupBox = _views[3]; // Get the groupbox for a dropdown question, _views[4] should do the same
			int amountOfAnswers = 0; // Start from 0, because the question is the first textbox

			var startLocation = groupBox.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxRadioQuestion").Location;

			foreach (TextBox cBox in groupBox.Controls.OfType<TextBox>())
			{
				amountOfAnswers++;
			}

			if (amountOfAnswers > 1)
			{
				startLocation = groupBox.Controls.OfType<TextBox>().Last().Location;
			}

			TextBox textBoxAnswer = new()
			{
				Location = new Point(startLocation.X, startLocation.Y + _lineSpacing),
				Name = "txtBoxRadioAnswer" + (amountOfAnswers + 1)
			};

			var btnAdd = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnAddRadioAnswer");
			var btnDelete = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnDeleteRadioAnswer");

			// Move the buttons down
			btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y + _lineSpacing);
			btnDelete.Location = new Point(btnDelete.Location.X, btnDelete.Location.Y + _lineSpacing);

			groupBox.Controls.Add(textBoxAnswer);
		}
		
		/// <summary>
		/// Delete the latest answer choice from the radio buttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteRadioAnswer(object sender, EventArgs e)
		{
			var groupBox = _views[3]; // Get the groupbox for a dropdown question, _views[4] should do the same
			var textBoxes = groupBox.Controls.OfType<TextBox>().ToList();

			if (textBoxes.Count > 1)
			{
				groupBox.Controls.Remove(textBoxes[textBoxes.Count - 1]);

				var btnAdd = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnAddRadioAnswer");
				var btnDelete = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnDeleteRadioAnswer");

				// Move the buttons up
				btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y - _lineSpacing);
				btnDelete.Location = new Point(btnDelete.Location.X, btnDelete.Location.Y - _lineSpacing);
			}
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
					Location = _questionStartLocation,
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

				groupBox.Controls.Add(textBoxQuestion);
				groupBox.Controls.Add(labelQuestion);
				groupBox.Controls.Add(textBoxAnswer);
				groupBox.Controls.Add(labelAnswer);

				groupBox.Hide();
				Controls.Add(groupBox);
				_views[0] = groupBox;
				_views[1] = groupBox;
			}
		}
	}
}