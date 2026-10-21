# 🗂️ 3. Arquitetura da informação e organização do repositório

## 3.1 Níveis documentais

| Local | Conteúdo / responsabilidade |
|---|---|
| `studytube-backend/README.md` | Porta de entrada: como rodar, configurar a chave do YouTube, endpoints e testes. |
| `studytube-backend/docs/` (este documento) | Requisitos, casos de uso, histórias, contrato, fluxos E2E, integração com o YouTube, testes, operação, rastreabilidade e revisão. |
| `studytube-frontend/docs/` | Mesma estrutura do ponto de vista da interface web: jornada, telas, validação no navegador e manual do usuário. |
| Swagger UI (`/swagger`) e `/openapi/v1.json` | Contrato OpenAPI gerado pela própria API em ambiente `Development` (fonte técnica do contrato). |

## 3.2 Organização da solution (DDD em camadas)

```text
StudyTube.slnx
├── StudyTube.Api/          Apresentação: VideosController, Requests/AddVideoRequest, Program.cs (DI, CORS, OpenAPI, migrations)
├── StudyTube.Services/     Aplicação: VideoService, VideoDto, IVideoService, IVideoRepository, IYouTubeApiClient, YouTubeVideoInfo
├── StudyTube.Domain/       Domínio: Video (entidade), YouTubeVideoId (value object), DomainException
├── StudyTube.Data/         Infraestrutura: StudyTubeDbContext, VideoConfiguration, VideoRepository, Migrations, YouTubeApiClient
├── StudyTube.Tests/        NUnit: Unit/Domain, Unit/Services, Integration/Data
└── docs/                   Esta documentação
```

| Camada | Projeto | Responsabilidades | Não pode |
|---|---|---|---|
| Apresentação | `StudyTube.Api` | Receber HTTP, converter `DomainException` em `400 { "error": ... }`, compor dependências. | Conter regra de negócio. |
| Aplicação | `StudyTube.Services` | Orquestrar os casos de uso (`AddFromUrlAsync`, `GetAllAsync`, `MarkAsWatchedAsync`, `MarkAsUnwatchedAsync`) e definir interfaces. | Conhecer EF Core ou HTTP concreto. |
| Domínio | `StudyTube.Domain` | Regras RN01–RN08 em `Video` e `YouTubeVideoId`. | Referenciar qualquer outro projeto ou framework. |
| Infraestrutura | `StudyTube.Data` | Persistência SQLite (EF Core), índice único de `YouTubeId`, cliente HTTP do YouTube. | Ser referenciada pelo domínio. |

**Direção das dependências:** `Api → Services, Data`; `Data → Services, Domain`; `Services → Domain`; `Domain →` (nenhuma). A visão gráfica está no diagrama de contêineres da [seção 5](05-diagramas-e-fluxos.md#52-c4-container).

## 3.3 Decisões de arquitetura registradas

| Decisão | Escolha atual | Justificativa |
|---|---|---|
| Nome da entidade | `Video` (o guia usa `VideoItem`) | Alinha entidade, tabela `Videos`, rota `/api/videos` e `VideoDto`. |
| Banco de dados | SQLite (`Data Source=studytube.db`) | Lista pessoal local, sem servidor de banco separado. |
| Migrations | `Database.Migrate()` na inicialização (`Program.cs`) | O banco é criado/atualizado ao subir a API; `dotnet ef database update` continua disponível. |
| Integração com o YouTube | Interface em `Services`, implementação em `Data` | Isola HTTP e permite *fakes* nos testes do `VideoService`. |
| Tratamento de erros | `DomainException` → `400 { "error": "mensagem" }` no controller | Mensagens de regra de negócio chegam prontas ao frontend. Erros de infraestrutura não são tratados (ver [Anexo C](anexo-c-integracao-youtube-data-api.md)). |
| CORS | Política `Frontends` com `AllowAnyOrigin`, `AllowAnyHeader`, `AllowAnyMethod` | Facilita o desenvolvimento local; deve ser restringida antes de publicar. |
| Porta HTTP | `5126` (perfil `http`); `7271` no perfil `https` | Mesma porta usada por `StudyTube.Api.http` e pelo padrão `VITE_API_URL` do frontend. |
| Autenticação | Ausente | Escopo atual: lista local de um estudante. |

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
