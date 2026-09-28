# Telefonla kurulum

Bilgisayar gerekmez. Adımlar cTrader ve FxPro arayüzüne göre değişebilir; farklıysa ekran görüntüsüyle bildir.

## A. Hesap (bir kez)

1. FxPro Direct (uygulama veya web) → **Yeni hesap** → Platform: **cTrader** → **Demo**.
2. Play Store'dan **cTrader** (Spotware) ya da FxPro cTrader uygulamasını kur; demo hesapla giriş yap (cTrader ID).
3. Japan 225 sembolünü bul, tam adını ve sözleşme bilgilerini (lot adımı, en küçük lot) not et.

## B. Botu yükleme (her yeni sürümde)

1. GitHub → `mechi-trader` → **Releases** → en son sürüm → `MechiTrader.algo` dosyasını indir.
2. Dosyaya dokun → **cTrader ile aç** (açmazsa: cTrader → Algo → cBots → Yükle/Upload → dosyayı seç).
3. cTrader → **Algo** → **cBots** listesinde `MechiTrader` görünmeli.

## B2. Aşama 1: "Merhaba" botunu deneme

Bu sürüm **işlem açmaz**; yalnızca başlangıçta bilgi loglar. Beklenen log satırları (sıra değişebilir):

- `Merhaba · MechiTrader başladı · Aşama 1 · Merhaba · işlem AÇMAZ`
- `Hesap: #10650955 · DEMO · … · bakiye … USD`
- `Sembol: #Japan225 · zaman dilimi: … (Minute15 benzeri) …`
- `Fiyat: ondalık … · pip … · tick … · spread …`
- `Lot: 1 lot = … birim · en küçük … · adım … · en büyük …`
- `Değer: pip değeri (1 birim) … · 1 lot = … USD/puan …` (beklenen ≈ 1 USD/puan; farklıysa bildir)
- `Son kapanmış mum: açılış … UTC · A … Y … D … K …`

## C. Başlatma

1. `MechiTrader` → **+ Örnek ekle (instance)** → sembol: `#Japan225` (spot, vadeli `#JP225_…` değil), zaman dilimi: **15dk (m15)**.
2. Parametreler: `Strategy = SAR_MACD`, `RiskPercent = 1` (demo), diğerleri varsayılan.
3. **Cloud'da başlat** (Start in Cloud). Telefon kapansa da bot çalışır.
4. Log sekmesinde `Başladı · SAR_MACD · Japan225 · M15` satırını gör.

## D. Takip ve durdurma

- Pozisyonlar ve bildirimler cTrader'ın kendi ekranında.
- Acil durdurma: bot örneği → **Durdur**. Açık pozisyonlar SL/TP ile brokerde kalır; istersen elle kapat.
- Parametre değiştirmek için: durdur → parametreyi değiştir → yeniden Cloud'da başlat.
