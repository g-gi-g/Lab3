using System.Reflection;

namespace Lab3;

public partial class MainPage : ContentPage
{ 
    private MainPageController controller;

    private List<Software> listOfSoftware;

    private string filePath;

	public MainPage()
	{
		InitializeComponent();

        controller = new MainPageController();
    }

	private void ShowProgrammInfoBtnClick(object sender, EventArgs e)
	{
		Navigation.PushAsync(new InfoPage());
	}

	private async void OpenJSONBtnClick(object sender, EventArgs e)
	{
        try
        {
            var result = await FilePicker.Default.PickAsync();

            if (result != null)
            {
                if (result.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                }
            }

            FileResult JSONFile = result;

            listOfSoftware = controller.Deserialize(JSONFile);

            UpdateGrid();

            FileNameEntry.Text = result.FileName;

            filePath = result.FullPath;
        }

        catch (Exception ex)
        {
            await DisplayAlert("Попередження", string.Format("Виникла наступна помилка: {0}", ex.Message), "Ок");
        }
    }

    private void EditListBtnClicked(object sender, EventArgs e)
    {

    }

    private void SearchBtnClicked(object sender, EventArgs e)
    {


        listOfSoftware = controller.Search();

        UpdateGrid();
    }

    private void UpdateGrid()
    {
        foreach (var child in ListGrid.Children)
        {
            if (ListGrid.GetRow(child) != 0)
            {
                ListGrid.Children.Remove(child);
            }
        }

        int rowNumber = 1;

        foreach (Software software in listOfSoftware) 
        {
            if (rowNumber >= ListGrid.RowDefinitions.Count)
            {
                ListGrid.AddRowDefinition(new RowDefinition());
            }

            int columnNumber = 1;

            Frame rowNumberGridCell = new Frame
            {
                Content = new Label { Text = rowNumber.ToString() },
                BackgroundColor = new Color(0.9f, 0.9f, 0.9f)
            };

            ListGrid.SetRow(rowNumberGridCell, rowNumber);
            ListGrid.SetColumn(rowNumberGridCell, 0);

            ListGrid.Children.Add(rowNumberGridCell);

            Type type = software.GetType();

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Frame newGridCell = new Frame
                {
                    Content = new Label { Text = (string)property.GetValue(software) },
                    BackgroundColor = new Color(0.9f, 0.9f, 0.9f)
                };

                ListGrid.SetRow(newGridCell, rowNumber);
                ListGrid.SetColumn(newGridCell, columnNumber);

                ListGrid.Children.Add(newGridCell);

                columnNumber++;
            }

            rowNumber++;
        }
    }
}

