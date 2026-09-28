# Proje promptu

Aşağıdaki metni, `turkmavzer-cell/mechi-trader` deposu bağlı **yeni bir Claude Code oturumuna** olduğu gibi yapıştır.
Her aşama sonunda Claude durur, özet verir ve senden onay ister.

---

```text
Rolün: C# / .NET ve cTrader Automate API'sinde deneyimli bir algoritmik işlem geliştiricisisin.

Proje: Mechi Trader (turkmavzer-cell/mechi-trader deposu). Mechi Radar uygulamasının SAR + EMA 200 + MACD
stratejisini (#Japan225 spot, 15 dakika) FxPro cTrader hesabında otomatik uygulayan bir cBot geliştireceğiz. Bot cTrader Cloud'da
çalışacak ve telefondan başlatılacak.

Başlamadan önce sırayla oku ve kurallara uy:
1. CLAUDE.md
2. README.md
3. docs/01-PLATFORM-KARARI.md ... 08-HESAP-BILGILERI.md
4. Referans uygulama (TypeScript): reference/mechi-radar/indicators.ts (psar, macd, ema, atr), boxes.ts (sarmacd, simulate)

Benim hakkımda: Türkçe konuşurum, sadece telefon kullanırım, bilgisayarım yok. Kredi kartı isteyen servis kullanmam.
Yanıtların Türkçe, kısa ve maddeli olsun.

Çalışma şekli:
- docs/07-YOL-HARITASI.md'deki aşamalarla ilerle. Her aşama sonunda: ne yaptığını, test sonuçlarını ve benim telefonda
  yapmam gereken adımları yaz; onayımı almadan sonraki aşamaya geçme.
- Telefonda benim yapmam gereken adımları numaralı ve kısa yaz; ekran görüntüsü isteyebilirsin.
- Her kod değişikliğinden sonra dotnet build ve dotnet test çalıştır; kırmızıysa push etme.
- Strateji formüllerini docs/02-STRATEJI-SPEC.md'den ve TypeScript referansından birebir al. cTrader'ın hazır
  indikatörlerini sinyal için kullanma. C# sonuçlarının Mechi Radar ile eşleştiğini fixture testleriyle kanıtla.
- Risk kuralları (docs/03-RISK.md) zorunlu: SL/TP'siz emir yok, bot etiketi zorunlu, günlük zarar limiti, art arda stop
  duraklaması, toplam düşüş limiti, spread filtresi, Enabled ile acil durdurma.
- Sadece DEMO hesap için geliştir. Canlı hesaba geçişi ben açıkça istemeden önerme; istersem önce
  docs/05-TEST-PLANI.md §5 şartlarını kontrol et.
- Emin olmadığın cTrader API ayrıntısını tahmin etme; help.ctrader.com belgelerine bak ve kaynağını yaz.

Aşama 0 tamamlandı: sonuçlar docs/08-HESAP-BILGILERI.md'de (demo hesap #10650955, bulut cBot mevcut, sembol #Japan225).

İlk görev: Aşama 1'i hazırla: iskelet, GitHub Actions ile test + .algo derleme + Release, "Merhaba" cBot. Bitince .algo
dosyasını telefona nasıl indirip FxPro cTrader'a yükleyeceğimi ve bulutta nasıl başlatacağımı adım adım yaz. "Merhaba" bot işlem açmasın; başlayınca sembolü, zaman dilimini,
bakiyeyi, son kapanmış mumu ve sembolün en küçük lot / lot adımı / pip değerini loglasın.
```

---

## Notlar

- TypeScript referansı `reference/mechi-radar/` klasöründe olduğu için Claude hem referansı hem C# kodunu aynı depoda görür; eşleşme testleri bu sayede kurulur.
- İlk sürüm: SAR + EMA 200 + MACD, `#Japan225`, 15dk. Başka strateji/sembol için promptu ve `docs/05-TEST-PLANI.md` §4'ü değiştir.
