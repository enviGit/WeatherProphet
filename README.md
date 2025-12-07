# Weather Prophet (Reborn)

<img width="793" height="640" alt="weatherProphet" src="https://github.com/user-attachments/assets/8c2bb523-dcdf-4a1f-83ce-bbfe62d20644" />

**Weather Prophet** is a modern, responsive weather dashboard built with **.NET 10** and **WPF**. It demonstrates a clean migration from legacy code to a professional **MVVM architecture**, featuring dynamic localization, dual-theme support (Dark/Light), and secure configuration management.

## 📥 Download & Demo

Want to see the dashboard in action without compiling?
You can download the latest stable executable from the releases page.

[**👉 Download Latest Release (v1.0.0)**](https://github.com/enviGit/WeatherProphet/releases/latest)

---

## 🚀 Key Features

### 🎨 Modern UI & UX
* **Dual Theme Engine:** Seamless switching between **"Deep Dark"** and **"Porcelain Light"** modes. The entire UI (borders, inputs, icons) adapts instantly.
* **Glassmorphism:** Custom window chrome with semi-transparent backgrounds and gradient borders.
* **Responsive Layout:** Grid-based layout that scales elegantly with window resizing.

### 🏗️ Architecture (MVVM)
* **Separation of Concerns:** Strict division between Logic (`Services`), Data (`Models`), and UI (`ViewModels`).
* **CommunityToolkit.Mvvm:** Utilizes RelayCommands and ObservableProperties for clean, boilerplate-free code.
* **Service Layer:** * `WeatherService`: Handles HTTP requests using `System.Net.Http.Json`.
    * `TranslationService`: Centralized localization logic.

### 🔒 Security & Performance
* **Embedded Configuration:** The API Key is loaded from an **Embedded Resource** stream rather than a loose file in the output directory, offering basic obfuscation.
* **Efficient Parsing:** Migrated from `Newtonsoft.Json` to the high-performance `System.Text.Json`.

---

## 🛠 Technical Stack

* **Framework:** .NET 10 (Desktop)
* **UI Framework:** WPF (Windows Presentation Foundation)
* **Design Pattern:** MVVM (Model-View-ViewModel)
* **Data Source:** OpenWeatherMap API
* **Tools:** Visual Studio 2026, CommunityToolkit.Mvvm

---

## 📄 License
This project is open-source and available under the [MIT License](LICENSE).
