# 🪪 Anexo A — Ficha de projeto

| Campo | Informação do projeto |
|---|---|
| Projeto / identificação | StudyTube API — DOC-STB-BE-001 · repositório `studytube-backend` |
| Objetivo de negócio | Ajudar o estudante a organizar videoaulas do YouTube: guardar o link com título, canal, miniatura e duração automáticos e saber o que já foi assistido. |
| Produto / canais | API REST (.NET 10). Consumida pela Web (React, `studytube-frontend`). App mobile React Native 🗺️ planejado. |
| Sistemas / repositórios | https://github.com/danibenfica/studytube-backend · https://github.com/danibenfica/studytube-frontend |
| Parceiros externos | Google — YouTube Data API v3 (metadados de vídeo). Cadastro: [Anexo C](anexo-c-integracao-youtube-data-api.md). |
| Usuários | Estudante (único perfil). Não há administrador nem autenticação. |
| Escopo | Cadastrar videoaula por URL, listar, marcar e desmarcar como assistida, validar URL/ID, impedir duplicidade, OpenAPI em desenvolvimento. |
| Fora de escopo | Playlists, `UserStudyProgress`, app mobile, mensageria (`StudyTube.Publisher`), relatórios (`StudyTube.DocumentGenerator`), autenticação e multiusuário. |
| Ambientes | Somente local: `http://localhost:5126` (perfil `http`), SQLite `studytube.db`, chave em User Secrets. CI no GitHub Actions. |
| Riscos e classificação | **Essencial** (com partes da ampliada). Riscos: dependência de chave/cota do Google (erro 500 não tratado); CORS aberto; sem autenticação. |
| Cronograma / marcos | Etapas 1–3 do guia (domínio, integração YouTube, endpoints) concluídas; etapa 4 Web concluída; mobile pendente. Documentação v1.0 em 21/10/2026. |
| Responsáveis e revisão | Owner: Daniele Benfica · Reviewer: mentoria do projeto · Data: 21/10/2026 |
| Links essenciais | [Requisitos](04.1-requisitos-e-regras.md) · [Casos de uso](04.2-casos-de-uso.md) · [Histórias](04.3-historias-de-usuario.md) · [Fluxos](anexo-b-fluxos-ponta-a-ponta.md) · [Arquitetura](03-arquitetura-da-informacao.md) · [Testes](04.5-testes-e-evidencias.md) · [Manual da API](anexo-f-manual-do-consumidor-da-api.md) |

**Critério de conclusão da ficha.** A ficha permite identificar produto, sistemas, integração com o YouTube, escopo, responsáveis e o ponto de entrada para os fluxos. ✅

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
