# ProductDescriber

**ProductDescriber** is a full-stack application built with ASP.NET Core MVC and Web API that generates meaningful product descriptions using a local LLM (Large Language Model). Given a product title and its features, the system creates SEO-friendly, human-like product summaries with the help of the `deepseek-r1:8b` model running via Ollama.

## 🚀 Features

- 📝 AI-generated product descriptions based on title and features
- ⚙️ Integration with locally hosted Ollama LLM (deepseek-r1:8b)
- 🔄 CQRS architecture with MediatR and AutoMapper
- 🧱 Clean layered architecture (MVC, API, Application, Domain, Infrastructure)
- 🧠 Prompt-based LLM request pipeline
- 🌐 Slug-based SEO URLs (e.g., `/acer-aspire-lite-15inch`)
- 🧩 Full CRUD support (Create, Read, Update, Delete)
- 🎨 Bootstrap 5 UI with loading animation
- 🔐 Identity and token-based extensible architecture
- 📦 Ready for Redis, RabbitMQ and caching integration

## 🛠️ Technologies Used

- ASP.NET Core MVC & Web API
- MediatR, AutoMapper
- FluentValidation
- CQRS Pattern
- Entity Framework Core (MSSQL/PostgreSQL)
- Ollama + DeepSeek-r1:8b
- Bootstrap 5
- RESTful APIs

## 🧪 How It Works

1. The user submits a product title and a set of features.
2. The system saves the product via Web API and sends the data to the LLM engine.
3. The LLM generates a clean, short, and user-friendly product description.
4. The generated description is shown to the user and saved to the database.
5. Each product has its own slug-based URL (like `/iphone-13-pro`).
6. The product can be edited or deleted from the product list page.

## 📂 Project Structure

```
ProductDescriber/
│
├── ProductDescriber.Api/         # Web API (LLM service, CRUD operations)
├── ProductDescriber.Web/         # MVC Frontend (Bootstrap UI, HttpClient)
├── ProductDescriber.Application/ # CQRS Commands/Queries, Services
├── ProductDescriber.Domain/      # Core entities and interfaces
├── ProductDescriber.Infrastructure/ # EF Core setup, Repositories, UnitOfWork
├── ProductDescriber.Base/        # Shared types (ApiResponse, base classes)
```

## ▶️ Getting Started

1. Install and start Ollama with the LLM model:

```bash
ollama pull deepseek-r1:8b
ollama run deepseek-r1:8b
```

2. Start the **API** project (`ProductDescriber.Api`)  
   It should run on `http://localhost:5050`

3. Start the **MVC** project (`ProductDescriber.Web`)  
   It should run on `http://localhost:5000`

4. Visit [http://localhost:5000/Product/Create](http://localhost:5000/Product/Create) and try it out!

## 🧑‍💻 Developer Notes

- The prompt is designed to create **clean, fluent, non-technical** product summaries.
- All product routes use **slugified** URLs for SEO-friendly navigation.
- You can **extend** the architecture to support chat-based feedback, rating, or multilingual prompts.


## 📄 License

This project is licensed under the MIT License. See `LICENSE` for details.

---

> ProductDescriber is a real-world example of how modern architectures and LLMs can be combined to create intelligent and interactive user-facing software.
