# Intermediate Libraries

To improve the maintainability of `JNetInterface`, intermediary projects were created to separate the functionality from the final assemblies.
* `Base`: Contains the base structures and classes that are usable by all components of `JNetInterface`.
* `Primitives`: Contains structures known in Java as primitives.
* `Native`: Contains the classes and basic APIs for using JNI through `JNetInterface`.
* `Extensions`: Contains some extensions and defined classes that require both the Native and Primitives components, which complete the APIs for the use of JNI through `JNetInterface`. Here, some optional `Throwable` class types are also declared that can help when building applications.
* `Implementation`: This is the standard native implementation of the `JNetInterface` APIs using `PInvoke` to access Java's invocation and native interfaces.