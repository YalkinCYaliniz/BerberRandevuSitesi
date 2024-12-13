using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

public class SacDeneModel
{
    private const string ApiUrl = "https://hairstyle-changer-pro.p.rapidapi.com/facebody/editing/hairstyle-pro";
    private const string ApiKey = "b833913418msh34293360c392990p168517jsnfa01e09de061";

    public async Task<string> GetStyledImage(string hairStyle, byte[] imageData)
    {
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-rapidapi-key", ApiKey);
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "hairstyle-changer-pro.p.rapidapi.com");

            var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(imageData), "file", "image.jpg" },
            { new StringContent(hairStyle), "hair_style" },
            { new StringContent("hairstyle"), "task_type" }
        };

            var response = await client.PostAsync(ApiUrl, content);
            var jsonResult = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"API Yanýtý: {jsonResult}");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonResult);
                if (result != null && result.ContainsKey("image_url"))
                {
                    return result["image_url"];
                }
                else
                {
                    Console.WriteLine("API'den dönen yanýt beklenen formatta deðil: " + jsonResult);
                }
            }
            else
            {
                Console.WriteLine($"API Hatasý: {response.StatusCode}, Yanýt: {jsonResult}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata oluþtu: " + ex.Message);
        }

        return null;
    }


}
