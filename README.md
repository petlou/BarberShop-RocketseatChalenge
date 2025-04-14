# 💈 BarberShop - Controle de Faturamento

[![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-blue?logo=postgresql)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Ready-blue?logo=docker)](https://www.docker.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](#📃-licença)
[![Language Support](https://img.shields.io/badge/i18n-pt%20%7C%20en-yellowgreen)](#🌐-regionalização)

Sistema de controle de **faturamentos** para barbearias, desenvolvido como parte de um desafio técnico da Rocketseat. Com essa aplicação, é possível cadastrar faturamentos diários, além de gerar **relatórios mensais em PDF e Excel** de forma simples e eficiente.

---

## 🚀 Tecnologias Utilizadas

- **.NET 8 (ASP.NET Core)** – Framework principal da API
- **PostgreSQL 17** – Banco de dados relacional
- **Docker & Docker Compose** – Containerização e orquestração de serviços
- **Entity Framework Core** – ORM para acesso a dados
- **FluentValidation** – Validação robusta de dados
- **PDFsharp + MigraDoc** – Geração de relatórios em PDF
- **ClosedXML** – Geração de planilhas Excel
- **AutoMapper** – Mapeamento entre DTOs e entidades
- **xUnit** – Testes automatizados
- **Clean Architecture** – Organização de código em camadas bem definidas
- **Suporte à Regionalização** – Português e Inglês

---

## 🧠 Boas Práticas e Arquitetura

O projeto segue os princípios de **Clean Architecture**, **SOLID** e **Clean Code**, proporcionando:

- **Separação clara de responsabilidades**
- **Facilidade de manutenção e escalabilidade**
- **Código testável e reutilizável**
- **Validações desacopladas** com FluentValidation
- **Tratamento consistente de exceções**
- **Regionalização nativa** com mensagens adaptadas

---

## 📦 Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/) e [Docker Compose](https://docs.docker.com/compose/)

### Passo a passo

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/petlou/BarberShop-RocketseatChalenge.git
   cd BarberShop-RocketseatChalenge
   ```

2. **Suba o banco de dados com Docker Compose:**

   ```bash
   docker-compose up -d
   ```

3. **Execute as migrations:**

   ```bash
   dotnet ef database update --project BarberShop.Infrastructure --startup-project BarberShop.API
   ```

4. **Inicie a aplicação:**

   ```bash
   dotnet run --project BarberShop.API
   ```

API disponível em: `https://localhost:7150` ou `http://localhost:5026`

---

## 📊 Funcionalidades

- ✅ Cadastro de faturamentos diários
- ✅ Listagem de faturamentos
- ✅ Geração de relatórios mensais:
  - 📄 PDF
  - 📊 Excel
- ✅ Validação com mensagens claras
- ✅ Regionalização: Português 🇧🇷 e Inglês 🇺🇸
- ✅ Testes automatizados com xUnit

---

## 📬 Exemplos de Requisições

### 🔹 Criar Faturamento

`POST /api/billing`

```json
{
	"title": "Corte Especial",
	"description": "Parcelado em 2x",
	"date": "2025-04-14T00:00:00.000Z",
	"amount": 150.0,
	"paymentType": 1
}
```

**Resposta:**

```json
{
	"title": "Corte Especial"
}
```

---

### 🔹 Listar Faturamentos

`GET /api/billing`

**Resposta:**

```json
{
	"billings": [
		{
			"id": "37334171-e0da-4a4c-a3fe-c5fc0d8084d8",
			"title": "Corte Especial",
			"amount": 150.0
		}
	]
}
```

---

### 🔹 Gerar Relatório em PDF

`GET /api/reports/pdf?month=2025-04`

Resposta: `application/pdf` (arquivo para download)

---

### 🔹 Gerar Relatório em Excel

`GET /api/reports/excel?month=2025-04`

Resposta: `application/octet-stream` (arquivo para download)

---

## 📁 Estrutura do Projeto

```
BarberShop.API              → Camada de apresentação
BarberShop.Application      → Casos de uso e lógica de negócios
BarberShop.Domain           → Entidades e contratos de domínio
BarberShop.Infrastructure   → Implementação de repositórios e persistência
BarberShop.Communication    → DTOs e objetos de comunicação
BarberShop.Exception        → Exceções personalizadas
Tests.*                     → Projetos de testes unitários
```

---

## 🌐 Regionalização

O projeto conta com suporte à regionalização, com mensagens adaptadas para:

- 🇧🇷 **Português (pt-BR)**
- 🇺🇸 **Inglês (en-US)**

---

## 📃 Licença

Este projeto foi desenvolvido para fins educacionais. Sinta-se à vontade para estudar, utilizar e adaptar!
