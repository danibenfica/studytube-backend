# 🔀 Anexo B — Especificação de fluxo ponta a ponta (EFI)

## FLOW-VID-001 — Adicionar videoaula

| Campo | Preenchimento |
|---|---|
| Identificação | FLOW-VID-001 \| v1.0 \| Implementado \| Daniele Benfica |
| Nome e objetivo | Adicionar videoaula: transformar um link do YouTube em uma videoaula com metadados na lista do estudante. |
| Atores e sistemas | Usuário (estudante) → StudyTube Web (`VideoForm`, `useVideos`, `videosApi.ts`) → StudyTube API (`VideosController`, `VideoService`, `YouTubeVideoId`, `Video`, `VideoRepository`) → YouTube Data API v3 → SQLite. |
| Gatilho e pré-condições | Usuário solicita a inclusão de um link. API em execução; chave `YouTube:ApiKey` configurada. |
| Entradas | `url` (string, obrigatória). Frontend: precisa casar com `^(https?://)?(www\.)?(youtube\.com\|youtu\.be)/`. Backend: RN01, RN02. |
| Fluxo de negócio | [UC_001](04.2-casos-de-uso.md#422-uc_001--adicionar-videoaula) e [fluxo funcional (Figura 7)](05-diagramas-e-fluxos.md#56-fluxo-funcional--adicionar-videoaula). |
| Fluxo técnico | [Sequência `POST /api/videos` (Figura 4)](05-diagramas-e-fluxos.md#53-uml-sequence--post-apivideos). |
| Regras e estados | RN01–RN05, RN08, RN09. Estado inicial: não assistida. |
| Contratos e dados | `POST /api/videos` → `201` `VideoDto` ([4.4](04.4-contrato-da-api.md)); YouTube `videos?part=snippet,contentDetails` ([Anexo C](anexo-c-integracao-youtube-data-api.md)); tabela `Videos` com índice único em `YouTubeId`. |
| Falhas e recuperação | `400 { "error" }` para FE_02–FE_06 e FE_08; `400` Problem Details para FE_01; `500` para falha do YouTube (FE_07) — **sem retry, sem timeout configurado** (vale o padrão do `HttpClient`, 100 s). Nada é gravado em caso de erro, então repetir a operação é seguro: se a primeira tentativa tiver gravado, a segunda recebe "Este vídeo já está na sua lista.". |
| Resultados ao usuário | Videoaula no topo da lista, campo do link limpo e progresso recalculado; ou mensagem de erro na tela. |
| Testes e evidências | TC-BE-01 a TC-BE-03, TC-BE-07 a TC-BE-13, TC-BE-16, TC-BE-17; frontend TC-FE de `isValidYouTubeUrl`; EV-BE-01, EV-BE-02. |
| Operação / monitoramento | Somente logs do console do ASP.NET Core (`Logging:LogLevel`). Sem correlação de requisições nem alertas. |
| Manual e release | [Anexo F](anexo-f-manual-do-consumidor-da-api.md) (API) e manual web (DOC-STB-FE-001, MAN-FE-01). Versão: `main` @ `382c379`. |

## FLOW-VID-002 — Consultar lista de videoaulas

| Campo | Preenchimento |
|---|---|
| Identificação | FLOW-VID-002 \| v1.0 \| Implementado \| Daniele Benfica |
| Nome e objetivo | Mostrar ao estudante as videoaulas cadastradas, mais recentes primeiro. |
| Atores e sistemas | Usuário → StudyTube Web (`useVideos` na carga da página) → `GET /api/videos` → `VideoService.GetAllAsync` → `VideoRepository.GetAllAsync` → SQLite. |
| Gatilho e pré-condições | Abertura da página web. API em execução. |
| Entradas | Nenhuma. |
| Fluxo de negócio | [UC_002](04.2-casos-de-uso.md#423-uc_002--consultar-lista-de-videoaulas). |
| Fluxo técnico | Controller → serviço → repositório (`OrderByDescending(AddedAt)`) → `VideoDto[]`. |
| Regras e estados | Sem regra de validação. |
| Contratos e dados | `GET /api/videos` → `200` `VideoDto[]` ([4.4](04.4-contrato-da-api.md)). |
| Falhas e recuperação | API fora do ar: o frontend mostra "Não consegui falar com a API. Ela está rodando? 🥺". Recuperação: subir a API e recarregar a página. |
| Resultados ao usuário | Cards com miniatura, título, canal e duração; indicador "X de Y assistidos 🌸"; ou "Nenhum vídeo ainda... cole seu link aqui! 🐰". |
| Testes e evidências | Backend: sem teste da ordenação (lacuna). Frontend: testes do `VideoCard` e de `formatDuration`. |
| Operação / monitoramento | Logs do console. |
| Manual e release | MAN-FE-02 (manual web). |

## FLOW-VID-003 — Marcar / desmarcar videoaula como assistida

| Campo | Preenchimento |
|---|---|
| Identificação | FLOW-VID-003 \| v1.0 \| Implementado \| Daniele Benfica |
| Nome e objetivo | Registrar se o estudante já assistiu uma videoaula, para acompanhar o progresso. |
| Atores e sistemas | Usuário → StudyTube Web (`VideoCard` → `useVideos.toggleWatched`) → `PATCH` ou `DELETE /api/videos/{id}/watched` → `VideoService` → `Video.MarkAsWatched`/`MarkAsUnwatched` → SQLite. |
| Gatilho e pré-condições | Usuário escolhe "✔ Já assisti" (não assistida) ou "↩ Desmarcar" (assistida). Videoaula cadastrada. |
| Entradas | `id` (GUID) na rota. |
| Fluxo de negócio | [UC_003](04.2-casos-de-uso.md#424-uc_003--marcar-videoaula-como-assistida) e [UC_004](04.2-casos-de-uso.md#425-uc_004--desmarcar-videoaula-como-assistida). |
| Fluxo técnico | Busca por id → regra de estado → `SaveChangesAsync` → `VideoDto`. |
| Regras e estados | RN06, RN07; [máquina de estados (Figura 5)](05-diagramas-e-fluxos.md#54-uml-state-machine--entidade-video). |
| Contratos e dados | `200` `VideoDto`; `400` RN06; `404` id inexistente. |
| Falhas e recuperação | `400`/`404`: a tela mostra a mensagem e não muda o card. Operação idempotente no desmarcar; o marcar repetido é rejeitado (RN06). |
| Resultados ao usuário | Card destacado como assistido e botão trocado; contador de progresso atualizado. |
| Testes e evidências | TC-BE-04 a TC-BE-06, TC-BE-14, TC-BE-15, TC-BE-18, TC-BE-19; frontend testes do `VideoCard`. |
| Operação / monitoramento | Logs do console. |
| Manual e release | MAN-FE-03 (manual web). |

> **REGRA DE PREENCHIMENTO** O fluxo funcional descreve o valor para o estudante; o fluxo técnico descreve chamadas, responsabilidades e dados. Uma falha do YouTube (500) não significa que o vídeo é inválido: repetir mais tarde pode funcionar.

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
