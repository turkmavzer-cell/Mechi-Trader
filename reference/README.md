# Referans kod (Mechi Radar kopyası)

Bu klasördeki TypeScript dosyaları **turkmavzer-cell/mechi-radar** deposunun `src/core/` klasöründen birebir kopyalanmıştır.
C# Core'un sinyalleri bu kodla aynı olmalıdır (`docs/02-STRATEJI-SPEC.md`).

- Kaynak commit: `c14e4fc` (mechi-radar, main)
- Dosyalar: `indicators.ts`, `sratr.ts`, `boxes.ts`, `candles.ts`, `types.ts`, `yahoo.ts`
- Elle düzenleme yapma. Strateji değişirse önce Mechi Radar'da değiştir, sonra dosyaları yeniden kopyala ve yukarıdaki commit'i güncelle.
- `tools/export-fixtures.ts` bu dosyaları `tsx` ile çalıştırarak eşleşme testi CSV'lerini üretir.
