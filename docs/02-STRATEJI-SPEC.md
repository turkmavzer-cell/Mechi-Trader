# Strateji tanımı (tek doğru kaynak)

Botun sinyalleri Mechi Radar'daki TypeScript koduyla **aynı** olmalı. Çelişki olursa referans koddur:

| Konu | Referans dosya (`reference/mechi-radar/`) |
|---|---|
| İndikatör formülleri | `indicators.ts` |
| SRA ve filtreleri, üst zaman dilimi | `sratr.ts` |
| Ortak pozisyon motoru, SAR + MACD, Squeeze | `boxes.ts` |
| Mum birleştirme (20dk, 2s, 4s) | `candles.ts` |

**İlk sürümde yalnızca SAR + EMA 200 + MACD uygulanır** (kullanıcı kararı: `#Japan225`, 15dk; bkz. §4a). Üst zaman dilimi gerekmez.
SRA ve diğerleri parametreyle seçilebilir hale sonraki aşamada gelir.

## 1. Genel kurallar (tüm stratejiler)

- Sinyaller yalnızca **kapanmış mumda** hesaplanır (cBot'ta `OnBar`: yeni mum açıldığında bir önceki mum kapanmıştır).
- Giriş: sinyal mumunun kapanışından hemen sonra, **piyasa emri**.
- ATR = ATR(14), Wilder yumuşatması, sinyal mumundaki değer.
- **Stop mesafesi = 1,5 × ATR**, **hedef mesafesi = 2 × stop mesafesi = 3 × ATR** (1:2).
  - Mechi Radar mesafeleri sinyal mumu kapanışından ölçer. Botta mesafeler **gerçekleşen giriş fiyatından** uygulanır
    (R sabit kalsın diye). Fark küçüktür; eşleşme testinde yalnızca giriş zamanı ve yönü karşılaştırılır.
- Pozisyon yalnızca stop ya da hedefle kapanır. Ters sinyal pozisyonu kapatmaz.
- **Aynı sembol + aynı strateji için tek pozisyon.** Açık pozisyon varken gelen sinyaller yok sayılır (Mechi Radar'daki `busyUntil`).
- Stop ve hedef, emirle birlikte brokere gönderilir (sunucu tarafı SL/TP). Bot kapansa bile pozisyon korunur.

## 1b. Takip eden kâr al (parametre `TrailingTp`, varsayılan açık önerilir)

Mechi Radar'daki "Takip eden TP" düğmesiyle aynı kural (referans: `boxes.ts → simulate`, `params.trail`):

- Stop aynı: giriş ∓ 1,5 ATR. Hedef seviyesi aynı: 2R.
- Fiyat hedefe ulaşınca pozisyon **kapanmaz**: stop **hedef seviyesine** çekilir (en az +2R güvence) ve fiyat kâr yönünde gittikçe
  görülen en iyi fiyatın `TrailAtr × ATR` gerisinden izler (`TrailAtr = 1,5`; ATR giriş mumundaki değer). Stop yalnızca kâr yönünde hareket eder.
- Fiyat takip stopuna dönünce pozisyon kapanır.
- **Botta uygulama:** TP brokere gönderilmez (yoksa hedefte kapanır). Emir yalnızca SL ile açılır; bot `OnTick`'te fiyat hedefe
  ulaşınca `ModifyPosition` ile SL'yi hedefe taşır, sonra en iyi fiyat değiştikçe SL'yi günceller. SL her zaman brokerdedir;
  bot durursa pozisyon son SL seviyesinde korunur (kâr en az hedef kadar, hedefe ulaşılmadıysa −1R).
- Mechi Radar'da takip mum bazında (hedef mumundan sonraki mumdan itibaren), botta tik bazında çalışır; sonuçlar birebir aynı olmaz.
  Eşleşme testinde yalnızca giriş/stop/hedef ve takip başlangıcı karşılaştırılır.
- Geçmiş test (Mechi Radar `research/SRATR.md`): tüm stratejilerde işlem başına ortalama kazanç yaklaşık iki katı; Japan 225 SAR + MACD
  15dk'da fark küçük (+31,0R → +28,6R, 82 işlem).

## 2. İndikatör formülleri (TradingView ile uyumlu)

Seri başındaki yetersiz veri NaN'dır; NaN içeren hesaplar sinyal üretmez.

- **SMA(n):** son n değerin aritmetik ortalaması.
- **RMA(n) (Wilder):** ilk değer = ilk n geçerli değerin SMA'sı; sonra `rma = (rma_önceki × (n−1) + x) / n`.
- **EMA(n):** `k = 2/(n+1)`; ilk değer = ilk n değerin SMA'sı; sonra `ema = x·k + ema_önceki·(1−k)`.
- **RSI(14):** `kazanç = max(Δkapanış, 0)`, `kayıp = max(−Δkapanış, 0)` (ilk mumda yok);
  `RSI = 100 − 100 / (1 + RMA14(kazanç) / RMA14(kayıp))`; kayıp ortalaması 0 ise RSI = 100.
- **RSI ortalaması:** RSI'ın SMA 14'ü; RSI'ın ilk geçerli değerinden itibaren hesaplanır.
- **Stokastik (14, 3, 3):** `ham = 100 × (kapanış − en düşük14) / (en yüksek14 − en düşük14)`; en yüksek = en düşük ise 50.
  `%K = SMA3(ham)`, `%D = SMA3(%K)`. Strateji **%K** kullanır. En yüksek/en düşük mumların high/low değerleridir.
- **ATR(14):** `TR = max(H−L, |H−öncekiC|, |L−öncekiC|)`, ilk mumda `H−L`; `ATR = RMA14(TR)`.

> cTrader'ın hazır `RelativeStrengthIndex`, `StochasticOscillator`, `AverageTrueRange` indikatörleri bu formüllerden
> (özellikle başlangıç ve yumuşatma türünde) farklı olabilir. **Hazır indikatör kullanılmaz**; yukarıdaki formüller
> `Indicators/` altında elle yazılır ve birim testle Mechi Radar çıktısına karşı doğrulanır.

## 3. Üst zaman dilimi

| İşlem zaman dilimi | Üst zaman dilimi |
|---|---|
| 15dk, 20dk | 1s |
| 30dk | 2s |
| 1s | 4s |
| 2s, 4s | 1g |
| 1g | Haftalık |

- Bir mumun kapanış anında yalnızca **o ana kadar kapanmış** üst zaman dilimi mumları kullanılır.
  Kural: üst mumun `açılış + süre ≤ alt mumun açılış + süre`.
  Haftalık mum Pazartesi 00:00 UTC'de başlar, süresi 7 gündür.
- cTrader'da oluşmakta olan son üst mum `Bars.Last(0)`'dır; **kullanılmaz**.

## 4a. SAR + EMA 200 + MACD — ilk sürüm

Referans: `boxes.ts → BOX_CANDIDATES['sarmacd']`, `indicators.ts → psar, macd, ema`.

**Parabolic SAR (0,02 / 0,02 / 0,2)**, mum dizisi `H, L`:
- `sar[0]` yok. `yukarı = H[1] ≥ H[0]`; `sar = yukarı ? L[0] : H[0]`; `ep = yukarı ? H[1] : L[1]`; `af = 0,02`; `sar[1] = sar`.
- Her `i ≥ 2`: `sar = sar + af·(ep − sar)`.
  - Yukarı trendde: `sar = min(sar, L[i−1], L[i−2])`; `L[i] < sar` ise dönüş: `yukarı = false`, `sar = ep`, `ep = L[i]`, `af = 0,02`;
    değilse `H[i] > ep` ise `ep = H[i]`, `af = min(af + 0,02, 0,2)`.
  - Aşağı trendde simetrik: `sar = max(sar, H[i−1], H[i−2])`; `H[i] > sar` ise dönüş; değilse `L[i] < ep` ise `ep = L[i]`, `af` artar.
  - `sar[i] = sar`.

**MACD (12, 26, 9):** `çizgi = EMA12(C) − EMA26(C)`; `sinyal = EMA9(çizgi)`, çizginin ilk geçerli değerinden başlar (SMA ile tohumlanır). **EMA 200** kapanıştan.

**Sinyal** (kapanan mum `i`):
1. `yön[i] = C[i] > sar[i] ? +1 : −1` (sar yoksa yön yok).
2. `yön[i−1]` geçerli ve `yön[i] ≠ yön[i−1]` ise dönüş var.
3. Dönüş `+1`'e ve `C[i] > EMA200[i]` ve `çizgi[i] > sinyal[i]` → **LONG**.
4. Dönüş `−1`'e ve `C[i] < EMA200[i]` ve `çizgi[i] < sinyal[i]` → **SHORT**.

Not: yön, SAR'ın iç trend durumuna göre değil **kapanışın SAR'a göre konumuna** göre belirlenir (referansla aynı olsun diye).

**Veri ihtiyacı:** EMA 200 için en az ~300 kapanmış 15dk mum (`MarketData.GetBars` geçmişi yeterli olmalı).

## 4. SRA (Stokastik-RSI-ATR) — sonraki sürüm

Her kapanan mum `i` için:

1. `yukarı = RSI[i−1] ≤ Ort[i−1] ve RSI[i] > Ort[i]`, `aşağı = RSI[i−1] ≥ Ort[i−1] ve RSI[i] < Ort[i]`.
2. Kapanmış **son 3** üst zaman dilimi mumunun %K değerlerine bak: birinde `< 20` ise `düşük`, birinde `> 80` ise `yüksek`.
3. `yukarı ve düşük` → **LONG**; `aşağı ve yüksek` → **SHORT**; değilse sinyal yok.
4. ATR geçersizse sinyal yok.

## 5. Sonraki sürümler (parametreyle seçilir)

- **SRA + EMA 200:** SRA şartına ek olarak LONG için `kapanış > EMA200`, SHORT için `kapanış < EMA200`.
- **SRA + ADX:** SRA şartına ek olarak `ADX(14) < 25`. ADX, +DM/−DM ve TR'nin RMA14'ü, `DX = 100·|+DI − −DI| / (+DI + −DI)`, `ADX = RMA14(DX)`.
- **TTM Squeeze:** Bollinger (20, 2) önceki mumda Keltner (20, 1,5 × ATR20, orta = EMA20) içindeydi, bu mumda değil
  (sıkışma bitti); momentum = `linreg(kapanış − ((en yüksek20 + en düşük20)/2 + SMA20)/2, 20)`;
  `mom > 0`, `mom > mom[i−1]`, `kapanış > SMA50` → LONG; tersi SHORT.

## 6. Bilinen farklar (kabul edilen)

- Mechi Radar mumları Yahoo verisinden, bot mumları FxPro fiyatından gelir. Fiyatlar, seans saatleri ve mum sınırları farklı
  olabilir (ör. Yahoo `NIY=F` vadeli ile FxPro Japan 225 CFD). Bu yüzden **aynı veri üzerinde** eşleşme beklenir, farklı
  veri kaynaklarında birebir aynı sinyal beklenmez. Bkz. `05-TEST-PLANI.md`.
- 20dk mum: cTrader'da `TimeFrame.Minute20` vardır; Mechi Radar 20dk'yı 5dk mumlardan birleştirir.
