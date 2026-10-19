# 🌸 StudyTube | Levantamento de Requisitos

> 📚 **Documento técnico de requisitos**
>
> Este documento descreve os requisitos funcionais e não funcionais do StudyTube, suas regras de negócio, atores e casos de uso.
>
> O conteúdo está alinhado ao guia da atividade e ao estado conhecido dos repositórios `studytube-backend` e `studytube-frontend`.
>
> **Legenda de status**
>
> - ✅ **Implementado:** funcionalidade existente no código atual.
> - 🗺️ **Planejado:** funcionalidade prevista no guia, ainda não implementada.

---

## 🧭 1. Contexto e objetivo

O **StudyTube** é um ecossistema de estudos voltado ao gerenciamento de videoaulas do YouTube.

O estudante informa o link de um vídeo, a API .NET consulta os metadados na YouTube Data API v3 e persiste as informações. O frontend React exibe título, canal, miniatura e duração, permitindo assistir aos vídeos e acompanhar o progresso dos estudos.

O guia da atividade prevê:

- 🏗️ Backend em .NET com Domain-Driven Design (DDD).
- 🔌 Integração com a YouTube Data API v3.
- 🌐 Frontend web desenvolvido em React.
- 📱 Aplicativo mobile desenvolvido em React Native.

Atualmente, o backend e o frontend web estão implementados. O aplicativo mobile, o gerenciamento de playlists, o progresso estruturado e a camada de mensageria ainda estão planejados.

## 👥 2. Atores

| Ator | Descrição |
|---|---|
| 🎓 Estudante | Pessoa que organiza a lista de videoaulas, assiste aos conteúdos e marca o que já estudou. |
| 🖥️ Sistema StudyTube | Conjunto formado pela API .NET e pelo frontend web, com possibilidade de expansão para um aplicativo mobile. |
| 🌐 YouTube Data API v3 | Serviço externo responsável por fornecer título, canal, miniatura e duração a partir do identificador do vídeo. |

### 🔐 Autenticação e armazenamento

O código atual não implementa autenticação de usuários. A lista é armazenada localmente no banco SQLite utilizado pela API.

Consequentemente, o sistema ainda não oferece isolamento de listas por estudante ou gerenciamento de múltiplas contas.

---

## ✨ 3. Requisitos funcionais

Os requisitos funcionais descrevem as operações e funcionalidades que o StudyTube deve oferecer.

### 📚 3.1. Funcionalidades implementadas

| ID | Requisito | Implementação |
|---|---|---|
| RF01 | O estudante informa a URL de um vídeo do YouTube e o sistema adiciona o vídeo à lista com metadados obtidos da YouTube Data API v3. | ✅ `POST /api/videos`, `VideoService.AddFromUrlAsync`, `VideoForm` |
| RF02 | O sistema lista os vídeos salvos, do mais recente para o mais antigo. | ✅ `GET /api/videos`, `listarVideos` |
| RF03 | O estudante marca um vídeo como assistido. | ✅ `PATCH /api/videos/{id}/watched`, `Video.MarkAsWatched` |
| RF04 | O estudante desmarca um vídeo, retornando ao estado não assistido. | ✅ `DELETE /api/videos/{id}/watched`, `Video.MarkAsUnwatched` |
| RF05 | O frontend exibe a miniatura, o título, o canal e a duração de cada vídeo. | ✅ `VideoCard` |
| RF06 | O estudante assiste ao vídeo na própria página web, por meio de um player incorporado. | ✅ `VideoPlayer`, `react-youtube` |
| RF07 | O frontend exibe o progresso, representado pela quantidade de vídeos assistidos em relação ao total. | ✅ `Progresso`, contagem em `useVideos` |
| RF08 | O frontend valida o link do YouTube no navegador antes de enviar a requisição de cadastro. | ✅ `validarLinkYoutube` |
| RF09 | A API rejeita URLs inválidas, links que não correspondem a vídeos do YouTube, IDs inválidos, vídeos duplicados e vídeos inexistentes no YouTube. | ✅ Regras em `YouTubeVideoId`, `VideoService` e `VideosController` |

### 🗺️ 3.2. Funcionalidades planejadas

