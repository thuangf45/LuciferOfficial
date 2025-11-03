# Environment Configuration Guide

**File:** `.env`  
**Location:** ./config  
**Purpose:**  
This file defines all environment-specific variables used by the **LuciferCore Framework**, including optional modules such as Web Server, AI Services, Database, and Manager components.

> This document defines all optional and required environment variables recognized by **LuciferCore Framework**.  
> Sections marked as **(Optional)** can be omitted if the corresponding module is not used.

---

## 1. Web Server Configuration (Optional)

This section is **not part of the LuciferCore core system**.  
It serves only as a **complete example setup** for running a standalone or integrated **web server** (e.g., hosting a frontend or API gateway).  
You may **rename variables or adjust structure** as long as they serve the same functional purpose.

| Variable | Description | Example |
|-----------|--------------|----------|
| `CERTIFICATE` | Path to the SSL certificate file (`.pfx` format) | `assets/tools/certificates/server.pfx` |
| `PORT` | Port number that the web server listens on | `2000` |
| `CERT_PASSWORD` | Password used to decrypt the SSL certificate | `qwerty` |
| `WWW` | Directory path containing static frontend files | `assets/client` |

**Example**
```env
CERTIFICATE=assets/tools/certificates/server.pfx
PORT=2000
CERT_PASSWORD=qwerty
WWW=assets/client
```

**Notes**

* This configuration is optional and serves as an example for users running a web server.
* You can rename the variables to match your preferred naming style.
* Use a valid SSL certificate in production.
* Never commit the `.pfx` file or its password to a public repository.

---

## 2. AI Services Configuration (Optional)

Defines paths and resources for AI-based components such as chatbots or trained models.

| Variable     | Description                                    | Example                       |
| ------------ | ---------------------------------------------- | ----------------------------- |
| `MODEL_PATH` | Path to the pre-trained AI model file (`.zip`) | `assets/AI/chatbot_model.zip` |
| `DATA_PATH`  | Path to the training dataset                   | `assets/AI/training_data.csv` |

**Example**

```env
MODEL_PATH=assets/AI/chatbot_model.zip
DATA_PATH=assets/AI/training_data.csv
```

**Notes**

* This section is optional and can be removed if no AI features are used.
* The model file must match the format supported by the framework.
* The service automatically loads the model during initialization.

---

## 3. Database Configuration (Optional)

Specifies how the framework connects to the database system.

| Variable                 | Description                                                             | Example     |
| ------------------------ | ----------------------------------------------------------------------- | ----------- |
| `DB_NAME`                | Name of the database                                                    | `base`      |
| `DB_USER`                | Database login username                                                 | `sa`        |
| `DB_PASSWORD`            | Password for the database user                                          | `svcntt`    |
| `DB_HOST`                | Database server address                                                 | `localhost` |
| `DB_PORT`                | Database connection port (`1433` for SQL Server, `5432` for PostgreSQL) | `1433`      |
| `DB_TRUSTED_CONNECTION`  | Enables Windows authentication (`true` / `false`)                       | `false`     |
| `DB_TIMEOUT`             | Connection timeout in seconds                                           | `30`        |
| `DB_MAX_POOL_SIZE`       | Maximum number of connections in the pool                               | `128`       |
| `DB_MIN_POOL_SIZE`       | Minimum number of connections in the pool                               | `8`         |
| `DB_COMMAND_TIMEOUT`     | Timeout for executing SQL commands (seconds)                            | `60`        |
| `DB_IDLE_TIMEOUT`        | Time before idle connections are closed (seconds)                       | `300`       |
| `DB_CONNECTION_LIFETIME` | Maximum lifetime of a connection (`0` = unlimited)                      | `0`         |
| `DB_POOLING`             | Enables connection pooling for better performance                       | `true`      |
| `DB_BATCH_SIZE`          | Maximum number of operations before batch flush                         | `100`       |
| `DB_FLUSH_INTERVAL`      | Periodic batch flush interval (milliseconds)                            | `5000`      |

**Example**

