**GOVERNANÇA DOCUMENTAL · STUDYTUBE**

# 🎓 StudyTube API — Documentação do projeto backend

**Documentação de projeto e integração de software · aplicação do padrão DOC-STD-001 ao repositório `studytube-backend`**

> **FINALIDADE** Documentar o backend do StudyTube de ponta a ponta: negócio → requisitos e casos de uso → contrato da API → integração com o YouTube → testes → operação → consumidor da API.

| Identificação | Valor |
|---|---|
| Organização / área | Projeto acadêmico StudyTube — Especialização em Engenharia de Software (disciplina Análise e Modelagem de Sistemas) |
| Projeto / repositório | StudyTube API — [`danibenfica/studytube-backend`](https://github.com/danibenfica/studytube-backend) |
| Responsável pelo documento | Daniele Benfica |
| Código / versão | DOC-STB-BE-001 \| v1.0 |
| Estado | Em revisão (aguardando aprovação da mentoria) |
| Data de emissão | 21/10/2026 |
| Versão do software documentada | branch `main`, commit `382c379` |
| Revisão prevista | A cada mudança de regra, contrato ou arquitetura (ver [6. Manutenção](06-manutencao.md)) |
| Aprovado por | [Pendente — mentoria do projeto] |

Documento irmão: a interface web tem a sua própria documentação no repositório [`studytube-frontend`](https://github.com/danibenfica/studytube-frontend/tree/main/docs) (DOC-STB-FE-001). Os dois documentos usam os **mesmos códigos de casos de uso (UC_xxx) e histórias de usuário (US_xxx)** para manter a rastreabilidade.

## 🧭 Como utilizar este documento

Esta pasta aplica o padrão corporativo de documentação ao backend do StudyTube, já preenchido com os dados reais do código. Os artefatos da disciplina (requisitos, diagrama de casos de uso, casos de uso completos e histórias de usuário) estão na seção 4, como artefatos do catálogo.

1. Comece pela [Ficha de projeto (Anexo A)](anexo-a-ficha-de-projeto.md) para entender o produto em uma página.
2. Leia os [requisitos e regras](04.1-requisitos-e-regras.md) e os [casos de uso](04.2-casos-de-uso.md).
3. Siga os fluxos ponta a ponta no [Anexo B](anexo-b-fluxos-ponta-a-ponta.md) e os diagramas na [seção 5](05-diagramas-e-fluxos.md).
4. Use a [matriz de rastreabilidade (Anexo D)](anexo-d-rastreabilidade-e-conformidade.md) para ir do requisito ao código e ao teste.
5. Ao mudar o código, atualize a documentação no mesmo PR ([seção 6](06-manutencao.md)).

## 📑 Sumário

- [1. Objetivo, escopo e princípios](01-objetivo-escopo-e-principios.md)
- [2. Referencial normativo e critérios de aplicação](02-referencial-normativo.md)
- [3. Arquitetura da informação e organização do repositório](03-arquitetura-da-informacao.md)
- [4. Catálogo de artefatos e criticidade](04-catalogo-de-artefatos.md)
  - [4.1 Requisitos e regras de negócio](04.1-requisitos-e-regras.md)
  - [4.2 Diagrama e especificação de casos de uso](04.2-casos-de-uso.md)
  - [4.3 Histórias de usuário e critérios de aceitação](04.3-historias-de-usuario.md)
  - [4.4 Contrato da API e mapa de dados](04.4-contrato-da-api.md)
  - [4.5 Testes e evidências](04.5-testes-e-evidencias.md)
  - [4.6 Procedimento operacional](04.6-procedimento-operacional.md)
- [5. Diagramas e fluxos ponta a ponta](05-diagramas-e-fluxos.md)
- [6. Manutenção da documentação](06-manutencao.md)
- [7. Verificação, aprovação e auditoria](07-verificacao-e-aprovacao.md)
- [Anexo A — Ficha de projeto](anexo-a-ficha-de-projeto.md)
- [Anexo B — Especificação de fluxo ponta a ponta (EFI)](anexo-b-fluxos-ponta-a-ponta.md)
- [Anexo C — Cadastro de integração externa: YouTube Data API v3](anexo-c-integracao-youtube-data-api.md)
- [Anexo D — Matriz de rastreabilidade e conformidade](anexo-d-rastreabilidade-e-conformidade.md)
- [Anexo E — Registro de revisão e não conformidade](anexo-e-registro-de-revisao.md)
- [Anexo F — Manual do consumidor da API](anexo-f-manual-do-consumidor-da-api.md)
- [Anexo G — Definition of Done documental](anexo-g-definition-of-done.md)
- [Referências](referencias.md)

> **INTERPRETAÇÃO** "Deve" = regra adotada neste projeto. "Quando aplicável" exige justificativa. ✅ = implementado no código atual; 🗺️ = planejado no guia da atividade, ainda não implementado. Referências a normas ISO indicam alinhamento, não certificação.

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
