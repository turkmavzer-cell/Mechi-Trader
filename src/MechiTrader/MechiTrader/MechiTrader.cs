using System;
using cAlgo.API;
using MechiTrader.Core;

namespace cAlgo.Robots;

/// <summary>
/// Aşama 1 "Merhaba" cBot'u. İşlem AÇMAZ; yalnızca başlangıçta hesap/sembol/mum bilgilerini loglar.
/// Amaç: .algo'nun telefondan yüklenip cTrader Cloud'da çalıştığını ve sembol sözleşme değerlerini doğrulamak.
/// Bulutta çalışmak için AccessRights.None zorunlu.
/// </summary>
[Robot(AccessRights = AccessRights.None, TimeZone = TimeZones.UTC, AddIndicators = false)]
public class MechiTrader : Robot
{
    public const string Version = "Aşama 1 · Merhaba";

    protected override void OnStart()
    {
        var tf = TimeFrame.ShortName;
        Print("Merhaba · MechiTrader başladı · {0} · işlem AÇMAZ", Version);
        Print("Etiket: {0}", BotLabel.Make("SAR_MACD", SymbolName, tf.ToUpperInvariant()));

        Print("Hesap: #{0} · {1} · {2} · bakiye {3} {4} · özsermaye {5}",
            Account.Number, Account.IsLive ? "GERÇEK" : "DEMO", Account.BrokerName,
            Fmt.N(Account.Balance, 2), Account.Asset.Name, Fmt.N(Account.Equity, 2));
        if (Account.IsLive)
            Print("UYARI: Bu bir GERÇEK hesap. Proje şu an yalnızca demo içindir.");

        Print("Sembol: {0} · zaman dilimi: {1} ({2}) · sunucu saati (UTC): {3:yyyy-MM-dd HH:mm:ss}",
            SymbolName, tf, TimeFrame.Name, Server.Time);

        LogSymbol();
        LogLastClosedBar();
    }

    private void LogSymbol()
    {
        var s = Symbol;
        Print("Fiyat: ondalık {0} · pip {1} · tick {2} · alış {3} · satış {4} · spread {5} ({6} puan)",
            s.Digits, Fmt.G(s.PipSize), Fmt.G(s.TickSize), Fmt.N(s.Bid, s.Digits), Fmt.N(s.Ask, s.Digits),
            Fmt.N(s.Spread, s.Digits), Fmt.N(s.Spread / s.PipSize, 1));

        Print("Lot: 1 lot = {0} birim · en küçük {1} birim ({2} lot) · adım {3} birim ({4} lot) · en büyük {5} lot",
            Fmt.G(s.LotSize),
            Fmt.G(s.VolumeInUnitsMin), Fmt.G(Lots.FromUnits(s.VolumeInUnitsMin, s.LotSize)),
            Fmt.G(s.VolumeInUnitsStep), Fmt.G(Lots.FromUnits(s.VolumeInUnitsStep, s.LotSize)),
            Fmt.G(Lots.FromUnits(s.VolumeInUnitsMax, s.LotSize)));

        Print("Değer: pip değeri (1 birim) {0} · tick değeri (1 birim) {1} · 1 lot = {2} {3}/puan · en küçük lot = {4} {3}/puan",
            Fmt.G(s.PipValue), Fmt.G(s.TickValue),
            Fmt.G(Lots.PointValue(s.LotSize, s.PipValue, s.PipSize)), Account.Asset.Name,
            Fmt.G(Lots.PointValue(s.VolumeInUnitsMin, s.PipValue, s.PipSize)));

        Print("İşlem açık mı: {0} · mod: {1}", s.IsTradingEnabled ? "evet" : "hayır", s.TradingMode);
    }

    private void LogLastClosedBar()
    {
        // Bars.Last(0) oluşmakta olan mumdur; son KAPANMIŞ mum Last(1).
        if (Bars.Count < 2)
        {
            Print("Mum: yetersiz veri ({0} mum)", Bars.Count);
            return;
        }
        var b = Bars.Last(1);
        var d = Symbol.Digits;
        Print("Son kapanmış mum: açılış {0:yyyy-MM-dd HH:mm} UTC · A {1} · Y {2} · D {3} · K {4} · toplam {5} mum yüklü",
            b.OpenTime, Fmt.N(b.Open, d), Fmt.N(b.High, d), Fmt.N(b.Low, d), Fmt.N(b.Close, d), Bars.Count);
    }

    protected override void OnStop()
    {
        Print("MechiTrader durdu.");
    }
}
