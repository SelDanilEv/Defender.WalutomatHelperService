namespace Defender.WalutomatHelperService.Infrastructure.Clients.Walutomat.Generated;

public partial class WalutomatClient
{
    public void SetApiKey(string apiKey)
    {
        _httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);
    }
}
