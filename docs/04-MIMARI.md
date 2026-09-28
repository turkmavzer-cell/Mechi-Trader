# Mimari

## Klasör yapısı (hedef)

```
mechi-trader/
├── MechiTrader.sln
├── src/
│   ├── MechiTrader.Core/            # Platformdan bağımsız, saf C# (net6.0): indikatörler, sinyaller, risk hesabı
│   │   ├── Indicators/              # Sma, Rma, Ema, Rsi, Stoch, Atr, Adx, Psar, Macd, Bollinger, Keltner, LinReg
│   │   ├── Strategies/              # SarMacd.cs (ilk), sonra Sra.cs (+ filtreler), Squeeze.cs → Signal(i, dir)
│   │   ├── HigherTimeframe.cs       # Kapanmış üst mum eşlemesi, haftalık birleştirme
│   │   └── Risk/                    # Hacim hesabı, günlük limit, art arda stop sayacı
│   └── MechiTrader.Bot/             # cTrader cBot (cTrader.Automate NuGet) → .algo
│       └── MechiTraderBot.cs        # OnStart / OnBar / OnPositionClosed; Core'u çağırır, emir gönderir
├── reference/mechi-radar/        # TypeScript referans (Mechi Radar kopyası)
├── tools/export-fixtures.ts     # Referans koddan CSV fixture üretimi (GitHub Actions)
├── tests/
│   └── MechiTrader.Tests/           # xUnit: formül testleri + Mechi Radar referans CSV'leriyle eşleşme
└── fixtures/                        # Mechi Radar'dan üretilen referans mumlar ve sinyaller (CSV)
```

- **Core** cTrader'a bağımlı değildir. Böylece testler Linux'ta (GitHub Actions) çalışır.
- **Bot** ince bir katmandır: `Bars` → `double[]` dizilerine çevirir, Core'dan sinyal alır, `ExecuteMarketOrder` ile emir verir.
- Bulut kısıtları nedeniyle bot dosya yazmaz, dışarıya HTTP isteği atmaz. Loglama `Print()` ile yapılır; bildirimler için cTrader'ın
  kendi pozisyon bildirimleri kullanılır.

## cBot parametreleri

| Parametre | Tür | Varsayılan | Açıklama |
|---|---|---|---|
| `Strategy` | enum | `SAR_MACD` | `SAR_MACD` (ilk sürüm); sonra `SRA`, `SRA_EMA200`, `SRA_ADX`, `SQUEEZE` |
| `StopAtr` | double | 1,5 | Stop = StopAtr × ATR(14) |
| `RewardRisk` | double | 2,0 | Hedef = RewardRisk × stop |
| `TrailingTp` | bool | true | Takip eden kâr al (spec §1b): hedefte kapanmaz, SL hedefe çekilir ve takip eder |
| `TrailAtr` | double | 1,5 | Takip mesafesi = TrailAtr × ATR (giriş mumu) |
| `RiskPercent` | double | 1,0 | İşlem başına bakiye riski (%) |
| `MaxDailyLossR` | double | 3 | Günlük zarar limiti (R) |
| `MaxConsecutiveLosses` | int | 6 | Art arda stop sonrası duraklama |
| `MaxDrawdownPercent` | double | 15 | Toplam düşüş limiti |
| `MaxSpreadFraction` | double | 0,10 | Spread / stop mesafesi üst sınırı |
| `MaxSlippagePips` | double | 5 | Kayma sınırı |
| `Enabled` | bool | true | Acil durdurma |

Zaman dilimi ve sembol, cBot'un başlatıldığı grafikten gelir (`TimeFrame`, `SymbolName`). Üst zaman dilimi
`02-STRATEJI-SPEC.md` tablosundan hesaplanır: `MarketData.GetBars(üstTf)`.

## OnBar akışı

1. `Enabled` değilse veya durdurma kurallarından biri etkinse çık.
2. Son kapanmış mum `Bars.Last(1)`. Kapanış, yüksek, düşük dizilerini hazırla (en az 300 mum).
3. (Yalnızca SRA ailesi) Üst zaman dilimi mumlarını al; oluşan son mumu çıkar; kapanmış mum eşlemesini yap. SAR + MACD'de bu adım yok.
4. Core stratejisinden son kapanmış mum için sinyal iste.
5. Aynı etiketle açık pozisyon varsa çık.
6. Spread kontrolü, hacim hesabı, `ExecuteMarketOrder(yön, sembol, hacim, etiket, stopPips, hedefPips)`.
7. Sonucu logla: `LONG GİRİŞ 65.900 · Stop 65.671 · Hedef 66.357 · hacim 0.04 · risk 0,9%`.

## Derleme hattı (GitHub Actions)

- Tetikleyici: `main`'e push ve elle.
- Adımlar: `dotnet test` (Core) → `dotnet build -c Release` (Bot, `cTrader.Automate`) → `.algo` dosyasını Release'e ekle
  (`v1.0.<run>` etiketi).
- Telefon: Release sayfasından `.algo` indirilir, cTrader Mobile ile açılır.
