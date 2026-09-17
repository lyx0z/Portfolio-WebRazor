# Portfolio

Personal site of [lyx0z](https://github.com/lyx0z). Built with Blazor WebAssembly on .NET 10.

## Structure

| Folder | What it is |
|---|---|
| `Portfolio.Web` | The website. Home page, about section and the project pages. |
| `GameOfLife.Core` | Conway's Game of Life engine. Pure C#, no UI, no dependencies. |
| `GameOfLife.Test.Units` | NUnit tests for the engine rules. |

## Run it

```
dotnet run --project Portfolio.Web
```

Then open the URL that gets printed, usually `https://localhost:7xxx`.

## Test it

```
dotnet test
```
