
# 🧪 Tripadvisor API Automation Framework

This is a lightweight API automation framework built in **C#** using `RestSharp`, `NUnit`, and `Serilog`. The framework is used to test [Tripadvisor RapidAPI](https://rapidapi.com/DataCrawler/api/tripadvisor16/) endpoints, log requests/responses, and validate API behavior.

## ⚙️ Tech Stack

| Tool       | Purpose                         |
|------------|----------------------------------|
| C# (.NET 8+) | Core language                   |
| RestSharp  | HTTP client                      |
| NUnit      | Test framework                   |
| Serilog    | Structured logging               |
| DI         | `Microsoft.Extensions.DependencyInjection` |
| Newtonsoft.Json | JSON (de)serialization      |

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- Access to [RapidAPI](https://rapidapi.com/DataCrawler/api/tripadvisor16/) + valid API key

### 📥 Clone the repo

```bash
git clone https://github.com/your-username/tripadvisor-api-tests.git
cd tripadvisor-api-tests
```

### 📦 Install dependencies

```bash
dotnet restore
```

### 🔧 Configure your API key

1. Open `appsettings.Debug.json`
2. Replace `<your-api-key>` with your actual RapidAPI key.

## 🧪 Run tests

From project root:

```bash
dotnet test
```

### ➕ Or run specific test (e.g. via Rider / VS Test Explorer)

You can run:

- `PrintCruisesSortedByCrewCount` — queries cruise data and logs ships sorted by crew count.

## CI/CD

[![.NET](https://github.com/e-chiruk-aqa/tripadvisor-api-tests/actions/workflows/dotnet.yml/badge.svg)](https://github.com/e-chiruk-aqa/tripadvisor-api-tests/actions/workflows/dotnet.yml)

## 📁 Logs & Attachments

Each test generates:

- `.log` file stored in the output folder
- logs include request/response payloads, errors, and timestamps
- attached to NUnit test context (`TestContext.AddTestAttachment`)

## 🔨 Customization

### Add new endpoint test

1. Extend `TripadvisorApiClient` or use base `HttpClient`
2. Create a new `TestFixture`
3. Register new classes in `ServiceCollectionExtension` if needed
