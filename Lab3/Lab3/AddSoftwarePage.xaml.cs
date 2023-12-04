namespace Lab3;

public partial class AddSoftwarePage : ContentPage
{
	public AddSoftwarePage()
	{
		InitializeComponent();
	}

	protected void BtnClicked(object sender, EventArgs e)
    {
        SoftwareRepository softwareRepository = SoftwareRepository.GetInstance();

        Software result = new Software();

        result.Name = NameEntry.Text;
        result.Annotation = AnnotationEntry.Text;
        result.Type = TypeEntry.Text;
        result.Version = VersionEntry.Text;
        result.Author = AuthorEntry.Text;
        result.TermsOfUsage = TermsOfUsageEntry.Text;
        result.DistributiveLocation = DistributiveLocationEntry.Text;

        softwareRepository.Add(result);

        Navigation.PopAsync();
    }
}