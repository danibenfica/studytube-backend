# 📘 Anexo F — Manual do consumidor da API

> **PÚBLICO** No backend, o "usuário" do manual é quem **consome a API**: o frontend web, um futuro app mobile ou um desenvolvedor testando pelo Swagger. O manual do estudante (telas) está na documentação do frontend.

| Seção | Conteúdo |
|---|---|
| F.1 Finalidade e audiência | Desenvolvedores de clientes da StudyTube API. Pré-requisito: API rodando localmente ([4.6](04.6-procedimento-operacional.md)). |
| F.2 Acesso e primeiros passos | Base `http://localhost:5126/api/videos`. Sem autenticação. Envie e receba JSON (`Content-Type: application/json`). Explore pelo Swagger em `http://localhost:5126/swagger`. |
| F.3 Tarefas principais | MAN-API-01 a MAN-API-04 abaixo. |
| F.4 Estados e resultados | Videoaula `watched: false` ⇄ `watched: true` ([5.4](05-diagramas-e-fluxos.md#54-uml-state-machine--entidade-video)). Erros de negócio sempre em `{ "error": "..." }`. |
| F.5 Problemas frequentes | Ver MAN-API-04. |
| F.6 Suporte / privacidade | Não envie dados pessoais: a API guarda só dados públicos do vídeo. Nunca peça nem exponha a chave do YouTube no cliente. |
| F.7 Versão e atualização | Compatível com `main` @ `382c379`. Owner: Daniele Benfica. Mudanças de contrato aparecem no [Anexo E](anexo-e-registro-de-revisao.md). |

## MAN-API-01 — Listar videoaulas

```http
GET http://localhost:5126/api/videos
```

Resposta `200`:

```json
[
  {
    "id": "ff28bf3c-082d-43e1-9d64-25d656ae0b9d",
    "youTubeId": "dQw4w9WgXcQ",
    "title": "Aula de DDD",
    "channelTitle": "Canal",
    "thumbnailUrl": "https://img",
    "duration": "00:10:00",
    "watched": false,
    "addedAt": "2026-10-19T13:46:41.8172481",
    "watchedAt": null
  }
]
```

*(dados de exemplo)*

## MAN-API-02 — Adicionar videoaula

```http
POST http://localhost:5126/api/videos
Content-Type: application/json

{ "url": "https://www.youtube.com/watch?v=dQw4w9WgXcQ" }
```

Resposta `201` com o `VideoDto` e o cabeçalho `Location`. Formatos aceitos: `watch?v=`, `youtu.be/`, `/embed/`, `/shorts/`, nos hosts `youtube.com`, `www.youtube.com` e `m.youtube.com`, sempre com `https://` (URL absoluta).

## MAN-API-03 — Marcar e desmarcar

```http
PATCH  http://localhost:5126/api/videos/{id}/watched
DELETE http://localhost:5126/api/videos/{id}/watched
```

Use o `id` (GUID) da videoaula, **não** o `youTubeId`. Ambos devolvem `200` com o `VideoDto` atualizado.

## MAN-API-04 — Tratar erros

| Resposta | Quando | O que o cliente deve fazer |
|---|---|---|
| `400 { "error": "..." }` | Regra de negócio (mensagens em [4.4.2](04.4-contrato-da-api.md#442-mensagens-de-erro-de-negócio-400--error---)) | Mostrar a mensagem ao usuário como está. |
| `400` Problem Details (`errors.Url`) | Corpo sem `url` | Validar o campo antes de enviar. |
| `404` | `id` inexistente ou não-GUID | Recarregar a lista. |
| `500` | Falha na YouTube Data API ou erro inesperado | Mostrar mensagem genérica e permitir tentar de novo. |
| Falha de rede | API parada ou porta errada | Avisar que a API está fora do ar. |

> **CRITÉRIO DO MANUAL** Um desenvolvedor deve conseguir listar, adicionar, marcar e desmarcar videoaulas e tratar todos os erros usando só este anexo.

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
