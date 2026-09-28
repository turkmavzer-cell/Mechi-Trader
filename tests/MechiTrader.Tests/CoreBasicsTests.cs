using MechiTrader.Core;
using Xunit;

namespace MechiTrader.Tests;

public class BotLabelTests
{
    [Fact]
    public void Etiket_riskBelgesindekiOrnekleAyni() =>
        Assert.Equal("MechiTrader-SARMACD-Japan225-M15", BotLabel.Make("SAR_MACD", "#Japan225", "m15".ToUpperInvariant()));

    [Theory]
    [InlineData("SAR_MACD", "#Japan225", "M15", "MechiTrader-SARMACD-Japan225-M15")]
    [InlineData("SRA", "EURUSD", "h4", "MechiTrader-SRA-EURUSD-h4")]
    [InlineData("SRA_EMA200", "#JP225_Z26", "H1", "MechiTrader-SRAEMA200-JP225Z26-H1")]
    public void Etiket_harfRakamDisiniAtar(string strategy, string symbol, string tf, string expected) =>
        Assert.Equal(expected, BotLabel.Make(strategy, symbol, tf));
}

public class LotsTests
{
    [Fact]
    public void BirimdenLot() => Assert.Equal(0.01, Lots.FromUnits(0.01, 1), 12);

    [Fact]
    public void Japan225_1Lot_1DolarPuan()
    {
        // 08-HESAP-BILGILERI: #Japan225 lot = 1 endeks → 1 lot = 1 $/puan. Pip/tick değerleri varsayımsal (bot gerçek değeri loglar).
        Assert.Equal(1.0, Lots.PointValue(units: 1, pipValuePerUnit: 0.01, pipSize: 0.01), 12);
        Assert.Equal(0.01, Lots.PointValue(units: 0.01, pipValuePerUnit: 1, pipSize: 1), 12);
    }

    [Fact]
    public void JP225Z26_1Lot_5DolarPuan() =>
        Assert.Equal(5.0, Lots.PointValue(units: 5, pipValuePerUnit: 1, pipSize: 1), 12);
}

public class FmtTests
{
    [Theory]
    [InlineData(1000.0, 2, "1000.00")]
    [InlineData(65900.123, 1, "65900.1")]
    public void N_noktaIleYazar(double v, int d, string expected) => Assert.Equal(expected, Fmt.N(v, d));

    [Theory]
    [InlineData(0.0001, "0.0001")]
    [InlineData(1.0, "1")]
    [InlineData(0.01, "0.01")]
    public void G_gereksizSifirYok(double v, string expected) => Assert.Equal(expected, Fmt.G(v));
}
