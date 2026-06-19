# Feature Switches

`Rxmxnx.JNetInterface.Core`, `Rxmxnx.JNetInterface`, and `Rxmxnx.JNetInterface.Mobile` expose several feature switches
that allow applications to optimize runtime performance, reduce NativeAOT binary size, improve assembly trimming, or
customize the runtime behavior.

Depending on the switch, it can be configured through:

* MSBuild properties
* `runtimeconfig.json`
* `AppContext.SetSwitch()`

**Note**: Only switches that support runtime configuration can be modified through `AppContext.SetSwitch()`.

---

# Core feature switches

The following feature switches are available in `Rxmxnx.JNetInterface.Core`.

## Metadata and runtime optimization

| Switch                                           | Description                                                                                                                    |
|--------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| `JNetInterface.DisableMetadataValidation`        | Disables runtime metadata validation. Recommended for production environments to reduce runtime overhead.                      |
| `JNetInterface.DisableTypeMetadataToString`      | Simplifies `JDataTypeMetadata.ToString()`. This reduces execution cost and helps reduce NativeAOT binary size.                 |
| `JNetInterface.DisableJaggedArrayAutoGeneration` | Disables reflection-based generation of nested array metadata. This avoids runtime reflection and improves NativeAOT trimming. |

### Notes

* The current state of these switches is exposed through static properties on `IVirtualMachine`.
* All switches except `DisableJaggedArrayAutoGeneration` may be configured at runtime through `AppContext`.

---

# Platform feature switches

The following feature switches are provided by `Rxmxnx.JNetInterface` and `Rxmxnx.JNetInterface.Mobile`, unless
otherwise specified.

## Debugging

| Switch                      | Description                                                                 |
|-----------------------------|-----------------------------------------------------------------------------|
| `JNetInterface.EnableTrace` | Enables the internal JNI trace. Intended for debugging and troubleshooting. |

---

## Automatic type registration

By default, several built-in Java types are automatically registered. Disabling unused registrations can improve
assembly trimming.

| Switch                                                  | Description                                                              |
|---------------------------------------------------------|--------------------------------------------------------------------------|
| `JNetInterface.DisableBuiltInThrowableAutoRegistration` | Disables automatic registration of built-in `java.lang.Throwable` types. |
| `JNetInterface.DisableReflectionAutoRegistration`       | Disables automatic registration of `java.lang.reflect` types.            |
| `JNetInterface.DisableNioAutoRegistration`              | Disables automatic registration of `java.nio` types.                     |

---

## Main class registration

### Primitive wrapper classes

Wrapper classes are registered as main classes by default.

Disabling any of the following switches may affect Java primitive boxing.

| Switch                            | Java type             |
|-----------------------------------|-----------------------|
| `DisableBooleanObjectMainClass`   | `java.lang.Boolean`   |
| `DisableByteObjectMainClass`      | `java.lang.Byte`      |
| `DisableCharacterObjectMainClass` | `java.lang.Character` |
| `DisableDoubleObjectMainClass`    | `java.lang.Double`    |
| `DisableFloatObjectMainClass`     | `java.lang.Float`     |
| `DisableIntegerObjectMainClass`   | `java.lang.Integer`   |
| `DisableLongObjectMainClass`      | `java.lang.Long`      |
| `DisableShortObjectMainClass`     | `java.lang.Short`     |

### Primitive types

| Switch                                      | Description                                                                                                                                                             |
|---------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `JNetInterface.DisablePrimitiveMainClasses` | Disables registration of Java primitive classes (`void`, `boolean`, `byte`, `char`, `double`, `float`, `int`, `long`, `short`). This may affect Java member reflection. |
| `JNetInterface.EnableVoidObjectMainClass`   | Registers `java.lang.Void` as a main class.                                                                                                                             |

---

## Runtime optimization

| Switch                                     | Description                                                                                                                                                       |
|--------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `JNetInterface.EnableFinalUserTypeRuntime` | Assumes that types whose metadata is marked as final do not require `GetObjectClass()`. This reduces JNI calls and can significantly improve runtime performance. |

---

## Android runtime switches

These switches are supported by both `Rxmxnx.JNetInterface` and `Rxmxnx.JNetInterface.Mobile`.

Each switch fixes the maximum supported Android API level, allowing unsupported platform-specific code to be removed
during trimming.

