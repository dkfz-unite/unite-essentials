# unite-essentials

Shared architecture, development workflows, and agent instructions:
[UNITE project documentation](https://github.com/dkfz-unite/.github/blob/main/README.md).

Shared `Unite.Essentials` NuGet package with general extensions and utilities.
It is a library, not a running service.

The current project includes enum aliases/JSON conversion and extensions for
collections, query expressions, entities, exceptions, and date/time values.
`Unite.Data.Context` and `Unite.Indices.Search` consume it; services can also
reference it directly. The plain `Unite.Data` entity project does not currently
reference this package.

TSV model parsing/writing belongs to the separate `Unite.Essentials.Tsv`
package from `unite-essentials-tsv`. The owner plans to extract duplicated
`JsonHttpClient` code into `unite-essentials-http`; that extraction is not a
current responsibility of this project.
