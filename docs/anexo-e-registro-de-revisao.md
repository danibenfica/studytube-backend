# 🗒️ Anexo E — Registro de revisão e não conformidade

## E.1 Histórico de versões

| Versão | Data | Autor | Descrição |
|---|---|---|---|
| v1.0 | 21/10/2026 | Daniele Benfica | Primeira versão completa no padrão DOC-STD-001: requisitos, casos de uso, histórias, contrato, fluxos E2E, integração, testes, operação, rastreabilidade e manual do consumidor. Substitui `docs/requisitos.md` e `docs/arquitetura.md`. |

## E.2 Registro da revisão v1.0

| Campo | Valor |
|---|---|
| Documento / revisão | DOC-STB-BE-001 · v1.0 · código revisado: `main` @ `382c379` |
| Objeto da revisão | Coerência entre documentação e código: regras, mensagens, códigos HTTP, testes e diagramas. |
| Critérios aplicados | Política DOC-STD-001 (seções 1–7, Anexos) e padrões da disciplina (casos de uso completos, FA/FE, histórias de usuário). Cláusulas ISO não avaliadas. |
| Amostra / evidência | Todos os arquivos de `StudyTube.Api`, `StudyTube.Services`, `StudyTube.Domain`, `StudyTube.Data` e `StudyTube.Tests`; execução de `dotnet test` (EV-BE-01); CI (EV-BE-02). |
| Revisor / data | Autora: Daniele Benfica, 21/10/2026 · Revisão da mentoria: [pendente] |
| Resultado | Aprovado com ressalvas (constatações abaixo, todas não bloqueantes). |
| Decisão final | [Pendente — mentoria do projeto] |

## E.3 Constatações

| ID | Descrição factual e impacto | Ação corretiva sugerida | Estado |
|---|---|---|---|
| NC-01 | Os antigos `docs/requisitos.md` e `docs/arquitetura.md` citavam nomes anteriores à refatoração (`listarVideos`, `Progresso`, `validarLinkYoutube`, `formatarDuracao`); hoje são `getVideos`, `WatchProgress`, `isValidYouTubeUrl`, `formatDuration`. | Substituídos por esta documentação. | Corrigida na v1.0 |
| NC-02 | O antigo `docs/arquitetura.md` dizia que `StudyTube.Api.http` e o frontend apontavam para a porta `5265` e que não havia testes de componentes React. Hoje tudo usa `5126` e existe `VideoCard.test.tsx`. | Substituído por esta documentação. | Corrigida na v1.0 |
| NC-03 | Falha na YouTube Data API (chave, cota, rede) gera `500` sem `{ "error" }`; o frontend mostra só "Algo deu errado 😢". Impacto: o estudante não sabe a causa. | Tratar `HttpRequestException` no serviço ou no controller e devolver `502`/`503` com mensagem. | Aberta |
| NC-04 | O `201 Created` aponta `Location: /api/videos/{id}`, mas não existe `GET /api/videos/{id}`. | Criar o endpoint ou usar `Created` sem `Location`. | Aberta |
| NC-05 | Validação diferente entre frontend e backend: o frontend aceita link sem `https://` (o backend responde "URL inválida.") e recusa `m.youtube.com` (que o backend aceita); também aceita qualquer caminho de `youtube.com` (o backend responde "Não é uma URL de vídeo do YouTube."). | Alinhar `isValidYouTubeUrl` com RN02. | Aberta |
| NC-06 | Duas inclusões simultâneas do mesmo vídeo passam pela verificação de duplicidade e a segunda viola o índice único → `500` em vez de "Este vídeo já está na sua lista.". Impacto baixo (uso pessoal). | Tratar `DbUpdateException` de índice único como RN05. | Aberta |
| NC-07 | RF02 (ordenação da lista) e `MarkAsUnwatchedAsync` do serviço não têm teste; não há testes HTTP ponta a ponta. | Adicionar testes de repositório para a ordenação e testes com `WebApplicationFactory`. | Aberta |
| NC-08 | O guia da atividade chama a entidade de `VideoItem`; o código usa `Video`. | Decisão registrada em [3.3](03-arquitetura-da-informacao.md). | Aceita (justificada) |
| NC-09 | CORS aceita qualquer origem (`AllowAnyOrigin`). | Restringir antes de qualquer publicação. | Aceita para ambiente local |

**Critério para uma constatação.** Cada linha indica fonte, evidência (arquivo/método), desvio, impacto e ação. Revisão de eficácia: na próxima versão do documento.

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
