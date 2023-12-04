namespace Lab3;

class SoftwareRepository
{
    private static SoftwareRepository
        softwareListContainer = null;

    private List<Software> ListOfSoftware;

    private SoftwareRepository()
    {
        ListOfSoftware = new List<Software>();
    }

    public static SoftwareRepository GetInstance()
    {
        if (softwareListContainer is null)
        {
            softwareListContainer = new SoftwareRepository();
        }

        return softwareListContainer;
    }

    public void Add(Software software)
    {
        ListOfSoftware.Add(software);
    }

    public void DeleteById(int ID)
    {
        ListOfSoftware.RemoveAt(ID);
    }

    public void SetList(List<Software> listToSet)
    {
        ListOfSoftware = listToSet;
    }

    public void SetByID(int ID, Software software)
    {
        ListOfSoftware[ID] = software;
    }

    public Software GetById(int ID)
    {
        return ListOfSoftware[ID];
    }

    public List<Software> GetAll() 
    {
        return ListOfSoftware;
    }

    public List<Software> SearchByParameters(Software parameters)
    { 
        var result = (from software in ListOfSoftware
                      where ((parameters.Name is null || software.Name == parameters.Name)
                      && (parameters.Annotation is null || software.Annotation == parameters.Annotation)
                      && (parameters.Type is null || software.Type == parameters.Type)
                      && (parameters.Version is null || software.Version == parameters.Version)
                      && (parameters.Author is null || software.Author == parameters.Author)
                      && (parameters.TermsOfUsage is null || software.TermsOfUsage == parameters.TermsOfUsage)
                      && (parameters.DistributiveLocation is null || software.DistributiveLocation == parameters.DistributiveLocation))
                      select software).ToList();

        return result;
    }
}