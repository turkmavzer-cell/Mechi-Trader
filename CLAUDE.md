# Mechi Trader — Claude için kurallar

Bu depo **Mechi Trader**: Mechi Radar stratejilerini FxPro cTrader hesabında otomatik uygulayan cBot.
Önce `README.md`, sonra `docs/` altındaki belgeleri sırayla oku.

## Kullanıcı
- Türkçe konuşur; yanıtlar Türkçe, kısa, maddeli, dalkavukluksuz.
- **Yalnızca telefon** kullanır. Bilgisayar gerektiren adım önerme; gerekiyorsa açıkça "bilgisayar gerekir" de ve alternatif sun.
- Kredi kartı isteyen servis önerme.

## Değişmez kurallar
1. **Strateji kuralları `docs/02-STRATEJI-SPEC.md`'dir.** Referans uygulama `reference/mechi-radar/` (TypeScript; Mechi Radar `src/core/` kopyası). C# Core bununla
   birebir aynı sonucu vermeli; formülü "daha iyi" diye değiştirme. Değişiklik önce Mechi Radar'da (`turkmavzer-cell/mechi-radar`) yapılır, sonra `reference/` yeniden kopyalanır.
2. **cTrader hazır indikatörlerini sinyal için kullanma**; formüller Core'da elle yazılır ve testle doğrulanır.
3. **Risk kuralları (`docs/03-RISK.md`) kodda zorunludur.** SL'siz emir, etiketsiz pozisyon, lot yuvarlayıp riski büyütme yok.
4. **Canlı hesap için kod/ayar önerme** — `docs/05-TEST-PLANI.md` §5 şartları sağlanmadan ve kullanıcı açıkça istemeden.
5. Geçmiş test sonuçlarını olduğundan iyi anlatma. Kenar ince; maliyetler hariç. "Garanti", "kesin kazanç" gibi ifade yok.
6. Core, cTrader'a bağımlı olmayan saf C#'tır; testler Linux'ta `dotnet test` ile çalışmalı.

## Derleme ve doğrulama
- Değişiklikten sonra: `dotnet build` ve `dotnet test`. Kırmızıysa push etme.
- Eşleşme testi fixture'ları `tools/export-fixtures.ts` (reference/ kodunu kullanır) ile GitHub Actions'ta üretilir. Elle CSV düzenleme.
- `.algo` sürümleri GitHub Actions ile Release'e yüklenir (`v1.0.<n>`).

## Git
- Ayrı depo: `turkmavzer-cell/mechi-trader`. Mechi Radar uygulaması `turkmavzer-cell/mechi-radar`; oradaki kodu bu projeden değiştirme.
- `reference/mechi-radar/` dosyalarını elle düzenleme; yalnızca Mechi Radar'dan yeniden kopyala ve kaynak commit'i `reference/README.md`'ye yaz.
