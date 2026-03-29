Simple Calculator (WinForms, .NET 8)

![CI](https://github.com/umurugesan1234/Calculator/actions/workflows/ci.yml/badge.svg)

This is a minimal Windows Forms calculator app targeting .NET 8.

How to build

Open a PowerShell terminal and run:

```powershell
cd 'C:\Muru\Office\Study\Working\Calculator'
dotnet build
```

To run the app (this will open a GUI):

```powershell
cd 'C:\Muru\Office\Study\Working\Calculator'
dotnet run
```

Files created:
- `Calculator.csproj` - project file (net8.0-windows, UseWindowsForms=true)
- `Program.cs` - application entry point
- `MainForm.cs` - main WinForms UI and logic
