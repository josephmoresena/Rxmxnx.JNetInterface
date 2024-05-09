`Rxmxnx.JNetInterface.Core` is the package containing only the base clasess and abstractions which may be useful for application in places where a dependency on a JVM is not desirable.

## Usage

This package is included automatically as a dependency of the main `Rxmxnx.JNetInterface` package. Usually, the core package is only explicitly installed in places where it is undesirable to use a real JNI implementation. 
For example, it can be installed to use to create a .NET JNI code but isolated from any unmanaged or native interop.

## API Access Restrictions
`Rxmxnx.JNetInterface.Core` publicly exposes the APIs necessary to develop JNI code using .NET, however, some of these APIs are restricted to prevent unauthorized access to sensitive and critical parts.
Currently only `Rxmxnx.JNetInterface`, which implements interoperability between .NET and the JVM, has access to these restricted APIs.