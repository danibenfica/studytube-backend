# 🔗 Anexo D — Matriz de rastreabilidade e conformidade

## D.1 Rastreabilidade de produto

| Requisito | Caso de uso / história | Fluxo | Contrato / serviço (código) | Teste / evidência | Manual |
|---|---|---|---|---|---|
| RF01 | UC_001, UC_008 / US_001 | FLOW-VID-001 | `POST /api/videos` · `VideoService.AddFromUrlAsync` · `YouTubeApiClient.GetVideoAsync` · INT-YOUTUBE-001 | TC-BE-01, TC-BE-11, TC-BE-16 / EV-BE-01, EV-BE-02 | MAN-API-02, MAN-FE-01 |
| RF02 | UC_002 / US_002 | FLOW-VID-002 | `GET /api/videos` · `VideoService.GetAllAsync` · `VideoRepository.GetAllAsync` | ⚠️ sem teste (lacuna NC-07) | MAN-API-01, MAN-FE-02 |
| RF03 | UC_003 / US_003 | FLOW-VID-003 | `PATCH /api/videos/{id}/watched` · `VideoService.MarkAsWatchedAsync` · `Video.MarkAsWatched` | TC-BE-04, TC-BE-05, TC-BE-14, TC-BE-15, TC-BE-18, TC-BE-19 / EV-BE-01 | MAN-API-03, MAN-FE-03 |
| RF04 | UC_004 / US_004 | FLOW-VID-003 | `DELETE /api/videos/{id}/watched` · `VideoService.MarkAsUnwatchedAsync` · `Video.MarkAsUnwatched` | TC-BE-06, TC-BE-18 / EV-BE-01 | MAN-API-03, MAN-FE-03 |
| RF05 | UC_001, UC_007, UC_008 / US_001 | FLOW-VID-001 (FE_01–FE_08) | `YouTubeVideoId.FromUrl`/`Create` · `VideoService` · `VideosController` (`DomainException` → 400) | TC-BE-02, TC-BE-03, TC-BE-07 a TC-BE-10, TC-BE-12, TC-BE-13, TC-BE-17 / EV-BE-01 | MAN-API-04 |
| RF06–RF10 | US_101–US_103 (planejadas) | — | Não implementado | N/A | N/A |

| Regra | Casos de uso / fluxos | Código | Testes |
|---|---|---|---|
| RN01 | UC_001 FE_04, UC_007 | `YouTubeVideoId.Create` | TC-BE-09 |
| RN02 | UC_001 FE_02, FE_03, UC_007 | `YouTubeVideoId.FromUrl` | TC-BE-07, TC-BE-08 |
| RN03 | UC_001 FE_08 | `Video.Create` | TC-BE-02 |
| RN04 | UC_001 FE_08 | `Video.Create` | TC-BE-03 |
| RN05 | UC_001 FE_05 | `VideoService.AddFromUrlAsync`, índice único | TC-BE-10, TC-BE-12, TC-BE-17 |
| RN06 | UC_003 FE_02 | `Video.MarkAsWatched` | TC-BE-05 |
| RN07 | UC_004 | `Video.MarkAsUnwatched` | TC-BE-06 |
| RN08 | UC_001 passo 6 | `Video.Create` | TC-BE-01 |
| RN09 | UC_001 FE_06, UC_008 FE_01 | `VideoService.AddFromUrlAsync` | TC-BE-13 |

## D.2 Matriz de conformidade (critérios internos)

| ID / fonte | Critério verificável | Evidência | Status / ação |
|---|---|---|---|
| DOC-001 / Política §1 | Projeto tem escopo, responsáveis e referências identificados. | [Índice](README.md), [Anexo A](anexo-a-ficha-de-projeto.md) | PASS |
| REQ-001 / Disciplina + 29148 | RF, RNF e RN numerados e ligados a casos de uso. | [4.1](04.1-requisitos-e-regras.md) | PASS |
| UC-001 / Disciplina | Casos de uso no formato completo com FA/FE numerados e destino definido. | [4.2](04.2-casos-de-uso.md) | PASS |
| US-001 / Disciplina | Histórias "Como/quero/para" com critérios de aceitação ligados aos fluxos. | [4.3](04.3-historias-de-usuario.md) | PASS |
| ARC-001 / Política §5 | Contexto, contêineres, sequência, estados e implantação documentados e conferidos com o código. | [Seção 5](05-diagramas-e-fluxos.md) | PASS |
| INT-001 / Política Anexo C | Integração externa cadastrada sem expor segredo. | [Anexo C](anexo-c-integracao-youtube-data-api.md) | PASS |
| TST-001 / Política §4 | Todo RF implementado tem teste automatizado. | [4.5](04.5-testes-e-evidencias.md) | PARTIAL — RF02 sem teste (NC-07). |
| USR-001 / Política Anexo F | Consumidor da API tem instruções atuais. | [Anexo F](anexo-f-manual-do-consumidor-da-api.md) | PASS |
| ISO 42010 / 29119-3 / 26514 §[ ] | Requisitos de cláusula das normas. | — | N/A — edição oficial licenciada não consultada; não avaliado. |

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
