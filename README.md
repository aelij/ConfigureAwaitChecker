ConfigureAwait Checker
======================

ReSharper extension and Roslyn analyzer that check for tasks are not awaited using `ConfigureAwait`.

Library code should use `ConfigureAwait(false)` with every await. Always specifying `ConfigureAwait` makes it clearer how the continuation is invoked and avoids synchonization bugs.
