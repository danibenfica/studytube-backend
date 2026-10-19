# 🎓 StudyTube API

> ✨ Seu cantinho de estudos, organizado vídeo por vídeo.

A **StudyTube API** é uma API REST desenvolvida em .NET para organizar uma lista pessoal de videoaulas do YouTube. Com ela, o estudante pode adicionar vídeos, consultar seus metadados e acompanhar quais conteúdos já foram estudados.

O projeto utiliza **Domain-Driven Design (DDD)** para separar as regras de negócio da infraestrutura, além de SQLite para persistência e integração com a YouTube Data API v3.

🌷 **Tecnologias principais:** C# · .NET 10 · ASP.NET Core · Entity Framework Core · SQLite · NUnit · YouTube Data API v3

---

## 🌸 Índice

- [💡 Sobre o projeto](#-sobre-o-projeto)
- [🌐 Endpoints](#-endpoints)
- [📦 Formato do VideoDto](#-formato-do-videodto)
- [🧰 Versões e dependências](#-versões-e-dependências)
- [🔑 Configuração da chave do YouTube](#-configuração-da-chave-do-youtube)
- [🚀 Como executar](#-como-executar-a-api)
- [🧪 Testes](#-testes)
- [📁 Estrutura da solution](#-estrutura-da-solution)
- [🏗️ Decisões de arquitetura](#️-decisões-de-arquitetura)
- [🗺️ Próximos passos](#️-próximos-passos)

---

## 💡 Sobre o projeto

A ideia é simples: o estudante envia o link de uma videoaula, a API consulta a **YouTube Data API v3** e salva as principais informações do conteúdo:

- 📝 Título do vídeo
- 📺 Nome do canal
- 🖼️ Miniatura
- ⏱️ Duração
- 📅 Data de inclusão
- ✅ Estado de conclusão dos estudos

Depois, é possível listar os vídeos e marcar ou desmarcar cada um como assistido.

O frontend web está disponível no repositório [studytube-frontend](https://github.com/danibenfica/studytube-frontend). Ele se comunica com a API por meio de requisições HTTP e respostas JSON.

O material da atividade também prevê um aplicativo mobile em React Native, mas ele ainda não faz parte da solution atual.

### ✨ Funcionalidades implementadas

- [x] Adicionar videoaulas por URL.
- [x] Consultar metadados do YouTube.
- [x] Persistir vídeos no SQLite.
- [x] Listar vídeos do mais recente para o mais antigo.
- [x] Marcar vídeos como assistidos.
- [x] Desmarcar vídeos como assistidos.
- [x] Validar URLs e IDs do YouTube.
- [x] Impedir o cadastro duplicado de um mesmo vídeo.
- [x] Documentar a API com OpenAPI e Swagger em desenvolvimento.
- [x] Testar as regras de negócio e a persistência.

### 📍 Endereços locais

| Recurso | Endereço |
|---|---|
| API | [http://localhost:5126](http://localhost:5126) |
| Swagger UI | [http://localhost:5126/swagger](http://localhost:5126/swagger) |
| Documento OpenAPI | [http://localhost:5126/openapi/v1.json](http://localhost:5126/openapi/v1.json) |

> 💡 O Swagger e o documento OpenAPI são disponibilizados quando o ambiente está configurado como `Development`.

A porta `5126` vem do perfil `http` definido em `StudyTube.Api/Properties/launchSettings.json`.

---

## 🌐 Endpoints

A API utiliza o controlador `VideosController`, com a rota-base `/api/videos`.

O ASP.NET Core não diferencia maiúsculas de minúsculas nesse caminho. Portanto, `/api/Videos` e `/api/videos` funcionam. Nos exemplos, usamos letras minúsculas para manter a consistência com o frontend e o arquivo `StudyTube.Api.http`.

As propriedades JSON são retornadas em `camelCase`.

🔒 **Autenticação:** não implementada atualmente.

### 📚 Consultar todos os vídeos

`GET /api/videos`

Retorna a lista de vídeos cadastrados, ordenada pela data de inclusão (`AddedAt`) em ordem decrescente.

**Resposta de sucesso:**

- `200 OK`: retorna um array de `VideoDto`.
- Lista vazia: retorna `[]`.

### ➕ Adicionar uma videoaula

`POST /api/videos`

Recebe a URL, consulta os metadados no YouTube e salva o vídeo.

**Corpo da requisição:**

```json
{
  "url": "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
}
```

#### 🔗 Formatos de URL aceitos

Desde que o ID tenha exatamente 11 caracteres válidos, a API aceita os seguintes formatos:

- `https://youtu.be/{id}`
- `https://youtube.com/watch?v={id}`
- `https://www.youtube.com/watch?v={id}`
- `https://m.youtube.com/watch?v={id}`
- `https://www.youtube.com/embed/{id}`
- `https://www.youtube.com/shorts/{id}`

O ID pode conter letras maiúsculas ou minúsculas, números, `_` e `-`.

#### 📬 Respostas possíveis

| Código HTTP | Significado |
|---|---|
| `201 Created` | Vídeo cadastrado com sucesso. Retorna o `VideoDto` e o cabeçalho `Location`. |
| `400 Bad Request` | URL inválida, ID inválido, vídeo duplicado ou vídeo não encontrado. |
| `500 Internal Server Error` | Falha inesperada, como erro de rede ou problema na integração com o Google. |

**Exemplo de erro de domínio:**

```json
{
  "error": "Este vídeo já está na sua lista."
}
```

Mensagens previstas:

- `URL inválida.`
- `Não é uma URL de vídeo do YouTube.`
- `ID do YouTube inválido.`
- `Este vídeo já está na sua lista.`
- `Vídeo não encontrado no YouTube.`

**E quando o campo `url` não é enviado?**

O ASP.NET Core retorna `400 Bad Request` usando o formato Problem Details:

```json
{
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Url": ["The Url field is required."]
  }
}
```

**⚠️ Atenção:** o cabeçalho `Location` aponta para `/api/videos/{id}`, mas a API ainda não implementa um endpoint `GET` para consultar um vídeo individual por ID.

Erros na comunicação com o Google, ausência de chave, credenciais recusadas ou limites de utilização podem resultar em `500 Internal Server Error`. Esse erro não segue necessariamente o formato `{ "error": "..." }`.

### ✅ Marcar como assistido

`PATCH /api/videos/{id}/watched`

Atualiza o estado do vídeo para assistido.

| Código HTTP | Significado |
|---|---|
| `200 OK` | Retorna o `VideoDto` atualizado. |
| `400 Bad Request` | O vídeo já está marcado como assistido. |
| `404 Not Found` | O ID não existe ou não corresponde ao formato GUID exigido pela rota. |

**Exemplo de erro:**

```json
{
  "error": "Este vídeo já foi marcado como assistido."
}
```

### ↩️ Desmarcar como assistido

`DELETE /api/videos/{id}/watched`

Retorna o vídeo ao estado não assistido.

- `200 OK`: retorna o `VideoDto` atualizado.
- `404 Not Found`: ocorre quando o ID não existe ou não está no formato GUID esperado.

Essa operação também pode ser executada quando o vídeo já estiver desmarcado.

---

## 📦 Formato do VideoDto

O `VideoDto` é o objeto utilizado para transportar os dados dos vídeos entre a API e os clientes.

**Exemplo de resposta:**

```json
{
  "id": "ff28bf3c-082d-43e1-9d64-25d656ae0b9d",
  "youTubeId": "dQw4w9WgXcQ",
  "title": "Aula de DDD",
  "channelTitle": "Canal",
  "thumbnailUrl": "https://img",
  "duration": "00:10:00",
  "watched": true,
  "addedAt": "2026-10-19T13:46:41.8172481",
  "watchedAt": "2026-10-19T13:46:41.8173555"
}
```

### 🧾 Campos disponíveis

| Campo | Formato e significado |
|---|---|
| `id` | UUID em formato de string. |
| `youTubeId` | Identificador do vídeo no YouTube, com 11 caracteres. |
| `title` | Título do vídeo. |
| `channelTitle` | Nome do canal. |
| `thumbnailUrl` | URL da miniatura. |
| `duration` | Duração serializada no formato constante `c` de `TimeSpan`. |
| `watched` | Booleano que indica se o vídeo foi marcado como assistido. |
| `addedAt` | Data e hora de inclusão. |
| `watchedAt` | Data e hora da conclusão ou `null` quando não assistido. |

### ⏱️ Como funciona a duração?

A YouTube Data API retorna a duração no padrão ISO 8601, por exemplo, `PT4M13S`.

O `YouTubeApiClient` converte esse valor usando `XmlConvert.ToTimeSpan`, e a API publica o `TimeSpan` resultante.

| Duração | Valor no JSON |
|---|---|
| 10 minutos | `"00:10:00"` |
| 45 segundos | `"00:00:45"` |
| 1 hora, 2 minutos e 3 segundos | `"01:02:03"` |
| 1 dia, 2 horas, 3 minutos e 4 segundos | `"1.02:03:04"` |
| 10 minutos e 5,25 segundos | `"00:10:05.2500000"` |

Quando há fração de segundo, o valor pode conter até sete dígitos decimais.

O padrão OpenAPI é:

```regex
^-?(\d+\.)?\d{2}:\d{2}:\d{2}(\.\d{1,7})?$
```

### 🕰️ Como funcionam as datas?

Os campos `addedAt` e `watchedAt` são gravados com `DateTime.UtcNow`.

No SQLite, o Entity Framework Core pode recuperar esses valores com `DateTimeKind.Unspecified`. Por isso, uma resposta de `GET` pode conter uma data sem o sufixo `Z`.

Já as respostas de `POST` e `PATCH` podem incluir `Z`, pois o objeto ainda pode estar em memória com `DateTimeKind.Utc`.

Essa diferença de serialização é importante ao integrar a API com outros clientes.

---

## 🧰 Versões e dependências

O SDK está fixado em `global.json` (10.0.402, `rollForward: latestFeature`). Os projetos usam `net10.0`.

| Componente | Versão |
|---|---|
| .NET SDK utilizado | `10.0.402` |
| `Microsoft.AspNetCore.OpenApi` | `10.0.11` |
| `Microsoft.EntityFrameworkCore.Design` | `10.0.13` |
| `Swashbuckle.AspNetCore.SwaggerUI` | `10.3.0` |
| `Microsoft.EntityFrameworkCore.Sqlite` | `10.0.13` |
| `NUnit` | `4.3.2` |
| `NUnit3TestAdapter` | `5.0.0` |
| `NUnit.Analyzers` | `4.7.0` |
| `Microsoft.NET.Test.Sdk` | `17.14.0` |
| `coverlet.collector` | `6.0.4` |
| `dotnet-ef` | `10.0.13` |

Os projetos `StudyTube.Domain` e `StudyTube.Services` não possuem dependências NuGet diretas.

A ferramenta `dotnet-ef` é utilizada para gerenciar migrations do Entity Framework Core.

---

## 🔑 Configuração da chave do YouTube

Para cadastrar novos vídeos, é necessário configurar uma chave da **YouTube Data API v3**.

Sem ela, ainda é possível listar os vídeos já salvos e alterar seu estado de conclusão, mas o cadastro de novos conteúdos depende da integração externa.

### 🌱 Criando uma chave de API

1. Acesse o [Google Cloud Console](https://console.cloud.google.com/).
2. Crie um projeto ou selecione um existente.
3. Abra **APIs e serviços → Biblioteca**.
4. Ative a **YouTube Data API v3**.
5. Entre em **APIs e serviços → Credenciais**.
6. Crie uma chave de API.
7. Restrinja a chave para a YouTube Data API v3, conforme as opções disponíveis.

### 🔐 Salvando a chave com User Secrets

O projeto `StudyTube.Api` já possui um `UserSecretsId` configurado:

```text
56a112e3-4717-4060-86ec-c0b6028fb0aa
```

Na raiz do repositório, execute:

```bash
dotnet user-secrets set "YouTube:ApiKey" "SUA_CHAVE" --project StudyTube.Api
```

Substitua `SUA_CHAVE` pela chave criada no Google Cloud Console.

> 🔒 Nunca coloque a chave real no README, no frontend, no `appsettings.json` versionado ou em commits públicos.

Os User Secrets são carregados no ambiente `Development`, definido pelo perfil `http`.

---

## 🚀 Como executar a API

Vamos colocar esse foguete para decolar! 🚀

### 📋 Pré-requisitos

Antes de começar, verifique se você possui:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Git](https://git-scm.com/)
- Uma chave da YouTube Data API v3
- Um terminal e um editor de código, como o JetBrains Rider

### 1. 📥 Restaurar as dependências

Execute na raiz do repositório, onde está o arquivo `StudyTube.slnx`:

```bash
dotnet restore StudyTube.slnx
```

Esse comando baixa as dependências necessárias para os projetos.

### 2. 🏗️ Compilar a solution

```bash
dotnet build StudyTube.slnx
```

A compilação verifica se os projetos conseguem ser construídos corretamente.

### 3. 🔑 Configurar a chave do YouTube

```bash
dotnet user-secrets set "YouTube:ApiKey" "SUA_CHAVE" --project StudyTube.Api
```

### 4. 🧩 Instalar o dotnet-ef

```bash
dotnet tool install --global dotnet-ef --version 10.0.13
```

Se a ferramenta já estiver instalada, atualize-a:

```bash
dotnet tool update --global dotnet-ef --version 10.0.13
```

### 5. 🗄️ Aplicar as migrations

```bash
dotnet ef database update --project StudyTube.Data --startup-project StudyTube.Api
```

Esse comando aplica a migration `InitialCreate` e cria o schema do banco.

**Observação:** essa etapa pode ser omitida na execução normal, pois `Program.cs` já chama `Database.Migrate()` durante a inicialização.

O banco utiliza a connection string:

```text
Data Source=studytube.db
```

Na configuração atual, o arquivo fica em `StudyTube.Api/studytube.db`. Arquivos `*.db` estão no `.gitignore`.

### 6. ▶️ Iniciar a API

```bash
dotnet run --project StudyTube.Api --launch-profile http
```

Prontinho! A API estará disponível em:

- 🌐 API: [http://localhost:5126](http://localhost:5126)
- 📘 Swagger: [http://localhost:5126/swagger](http://localhost:5126/swagger)
- 📄 OpenAPI: [http://localhost:5126/openapi/v1.json](http://localhost:5126/openapi/v1.json)

### 🔒 Executar com HTTPS

Também existe um perfil `https`, configurado para utilizar `https://localhost:7271` e `http://localhost:5126`.

```bash
dotnet run --project StudyTube.Api --launch-profile https
```

Para criar o certificado de desenvolvimento:

```bash
dotnet dev-certs https
```

Para confiar no certificado:

**Windows e macOS**

```bash
dotnet dev-certs https --trust
```

**Linux**

O processo de confiança depende da distribuição e do navegador. Durante o desenvolvimento, o perfil HTTP é uma alternativa mais simples.

### 🛠️ Se o comando dotnet-ef não for encontrado

Verifique se o diretório das ferramentas globais está no `PATH`.

| Sistema | Diretório |
|---|---|
| Windows | `%USERPROFILE%\.dotnet\tools` |
| macOS/Linux | `$HOME/.dotnet/tools` |

### 🗂️ Onde ficam os User Secrets?

| Sistema | Localização |
|---|---|
| Windows | `%APPDATA%\Microsoft\UserSecrets\56a112e3-4717-4060-86ec-c0b6028fb0aa\secrets.json` |
| macOS/Linux | `~/.microsoft/usersecrets/56a112e3-4717-4060-86ec-c0b6028fb0aa/secrets.json` |

Esses arquivos são gerenciados pelo .NET e não devem ser adicionados ao repositório.

---

## 🧪 Testes

O projeto utiliza **NUnit** para verificar o comportamento do backend.

### ▶️ Executar todos os testes

Na raiz da solution:

```bash
dotnet test StudyTube.slnx
```

### 🧩 Executar somente os testes unitários

```bash
dotnet test StudyTube.slnx --filter "FullyQualifiedName!~StudyTube.Tests.Integration"
```

### 🗄️ Executar somente os testes de integração

```bash
dotnet test StudyTube.slnx --filter "FullyQualifiedName~StudyTube.Tests.Integration"
```

### 📊 Cobertura atual

| Categoria | Quantidade |
|---|---:|
| Testes unitários | 19 |
| Testes de integração | 4 |
| Total informado | 23 |

Os testes unitários cobrem regras do domínio e comportamentos do `VideoService`. Os testes de integração verificam o repositório com SQLite em memória.

Nos testes unitários do serviço, o repositório e o cliente do YouTube são substituídos por *fakes*. Isso permite verificar os comportamentos sem depender de uma conexão real com o Google.

Nos testes de integração, uma conexão SQLite `:memory:` é criada e o schema é inicializado com `EnsureCreated`.

> 🧪 **Limitação conhecida:** a API ainda não possui testes de ponta a ponta executando requisições HTTP reais contra a aplicação.

---

## 📁 Estrutura da solution

A solution está organizada em projetos com responsabilidades distintas.

```text
StudyTube.slnx
│
├── StudyTube.Api/
│   ├── Controllers/
│   │   └── VideosController.cs
│   ├── Program.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── StudyTube.Api.http
│   ├── appsettings.json
│   └── wwwroot/
│       └── swagger-ui/
│           └── custom.css
│
├── StudyTube.Domain/
│   ├── Entities/
│   │   └── Video.cs
│   ├── ValueObjects/
│   │   └── YouTubeVideoId.cs
│   └── Exceptions/
│       └── DomainException.cs
│
├── StudyTube.Services/
│   ├── Videos/
│   │   ├── VideoService.cs
│   │   └── VideoDto.cs
│   ├── Interfaces/
│   │   ├── IVideoRepository.cs
│   │   └── IYouTubeApiClient.cs
│   └── YouTube/
│       └── YouTubeVideoInfo.cs
│
├── StudyTube.Data/
│   ├── StudyTubeDbContext.cs
│   ├── Configurations/
│   │   └── VideoConfiguration.cs
│   ├── Repositories/
│   │   └── VideoRepository.cs
│   ├── ExternalServices/
│   │   └── YouTube/
│   └── Migrations/
│
└── StudyTube.Tests/
    ├── Unit/
    │   ├── Domain/
    │   └── Services/
    └── Integration/
        └── Data/
```

### 📝 Componentes previstos no material da atividade

Os seguintes projetos e tipos ainda não fazem parte da solution atual:

- `StudyTube.Publisher`
- `StudyTube.DocumentGenerator`
- `Playlist`
- `UserStudyProgress`
- `PlaylistsController`

Eles podem ser considerados em futuras etapas, conforme os requisitos da atividade.

---

## 🏗️ Decisões de arquitetura

O StudyTube foi organizado em quatro projetos principais, com responsabilidades separadas e dependências controladas.

### 🧱 Camadas e dependências

| Projeto | Responsabilidade |
|---|---|
| `StudyTube.Domain` | Entidades, value objects e regras centrais de negócio. |
| `StudyTube.Services` | Casos de uso, DTOs e interfaces das dependências necessárias. |
| `StudyTube.Data` | Persistência, Entity Framework Core, SQLite e comunicação HTTP com o YouTube. |
| `StudyTube.Api` | Endpoints, injeção de dependências, configuração HTTP e inicialização da aplicação. |

A direção das dependências é:

```text
StudyTube.Api
   ├── StudyTube.Services
   └── StudyTube.Data
          ├── StudyTube.Services
          └── StudyTube.Domain

StudyTube.Services
   └── StudyTube.Domain

StudyTube.Domain
   └── Sem dependências dos outros projetos
```

O domínio não depende de SQLite, Entity Framework Core, HTTP ou do formato original de resposta do YouTube.

Isso facilita a manutenção e permite modificar a infraestrutura sem precisar reescrever as regras centrais da aplicação.

### 🔌 Integração com o YouTube

A interface `IYouTubeApiClient` fica em `StudyTube.Services`, enquanto a implementação concreta `YouTubeApiClient` está em `StudyTube.Data/ExternalServices/YouTube`.

Essa separação mantém a regra de negócio independente da forma como a consulta externa é realizada.

O cliente concreto utiliza:

- `HttpClient` para a comunicação HTTP.
- A base `https://www.googleapis.com/youtube/v3/`.
- O recurso `videos?part=snippet,contentDetails`.
- A configuração `YouTube:ApiKey`.
- A qualidade `high` para miniaturas, com fallback para `default`.

A resposta original do Google é representada por um record privado chamado `YouTubeItem`. Ela não precisa se tornar uma entidade do domínio.

### 🗄️ Persistência com SQLite

O SQLite foi escolhido porque a aplicação mantém uma lista pessoal de videoaulas e não precisa de um servidor de banco de dados separado para o escopo atual.

O Entity Framework Core gerencia o acesso aos dados, enquanto `VideoConfiguration` e a migration definem o mapeamento da tabela `Videos`.

Essas configurações também estabelecem as restrições aplicáveis aos campos e o índice único de `YouTubeId`, reforçando a prevenção de duplicidades no banco.

A conversão entre o texto armazenado no SQLite e o value object `YouTubeVideoId` também fica nessa configuração.

### 🎓 Video ou VideoItem?

O material da atividade utiliza o nome `VideoItem`, mas a implementação atual utiliza `Video`.

A escolha foi manter `Video`, pois esse nome já está alinhado à tabela `Videos`, à rota `/api/videos` e ao DTO `VideoDto`.

### 🌍 Configuração de CORS

A política CORS se chama `Frontends` e permite qualquer origem, cabeçalho e método por meio de `AllowAnyOrigin`, `AllowAnyHeader` e `AllowAnyMethod`.

Isso facilita o desenvolvimento com o React em outra origem e com outros clientes.

**Nota de segurança:** essa política é permissiva. Antes de publicar a API em produção, recomenda-se restringir as origens autorizadas conforme os clientes efetivamente utilizados.

### 🚀 Inicialização do banco

O `Program.cs` executa `Database.Migrate()` durante a inicialização, antes de a API começar a atender requisições.

Dessa forma, as migrations pendentes são aplicadas automaticamente. O comando `dotnet ef database update` também pode ser utilizado para atualizar o banco manualmente.

---

## 🗺️ Próximos passos

O núcleo do StudyTube já permite organizar videoaulas e acompanhar sua conclusão. A partir dessa base, algumas evoluções possíveis são:

- [ ] Implementar playlists de estudo.
- [ ] Criar um modelo de progresso estruturado.
- [ ] Desenvolver o aplicativo mobile em React Native.
- [ ] Adicionar testes de ponta a ponta para os endpoints HTTP.
- [ ] Avaliar autenticação e separação de dados por usuário.
- [ ] Revisar a política CORS antes da publicação.
- [ ] Implementar relatórios ou certificados, se necessário.
- [ ] Avaliar a necessidade de mensageria conforme a evolução dos requisitos.

Essas funcionalidades são planejadas e não devem ser consideradas implementadas até que existam no código.

---

## 💖 Considerações finais

O StudyTube combina uma API REST em .NET, regras de negócio isoladas com DDD, persistência em SQLite e integração com o YouTube para simplificar a organização de videoaulas.

A arquitetura foi pensada para manter o núcleo do sistema enxuto e testável, permitindo que novas funcionalidades sejam incorporadas gradualmente.

**Pequenos passos, código organizado e estudos em andamento.** 🌷📚

---

<sub>📌 Documentação técnica do StudyTube API. As versões, quantidades de testes, rotas e comportamentos descritos correspondem ao levantamento fornecido e devem ser atualizados quando a implementação mudar.</sub>
