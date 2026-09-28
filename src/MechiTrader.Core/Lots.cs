namespace MechiTrader.Core;

/// <summary>
/// Birim (units) ↔ lot çevirisi. cTrader hacmi birim cinsinden ister; lot = birim / LotSize.
/// </summary>
public static class Lots
{
    public static double FromUnits(double units, double lotSize) => units / lotSize;

    /// <summary>
    /// Birim cinsinden 1 fiyat puanı (1,0 fiyat değişimi) hareketinin hesap para birimindeki değeri, verilen hacim için.
    /// pipValuePerUnit: cTrader Symbol.PipValue (1 birim için 1 pip değeri), pipSize: Symbol.PipSize.
    /// </summary>
    public static double PointValue(double units, double pipValuePerUnit, double pipSize) =>
        units * pipValuePerUnit / pipSize;
}