```env
DB_NAME=base
DB_USER=sa
DB_PASSWORD=svcntt
DB_HOST=localhost
DB_PORT=1433
DB_TRUSTED_CONNECTION=false
DB_TIMEOUT=30
DB_MAX_POOL_SIZE=128
DB_MIN_POOL_SIZE=8
DB_COMMAND_TIMEOUT=60
DB_IDLE_TIMEOUT=300
DB_CONNECTION_LIFETIME=0
DB_POOLING=true
DB_BATCH_SIZE=100
DB_FLUSH_INTERVAL=5000
```

**Notes**

* All database settings are optional and can be disabled if persistence is not required.
* Use separate `.env` files for development and production environments.
* Do not commit credentials or sensitive database information to source control.

---

## 4. Manager Configuration (Optional)

### 4.1 Notify Manager (SMTP Configuration)

Used for sending notifications, alerts, or password recovery emails.

| Variable    | Description                                   | Example                |
| ----------- | --------------------------------------------- | ---------------------- |
| `SMTP_HOST` | SMTP server address                           | `smtp.gmail.com`       |
| `SMTP_PORT` | SMTP port (`587` for TLS, `465` for SSL)      | `587`                  |
| `SMTP_PASS` | Application-specific password or API key      | `aysd pgdv lfib ldll`  |
| `USE_SSL`   | Enables SSL/TLS encryption (`true` / `false`) | `true`                 |
| `SMTP_USER` | Email address used as the sender              | `kingnemacc@gmail.com` |

**Example**

```env
SMTP_HOST=smtp.gmail.com
SMTP_PORT=587
SMTP_PASS=aysd pgdv lfib ldll
USE_SSL=true
SMTP_USER=kingnemacc@gmail.com
```

**Notes**

* Optional; only required if the notification system is enabled.
* When using Gmail, enable two-factor authentication and use an App Password.
* Never expose real email credentials in shared repositories.

---

### 4.2 Simulation Manager (Optional)

Defines system limits for event or task processing.

| Variable          | Description                                 | Example |
| ----------------- | ------------------------------------------- | ------- |
| `LIMITER_WORKERS` | Maximum number of concurrent worker threads | `10`    |

**Example**

```env
LIMITER_WORKERS=10
```

---

## 5. RSA Key Configuration (Optional)

Contains RSA key information used for encryption and authentication.

| Variable | Description                          |
| -------- | ------------------------------------ |
| `n`      | RSA modulus value (large integer)    |
| `d`      | Private exponent (keep confidential) |
| `e`      | Public exponent (commonly `65537`)   |

**Example (truncated)**

```env
n=6554051314125035263...
d=2838952175189305293...
e=65537
```

**Notes**

* Optional unless cryptographic operations are required.
* Only distribute the public key (`n` and `e`) for verification.
* Keep the private key (`d`) secure and never commit it to version control.

---

## 6. Summary

| Section     | Purpose                                    | Related Files                      |
| ----------- | ------------------------------------------ | ---------------------------------- |
| Web Server  | Example configuration for HTTP/HTTPS setup | `server.pfx`, `assets/client` |
| AI Services | Defines model and dataset paths            | `assets/AI/*`                      |
| Database    | Sets up database connections               | `base`                             |
| Manager     | Handles SMTP and system task management    | `smtp.gmail.com`                   |
| RSA Keys    | Provides encryption and signing capability | Internal keypair                   |

---

## 7. Quick Setup

1. Copy the `.env` example file.
2. Update the values according to your environment.
3. Run the application:

   ```bash
   dotnet run
   ```

**Tip:** If using the **LuciferCore CLI**, you can generate an environment file automatically:

```bash
lucifer init
# or
lucifer create config/.env
```

---

## Final Note

The `config/.env` file included in this repository is **only a sample template**.  
**Do not** upload or publish your real `.env` file containing private credentials, keys, or secrets.  
Keep your configuration secure — and good luck building with **LuciferCore**!

---

**LuciferCore Framework**  
Maintained by [Lucifer](https://github.com/thuangf45)
