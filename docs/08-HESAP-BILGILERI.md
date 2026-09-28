# Hesap ve sembol bilgileri (kullanıcının telefonundan doğrulandı)

Tarih: 2026-09-28. Kaynak: kullanıcının FxPro cTrader Mobile (sürüm 5.10.200) ekran görüntüleri.

## Hesaplar

| Hesap | Platform | Tür | Not |
|---|---|---|---|
| #10650955 | cTrader (FxPro) | **Demo**, Hedging, USD, 1:200, 1.000 $ | Bot testi bu hesapta |
| #8267433 | cTrader (FxPro) | Gerçek, Hedging, 0 $ | Canlıya geçiş şartları sağlanana kadar kullanılmaz |
| #9604903 | MT4 Standard | Gerçek | Bu projede kullanılmaz |

- Uygulama: **FxPro cTrader** (FxPro'nun cTrader sürümü). Spotware'in genel cTrader uygulamasında FxPro girişi FxPro uygulamasına yönlendiriyor;
  FxPro cTrader ile sorunsuz.
- cTrader'ım → **algoritmik** → **cBot'lar**: "cTrader Bulut üzerinden her platformda 7/24 çalışan otomatik işlem robotları" — **bulut çalıştırma mevcut**.
- Henüz doğrulanmadı: kendi `.algo` dosyamızı telefondan yükleme (Aşama 1'deki "Merhaba" bot ile denenecek).

## Semboller (Japan 225)

| Sembol | Açıklama |
|---|---|
| `#JP225_M26`, `#JP225_U26`, `#JP225_Z26` | Japan225 Nikkei Index Futures (Haziran/Eylül/Aralık 2026) — vadeli, vade sonunda rollover gerekir |
| `#Japan225` | Japan 225 Spot Index — vadesiz; **bot için tercih edilen** (sözleşme bilgileri henüz alınmadı) |

### `#JP225_Z26` sözleşme bilgileri

| Alan | Değer |
|---|---|
| Karşıt varlık | USD |
| Min. değişim / ondalık | 1 / 0 |
| **Lot büyüklüğü** | **5 endeks** → 1 lot = 5 $/puan, 0,01 lot = 0,05 $/puan |
| Komisyon / swap | 0 / 0 |
| Min. / maks. işlem, adım | 0,01 / 500 lot, adım 0,01 |
| Spread (gözlenen) | 10 puan |
| Saatler | Pzt–Cum 01:00:45 – 23:59:45 (UTC+3) |
| Son işlem / vade | 07 Dec 10:00 / 11 Dec 00:59 |
| Kaldıraç kademeleri | ≤500k 1:50, ≤1m 1:25, ≤2m 1:10, ≤3m 1:6,25, >3m 1:5 |

## Lot hesabı sonucu (önemli)

- 4s'te stop ≈ 850 puan → 0,01 lot riski ≈ **42 $**. 1.000 $ bakiye, %1 risk (10 $) ile en küçük lot bile fazla → bot işlem **açmaz**.
- 20dk'da stop ≈ 270 puan → 0,01 lot ≈ 13,5 $ (%1,35).
- Seçenekler: demo bakiyeyi ~4.500 $ yapmak, `#Japan225` spot sözleşmesinin puan değerine bakmak ya da daha kısa zaman dilimi.
  Risk oranını %4–5'e çıkarmak önerilmez. Karar kullanıcıyla verilecek.
