using System.Text.Json;

namespace Lab3;

static class JSONSerializer
{
    public static void SerializeJSON(List<Software> databaseList, string path)
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        using (FileStream fileStream = new FileStream(path, FileMode.Create)) 
        {
            JsonSerializer.Serialize(fileStream, databaseList, options);
        }
    }

    public static List<Software> DeserializeJSON(string path)
    {
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            var result = JsonSerializer.Deserialize<List<Software>>(fileStream);
            fileStream.Close();
            return result;
        }
    }
}