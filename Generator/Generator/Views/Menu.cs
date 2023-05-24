namespace Generator
{
	public partial class Menu : Form
	{
		private readonly List<string> searchOptions = new();
		public Menu()
		{
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
	}
}