using MoodleSyntaxGenerator.Controllers;
using System.Diagnostics.Eventing.Reader;

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
			CreateMultipleChoiceInputs();
			CreateRadioButtonInputs();
			CreateNumericInputs();

			_views[_selectedQuestion].Show();
		}

		private void InitializeSearch()
		{
			List<string> searchOptions = new()
			{
				"Lyhyt vastaus",
				"Lyhyt vastaus (merkkikokoriippuvainen)",
				"Monivalinta",
				"Valintanappi (pysty)",
				"Valintanappi (vaaka)",
				"Numeerinen"
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
		private void Menu_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				// Display a confirmation dialog when the user tries to close the form.
				DialogResult result = MessageBox.Show("Haluatko varmasti sulkea sovelluksen?", "Vahvistus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					// If the user selects 'No', cancel the form closing event.
					e.Cancel = true;
				}
				else
				{
					// If the user selects 'Yes', close the application.
					Application.Exit();
				}
			}
		}


		/// <summary>
		/// Calls the correct method based on the chosen question type
		/// </summary>
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
					moodleSyntax = GenerateMultipleChoiceSyntax(currentGroupBox);
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

		/// <summary>
		/// Generates the syntax for a numeric question based on the inputs
		/// </summary>
		/// <param name="groupBoxToSearchFrom"></param>
		/// <returns></returns>
		private static string GenerateNumericSyntax(GroupBox groupBoxToSearchFrom)
		{
			groupBoxToSearchFrom.AutoSize = true;

			var question = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericQuestion");
			var answer = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericAnswer");
			var tolerance = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxNumericTolerance");

			try
			{
				return SyntaxController.GenerateNumeric(question, answer, tolerance);
			}

			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return "";
			}
		}

		/// <summary>
		/// Generates the syntax for a multiple choice question based on the inputs
		/// </summary>
		/// <param name="groupBoxToSearchFrom"></param>
		/// <returns></returns>
		private static string GenerateMultipleChoiceSyntax(GroupBox groupBoxToSearchFrom)
		{
			List<(TextBox answer, CheckBox isCorrect)> answers = new();
			groupBoxToSearchFrom.AutoSize = true;
			int amountOfAnswers = 0;
			var text = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxMultipleChoiceText");
			var question = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxMultipleChoiceQuestion");

			foreach (CheckBox cBox in groupBoxToSearchFrom.Controls.OfType<CheckBox>())
			{
				amountOfAnswers++;
				var answer = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxMultipleChoiceAnswer" + amountOfAnswers);
				// Only add the answer if there is something written in the textbox
				if (!string.IsNullOrWhiteSpace(answer.Text))
				{
					answers.Add((answer, cBox));
				}
			}

			try
			{
				return SyntaxController.GenerateMultipleChoiceDown(text, question, answers);
			}

			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return "";
			}
		}

		/// <summary>
		/// Generates the syntax for a short answer question based on the inputs
		/// </summary>
		/// <param name="groupBoxToSearchFrom"></param>
		/// <param name="isCaseSensitive"></param>
		/// <returns></returns>
		private string GenerateShortAnswerSyntax(GroupBox groupBoxToSearchFrom, bool isCaseSensitive)
		{
			groupBoxToSearchFrom.AutoSize = true;

			var question = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxShortAnswerQuestion");
			var answer = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxShortAnswerAnswer");

			try
			{
				return SyntaxController.GenerateShortAnswers(question, answer, isCaseSensitive);
			}

			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return "";
			}
		}

		/// <summary>
		/// Generates the syntax for a radio question based on the inputs
		/// </summary>
		/// <param name="groupBoxToSearchFrom"></param>
		/// <param name="isHorizontal"></param>
		/// <returns></returns>
		private string GenerateRadioSyntax(GroupBox groupBoxToSearchFrom, bool isHorizontal)
		{
			List<TextBox> answers = new();
			groupBoxToSearchFrom.AutoSize = true;
			int amountOfAnswers = 0; // Start from 0, because the question is a textbox
			var question = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxRadioQuestion");

			foreach (TextBox cBox in groupBoxToSearchFrom.Controls.OfType<TextBox>())
			{
				amountOfAnswers++;
				if (amountOfAnswers > 1)
				{
					var answer = groupBoxToSearchFrom.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtBoxRadioAnswer" + amountOfAnswers);
					// Only add the answer if there is something written in the textbox
					if (!string.IsNullOrWhiteSpace(answer.Text))
					{
						answers.Add(answer);
					}
				}
			}

			try
			{
				return SyntaxController.GenerateRadioButtons(question, answers, isHorizontal);
			}

			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return "";
			}
		}

		// Numeric
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
					Location = new Point(labelQuestion.Location.X, labelQuestion.Location.Y + _lineSpacing),
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
					Location = new Point(labelAnswer.Location.X, labelAnswer.Location.Y + _lineSpacing),
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
					Location = new Point(labelTolerance.Location.X, labelTolerance.Location.Y + _lineSpacing),
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

		// Multiple choice
		/// <summary>
		/// Creates the input fields for a multiple choice question
		/// </summary>
		private void CreateMultipleChoiceInputs()
		{
			string groupBoxName = "groupBoxMultipleChoice";
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
					Name = "lblMultipleChoiceText",
					Text = "Syötä teksti"
				};

				TextBox textBoxText = new()
				{
					Location = new Point(labelText.Location.X, labelText.Location.Y + _lineSpacing),
					Name = "txtBoxMultipleChoiceText",
					Multiline = true
				};

				Label labelQuestion = new()
				{
					Location = new Point(labelText.Location.X + textBoxText.Width + 5, labelText.Location.Y),
					Name = "lblMultipleChoiceQuestion",
					Text = "Syötä kysymys"
				};

				TextBox textBoxQuestion = new()
				{
					Location = new Point(labelQuestion.Location.X, labelQuestion.Location.Y + _lineSpacing),
					Name = "txtBoxMultipleChoiceQuestion",
					Multiline = true
				};

				Label labelAnswer = new()
				{
					Location = new Point(textBoxText.Location.X, textBoxText.Location.Y + _lineSpacing),
					Name = "lblMultipleChoiceAnswer",
					Text = "Vastausvaihtoehdot",
					AutoSize = true
				};

				Button btnAddAnswer = new()
				{
					Location = new Point(labelAnswer.Location.X, labelAnswer.Location.Y + _lineSpacing),
					Name = "btnAddMultipleChoiceAnswer",
					Text = "Lisää vastaus",
					AutoSize = true
				};
				btnAddAnswer.Click += AddMultipleChoiceAnswer;

				Button btnDeleteAnswer = new()
				{
					AutoSize = true,
					Location = new Point(btnAddAnswer.Location.X + btnAddAnswer.Width + 10, btnAddAnswer.Location.Y),
					Name = "btnDeleteMultipleChoiceAnswer",
					Text = "Poista vastaus"
				};
				btnDeleteAnswer.Click += DeleteMultipleChoiceAnswer;


				groupBox.Controls.Add(textBoxText);
				groupBox.Controls.Add(labelText);
				groupBox.Controls.Add(textBoxQuestion);
				groupBox.Controls.Add(labelQuestion);
				groupBox.Controls.Add(labelAnswer);
				groupBox.Controls.Add(btnAddAnswer);
				groupBox.Controls.Add(btnDeleteAnswer);

				groupBox.Hide();
				Controls.Add(groupBox);
				_views[2] = groupBox;
			}
		}

		/// <summary>
		/// Adds an answer to a multiple choice question
		/// </summary>
		private void AddMultipleChoiceAnswer(object sender, EventArgs e)
		{
			// Get the groupbox for a multiple choice question
			var groupBox = _views[2];
			int amountOfAnswers = 0;

			var startLocation = groupBox.Controls.OfType<Label>().FirstOrDefault(c => c.Name == "lblMultipleChoiceAnswer").Location;

			foreach (CheckBox cBox in groupBox.Controls.OfType<CheckBox>())
			{
				amountOfAnswers += 1;
			}

			if (amountOfAnswers > 0 && amountOfAnswers <= 10)
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
				Name = "txtBoxMultipleChoiceAnswer" + (amountOfAnswers + 1)
			};

			var btnAddAnswer = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnAddMultipleChoiceAnswer");
			var btnDeleteAnswer = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnDeleteMultipleChoiceAnswer");

			// Move the button down
			btnAddAnswer.Location = new Point(btnAddAnswer.Location.X, btnAddAnswer.Location.Y + _lineSpacing);
			btnDeleteAnswer.Location = new Point(btnDeleteAnswer.Location.X, btnDeleteAnswer.Location.Y + _lineSpacing);

			groupBox.Controls.Add(textBoxAnswer);
			groupBox.Controls.Add(checkBoxIsCorrect);
		}

		/// <summary>
		/// Delete the latest multiple choice answer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteMultipleChoiceAnswer(object sender, EventArgs e)
		{
			// Get the groupbox for a multiple choice question
			var groupBox = _views[2];

			var textBoxes = groupBox.Controls.OfType<TextBox>().ToList();
			var checkBoxes = groupBox.Controls.OfType<CheckBox>().ToList();

			if (checkBoxes.Count > 1)
			{
				groupBox.Controls.Remove(textBoxes[textBoxes.Count - 1]);
				groupBox.Controls.Remove(checkBoxes[checkBoxes.Count - 1]);


				var btnAdd = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnAddMultipleChoiceAnswer");
				var btnDelete = groupBox.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnDeleteMultipleChoiceAnswer");

				// Move the buttons up
				btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y - _lineSpacing);
				btnDelete.Location = new Point(btnDelete.Location.X, btnDelete.Location.Y - _lineSpacing);
			}
		}

		// Radio button
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
					Location = new Point(labelText.Location.X, labelText.Location.Y + _lineSpacing),
					Name = "txtBoxRadioQuestion"
				};

				Label lblAnswer = new()
				{
					Location = new Point(textBoxQuestion.Location.X, textBoxQuestion.Location.Y + _lineSpacing),
					Name = "lblRadioAnswer",
					Text = "Vastausvaihtoehdot",
					AutoSize = true
				};

				Button btnAddAnswer = new()
				{
					Location = new Point(lblAnswer.Location.X, lblAnswer.Location.Y + _lineSpacing),
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
				groupBox.Controls.Add(lblAnswer);

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
			var groupBox = _views[3]; // Get the groupbox for a multiple choice question, _views[4] should do the same
			int amountOfAnswers = 0; // Start from 0, because the question is the first textbox

			var startLocation = groupBox.Controls.OfType<Label>().FirstOrDefault(c => c.Name == "lblRadioAnswer").Location;

			foreach (TextBox cBox in groupBox.Controls.OfType<TextBox>())
			{
				amountOfAnswers++;
			}

			if (amountOfAnswers > 1 && amountOfAnswers <= 11)
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
			var groupBox = _views[3]; // Get the groupbox for a multiple choice question, _views[4] should do the same
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

		// Short answer
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
					Location = new Point(labelQuestion.Location.X, labelQuestion.Location.Y + _lineSpacing),
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
					Location = new Point(labelAnswer.Location.X, labelAnswer.Location.Y + _lineSpacing),
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