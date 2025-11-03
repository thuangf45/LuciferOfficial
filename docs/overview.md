# LuciferCore Framework – Overview

LuciferCore is a lightweight yet powerful .NET framework designed for building modular server architectures with integrated CLI support. It emphasizes simplicity, performance, and extensibility, allowing developers to focus on core logic while the framework handles boilerplate concerns like networking, lifecycle management, and event scheduling.

It includes the following key components:

| Component                  | Description                                                                 |
|----------------------------|-----------------------------------------------------------------------------|
| **LuciferCore**            | Core runtime framework that provides networking, server session management, and service hosting capabilities. |
| **Lucifer.CLI**            | Command-line interface (CLI) tool for generating templates, managing builds, and scaffolding projects. |
| **LuciferOfficial (Templates)** | Official repository containing ready-to-use examples and templates built on top of LuciferCore. |

---

## Architecture Overview

LuciferCore is composed of decoupled modular subsystems, each designed for high performance and seamless integration. The framework draws inspiration from established patterns—such as Unity's simulation model and high-throughput networking predecessors—to deliver an automated, developer-friendly experience. Key namespaces and their roles are outlined below:

| Namespace/Module       | Purpose                                                                 |
|------------------------|-------------------------------------------------------------------------|
| **`LuciferCore.Main`** | Houses the entry-point classes for system initialization and runtime management. The flagship `HostServer` class serves as the central runtime host, enabling service registration, console command handling, and overall orchestration. Initialization is as simple as:<br><br>```csharp
| **`LuciferCore.NetCoreServer`** | Builds upon proven high-performance networking foundations (inheriting from established TCP/UDP/WebSocket implementations) to provide a robust communication layer. Supports **TCP**, **UDP**, and **WebSocket** protocols with optimized throughput for real-time applications. Developers can extend base classes like `WsServer` and `WsClient` for custom handlers. |
| **`LuciferCore.Core`** | Core utilities for lifecycle management, logging, and command parsing. It includes the `Simulation` class, inspired by Unity's architecture, which enables centralized singleton registration via `GetModel<T>()`. Events are scheduled into a priority heap queue (heapqueue) based on timestamps, with a tick-based system for efficient execution. This automates background processes, coroutines, and timed tasks, reducing manual orchestration. |
| **`LuciferCore.Manager`** | High-level extensions for common server needs, including **SMTP notifications**, **data serialization**, and task simulation helpers. |
| **`Lucifer.CLI`**      | Developer tools for project scaffolding, template generation, and framework updates. Commands like `lucifer create core/Handler` allow quick instantiation of pre-built templates (e.g., session handlers) from the `./core` directory, streamlining development. |

All subsystems are decoupled and modular, allowing independent operation or custom integration. Automation is a core tenet: once services are registered (e.g., via the `[Server]` attribute), they auto-start on `Run()`. Developers can focus solely on writing session handlers and API logic, with ready-made templates in `./core` for rapid prototyping—copy them directly or generate via CLI.

### Key Automation Features
- **Attributes for Routing and Security**: Use `[HttpGet]`, `[HttpPost]`, etc., for automatic API routing. Pair with `[Authorize]` to enforce authentication before invocation.
- **Service Registration**: Annotate classes with `[Server]` to auto-register and launch them within the host ecosystem.
- **Ongoing Development**: Additional features (e.g., advanced coroutine chaining and heapqueue optimizations) are in active refinement within the core modules, with public release pending final validation.

This design ensures everything runs autonomously, simplifying your workflow while maintaining enterprise-grade reliability.

---

## Directory Structure

```
/LuciferCore
├── config/                 # Example environment configurations (.env files)
├── source/
│   ├── LuciferCore/        # Main framework source (namespaces: Main, NetCoreServer, Core, Manager)
│   ├── Lucifer.CLI/        # Command-line tool implementation
│   └── Lucifer.Demo/       # Example/demo projects with runnable templates
├── docs/                   # Comprehensive documentation
├── tool/certificates/      # Development certificates and security templates
├── init.json               # Lucifer.CLI initialization configuration
└── README.md               # Project overview, quick-start guide, and contribution details
```

---

## Core Design Principles

- **Minimal Dependencies**: 100% C# and native .NET, eschewing heavy external frameworks for lean, portable code.
- **Extensible CLI**: Streamlines setup, updates, code generation, and template instantiation to accelerate development.
- **Unified Configuration System**: Leverages `.env`-based files for centralized, environment-agnostic module configuration.
- **Modular and Automated Components**: Subsystems operate independently or in tandem, with built-in automation (e.g., singleton registration, event ticking) to minimize boilerplate.

---

> LuciferCore serves as both a *foundation framework* and a *comprehensive development ecosystem* for .NET server-based projects, empowering developers to build scalable, high-performance applications with minimal overhead.