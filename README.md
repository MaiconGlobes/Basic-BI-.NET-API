## Rotas 🌍
O swagger ainda não foi implementado, então as rotas estão disponiveis abaixo, lembrando que nenhum possui token no header até o moemnto.
- GET    : /apply-migrate
- GET    : /revert-migrate
- GET    : /user/all
- POST   : /user/register
- PUT    : [EM DESENVOLVIMENTO]
- DELETE : [EM DESENVOLVIMENTO]

## Request e Response 📜
Aplicação da migration no banco. Cria o database e todas as dependências.
- GET : http://localhost:5005/apply-migrate/
```bash
[No Body]
```

Reversão da migration no banco. Destroi o database e todas as dependências.
- GET : http://localhost:5005/revert-migrate
```bash
[No Body]
```

Realiza a busca de todos os usuários no banco (sem paginação até o momento).
- GET : http://localhost:5005/user/all
```bash
[No Body]
```
Realiza a cadastro do usuário no banco (sem authorization até o momento (nas próximas releases será exigido token no header em cada requisição).
- POST : http://localhost:5005/user/register
```bash
{
  "apelido": "apelido",
  "email": "email.teste@example.com",
  "senha": "123456",
  "pessoa": {
    "nome": "Nome completo",
    "cpf_cnpj": "07640502485",
    "telefone": "19999999999"
  }
}
```

## Padrão de retorno
Todos os responses possui um padrão de retorno para identificação do staus, erro, correções e dados.

SUCESSO: 200 OK - [Get com sucesso]
```bash
{
  "retorno": {
    "status": "Sucesso",
    "codigo_status": 3,
    "dados": [
      {
        "id": 1,
        "apelido": "Apelido qualquer",
        "email": "email.teste@example.com",
        "token": "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
        "pessoaId": 1,
        "pessoa": {
          "nome": "Nome completo",
          "cpf_cnpj": "07640502485",
          "telefone": "19999999999"
        }
      }
    ]
  }
}
```

SUCESSO: 201 Created - [Criar registro]
```bash
{
  "retorno": {
    "status": "Sucesso",
    "codigo_status": 3,
    "dados": [
      {
        "apelido": "Apelido qualquer",
        "email": "email.teste@example.com",
        "token": "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
        "pessoaId": 1,
        "pessoa": {
          "nome": "Nome completo",
          "cpf_cnpj": "07640502485",
          "telefone": "19999999999"
        }
      }
    ]
  }
}
```
SUCESSO: 200 Ok - [Registro não localizado]
```bash
{
  "retorno": {
    "status": "Não localizado",
    "codigo_status": 2,
    "mensagens": [
      {
        "codigo": "19",
        "descricao": "Usuário não localizado para edição dos dados."
      }
    ]
  }
}
```

