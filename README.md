# ⚡ Telecom Engineer Toolkit

<p align="center">
  <strong>Professional Offline Engineering Workspace for Telecom Engineers</strong>
</p>

<p align="center">
  RF • Microwave • Fiber • Power • IP Networking • Site Management
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
  <img src="https://img.shields.io/badge/C%23-Desktop-239120?style=for-the-badge&logo=csharp&logoColor=white">
  <img src="https://img.shields.io/badge/WPF-Windows-0078D4?style=for-the-badge&logo=windows&logoColor=white">
  <img src="https://img.shields.io/badge/SQLite-Local%20Database-003B57?style=for-the-badge&logo=sqlite&logoColor=white">
</p>

<p align="center">
  <img src="https://img.shields.io/badge/AI-Not%20Required-555555?style=flat-square">
  <img src="https://img.shields.io/badge/Internet-Not%20Required-2EA44F?style=flat-square">
  <img src="https://img.shields.io/badge/Cloud-Not%20Required-2EA44F?style=flat-square">
  <img src="https://img.shields.io/badge/Status-Active%20Development-orange?style=flat-square">
</p>

---

## 🛰️ What Is This?

**Telecom Engineer Toolkit** is a Windows desktop application built to give telecom engineers a single workspace for routine engineering calculations, site documentation, equipment records, and project calculations.

It is designed around one principle:

> **Keep engineering work fast, local, structured, and independent from cloud services.**

The application does **not** require AI, cloud infrastructure, remote APIs, or an Internet connection to perform its core functions.

---

# 🎯 Core Capabilities

<table>
<tr>
<td width="33%" align="center">

### 📡 RF Engineering

RF calculations, link budgets, EIRP, FSPL, received power and fade margin.

</td>

<td width="33%" align="center">

### 📶 Microwave

Point-to-point microwave calculations, Fresnel zones, LOS, FSPL and link margin.

</td>

<td width="33%" align="center">

### 🔦 Fiber

Optical loss budgets, RX power, attenuation, connectors, splices and margin.

</td>
</tr>

<tr>
<td width="33%" align="center">

### 🔋 Power

Site load, energy consumption and battery backup calculations.

</td>

<td width="33%" align="center">

### 🌐 Networking

IPv4 subnetting, CIDR, masks, wildcard masks and host ranges.

</td>

<td width="33%" align="center">

### 🗂️ Site Management

Local site database, equipment inventory and engineering notes.

</td>
</tr>
</table>

---

# 🧰 Engineering Toolbox

### 📡 RF Calculator

| Calculation            | Supported |
| ---------------------- | :-------: |
| Frequency ↔ Wavelength |     ✅     |
| dBm ↔ Watt             |     ✅     |
| EIRP                   |     ✅     |
| FSPL                   |     ✅     |
| Received Power         |     ✅     |

---

### 📊 RF Link Budget

Build a complete RF link budget from transmitter to receiver.

**Inputs**

```text
Frequency
Distance
TX Power
TX Antenna Gain
RX Antenna Gain
Cable / System Loss
Receiver Sensitivity
```

**Outputs**

```text
Free Space Path Loss
Total System Loss
Received Power
Fade Margin
Link Status
```

**Link status**

`PASS` → `MARGINAL` → `FAIL`

---

# 🔦 Fiber Loss Budget

Calculate optical link performance using:

```text
Fiber Length
Fiber Attenuation
Splice Count
Connector Count
TX Optical Power
RX Sensitivity
```

### Result

```text
Total Optical Loss
Expected RX Power
Optical Margin
Link Status
```

| Status     | Meaning                            |
| ---------- | ---------------------------------- |
| 🟢 PASS    | Link has sufficient optical margin |
| 🟡 WARNING | Margin is becoming limited         |
| 🔴 FAIL    | Link budget is insufficient        |

---

# 🔋 Power Engineering

## Battery Backup

Calculate telecom battery requirements based on:

```text
Load
System Voltage
Battery Capacity
Battery Quantity
Efficiency
Depth of Discharge
Required Backup Time
```

### Automatically calculates

> **Required Capacity → Available Energy → Estimated Backup Time → Required Battery Quantity**

---

## ⚡ Site Power

Create a site power profile by adding multiple devices.

Each device contains:

