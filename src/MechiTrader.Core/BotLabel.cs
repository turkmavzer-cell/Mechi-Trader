using System.Text;

namespace MechiTrader.Core;

/// <summary>
/// Pozisyon etiketi: "MechiTrader-&lt;strateji&gt;-&lt;sembol&gt;-&lt;tf&gt;" (docs/03-RISK.md → Emir güvenliği).
/// Sembol ve strateji adındaki harf/rakam dışı karakterler atılır: "#Japan225" → "Japan225", "SAR_MACD" → "SARMACD".
/// </summary>
public static class BotLabel
{
    public const string Prefix = "MechiTrader";

    public static string Make(string strategy, string symbol, string timeFrame) =>
        $"{Prefix}-{Clean(strategy)}-{Clean(symbol)}-{Clean(timeFrame)}";

    private static string Clean(string s)
    {
        var sb = new StringBuilder(s.Length);
        foreach (var ch in s)
            if (char.IsLetterOrDigit(ch)) sb.Append(ch);
        return sb.ToString();
    }
}