| ID | Requisito | Status e referência |
|---|---|---|
| RF10 | Gerenciar playlists de estudo, incluindo `Playlist` e `PlaylistsController`. | 🗺️ Previsto no guia; não existe na solution atual. |
| RF11 | Registrar o progresso estruturado de estudo por meio de `UserStudyProgress`. | 🗺️ Previsto no guia; não existe na solution atual. |
| RF12 | Desenvolver um aplicativo mobile em React Native que consuma a mesma API e utilize `react-native-youtube-iframe` para reprodução. | 🗺️ Previsto na etapa 4 do guia; o repositório mobile ainda não existe. |
| RF13 | Implementar uma camada de mensageria ou eventos, representada por `StudyTube.Publisher`. | 🗺️ Previsto na estrutura do guia; o projeto ainda não existe. |
| RF14 | Implementar geração de relatórios ou certificados por meio de `StudyTube.DocumentGenerator`. | 🗺️ Funcionalidade opcional no guia; o projeto ainda não existe. |

As funcionalidades planejadas não devem ser consideradas disponíveis na versão atual.

---

## ⚙️ 4. Requisitos não funcionais

Os requisitos não funcionais definem características técnicas, restrições e critérios de qualidade da solução.

| ID | Requisito | Como aparece no código |
|---|---|---|
| RNF01 | **Portabilidade:** permitir desenvolvimento e execução em Windows, macOS e Linux. | `dotnet` e `npm` são multiplataforma; o banco utiliza SQLite em arquivo; o perfil HTTP evita diferenças de certificados HTTPS durante a execução local. |
| RNF02 | **Testabilidade do backend:** utilizar NUnit para testes unitários e de integração. | `StudyTube.Tests` utiliza NUnit 4.x, fakes de repositório e cliente do YouTube nos testes unitários, além de SQLite `:memory:` nos testes de integração. |
| RNF03 | **Testabilidade do frontend:** utilizar Vitest para testes unitários. | Testes de `formatarDuracao` e `validarLinkYoutube`; o Husky executa `npm test` no hook de pre-commit. |
| RNF04 | **Proteção da chave da YouTube Data API v3:** manter credenciais fora do controle de versão. | A chave é configurada em `YouTube:ApiKey`, por User Secrets, e não deve ser versionada em `appsettings.json`; arquivos `*.db` são ignorados pelo Git. |
| RNF05 | **Manutenibilidade e isolamento de responsabilidades:** aplicar DDD e separar as camadas. | O domínio não depende de EF Core ou HTTP; a API orquestra as requisições; o frontend conhece apenas as rotas e os contratos JSON. |
| RNF06 | **Documentação da API em desenvolvimento:** disponibilizar documentação interativa e especificação OpenAPI. | Swagger UI em `/swagger` e documento `/openapi/v1.json` no ambiente `Development`. |
| RNF07 | **Desempenho adequado para uma lista pessoal local:** manter a solução simples e compatível com o escopo atual. | SQLite local, uma chamada HTTP ao Google por vídeo novo e ausência de autenticação ou multi-tenancy. |

Esses itens descrevem as escolhas técnicas atuais. Eles não representam, por si só, garantias de desempenho ou segurança em produção.

---

## 💎 5. Regras de negócio do domínio

As regras abaixo foram extraídas dos comportamentos de `Video`, `YouTubeVideoId` e `VideoService`.

| ID | Regra |
|---|---|
| RN01 | O identificador do YouTube deve conter exatamente 11 caracteres, aceitando letras de `A` a `Z`, letras de `a` a `z`, números de `0` a `9`, sublinhado (`_`) e hífen (`-`). |
| RN02 | A URL deve ser absoluta e corresponder a um host e caminho aceitos para vídeos do YouTube: `youtu.be`, `youtube.com`, `www.youtube.com` ou `m.youtube.com`, com formatos como `/watch?v=`, `/embed/` e `/shorts/`. |
| RN03 | O título do vídeo é obrigatório e não pode ser nulo ou conter apenas espaços. |
| RN04 | A duração do vídeo não pode ser negativa. |
| RN05 | O mesmo `YouTubeId` não pode ser adicionado duas vezes à lista. |
| RN06 | Não é permitido marcar como assistido um vídeo que já esteja marcado como assistido. |
| RN07 | Desmarcar um vídeo limpa o estado: `Watched = false` e `WatchedAt = null`. A operação pode ser chamada mesmo que o vídeo já esteja desmarcado. |
| RN08 | Um vídeo recém-criado começa como não assistido, com `Watched = false` e `WatchedAt = null`. |
| RN09 | Se o YouTube não retornar o vídeo solicitado, a inclusão falha com a mensagem `Vídeo não encontrado no YouTube.` |

Essas regras são aplicadas principalmente no domínio e na camada de aplicação. A validação no frontend melhora a experiência do usuário, mas não substitui a validação realizada pelo backend.

---

## 🔄 6. Casos de uso

### UC01 | Adicionar vídeo pela URL

**Ator principal:** Estudante.

