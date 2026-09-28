# Yol haritası

## Aşama 0 — Doğrulama (kullanıcı, telefon)
- [x] FxPro cTrader demo hesabı açıldı (#10650955, 1.000 $)
- [x] cTrader Mobile'da Algo → cBots → Cloud seçeneği var (FxPro cTrader, "cTrader Bulut 7/24")
- [ ] Deneme `.algo` dosyası telefondan yüklenebildi (Aşama 1'deki "Merhaba" botu)
- [x] Japan 225 sembol bilgileri alındı; bot `#Japan225` (spot) kullanacak — bkz. `08-HESAP-BILGILERI.md`

## Aşama 1 — İskelet ve derleme hattı
- [ ] Depo kökünde .NET çözümü: Core, Bot, Tests
- [ ] GitHub Actions: test + `.algo` derleme + Release (`v1.0.<n>`)
- [ ] "Merhaba" cBot: başlayınca sembol, zaman dilimi, bakiye ve üst TF son kapanmış mumunu loglar; işlem açmaz

## Aşama 2 — Core: indikatörler ve SRA
- [ ] Sma, Rma, Ema, Rsi, Stoch, Atr (+ birim testleri)
- [ ] Üst zaman dilimi eşlemesi + haftalık birleştirme
- [ ] SRA sinyali ve pozisyon simülasyonu (Mechi Radar `simulate` ile aynı)
- [ ] `tools/export-fixtures.ts` (reference/ kodu + Yahoo) + GitHub Actions ile CSV üretimi
- [ ] Eşleşme testleri yeşil

## Aşama 3 — Bot: emir ve risk
- [ ] Hacim hesabı, SL/TP'li piyasa emri, etiket
- [ ] Günlük limit, art arda stop, toplam düşüş, spread filtresi, `Enabled`
- [ ] Yeniden başlatmada açık pozisyonu tanıma
- [ ] Demo'da ilk işlem (kullanıcı kontrol eder)

## Aşama 4 — Demo ileri testi (en az 8 hafta)
- [ ] Haftalık kontrol listesi (`05-TEST-PLANI.md` §4)
- [ ] Canlıya geçiş şartları değerlendirmesi

## Aşama 5 — Genişletme (isteğe bağlı)
- [ ] SRA + EMA 200, SRA + ADX, SAR + MACD, Squeeze
- [ ] Birden fazla sembol / zaman dilimi (canlıda en fazla 10 bulut örneği)
- [ ] Canlı hesap (kullanıcı onayıyla, düşük risk)
