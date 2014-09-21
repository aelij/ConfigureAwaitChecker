ReSharper Plugins
=================

ConfigureAwait Checker
----------------------

Validates that tasks are awaited using ConfigureAwait().

Library code should call ConfigureAwait ubiquitously. Always specifying ConfigureAwait makes it clearer how the continuation is invoked and avoids synchonization bugs.
