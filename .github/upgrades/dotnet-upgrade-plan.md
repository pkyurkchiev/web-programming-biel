# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade PM.WebServices\PM.WebServices.csproj

## Settings

This section contains settings and data used by execution steps.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                               | Current Version | New Version | Description                                   |
|:-------------------------------------------|:---------------:|:-----------:|:----------------------------------------------|
| Converto                                   | 6.0.0           | 6.0.0       | No update needed                              |
| Microsoft.EntityFrameworkCore.SqlServer    | 7.0.13          | 10.0.0      | Recommended for .NET 10.0                     |
| Newtonsoft.Json                            | 13.0.3          | 13.0.4      | Recommended for .NET 10.0                     |
| Swashbuckle.AspNetCore                     | 6.5.0           | 6.5.0       | No update needed                              |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### PM.WebServices\PM.WebServices.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`

NuGet packages changes:
  - Microsoft.EntityFrameworkCore.SqlServer should be updated from `7.0.13` to `10.0.0` (*recommended for .NET 10.0*)
  - Newtonsoft.Json should be updated from `13.0.3` to `13.0.4` (*recommended for .NET 10.0*)
