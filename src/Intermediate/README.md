# Intermediate Libraries

To improve the maintainability of `JNetInterface`, intermediary projects were created to separate the functionality from
the final assemblies.

* `Base`: Contains the base structures and classes that are usable by all components of `JNetInterface`.
* `Primitives`: Contains structures known in Java as primitives.
* `Native`: Contains the classes and basic APIs for using JNI through `JNetInterface`.
* `Extensions`: Contains some extensions and defined classes that require both the Native and Primitives components,
  which complete the APIs for the use of JNI through `JNetInterface`. Here, some optional `Throwable` class types are
  also declared that can help when building applications.
* `Android`: Contains the Android API-level features that allow `JNetInterface` to run on the Android OS and enable
  efficient trimming for that platform.
* `Java`: Contains the Java SE version-specific features that allow `JNetInterface` to run with different JRE versions
  and enable efficient trimming when the target version is specified.
* `Trace`: Contains the implementation of the `JNetInterface` tracing feature. This feature is disabled by default and
  should only be enabled for debugging purposes.
* `Jni`: Contains the native types required to interoperate with JNI up to version 1.6.
* `ModernJni`: Extends `Jni` with native types required to interoperate with the latest JNI versions.
* `Common`: Contains the base managed resources used by the native `JNetInterface` implementation.
* `Interop`: Contains the native implementation of the core `JNetInterface` APIs using P/Invoke to access Java’s
  invocation and native interfaces.
* `Implementation`: Provides the standard native implementation of the `JNetInterface` APIs built on top of `Interop`,
  including support for Java’s Invocation APIs.
* `Mobile`: Provides the standard native implementation of the `JNetInterface` APIs built on top of `Java.Interop`,
  used on .NET for Android.