namespace Lab3;

static class Search
{ 
    public static List<Software> SearchByParameters(SearchParameters searchParameters)
    {
        var result = (from software in searchParameters.softwareList
                      where ((searchParameters.Name == "" || software.Name == searchParameters.Name)
                      && (searchParameters.Annotation == "" || software.Annotation == searchParameters.Annotation)
                      && (searchParameters.Type == "" || software.Type == searchParameters.Type)
                      && (searchParameters.Version == "" || software.Version == searchParameters.Version)
                      && (searchParameters.Author == "" || software.Author == searchParameters.Author)
                      && (searchParameters.TermsOfUsage == "" || software.TermsOfUsage == searchParameters.TermsOfUsage)
                      && (searchParameters.DistributiveLocation == "" || software.DistributiveLocation == searchParameters.DistributiveLocation))
                      select software).ToList();

        return result;
    }
}