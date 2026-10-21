# 🎯 1. Objetivo, escopo e princípios

**Objetivo.** Permitir que qualquer pessoa entenda o propósito da StudyTube API, os fluxos ponta a ponta, os sistemas envolvidos, a integração com o YouTube, a validação, a operação e o consumo da API **sem começar pelo código-fonte**.

**Escopo.** Repositório `studytube-backend`: solution .NET 10 (`StudyTube.slnx`) com os projetos `StudyTube.Api`, `StudyTube.Services`, `StudyTube.Domain`, `StudyTube.Data` e `StudyTube.Tests`, banco SQLite e integração com a YouTube Data API v3.

**Fora de escopo deste documento.** A interface web (documentada em DOC-STB-FE-001, no repositório `studytube-frontend`) e os itens ainda planejados no guia da atividade: `Playlist`, `PlaylistsController`, `UserStudyProgress`, aplicativo mobile em React Native, `StudyTube.Publisher` e `StudyTube.DocumentGenerator`. Eles aparecem nos requisitos como 🗺️ planejados.

## 🧩 1.1 Contexto do produto

O **StudyTube** é um ecossistema de estudos para organizar videoaulas do YouTube. O estudante informa o link de um vídeo; a API extrai o identificador, consulta título, canal, miniatura e duração na YouTube Data API v3 e grava a videoaula em SQLite. Depois, o estudante lista as videoaulas e marca ou desmarca cada uma como assistida.

## 📌 1.2 Princípios aplicados neste projeto

- **Orientação por jornada:** a entrada da documentação é o fluxo "adicionar → listar → assistir → marcar", não a estrutura de pastas.
- **Fonte única:** cada informação tem um único lugar; os outros documentos apontam para ele por link (ex.: regras de negócio só em [4.1](04.1-requisitos-e-regras.md)).
- **Rastreabilidade conforme risco:** requisito → caso de uso → história → fluxo → código → teste ([Anexo D](anexo-d-rastreabilidade-e-conformidade.md)).
- **Documentação junto com o código:** a pasta `docs/` é versionada no mesmo repositório e revisada no mesmo pull request.
- **Identificação:** todo documento tem responsável, estado, versão e data (tabela de identificação no [índice](README.md)).
- **Sem segredos:** a chave da YouTube Data API **nunca** aparece neste documento nem em `appsettings.json`; ela fica em User Secrets.
- **Sem promessa de certificação:** as normas citadas orientam a estrutura; este documento não comprova conformidade ISO.

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
