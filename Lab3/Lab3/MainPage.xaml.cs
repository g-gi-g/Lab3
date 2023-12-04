using System.Dynamic;
using System.Reflection;

namespace Lab3;

public partial class MainPage : ContentPage
{
    private string filePath;

    public MainPage()
    {
        InitializeComponent();
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

            SoftwareRepository softwareListRepository = SoftwareRepository.GetInstance();

            softwareListRepository.SetList(JSONSerializer.DeserializeJSON(JSONFile.FullPath));

            await UpdateGrid();

            FileNameEntry.Text = result.FileName;

            filePath = result.FullPath;

            SaveResultBtn.IsEnabled = true;
        }

        catch (Exception ex)
        {
            await DisplayAlert("Попередження", string.Format("Виникла наступна помилка: {0}", ex.Message), "Ок");
        }
    }

    private async void AddToListBtnClicked(object sender, EventArgs e)
    {
        try
        {
            var addSoftwarePage = new AddSoftwarePage();

            addSoftwarePage.NavigatingFrom += async (s, args) =>
            {
                await UpdateGrid();
            };

            await Navigation.PushAsync(addSoftwarePage);
        }

        catch (Exception ex)
        {
            await DisplayAlert("Попередження", string.Format("Виникла наступна помилка: {0}", ex.Message), "Ок");
        }
    }

    private async void SearchBtnClicked(object sender, EventArgs e)
    {
        try
        {
            var searchParametersSelectionPage = new SearchParametersSelectionPage();

            searchParametersSelectionPage.NavigatingFrom += async (s, args) =>
            {
                await UpdateGrid();
            };

            await Navigation.PushAsync(searchParametersSelectionPage);
        }

        catch (Exception ex)
        {
            await DisplayAlert("Попередження", string.Format("Виникла наступна помилка: {0}", ex.Message), "Ок");
        }
    }

    private async void SaveBtnClicked(object sender, EventArgs e)
    {
        try
        {
            SoftwareRepository softwareListRepository = SoftwareRepository.GetInstance();

            JSONSerializer.SerializeJSON(softwareListRepository.GetAll(), filePath);
        }

        catch (Exception ex)
        {
            await DisplayAlert("Попередження", string.Format("Виникла наступна помилка: {0}", ex.Message), "Ок");
        }
    }

    private async void SaveAsBtnClicked(object sender, EventArgs e)
    {
        try
        {
            SoftwareRepository softwareListRepository = SoftwareRepository.GetInstance();

            var result = await FilePicker.Default.PickAsync();

            JSONSerializer.SerializeJSON(softwareListRepository.GetAll(), result.FullPath);

            filePath = result.FullPath;

            FileNameEntry.Text = result.FileName;

            SaveResultBtn.IsEnabled = true;
        }

        catch (Exception ex)
        {
            await DisplayAlert("Попередження", string.Format("Виникла наступна помилка: {0}", ex.Message), "Ок");
        }
    }

    private async void ChangeInfo(dynamic elementInfo)
    {
        await Navigation.PushAsync(new ChangeParameterPage(elementInfo.btn, 
            elementInfo.elementID, elementInfo.propertyID));
    }

    private async void DeleteSoftware(int index)
    {
        SoftwareRepository softwareListRepository = SoftwareRepository.GetInstance();

        softwareListRepository.DeleteById(index);

        await UpdateGrid();
    }
    
    private async Task UpdateGrid()
    {
        var childrenToRemove = new List<IView>();

        foreach (var child in ListGrid.Children)
        {
            if (ListGrid.GetRow(child) != 0)
            {
                childrenToRemove.Add(child);
            }
        }

        foreach (var child in childrenToRemove)
        {
            ListGrid.Children.Remove(child);
        }

        int rowNumber = 1;

        SoftwareRepository softwareListRepository = SoftwareRepository.GetInstance();

        foreach (Software software in softwareListRepository.GetAll()) 
        {
            if (rowNumber >= ListGrid.RowDefinitions.Count)
            {
                ListGrid.AddRowDefinition(new RowDefinition());
            }

            int columnNumber = 1;

            Button rowNumberGridCell = new Button
            {
                BackgroundColor = new Color(0.9f, 0.9f, 0.9f),
                Text = rowNumber.ToString(),
                TextColor = Color.FromArgb("#000000"),
                Command = new Command<int>((index) => DeleteSoftware(index - 1))
            };

            rowNumberGridCell.CommandParameter = rowNumber;

            ListGrid.SetRow(rowNumberGridCell, rowNumber);
            ListGrid.SetColumn(rowNumberGridCell, 0);

            ListGrid.Children.Add(rowNumberGridCell);

            Type type = software.GetType();

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Button newGridCell = new Button
                {
                    BackgroundColor = new Color(0.9f, 0.9f, 0.9f),
                    Text = (string)property.GetValue(software),
                    TextColor = Color.FromArgb("#000000"),
                    Command = new Command<dynamic>((par) => ChangeInfo(par))
                };

                dynamic commandParameter = new ExpandoObject();
                commandParameter.btn = newGridCell;
                commandParameter.elementID = rowNumber - 1;
                commandParameter.propertyID = columnNumber - 1;


                newGridCell.CommandParameter = commandParameter;

                ListGrid.SetRow(newGridCell, rowNumber);
                ListGrid.SetColumn(newGridCell, columnNumber);

                ListGrid.Children.Add(newGridCell);

                columnNumber++;
            }

            rowNumber++;
        }
    }
}