| Feature switch                                  | Android API |
|-------------------------------------------------|------------:|
| `JNetInterface.FixedRuntime.Dalvik`             |           1 |
| `JNetInterface.FixedRuntime.PetitFour`          |           2 |
| `JNetInterface.FixedRuntime.Cupcake`            |           3 |
| `JNetInterface.FixedRuntime.Donut`              |           4 |
| `JNetInterface.FixedRuntime.Eclair`             |           5 |
| `JNetInterface.FixedRuntime.Eclair6`            |           6 |
| `JNetInterface.FixedRuntime.Eclair7`            |           7 |
| `JNetInterface.FixedRuntime.Froyo`              |           8 |
| `JNetInterface.FixedRuntime.Gingerbread`        |           9 |
| `JNetInterface.FixedRuntime.Gingerbread10`      |          10 |
| `JNetInterface.FixedRuntime.Honeycomb`          |          11 |
| `JNetInterface.FixedRuntime.Honeycomb12`        |          12 |
| `JNetInterface.FixedRuntime.Honeycomb13`        |          13 |
| `JNetInterface.FixedRuntime.IceCreamSandwich`   |          14 |
| `JNetInterface.FixedRuntime.IceCreamSandwich15` |          15 |
| `JNetInterface.FixedRuntime.JellyBean`          |          16 |
| `JNetInterface.FixedRuntime.JellyBean17`        |          17 |
| `JNetInterface.FixedRuntime.JellyBean18`        |          18 |
| `JNetInterface.FixedRuntime.KitKat`             |          19 |
| `JNetInterface.FixedRuntime.KitKat20`           |          20 |
| `JNetInterface.FixedRuntime.Lollipop`           |          21 |
| `JNetInterface.FixedRuntime.Lollipop22`         |          22 |
| `JNetInterface.FixedRuntime.Marshmallow`        |          23 |
| `JNetInterface.FixedRuntime.Nougat`             |          24 |
| `JNetInterface.FixedRuntime.Nougat25`           |          25 |
| `JNetInterface.FixedRuntime.Oreo`               |          26 |
| `JNetInterface.FixedRuntime.Oreo27`             |          27 |
| `JNetInterface.FixedRuntime.Pie`                |          28 |
| `JNetInterface.FixedRuntime.Android10`          |          29 |
| `JNetInterface.FixedRuntime.Android11`          |          30 |
| `JNetInterface.FixedRuntime.Android12`          |          31 |
| `JNetInterface.FixedRuntime.Android12L`         |          32 |
| `JNetInterface.FixedRuntime.Android13`          |          33 |
| `JNetInterface.FixedRuntime.Android14`          |          34 |
| `JNetInterface.FixedRuntime.Android15`          |          35 |
| `JNetInterface.FixedRuntime.Android16`          |          36 |
| `JNetInterface.FixedRuntime.Android17`          |          37 |

### Notes

* Using one of these switches on a device with a lower API level throws a runtime exception.
* On `Rxmxnx.JNetInterface`, these switches are only valid on Android (`linux-bionic`).
* They improve assembly trimming by removing unsupported Android platform types.
* `JNetInterface.FixedRuntime.Android` is exclusive to `Rxmxnx.JNetInterface` and fixes the maximum JNI version to *
  *0x00010006**.

---

## Java SE runtime switches

These switches are available only in `Rxmxnx.JNetInterface`.

`JNetInterface.FixedRuntime.Standard` configures the runtime as a Java SE platform, allowing Android-specific types to
be removed during trimming.

### Supported Java versions

| Feature switch                    | Java version | JNI version  |
|-----------------------------------|--------------|--------------|
| `JNetInterface.FixedRuntime.SEd2` | Java SE 1.2  | `0x00010002` |
| `JNetInterface.FixedRuntime.SEd3` | Java SE 1.3  | `0x00010002` |
| `JNetInterface.FixedRuntime.SEd4` | Java SE 1.4  | `0x00010004` |
| `JNetInterface.FixedRuntime.J5`   | Java SE 5    | `0x00010004` |
| `JNetInterface.FixedRuntime.J6`   | Java SE 6    | `0x00010006` |
| `JNetInterface.FixedRuntime.J7`   | Java SE 7    | `0x00010006` |
| `JNetInterface.FixedRuntime.J8`   | Java SE 8    | `0x00010008` |
| `JNetInterface.FixedRuntime.J9`   | Java SE 9    | `0x00090000` |
| `JNetInterface.FixedRuntime.J10`  | Java SE 10   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J11`  | Java SE 11   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J12`  | Java SE 12   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J13`  | Java SE 13   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J14`  | Java SE 14   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J15`  | Java SE 15   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J16`  | Java SE 16   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J17`  | Java SE 17   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J18`  | Java SE 18   | `0x000A0000` |
| `JNetInterface.FixedRuntime.J19`  | Java SE 19   | `0x00130000` |
| `JNetInterface.FixedRuntime.J20`  | Java SE 20   | `0x00140000` |
| `JNetInterface.FixedRuntime.J21`  | Java SE 21   | `0x00150000` |
| `JNetInterface.FixedRuntime.J22`  | Java SE 22   | `0x00150000` |
| `JNetInterface.FixedRuntime.J23`  | Java SE 23   | `0x00150000` |
| `JNetInterface.FixedRuntime.J24`  | Java SE 24   | `0x00150000` |
| `JNetInterface.FixedRuntime.J25`  | Java SE 25   | `0x00150000` |
| `JNetInterface.FixedRuntime.J26`  | Java SE 26   | `0x00150000` |

### Notes

* These switches are exclusive to `Rxmxnx.JNetInterface` and have no effect when used with
  `Rxmxnx.JNetInterface.Mobile`.
* Using any of these switches from an Android process (`linux-bionic` zygote) throws a runtime exception.
* The selected switch also fixes the maximum supported JNI version shown in the table above.
* Limiting the maximum supported Java version improves assembly trimming by removing incompatible platform-specific
  types.

