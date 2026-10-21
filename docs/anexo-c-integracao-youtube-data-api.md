# 🌐 Anexo C — Cadastro de integração externa: YouTube Data API v3

| Campo | Preenchimento |
|---|---|
| Identificação | INT-YOUTUBE-001 \| owner: Daniele Benfica \| status: em uso (desenvolvimento local) |
| Provedor / finalidade | Google LLC — YouTube Data API v3. Finalidade: obter título, canal, miniaturas e duração de um vídeo a partir do ID. Suporte: documentação pública do Google para desenvolvedores; não há contrato de suporte. |
| Responsabilidade técnica | Repositório `studytube-backend`. Contrato interno `IYouTubeApiClient` e `YouTubeVideoInfo` (`StudyTube.Services`); implementação `YouTubeApiClient` (`StudyTube.Data/ExternalServices/YouTube`); registro em `Program.cs` com `AddHttpClient`. |
| Contrato | API REST pública do Google, versão v3. Recurso usado: `GET videos` com `part=snippet,contentDetails`, `id={youTubeId}` e `key`. |
| Ambientes e endpoints | Base única para todos os ambientes: `https://www.googleapis.com/youtube/v3/`. Não há sandbox separado. |
| Autenticação / autorização | Chave de API (API key) enviada no parâmetro `key`. A chave é lida de `YouTube:ApiKey` e guardada em **User Secrets** no desenvolvimento (`UserSecretsId` no `StudyTube.Api.csproj`). Não fica em `appsettings.json`, no Git, no frontend nem neste documento. Recomenda-se restringir a chave à YouTube Data API v3 no Google Cloud Console. |
| Dados e mapeamento | `items[0].snippet.title` → `Title` · `items[0].snippet.channelTitle` → `ChannelTitle` · `items[0].snippet.thumbnails.high.url` (senão `default.url`, senão vazio) → `ThumbnailUrl` · `items[0].contentDetails.duration` (ISO 8601, ex.: `PT4M13S`) → `Duration` via `XmlConvert.ToTimeSpan`. Envia somente o ID público do vídeo; **nenhum dado pessoal** é enviado. |
| Semântica transacional | Consulta somente leitura e sem efeito no provedor. É chamada **depois** da verificação de duplicidade e **antes** de gravar: se falhar, nada é gravado. Não há cache dos metadados. |
| Falhas | `items` vazio → `null` → `400 "Vídeo não encontrado no YouTube."` (RN09). Erro HTTP do Google (chave ausente, inválida ou sem permissão, cota excedida) ou falha de rede → `HttpRequestException` **não tratada** → `500` sem corpo `{ "error" }`. Timeout: padrão do `HttpClient` (100 s), sem configuração própria. **Sem retry** e sem circuit breaker. |
| Operação | Sem SLA contratado. A cota e os limites de uso são os definidos pelo Google para o projeto do Cloud Console; o código não controla nem registra consumo de cota. Observabilidade: apenas logs do ASP.NET Core no console. |
| Rastreabilidade | RF01, RF05, RNF04 · UC_001, UC_008 · FLOW-VID-001 · TC-BE-11, TC-BE-13 (com *fake*; a integração real não tem teste automatizado) · decisão "Integração com o YouTube" em [3.3](03-arquitetura-da-informacao.md#33-decisões-de-arquitetura-registradas). |

---

## 📚 Referências

<!-- REFS-INICIO -->
- COCKBURN, Alistair. **Escrevendo casos de uso eficazes**: um guia prático para desenvolvedores de software. Tradução: Roberto Vedoato. Porto Alegre: Bookman, 2005. (Ciência da computação). ISBN 9788536304571.
- PRESSMAN, Roger S.; MAXIM, Bruce. **Engenharia de software**: uma abordagem profissional. Tradução: Francisco Araújo da Costa. 9. ed. Porto Alegre: McGraw-Hill, 2021. E-book. ISBN 9786558040118.
- REINEHR, Sheila. **Engenharia de requisitos**. Porto Alegre: SAGAH, 2020. E-book. (Engenharia de software). ISBN 9786556900674.
- EASY AGILE. **Use cases vs. user stories**: how they differ and when to use them. 8 jun. 2021. Disponível em: https://www.easyagile.com/blog/use-cases-vs-user-stories/. Acesso em: 21 out. 2026.
- PUCKETT, Dan. **Será que os casos de uso têm lugar no Scrum?** Tradução: Marcelo Costa. InfoQ Brasil, 19 nov. 2010. Disponível em: https://www.infoq.com/br/news/2010/11/use-cases-in-scrum/. Acesso em: 21 out. 2026.
- Lista completa (normas ISO/IEC/IEEE, UML, C4, OpenAPI e material da disciplina): [referencias.md](referencias.md).
<!-- REFS-FIM -->
