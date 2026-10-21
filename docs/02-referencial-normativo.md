# 📐 2. Referencial normativo e critérios de aplicação

| Fonte original | Uso neste projeto |
|---|---|
| ISO/IEC/IEEE 12207:2026 | Referência para o ciclo de vida: requisitos → implementação → verificação (CI) → manutenção. |
| ISO/IEC/IEEE 15289:2019 | Referência para o propósito e o conteúdo de cada item de informação (seções e anexos deste documento). |
| ISO/IEC/IEEE 29148:2018 | Requisitos RF/RNF/RN identificados, verificáveis e rastreáveis ([4.1](04.1-requisitos-e-regras.md), [Anexo D](anexo-d-rastreabilidade-e-conformidade.md)). |
| ISO/IEC/IEEE 42010:2022 | Descrição arquitetural por pontos de vista: contexto, contêineres, sequência, estados e implantação ([seção 5](05-diagramas-e-fluxos.md)). |
| ISO/IEC/IEEE 29119-3:2021 | Documentação de testes e evidências ([4.5](04.5-testes-e-evidencias.md)). |
| ISO/IEC/IEEE 26514:2022 | Informação para o usuário da API ([Anexo F](anexo-f-manual-do-consumidor-da-api.md)). |
| ISO/IEC 25010:2023 | Características de qualidade usadas para classificar os RNF. |
| ISO/IEC 20246:2017 | Estrutura da revisão registrada no [Anexo E](anexo-e-registro-de-revisao.md). |

**Padrões da disciplina Análise e Modelagem de Sistemas** (obrigatórios nos artefatos de requisitos):

| Padrão | Como é aplicado |
|---|---|
| UML — Diagrama de Casos de Uso | Atores, casos de uso (verbo no infinitivo), fronteira do sistema e relações `<<include>>` / `<<extend>>` ([4.2](04.2-casos-de-uso.md)). |
| Caso de uso no formato completo | Tabela com NOME DO CASO DE USO, ATORES, PRÉ-CONDIÇÕES, PÓS-CONDIÇÕES, FLUXO PRINCIPAL, FLUXOS ALTERNATIVOS (FA_xx, passos `N.a.1`) e FLUXOS DE EXCEÇÃO (FE_xx), cada um indicando "Retorna ao passo N do fluxo principal" ou "Fim do caso de uso". |
| Histórias de usuário | "Como <papel>, quero <ação>, para <benefício>" com critérios de aceitação, ligadas aos casos de uso ([4.3](04.3-historias-de-usuario.md)). |

**Complementos adotados:** UML para sequência e estados; C4 Model para contexto e contêineres; OpenAPI para o contrato (gerado em `/openapi/v1.json`); Mermaid como formato-fonte versionável dos diagramas (arquivos `.mmd` em `docs/img`). Não são normas ISO.

> **ATENÇÃO À EDIÇÃO** As cláusulas das normas ISO não foram consultadas na edição oficial licenciada. Por isso a matriz de conformidade ([Anexo D](anexo-d-rastreabilidade-e-conformidade.md)) usa apenas critérios internos verificáveis e marca as cláusulas ISO como "não avaliadas".

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
