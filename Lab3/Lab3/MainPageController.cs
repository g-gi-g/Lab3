namespace Lab3;

class MainPageController
{
    public List<Software> Deserialize(FileResult jsonFile)
    {
        return JSONSerealizer.DeserealizeJSON(jsonFile.FullPath);
    }

    public void Serialize(List<Software> softList, string path) 
    {
        JSONSerealizer.SerealizeJSON(softList, path);
    }

    public List<Software> SearchResult(SearchParameters parameters)
    {
        return Search.SearchByParameters(parameters);
    }
}