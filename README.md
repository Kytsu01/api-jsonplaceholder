# JsonPlaceholderImporter – .NET 8 Web API

API em .NET 8 que consome a JSONPlaceholder, faz uma normalização simples dos dados (Trim/Lower) e salva em SQL Server via EF Core.
Disponibiliza endpoints de importação, CRUD básicos e listas paginadas. Há também exemplo de RAW SQL.

### Stack

- .NET 8 (ASP.NET Core Web API)

- Entity Framework Core 8 (SQL Server)

- Swagger (Swashbuckle)

- HttpClient

- JSONPlaceholder

### Como rodar


1. Criar/atualizar o banco:
    ```
    
    cd JsonPlaceholderImporter
    dotnet ef database update --project ./JsonPlaceholderImporter/JsonPlaceholderImporter.csproj

    ```

2. Subir a API:
    ```
    
    dotnet run --project ./JsonPlaceholderImporter/JsonPlaceholderImporter.csproj

    ```

3. Abrir o Swagger: `https://localhost:<porta>/swagger`

### Importação de dados

- Tudo de uma vez: POST /api/import/all
  - (ordem: Users → Posts → Comments → Albums → Photos → Todos)

- Por recurso:

  - `POST /api/import/users`

  - `POST /api/import/posts`

  - `POST /api/import/comments`

  - `POST /api/import/albums`

  - `POST /api/import/photos`

  - `POST /api/import/todos`

**Obs.: não há verificação de duplicação, importar novamente pode criar registros repetidos.**

### Endpoints de leitura (paginação)

- `GET /api/users`

- `GET /api/users/{id:int} — detalhe do usuário`

- `GET /api/users/post-count — posts do usuário`

- `GET /api/posts?page=1&pageSize=20 — lista paginada de posts`

- `GET /api/albums?page=1&pageSize=20 — lista paginada de álbuns`

- `GET /api/albums/{id:int} — detalhe de um álbum`

- `GET /api/photos?page=1&pageSize=20 — lista paginada de fotos`

### Como atualizar (PUT) — ID na rota **e** no corpo

Para evitar atualizações no recurso errado, o `PUT` exige **ID na URL** e **o mesmo ID no corpo**.
Se os IDs não coincidirem, a API retorna **400 Bad Request**.

Exemplo (Users):
- Rota: `PUT /api/users/5`
- Body:

        
  
        {
          "id": 5,
          "name": "Leanne Graham",
          "username": "Bret",
          "email": "sincere@april.biz",
          "phone": "1-770-736-8031 x56442",
          "website": "hildegard.org",
          "address": {
            "street": "Kulas Light",
            "city": "Gwenborough",
            "zipCode": "92998-3874",
            "geo": { "lat": "-37.3159", "lng": "81.1496" }
          },
          "company": {
            "name": "Romaguera-Crona",
            "catchPhrase": "…",
            "bs": "…"
          }
        }
  
        

### RAW SQL (exemplo)

- `GET /api/users/post-count` — usuários com contagem de posts via `FromSqlRaw`.

### POST com DTO (payload simples)

Para deixar o Swagger limpo, os POSTs usam DTOs. Exemplo (`POST /api/users`):
    ```

    {
      "name": "Leanne Graham",
      "username": "Bret",
      "email": "sincere@april.biz",
      "phone": "1-770-736-8031 x56442",
      "website": "hildegard.org",
      "address": {
        "street": "Kulas Light",
        "city": "Gwenborough",
        "zipCode": "92998-3874",
        "geo": { "lat": "-37.3159", "lng": "81.1496" }
      },
      "company": {
        "name": "Romaguera-Crona",
        "catchPhrase": "…",
        "bs": "…"
      }
    }
    
    

### Processamento/normalização

- `Trim()` em strings e `.ToLower()` onde faz sentido.

- Respostas grandes foram “resumidas” com paginação.

- Navegações de volta têm `[JsonIgnore]` para evitar loops de JSON.

