using System.Globalization;

namespace MechiTrader.Core;

/// <summary>Log metinleri için kültürden bağımsız sayı biçimi (bulut sunucusunun dili farklı olabilir).</summary>
public static class Fmt
{
    public static string N(double v, int digits) => v.ToString("F" + digits, CultureInfo.InvariantCulture);

    /// <summary>Anlamlı basamakları koruyarak yazar (ör. 0.0001 → "0.0001", 1 → "1").</summary>
    public static string G(double v) => v.ToString("0.##########", CultureInfo.InvariantCulture);
}