```text
Device Name
Quantity
Power Consumption
Operating Hours
```

The application calculates:

| Parameter          | Result    |
| ------------------ | --------- |
| Instantaneous Load | W         |
| Daily Energy       | Wh/day    |
| Monthly Energy     | kWh/month |

---

# 🌐 IP / Subnet Calculator

Enter an IPv4 network using CIDR notation.

### Example

```text
192.168.10.0/24
```

### Returns

```text
Network Address
Broadcast Address
First Host
Last Host
Host Count
Subnet Mask
Wildcard Mask
```

---

# 📶 Microwave Link

A dedicated point-to-point microwave calculation module.

### Inputs

```text
Site A Coordinates
Site B Coordinates
Site Heights
Frequency
TX Power
Antenna Gain
System Loss
```

### Calculates

```text
Link Distance
Line of Sight
Fresnel Zone
FSPL
Received Power
Link Margin
```

The module also provides a simplified visual representation of the link between both sites.

> **Note:** This is an engineering calculation and planning tool. It is not intended to replace professional RF propagation or terrain-analysis software.

---

# 🗺️ Coverage Planner

A lightweight local planning environment for telecom sites and sectors.

### Site Parameters

```text
Coordinates
Tower Height
Azimuth
Beamwidth
Coverage Radius
```

Sites and sectors can be visualized on a local canvas.

### Scope

**Useful for:**

* Preliminary planning
* Sector visualization
* Site documentation
* Basic coverage representation

**Not a replacement for:**

* Professional RF propagation software
* Terrain modeling
* Drive-test analysis
* Commercial RF planning platforms

---

# 🏗️ Site Notebook

Maintain a structured local database for telecom sites.

### Site Record

```text
Site ID
Site Name
Coordinates
Tower Height
Site Type
Technology
Engineering Notes
```

### Operations

`CREATE` · `UPDATE` · `DELETE` · `VIEW`

All records remain inside the local SQLite database.

---

# 📦 Equipment Database

Maintain equipment records directly inside the application.

### Equipment Record

```text
Category
Manufacturer
Model
Specifications
```

### Operations

`CREATE` · `UPDATE` · `DELETE` · `VIEW`

---

# 💾 Project Workspace

Engineering calculations can be stored as projects and accessed later.

### Supported Operations

```text
Save
View
Refresh
Delete
Export CSV
```

This allows calculations to become part of a persistent engineering workspace rather than temporary values inside a calculator.

---

# 🖥️ Dashboard

Everything starts from one central dashboard.

```text
                    ┌──────────────────────┐
                    │      DASHBOARD       │
                    └──────────┬───────────┘
                               │
       ┌───────────┬───────────┼───────────┬───────────┐
       ▼           ▼           ▼           ▼           ▼
      RF       Microwave     Fiber       Power       IP
       │           │           │           │           │
       └───────────┴───────────┴───────────┴───────────┘
                               │
                     ┌─────────┴─────────┐
                     ▼                   ▼
                   Sites              Equipment
                     │                   │
                     └─────────┬─────────┘
                               ▼
                           Projects
```

---

# 🧱 Architecture

The application follows a modular **MVVM architecture**.

```text
┌─────────────────────────────────────────────┐
│                 Toolkit.UI                  │
│               WPF / XAML UI                 │
└──────────────────────┬──────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────┐
│            Toolkit.Application              │
│         ViewModels / Commands / Logic       │
└──────────────────────┬──────────────────────┘
                       │
             ┌─────────┴─────────┐
             ▼                   ▼
┌─────────────────────┐ ┌─────────────────────┐
│  Engineering.Core   │ │ Toolkit.Infrastructure│
│                     │ │                     │
│ Calculators         │ │ SQLite              │
│ Engineering Logic   │ │ Models              │
│ Units               │ │ Database Services   │
└─────────────────────┘ └─────────────────────┘
```

---

# 🛠️ Technology Stack

<div align="center">

|         Technology        | Role                     |
| :-----------------------: | ------------------------ |
|           **C#**          | Application Development  |
|        **.NET 10**        | Runtime / Framework      |
|          **WPF**          | Desktop Interface        |
|          **MVVM**         | Application Architecture |
|         **SQLite**        | Local Data Storage       |
| **Microsoft.Data.Sqlite** | Database Access          |
|         **xUnit**         | Unit Testing             |

