# .NET 10 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10 upgrade.
3. Upgrade BP.Infrastructure\BP.Infrastructure.csproj
4. Upgrade BP.Domain\BP.Domain.csproj
5. Upgrade BP.Repository\BP.Repository.csproj
6. Upgrade BP.ApplicationServices\BP.ApplicationServices.csproj
7. Upgrade BP.WebAPIServices\BP.WebAPIServices.csproj

## Settings

This section contains settings and data used by execution steps.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                      | Current Version | New Version       | Description                                   |
|:--------------------------------------------------|:---------------:|:-----------------:|:----------------------------------------------|
| Microsoft.AspNetCore.Mvc.NewtonsoftJson           | 7.0.13          | 10.0.0            | Recommended for .NET 10                       |
| Microsoft.EntityFrameworkCore.SqlServer           | 7.0.13          | 10.0.0            | Recommended for .NET 10                       |
| Microsoft.EntityFrameworkCore.Tools               | 7.0.13          | 10.0.0            | Recommended for .NET 10                       |
| Microsoft.Extensions.Logging.Debug                | 7.0.0           | 10.0.0            | Recommended for .NET 10                       |
| Microsoft.VisualStudio.Web.CodeGeneration.Design  | 7.0.11          | 10.0.0-rc.1.25458.5 | Recommended for .NET 10                     |
| Newtonsoft.Json                                   | 13.0.3          | 13.0.4            | Recommended for .NET 10                       |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### BP.Infrastructure\BP.Infrastructure.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`

#### BP.Domain\BP.Domain.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`

#### BP.Repository\BP.Repository.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`

#### BP.ApplicationServices\BP.ApplicationServices.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`

#### BP.WebAPIServices\BP.WebAPIServices.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Mvc.NewtonsoftJson should be updated from `7.0.13` to `10.0.0` (*recommended for .NET 10*)
  - Microsoft.EntityFrameworkCore.SqlServer should be updated from `7.0.13` to `10.0.0` (*recommended for .NET 10*)
  - Microsoft.EntityFrameworkCore.Tools should be updated from `7.0.13` to `10.0.0` (*recommended for .NET 10*)
  - Microsoft.Extensions.Logging.Debug should be updated from `7.0.0` to `10.0.0` (*recommended for .NET 10*)
  - Microsoft.VisualStudio.Web.CodeGeneration.Design should be updated from `7.0.11` to `10.0.0-rc.1.25458.5` (*recommended for .NET 10*)
  - Newtonsoft.Json should be updated from `13.0.3` to `13.0.4` (*recommended for .NET 10*)
