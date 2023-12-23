using System.Net;

namespace _3;

public static class TaskInput
{
    
    public static string GetExample(string example)
    {
        return example;
    }
    
    public static async Task<Stream> FetchAndSave(int day)
    {
        string fileName = $"../../../day{day}";
        
        if (File.Exists(fileName))
        {
            FileStream fileStream = File.OpenRead(fileName);
            return fileStream;
        }
        
        var endpointUrl = $"https://adventofcode.com/2023/day/{day}/input";

        const string cookieValue = "session=53616c7465645f5fa34cb71370763205737dc33d7de3d16360c1f1fe204ae93bed02683e3bd14d76aca903396a1b4ff001087c1fa425d2b8b70e231c3c734ec5";

        var handler = new HttpClientHandler();
        handler.CookieContainer = new CookieContainer();

        var cookieUri = new Uri(endpointUrl);
        handler.CookieContainer.SetCookies(cookieUri, cookieValue);

        using var httpClient = new HttpClient(handler);
        try
        {
            var response = await httpClient.GetAsync(endpointUrl);

            if (response.IsSuccessStatusCode)
            {
                // Read the content as a stream
                var inputStream = await response.Content.ReadAsStreamAsync();

                // Save the content to a file
                await using FileStream fileStream = File.Create(fileName);
                await inputStream.CopyToAsync(fileStream);

                return inputStream;
            }

            Console.WriteLine("Error Status Code: " + response.StatusCode);

            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Error Content: " + errorContent);

            // You may choose to throw an exception here or handle the error in some way
            throw new Exception($"Error Status Code: {response.StatusCode}, Error Content: {errorContent}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            throw;
        }
    }
}