</div>

---

# 📁 Project Structure

```text
TelecomEngineerToolkit/
│
├── src/
│   │
│   ├── Engineering.Core/
│   │   ├── Calculators/
│   │   │   ├── RF/
│   │   │   ├── Fiber/
│   │   │   ├── Battery/
│   │   │   ├── SitePower/
│   │   │   ├── LinkBudget/
│   │   │   ├── Microwave/
│   │   │   ├── IP/
│   │   │   └── Coverage/
│   │   │
│   │   ├── Helpers/
│   │   └── Units/
│   │
│   ├── Toolkit.Application/
│   │   └── ViewModels/
│   │
│   ├── Toolkit.Infrastructure/
│   │   ├── Models/
│   │   └── Services/
│   │
│   ├── Toolkit.UI/
│   │   ├── Views/
│   │   ├── MainWindow.xaml
│   │   └── SaveCalculationWindow.xaml
│   │
│   └── Toolkit.Tests/
│
└── TelecomEngineerToolkit.sln
```

---

# 🗄️ Local Data Architecture

The application uses SQLite as its local persistence layer.

```text
┌─────────────────────┐
│     toolkit.db      │
└──────────┬──────────┘
           │
     ┌─────┼─────┬──────────────┐
     ▼     ▼     ▼              ▼
   Sites Equipment Projects Saved
```

### Main Entities

`Sites`

`EquipmentItems`

`Projects`

`SavedCalculations`

No external database server is required.

---

# 🔐 Offline by Design

This project intentionally avoids unnecessary external dependencies.

| Dependency        | Required |
| ----------------- | :------: |
| Internet          |     ❌    |
| Cloud             |     ❌    |
| Remote API        |     ❌    |
| External Database |     ❌    |
| AI Service        |     ❌    |
| Local SQLite      |     ✅    |
| Windows           |     ✅    |

> The engineer controls the data. The calculations remain local.

---

# 🚀 Getting Started

## Requirements

```text
Windows 10
Windows 11
.NET 10 SDK
```

---

## Clone

```bash
git clone <YOUR_REPOSITORY_URL>
cd TelecomEngineerToolkit
```

## Build

```bash
dotnet build
```

## Run

```bash
dotnet run --project src/Toolkit.UI
```

---

# 🧪 Validation

Engineering inputs are validated before calculations are performed.

Examples:

```text
Invalid Frequency
Invalid Distance
Negative Values
Invalid IPv4 Address
Invalid CIDR
Missing Required Parameters
```

The application reports invalid input directly through the user interface instead of silently producing unreliable results.

---

# 🗺️ Roadmap

### Engineering

* [ ] Advanced RF calculations
* [ ] Microwave rain attenuation
* [ ] Terrain profile analysis
* [ ] Extended fiber calculations
* [ ] Additional power-system calculations

### Data

* [ ] Site import/export
* [ ] Equipment import/export
* [ ] Advanced project organization
* [ ] PDF engineering reports

### Application

* [ ] Improved visualization
* [ ] Expanded unit testing
* [ ] Installer package
* [ ] MSIX deployment
* [ ] Inno Setup deployment

---

# 📌 Project Scope

Telecom Engineer Toolkit is intended to assist with:

> **Routine engineering calculations + preliminary planning + site documentation + equipment management + project records**

It does **not** attempt to replace specialized systems such as:

* OSS/BSS
* Enterprise NMS
* Spectrum Management Systems
* Professional RF Propagation Software
* Advanced Terrain Analysis Platforms
* Production Network Configuration Systems

---

# 🤝 Contributing

Contributions should preserve the modular architecture.

When adding a calculator:

```text
1. Put calculation logic in Engineering.Core
2. Keep UI logic inside the UI/Application layers
3. Follow MVVM
4. Add unit tests
5. Keep calculations deterministic
6. Avoid unnecessary external dependencies
```

---

# 📄 License

This project is currently intended for **personal and internal engineering use**.

No open-source license has been applied at this stage.

---

<p align="center">

## 📡 Telecom Engineer Toolkit

**Calculate. Plan. Document. Manage.**

Built as a practical engineering workspace for telecom professionals.

</p>

<p align="center">
  <sub>Active Development • Offline First • Modular Architecture</sub>
</p>
