# LogicBuilder.App.Maui.Utils

A reusable library containing the methods and classes used by a .NET MAUI client application that runs a Logic Builder workflow. It connects the Logic Builder rules engine (`LogicBuilder.RulesDirector`) to the MAUI app's screens and navigation.

## What's included

- **`FlowManager` / `IFlowManager`** – Starts and advances a workflow (`Start`, `Next`, `NewFlowStart`), and handles `FlowComplete`, `Terminate` and backup of the current business data. Exposes the `Progress`, `Director`, `FlowDataCache`, `FlowActivity`, `RulesCache` and `ServiceProvider` for the running flow.
- **`Director`** – Extends `AppDirectorBase` and delegates to the flow manager. Its `FlowState` property saves and restores the driver, selection and module stacks so a flow can be resumed.
- **`DialogFunctions` / `IDialogFunctions`** – Functions callable from Logic Builder rules to display forms (edit form, detail form, edit collection, etc.). They are annotated with Logic Builder attributes such as `AlsoKnownAs`, `FunctionGroup` and `Comments`.
- **`Actions` / `IActions`** – Rule-callable actions, e.g. `UpdateNavigationBar` (known to rules as `SetupNavigationMenu`).
- **`FlowFactory` / `IFlowFactory`** – Factories for creating flow objects.
- **`ActionUtilities` / `DialogFunctionUtilities`** – Helpers supporting the actions and dialog functions.

## Usage

Register the services in the MAUI app's startup:

```csharp
builder.Services.AddMauiUtilsServices();
```

This registers `IFlowManager`, `IFlowDataCache`, `IScreenData`, `Progress`, `IDialogFunctions` and `IActions`.

## Projects

- `LogicBuilder.App.Maui.Utils` – the library (targets .NET Standard 2.0).
- `LogicBuilder.App.Maui.Utils.Tests` – xUnit tests that exercise the flow manager (starting new flows, terminating flows) using test implementations of the logger and services.