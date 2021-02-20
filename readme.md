# Localiza - Locação de Carros
## _REST API desenvolvida para realizar alocação de veículos._

## Estrutura do Projeto:

- API - Onde fica a parte web do projeto
- Infra - Ficam as configurações referentes a banco de dados, geração de PDF.
- Tests - Ficam os testes referentes a aplicação.
- Domain - Onde ficam as regras negocios referentes a aplicação.

## Features

- A aplicação tem a funcionalidade de locações de veículos.
- Simulação de alocação de veículos.
- Impressões de PDFs na alocação e Entregas de veículos.
- Sistema possui autenticação por cliente e operador.
- Cadastros de Categoria, Modelos e Marcas referentes ao Carros.
- Cadastros do Carro, Cliente e Operadores.
- Disponibilizada a rota com as opções de combustivéis disponibilizadas para o automovel.

## Tech

Dillinger uses a number of open source projects to work properly:

- [.Net 5] - Tecnologia utilizada no Backend
- [Swagger] - Inclusão de Documentação Swagger
- [Autenticação JWT] - Inclusão de Autenticação por usuário
- [Tests] - Inclusão de Camda de Testes
- Git
- Clean Code
- Azure
- TDD

## Modulos

- Usuários: separados em dois tipos Clientes e Operadores.
- Carros: Onde serão cadastrado os carros de acordo com suas caracteristicas.
- Agendamentos: Onde serão realizados as alocações de veículos.
- Simulação de agendamento de veículos
- Checklist de alocações de veículos.

## Installation

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.Net 5](https://dotnet.microsoft.com/download/dotnet/5.0). 
Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/)

### 🎲 Rodando o Back End (servidor)

```bash
# Clone este repositório
$ git clone <https://github.com/Micheler720/Localiza.git>

# Acesse a pasta do projeto no terminal/cmd
$ cd Localiza

# Vá para a pasta da API
$ cd API

# Execute a aplicação em modo de desenvolvimento
$ dotnet run

# O servidor inciará na porta:5001 - acesse <http://localhost:5001>
## License

MIT
 

