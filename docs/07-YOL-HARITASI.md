# Yol haritası

## Aşama 0 — Doğrulama (kullanıcı, telefon)
- [x] FxPro cTrader demo hesabı açıldı (#10650955, 1.000 $)
- [x] cTrader Mobile'da Algo → cBots → Cloud seçeneği var (FxPro cTrader, "cTrader Bulut 7/24")
- [ ] Deneme `.algo` dosyası telefondan yüklenebildi (Aşama 1'deki "Merhaba" botu)
- [x] Japan 225 sembol bilgileri alındı; bot `#Japan225` (spot) kullanacak — bkz. `08-HESAP-BILGILERI.md`

## Aşama 1 — İskelet ve derleme hattı
- [ ] Depo kökünde .NET çözümü: Core, Bot, Tests
- [ ] GitHub Actions: test + `.algo` derleme + Release (`v1.0.<n>`)
- [ ] "Merhaba" cBot: başlayınca sembol, zaman dilimi, bakiye, son kapanmış mum ve lot bilgilerini loglar; işlem açmaz

## Aşama 2 — Core: indikatörler ve SAR + EMA 200 + MACD
- [ ] Sma, Rma, Ema, Atr, Macd, Psar (+ birim testleri)
- [ ] SAR + MACD sinyali ve pozisyon simülasyonu (Mechi Radar `simulate` ile aynı)
- [ ] `tools/export-fixtures.ts` (reference/ kodu + Yahoo) + GitHub Actions ile CSV üretimi
- [ ] Eşleşme testleri yeşil

## Aşama 3 — Bot: emir ve risk
- [ ] Hacim hesabı, SL/TP'li piyasa emri, etiket
- [ ] Günlük limit, art arda stop, toplam düşüş, spread filtresi, `Enabled`
- [ ] Takip eden kâr al (`TrailingTp`, spec §1b): TP brokere gönderilmez, hedefte SL en iyi fiyatın 1,5 ATR gerisine, `OnTick`'te takip
- [ ] Yeniden başlatmada açık pozisyonu tanıma (takipteki pozisyonun takip durumunu da SL seviyesinden geri kurma)
- [ ] Demo'da ilk işlem (kullanıcı kontrol eder)

## Aşama 4 — Demo ileri testi (en az 8 hafta)
- [ ] Haftalık kontrol listesi (`05-TEST-PLANI.md` §4)
- [ ] Canlıya geçiş şartları değerlendirmesi

## Aşama 5 — Genişletme (isteğe bağlı)
- [ ] SRA (+ üst zaman dilimi eşlemesi), SRA + EMA 200, SRA + ADX, Squeeze
- [ ] Birden fazla sembol / zaman dilimi (canlıda en fazla 10 bulut örneği)
- [ ] Canlı hesap (kullanıcı onayıyla, düşük risk)
