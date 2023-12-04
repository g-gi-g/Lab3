namespace Lab3;

public partial class SearchParametersSelectionPage : ContentPage
{
	public SearchParametersSelectionPage()
	{
		InitializeComponent();
	}

	private void BtnClicked(object sender, EventArgs e)
	{ 
		SoftwareRepository softwareRepository = SoftwareRepository.GetInstance();

        Software searchParameters = new Software();

        searchParameters.Name = NameEntry.Text;
        searchParameters.Annotation = AnnotationEntry.Text;
        searchParameters.Type = TypeEntry.Text;
        searchParameters.Version = VersionEntry.Text;
        searchParameters.Author = AuthorEntry.Text;
        searchParameters.TermsOfUsage = TermsOfUsageEntry.Text;
        searchParameters.DistributiveLocation = DistributiveLocationEntry.Text;

        List<Software> result = softwareRepository.SearchByParameters(searchParameters);

        softwareRepository.SetList(result);

        Navigation.PopAsync();
    }
}