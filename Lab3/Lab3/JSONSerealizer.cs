using System.Text.Json;

namespace Lab3;

static class JSONSerealizer
{
    public static async void SerealizeJSON(List<Software> databaseList, string path)
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate)) 
        {
            await JsonSerializer.SerializeAsync(fileStream, databaseList, options);
        }
    }

    public static List<Software> DeserealizeJSON(string path)
    {
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            var result = JsonSerializer.Deserialize<List<Software>>(fileStream);

            return result;
        }
    }
}