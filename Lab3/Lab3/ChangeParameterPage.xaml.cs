namespace Lab3;

public partial class ChangeParameterPage : ContentPage
{
	private Button elementToChange;

	private int elementIndex;

	private int parameterIndex;

	public ChangeParameterPage(Button elementToChange, int elementIndex, int parameterIndex)
	{
		InitializeComponent();

		this.elementToChange = elementToChange;
		this.elementIndex = elementIndex;
		this.parameterIndex = parameterIndex;
	}

	private async void BtnClicked(object sender, EventArgs e)
	{
		elementToChange.Text = Entry.Text;

        SoftwareRepository softwareListRepository = SoftwareRepository.GetInstance();

		Software software = softwareListRepository.GetById(elementIndex);

		switch (parameterIndex)
		{ 
			case 0:
				software.Name = Entry.Text;
				break;

			case 1:
                software.Annotation = Entry.Text;
                break;

			case 2:
				software.Type = Entry.Text;
				break;

			case 3:
				software.Version = Entry.Text;
				break;

			case 4:
				software.Author = Entry.Text;
				break;

			case 5:
				software.TermsOfUsage = Entry.Text;
				break;

			case 6:
				software.DistributiveLocation = Entry.Text;
				break;
        }

        await Navigation.PopAsync();
	}
}