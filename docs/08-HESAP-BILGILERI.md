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
| `#Japan225` | Japan 225 Spot Index — vadesiz; **bot bu sembolde çalışacak** |

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

### `#Japan225` sözleşme bilgileri (seçilen sembol)

| Alan | Değer |
|---|---|
| Karşıt varlık | USD |
| Min. değişim / ondalık | 0,01 / 2 |
| **Lot büyüklüğü** | **1 endeks** → 1 lot = 1 $/puan, 0,01 lot = 0,01 $/puan |
| Komisyon | 0 |
| Min. / maks. işlem | 0,01 / 500 (adım 0,01) |
| Spread (gözlenen) | 17 puan |
| Swap uzun / kısa | −6,5306 / −3,0551 pip (3 günlük swap: Cuma). Birim ve gerçek tutar demoda ilk açık pozisyonla doğrulanacak |
| Saatler | Pzt–Cum 01:00:45 – 23:59:45 (UTC+3) |
| Kaldıraç kademeleri | ≤150k 1:200, ≤300k 1:100, ≤450k 1:50, >450k 1:33 |

## Lot hesabı sonucu

- **`#JP225_Z26`:** 4s'te stop ≈ 850 puan → 0,01 lot riski ≈ 42 $. 1.000 $ / %1 riskte en küçük lot bile fazla → bot işlem açamaz.
- **`#Japan225` (seçilen):** 4s'te stop ≈ 850 puan → 0,01 lot riski ≈ **8,5 $**. 1.000 $ / %1 risk (10 $) ile bot 0,01 lot açar,
  gerçek risk ≈ %0,85. Lot adımı 0,01 olduğu için 1.000 $ bakiyede risk kademesi kaba (0,01 lot ≈ %0,85); daha büyük bakiyede incelir.
- Spread maliyeti ≈ 17 / 850 ≈ 0,02R / işlem (4s).
- Bot, `Symbol.VolumeInUnitsMin`, `Symbol.VolumeInUnitsStep`, `Symbol.PipValue` / `TickValue` değerlerini çalışırken okumalı;
  yukarıdaki sayılar yalnızca doğrulama içindir, koda sabit yazılmaz.
