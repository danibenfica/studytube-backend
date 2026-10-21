# 📦 4. Catálogo de artefatos

Foram adotados somente os artefatos necessários ao risco e ao público do projeto. Cada artefato tem um único local.

| Artefato | Responsável | Gatilho / critério | Local neste projeto | Estado |
|---|---|---|---|---|
| Requisitos e regras de negócio | Daniele Benfica | Toda funcionalidade relevante ou integração. | [4.1 Requisitos e regras](04.1-requisitos-e-regras.md) | ✅ |
| Diagrama e especificação de casos de uso | Daniele Benfica | Toda funcionalidade exposta ao usuário. | [4.2 Casos de uso](04.2-casos-de-uso.md) | ✅ |
| Histórias de usuário e critérios de aceitação | Daniele Benfica | Todo caso de uso implementado ou planejado. | [4.3 Histórias de usuário](04.3-historias-de-usuario.md) | ✅ |
| Fluxo ponta a ponta e exceções | Daniele Benfica | Toda jornada com dependência entre sistemas. | [Anexo B](anexo-b-fluxos-ponta-a-ponta.md) e [seção 5](05-diagramas-e-fluxos.md) | ✅ |
| Contrato e mapa de dados | Daniele Benfica | Toda interface exposta. | [4.4 Contrato da API](04.4-contrato-da-api.md) + `/openapi/v1.json` | ✅ |
| Cadastro de integração externa | Daniele Benfica | Toda integração com terceiros. | [Anexo C — YouTube Data API v3](anexo-c-integracao-youtube-data-api.md) | ✅ |
| Testes e evidências | Daniele Benfica | Cada mudança validada. | [4.5 Testes e evidências](04.5-testes-e-evidencias.md) | ✅ |
| Procedimento operacional | Daniele Benfica | Execução local, migrations e CI. | [4.6 Procedimento operacional](04.6-procedimento-operacional.md) | ✅ |
| Manual do usuário | Daniele Benfica | Quando há impacto ao usuário. | Backend: [Anexo F — manual do consumidor da API](anexo-f-manual-do-consumidor-da-api.md). Usuário final: manual web em DOC-STB-FE-001. | ✅ |
| Registro de revisão / aprovação | Daniele Benfica (autora) + mentoria (revisão) | A cada versão do documento. | [Anexo E](anexo-e-registro-de-revisao.md) | Em revisão |

## ⚖️ Criticidade e adequação

| Classe | Diretriz | Aplicação no StudyTube API |
|---|---|---|
| Essencial | Visão geral, fluxo, contrato, testes, operação e manual quando houver impacto ao usuário. | **Classe adotada.** Projeto acadêmico, uso local, sem dados pessoais, sem pagamentos e sem autenticação. |
| Ampliada | Sequência, estados, segurança, observabilidade e dependências. | Adotado **parcialmente**: há sequência, máquina de estados e cadastro da integração externa, porque o cadastro depende de um sistema de terceiros. |
| Regulada/crítica | Requisitos regulatórios, controles e auditoria formal. | Não aplicável: não há requisito regulatório nem contratual. |

A classe deve ser reavaliada se o projeto passar a ter autenticação, múltiplos usuários ou publicação em produção.

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
