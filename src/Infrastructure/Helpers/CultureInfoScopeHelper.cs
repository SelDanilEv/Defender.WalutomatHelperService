using System.Globalization;

namespace Defender.WalutomatHelperService.Infrastructure.Helpers;

public class CultureInfoScopeHelper : IDisposable
{
    private readonly CultureInfo originalCulture;

    public CultureInfoScopeHelper(string cultureName)
    {
        this.originalCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo(cultureName);
        CultureInfo.CurrentUICulture = new CultureInfo(cultureName);
    }

    public void Dispose()
    {
        CultureInfo.CurrentCulture = originalCulture;
        CultureInfo.CurrentUICulture = originalCulture;
    }
}