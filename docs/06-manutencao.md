# 🔧 6. Manutenção da documentação

## 6.1 Fluxo de manutenção

Mudança → análise de impacto (quais UC, RN, RF e testes mudam?) → atualização de código, testes e `docs/` **na mesma branch** → pull request → CI verde → revisão → merge na `main`.

Convenções do repositório:

- Branches por assunto (ex.: `refactor/naming`, `docs/casos-de-uso`).
- Mensagens no padrão Conventional Commits (`feat:`, `fix:`, `docs:`, `refactor:`, `test:`).
- O CI (`.github/workflows/ci.yml`) roda em todo pull request.

## 6.2 Gatilhos obrigatórios de revisão

- Mudança em regra de negócio, mensagem de erro ou código HTTP → atualizar [4.1](04.1-requisitos-e-regras.md), [4.2](04.2-casos-de-uso.md), [4.4](04.4-contrato-da-api.md) e o frontend.
- Mudança nos estados de `Video` → atualizar a máquina de estados ([5.4](05-diagramas-e-fluxos.md#54-uml-state-machine--entidade-video)).
- Mudança na integração com o YouTube (campos, cota, autenticação) → atualizar o [Anexo C](anexo-c-integracao-youtube-data-api.md).
- Novo endpoint ou entidade (ex.: `Playlist`) → novo RF, UC, US, linhas na matriz ([Anexo D](anexo-d-rastreabilidade-e-conformidade.md)) e testes.
- Mudança de porta, banco ou CI → atualizar [4.6](04.6-procedimento-operacional.md) e o diagrama de implantação.
- Defeito causado por documentação errada → registrar no [Anexo E](anexo-e-registro-de-revisao.md).

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
