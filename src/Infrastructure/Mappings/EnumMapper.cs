namespace Defender.WalutomatHelperService.Infrastructure.Mappings;

public class EnumMapper<TEnumTo>
    where TEnumTo : Enum
{
    public static bool TryMapEnum<TEnumFrom>(TEnumFrom value, out TEnumTo mappedValue) where TEnumFrom : Enum
    {
        return TryMapEnum(value.ToString(), out mappedValue);
    }

    public static bool TryMapEnum(string value, out TEnumTo mappedValue)
    {
        try
        {
            mappedValue = (TEnumTo)Enum.Parse(typeof(TEnumTo), value);
            return true;
        }
        catch (ArgumentException)
        {
            mappedValue = default;
            return false;
        }
    }
}