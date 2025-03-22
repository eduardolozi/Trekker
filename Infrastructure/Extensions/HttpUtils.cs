namespace Infrastructure.Extensions;

public static class HttpUtils
{
    public static async Task ValidateResponse(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error: {response.StatusCode} - {errorContent}");
        }
    }
}