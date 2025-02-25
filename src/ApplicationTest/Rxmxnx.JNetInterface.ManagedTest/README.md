# JNetInterface Testing API

This document provides an overview of the testing APIs exposed through `EnvironmentProxy`, `ThreadProxy`, and
`VirtualMachineProxy` to facilitate unit testing with `JNetInterface`. These proxies allow for the simulation and
validation of Java Native Interface (JNI) interactions within a .NET environment.

## Overview

The provided tests cover various aspects of the JNI interactions, such as:

- Retrieving strings from Java objects.
- Fetching thread-related information.
- Printing runtime details.
- Manipulating fields in Java objects.
- Handling exceptions thrown from Java methods.
- Simulating different types of JNI references (local, global, weak, etc.).
- Managing native memory using fixed context adapters.
- Creating and handling proxy classes, proxy objects, global/weak proxy objects, exception proxies, and primitive
  wrapper proxies.
- Creating and managing array proxies (including multi-dimensional arrays and primitive arrays).
- Creating and managing string proxies.

Each test case demonstrates how `EnvironmentProxy`, `ThreadProxy`, and `VirtualMachineProxy` can be used to generate
real use cases, interact with JNI, and validate behavior by simulating exceptions, responses, JNI calls, and native
memory management in a controlled test environment.

## Test Cases

### 1. `GetHelloStringTest`

This test verifies that a `JHelloDotnetObject` instance can return a `JStringObject` containing the expected string
value.

- **Setup:**
    - `EnvironmentProxy` is mocked.
    - `JClassObject` instances are created for `java.lang.Class`, `com.rxmxnx.dotnet.test.HelloDotnet`, and
      `java.lang.String`.
    - A `JStringObject` is instantiated using `EnvironmentProxy.CreateString`.
    - `IManagedCallback` is used to interact with JNI.
- **Assertions:**
    - The method `callback.GetHelloString(jLocal)` returns the expected `JStringObject`.
    - `envProxy.Create` is invoked once with the correct string value.

### 2. `GetThreadIdTest`

This test ensures that the correct managed thread ID is retrieved when interacting with `JHelloDotnetObject`.

- **Setup:**
    - `EnvironmentProxy` is mocked and configured to return a `VirtualMachineProxy`.
    - `JClassObject` is initialized for `com.rxmxnx.dotnet.test.HelloDotnet`.
    - `IManagedCallback` is used for JNI interactions.
- **Assertions:**
    - The returned thread ID matches `Environment.CurrentManagedThreadId`.
    - `writer.WriteLine(jObjClass)` is invoked once.

### 3. `PrintRuntimeInformationTest`

This test verifies that runtime information of a `JHelloDotnetObject` and a `JStringObject` is correctly printed.

- **Setup:**
    - Mocked `EnvironmentProxy`, `TextWriter`, and JNI references.
    - Created `JClassObject` instances.
    - `JStringObject` created with a GUID-based string to generate a random test string, demonstrating controlled JNI
      interactions.
- **Assertions:**
    - `writer.WriteLine(jLocal)` is called once.
    - `writer.WriteLine(jString.Value)` is called once.

### 4. `ProcessFieldTest`

This test verifies that a string field (`s_field`) of a `JHelloDotnetObject` is properly processed and updated.

- **Setup:**
    - `EnvironmentProxy`, `TextWriter`, and `INativeMemoryAdapter` are mocked.
    - `JClassObject` instances are created.
    - `JStringObject` instances are initialized with transformed values.
    - Fixed native memory contexts are utilized for field processing.
- **Assertions:**
    - `GetField<JStringObject>` retrieves the expected field value.
    - `GetUtf8Sequence` is called to obtain the raw UTF-8 data.
    - `SetField` updates the field with the transformed value.

### 5. `ThrowTest`

This test ensures that an exception thrown from a Java method is properly handled and logged.

- **Setup:**
    - `EnvironmentProxy`, `ThreadProxy`, and `TextWriter` are mocked.
    - Different JNI references (`JGlobalRef`, `JWeakRef`) are created to demonstrate the handling of various reference
      types.
    - `JClassObject` instances for `java.lang.NullPointerException` and `com.rxmxnx.dotnet.test.HelloDotnet`.
    - A `ThrowableException` is created and assigned a stack trace.
- **Assertions:**
    - `CallMethod` throws an exception when called.
    - The thrown exception is captured and processed correctly.
    - The expected JNI calls (`CreateWeak`, `GetObjectClass`, `GetTypeMetadata`) are executed.
    - The internal `ThrowableException` object maintains a global reference, and when accessed, it generates a
      global-weak reference for further interactions.
    - The test calls `.ToString()` on the internal throwable object to validate its behavior.

### 6. `SumArrayTest`

This test ensures the correct behavior of the static sum array function, working with integer array proxies.

- **Setup:**
    - A one-dimensional integer array proxy is created.
    - The static method `SumArrayDefaultTest` is invoked.
- **Assertions:**
    - The sum of the array elements is correctly calculated.

### 7. `GetIntArrayArrayTest`

This test verifies the retrieval of a two-dimensional integer array proxy from JNI.

- **Setup:**
    - A multi-dimensional integer array proxy is created.
    - The static method `GetIntArrayArrayDefaultTest` is invoked.
- **Assertions:**
    - The returned array structure is correct and contains expected values.

### 8. `PrintClassTest`

This test checks that class-related information is correctly printed.

- **Setup:**
    - JNI class objects are created and used for logging.
    - The static method `PrintClassDefaultTest` is invoked.
- **Assertions:**
    - Class details are correctly output to the console/log.

### 9. `GetVoidClassTest`

This test ensures that void-class behavior is properly managed.

- **Setup:**
    - The void-class reference is retrieved.
    - The static method `GetVoidClassDefaultTest` is invoked.
- **Assertions:**
    - The void-class type is correctly identified and processed.

### 10. `GetPrimitiveClassesTest`

This test verifies the handling of primitive Java class objects within JNI.

- **Setup:**
    - Various primitive class references (`int`, `boolean`, `char`, etc.) are retrieved.
    - The static method `GetPrimitiveClassesDefaultTest` is invoked.
- **Assertions:**
    - The primitive class types match their expected JNI representations.

## Summary

These test cases demonstrate how `EnvironmentProxy`, `ThreadProxy`, and `VirtualMachineProxy` facilitate unit testing
within `JNetInterface`. By leveraging the exposed APIs, controlled JNI interactions can be simulated, including
exception
handling, object references, JNI method calls, and native memory management. This ensures the reliability and
correctness of JNI-based logic in .NET applications.
