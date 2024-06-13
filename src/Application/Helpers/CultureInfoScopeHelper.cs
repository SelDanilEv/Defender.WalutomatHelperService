using System.Globalization;

namespace Defender.WalutomatHelperService.Application.Helpers;

public class CultureInfoScopeHelper : IDisposable
{
    private readonly CultureInfo _originalCulture;

    public CultureInfoScopeHelper(string cultureName)
    {
        _originalCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo(cultureName);
        CultureInfo.CurrentUICulture = new CultureInfo(cultureName);
    }

    public void Dispose()
    {
        CultureInfo.CurrentCulture = _originalCulture;
        CultureInfo.CurrentUICulture = _originalCulture;
    }
}