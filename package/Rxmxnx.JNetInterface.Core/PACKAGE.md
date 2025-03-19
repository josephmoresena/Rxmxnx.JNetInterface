[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=bugs)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=coverage)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)

---

# Description

`Rxmxnx.JNetInterface.Core` is a package that provides essential base classes and abstractions for working with JNI in
.NET. It is designed for scenarios where direct dependency on a JVM is not required or desirable.

## Usage

This package is automatically included as a dependency of the main `Rxmxnx.JNetInterface` package. However, it can be
explicitly installed when a real JNI implementation is not needed. For example, it allows developers to write .NET JNI
code while keeping it isolated from unmanaged or native interop.

## API Access Restrictions

While `Rxmxnx.JNetInterface.Core` exposes the necessary APIs for developing JNI-based code in .NET, some APIs are
intentionally restricted to prevent unauthorized access to critical functionality. Currently, only
`Rxmxnx.JNetInterface`, which facilitates interoperability between .NET and the JVM, has access to these restricted
APIs.

## JNI Proxies

`Rxmxnx.JNetInterface.Core` includes specialized types that enable unit testing without requiring a JVM, making it
easier to develop and test JNI-based .NET applications in isolated environments.

---

# Documentation

API documentation can be found in the source code in documentation comments.

Learn more from [README on GitHub](https://github.com/josephmoresena/Rxmxnx.JNetInterface#readme).

---

# License

This project is licensed under the **MIT License**.