**Objetivo:** cadastrar uma videoaula na lista pessoal.

**Fluxo principal:**

1. O estudante cola a URL no formulário do frontend.
2. O frontend valida o link por meio de `validarLinkYoutube`, conforme RF08.
3. Se o link for inválido, o frontend apresenta um erro e não chama a API.
4. O frontend envia `POST /api/videos` com um corpo JSON contendo a URL.
5. A API extrai e valida o ID por meio de `YouTubeVideoId.FromUrl`.
6. O `VideoService` verifica se o vídeo já existe.
7. A API consulta os metadados do YouTube.
8. O domínio cria a entidade `Video`.
9. O repositório persiste o registro.
10. A API retorna HTTP `201 Created` com um `VideoDto`.
11. O frontend atualiza a lista exibida.

**Exemplo de requisição:**

```http
POST /api/videos
Content-Type: application/json
```

```json
{
  "url": "https://www.youtube.com/watch?v=VIDEO_ID"
}
```

**Exceções esperadas:**

- URL inválida ou ID malformado.
- Vídeo duplicado.
- Vídeo inexistente no YouTube.
- Falha de comunicação com a API externa.

### UC02 | Listar vídeos

**Ator principal:** Estudante.

**Objetivo:** visualizar as videoaulas cadastradas.

**Fluxo principal:**

1. O estudante abre a página ou conclui uma operação que exige atualizar a lista.
2. O frontend chama `GET /api/videos`.
3. A API consulta os registros persistidos.
4. A API retorna um array de `VideoDto` ordenado por `AddedAt` em ordem decrescente.
5. A tela renderiza os cards e o indicador de progresso.

**Resultado esperado:** os vídeos mais recentemente adicionados aparecem primeiro.

### UC03 | Assistir na página

**Ator principal:** Estudante.

**Objetivo:** reproduzir uma videoaula sem sair da aplicação.

**Fluxo principal:**

1. O estudante escolhe um vídeo no card correspondente.
2. O `VideoPlayer` é aberto.
3. O player `react-youtube` recebe o `youTubeId`.
4. O estudante assiste ao vídeo.
5. O estudante pode fechar o player.

**Resultado esperado:** o vídeo é reproduzido por meio do player incorporado do YouTube.

### UC04 | Marcar ou desmarcar como assistido

**Ator principal:** Estudante.

**Objetivo:** atualizar o estado de estudo de uma videoaula.

**Fluxo principal:**

1. O estudante alterna o estado do vídeo no card.
2. Se o vídeo ainda não foi assistido, o frontend chama `PATCH /api/videos/{id}/watched`.
3. Se o vídeo já foi assistido, o frontend chama `DELETE /api/videos/{id}/watched`.
4. O domínio aplica `MarkAsWatched` ou `MarkAsUnwatched`.
5. A API devolve o `VideoDto` atualizado.
6. O frontend atualiza a lista e recalcula o progresso.

**Resultado esperado:** o estado de assistido é persistido e refletido na interface.

### UC05 | Consultar progresso na web

**Ator principal:** Estudante.

**Objetivo:** acompanhar quantas videoaulas já foram assistidas.

**Fluxo principal:**

1. O frontend carrega a lista de vídeos.
2. O hook `useVideos` contabiliza os registros cujo campo `watched` é `true`.
3. O componente `Progresso` apresenta a quantidade assistida em relação ao total.
4. Após uma alteração no estado de um vídeo, o indicador é recalculado.

**Resultado esperado:** o estudante visualiza o progresso com base nos vídeos existentes na lista.

**Limitação:** não existe um endpoint dedicado ao progresso estruturado. O componente calcula os valores a partir da lista carregada. `UserStudyProgress` permanece planejado.

---

## 🧪 7. Verificação dos requisitos

Os requisitos devem ser verificados por testes automatizados e testes funcionais, de acordo com a responsabilidade de cada camada.

| Área | Estratégia de verificação |
|---|---|
| Domínio | Testar a criação de vídeos, validação do identificador, duração e transições de estado. |
| Aplicação | Testar o cadastro, a rejeição de duplicatas e o comportamento com fakes do repositório e do cliente do YouTube. |
| Persistência | Verificar as operações do repositório usando SQLite em memória. |
| API | Verificar as respostas HTTP, os contratos JSON e os cenários de erro. |
| Frontend | Testar a formatação da duração e a validação de URLs com Vitest. |
| Integração web | Validar o cadastro, a listagem, o player e a atualização do progresso com o backend em execução. |

A existência de um requisito não significa que todos os testes correspondentes já estejam implementados. A cobertura deve ser confirmada nos projetos de teste.

---
