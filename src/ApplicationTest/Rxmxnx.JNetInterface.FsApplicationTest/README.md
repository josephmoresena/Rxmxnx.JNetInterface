# F# Support

To support the use of `JNetInterface` in F#, it is necessary to explicitly use some constructors and methods that are 
not usually required in C# code.

## Primitives
In C#, `JNetInterface` uses operators to convert from one primitive type to another. However, this functionality must 
be done explicitly in F# using constructors.

```fsharp
let booleanValue = JBoolean true
let byteValue = JByte -2y
let charValue = JChar '\n'
let doubleValue = JDouble 3.14159265359
let floatValue = JFloat 2.71828f
let intValue = JInt 486
let longValue = JLong 3000000000L
let shortValue = JShort 1024s
```

## Reference Types
To map reference types from Java through `JNetInterface`, it is necessary to create classes (according to the rules 
established for each case) that implement the indicated interfaces according to the reference type to be mapped.

Unlike C#, F# does not support virtual implementation of static methods in interfaces, so it may be necessary to 
implement homonymous methods according to the `JNetInterface` hierarchy.

### Extensible class (`java.lang.Package`)
```fsharp
type JPackageObject =
    inherit JLocalObject

    static let typeMetadata =
        JLocalObject.TypeMetadataBuilder<JPackageObject>
            .Create("java/lang/Package"B)
            .Build()

    new(initializer: IReferenceType.ClassInitializer) = { inherit JLocalObject(initializer) }
    new(initializer: IReferenceType.GlobalInitializer) = { inherit JLocalObject(initializer) }
    new(initializer: IReferenceType.ObjectInitializer) = { inherit JLocalObject(initializer) }

    interface IDataType<JPackageObject> with
        static member get_Metadata() = typeMetadata

    interface IReferenceType<JPackageObject> with
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JPackageObject(initializer)

    interface IClassType<JPackageObject> with
        static member get_Metadata() = typeMetadata
        static member Create(initializer: IReferenceType.ClassInitializer) = new JPackageObject(initializer)
        static member Create(initializer: IReferenceType.GlobalInitializer) = new JPackageObject(initializer)
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JPackageObject(initializer)

    static member GetPackage(env: IEnvironment, packageName: String) : JPackageObject =
        GetPackageDefinition.GetPackage(env, packageName)

and private GetPackageDefinition private () =
    inherit JFunctionDefinition<JPackageObject>("getPackage"B, JArgumentMetadata.Get<JStringObject>())

    static member val private Instance = GetPackageDefinition() with get

    member this.GetPackage(packageName: JStringObject) : JPackageObject =
        let env = packageName.Environment
        let args = this.CreateArgumentsArray()
        let packageClass = JClassObject.GetClass<JPackageObject>(env)
        args.[0] <- packageName
        this.StaticInvoke(packageClass, args)

    static member GetPackage(env: IEnvironment, packageName: String) : JPackageObject =
        use jString = JStringObject.Create(env, packageName)
        env.WithFrame(3, jString, GetPackageDefinition.Instance.GetPackage)
```

### Non-instantiable class (`java.lang.Math`):
```fsharp
[<Sealed>]
type JMathObject private () =
    inherit JLocalObject.Uninstantiable<JMathObject>()

    static let typeMetadata =
        JLocalObject.TypeMetadataBuilder<JMathObject>.Create("java/lang/Math"B).Build()

    static let eDef = JFieldDefinition<JDouble>("E"B)
    static let piDef = JFieldDefinition<JDouble>("PI"B)
    static let absDef = MathFuncDefinition<JDouble, JDouble>("abs"B)
    static let atan2Def = MathFuncDefinition<JDouble, JDouble, JDouble>("atan2"B)

    static member GetE(env: IEnvironment) : JDouble = JMathObject.GetField(env, eDef)
    static member GetPi(env: IEnvironment) : JDouble = JMathObject.GetField(env, piDef)
    static member Abs(env: IEnvironment, value: JDouble) : JDouble = absDef.Invoke(env, value)
    static member Atan2(env: IEnvironment, y: JDouble, x: JDouble) : JDouble = atan2Def.Invoke(env, y, x)

    interface IDataType<JMathObject> with
        static member get_Metadata() = typeMetadata

    interface IReferenceType<JMathObject> with
        static member Create(initializer: IReferenceType.ObjectInitializer) =
            IUninstantiableType.ThrowInstantiation<JMathObject>()

    interface IUninstantiableType<JMathObject> with
        static member get_Metadata() = typeMetadata

        static member Create(initializer: IReferenceType.ClassInitializer) =
            IUninstantiableType.ThrowInstantiation<JMathObject>()

        static member Create(initializer: IReferenceType.GlobalInitializer) =
            IUninstantiableType.ThrowInstantiation<JMathObject>()

        static member Create(initializer: IReferenceType.ObjectInitializer) =
            IUninstantiableType.ThrowInstantiation<JMathObject>()

    static member private GetField<'T
        when 'T: unmanaged and 'T :> IPrimitiveType<'T> and 'T: (new: unit -> 'T) and 'T :> ValueType>
        (
            env: IEnvironment,
            field: JFieldDefinition<'T>
        ) : 'T =
        let state = { Environment = env; Def = field }
        env.WithFrame(2, state, JMathObject.GetField<'T>)

    static member private GetField<'T
        when 'T: unmanaged and 'T :> IPrimitiveType<'T> and 'T: (new: unit -> 'T) and 'T :> ValueType>
        (state: FieldState<'T>)
        : 'T =
        let mathClass = JClassObject.GetClass<JMathObject>(state.Environment)
        state.Def.StaticGet(mathClass)

and [<Struct>] FieldState<'T
    when 'T: struct and 'T :> IPrimitiveType<'T> and 'T: unmanaged and 'T: (new: unit -> 'T) and 'T :> ValueType> =
    { Environment: IEnvironment
      Def: JFieldDefinition<'T> }

and private MathFuncDefinition<'TF, 'TX when 'TF :> IDataType<'TF> and 'TX :> IDataType<'TX>>
    (funcName: ReadOnlySpan<byte>) =
    inherit JFunctionDefinition<'TF>(funcName, JArgumentMetadata.Get<'TX>())

    member this.Invoke(env: IEnvironment, x: 'TX) : 'TF =
        let state = { Environment = env; Def = this; X = x }
        env.WithFrame(2, state, MathFuncDefinition<'TF, 'TX>.Invoke)

    static member Invoke(state: State<'TF, 'TX>) : 'TF =
        let mathClass = JClassObject.GetClass<JMathObject>(state.Environment)
        let args = state.Def.CreateArgumentsArray()
        args.[0] <- state.X :> IObject
        state.Def.StaticInvoke(mathClass, args)

and [<Struct>] private State<'TF, 'TX when 'TF :> IDataType<'TF> and 'TX :> IDataType<'TX>> =
    { Environment: IEnvironment
      Def: MathFuncDefinition<'TF, 'TX>
      X: 'TX }

and private MathFuncDefinition<'TF, 'TX, 'TY
    when 'TF :> IDataType<'TF> and 'TX :> IDataType<'TX> and 'TY :> IDataType<'TY>>(funcName: ReadOnlySpan<byte>) =
    inherit JFunctionDefinition<'TF>(funcName, JArgumentMetadata.Get<'TX>(), JArgumentMetadata.Get<'TY>())

    member this.Invoke(env: IEnvironment, x: 'TX, y: 'TY) : 'TF =
        let state =
            { Environment = env
              Def = this
              X = x
              Y = y }

        env.WithFrame(2, state, MathFuncDefinition<'TF, 'TX, 'TY>.Invoke)

    static member Invoke(state: State<'TF, 'TX, 'TY>) : 'TF =
        let mathClass = JClassObject.GetClass<JMathObject>(state.Environment)
        let args = state.Def.CreateArgumentsArray()
        args.[0] <- state.X :> IObject
        args.[1] <- state.Y :> IObject
        state.Def.StaticInvoke(mathClass, args)

and [<Struct>] private State<'TF, 'TX, 'TY
    when 'TF :> IDataType<'TF> and 'TX :> IDataType<'TX> and 'TY :> IDataType<'TY>> =
    { Environment: IEnvironment
      Def: MathFuncDefinition<'TF, 'TX, 'TY>
      X: 'TX
      Y: 'TY }
```

### Throwable class (`java.lang.CloneNotSupportedException`):
```fsharp
type JCloneNotSupportedExceptionObject =
    inherit JExceptionObject
    static let typeMetadata =
        JThrowableObject.TypeMetadataBuilder<JExceptionObject>
            .Create<JCloneNotSupportedExceptionObject>("java/lang/CloneNotSupportedException"B)
            .Build()

    new(initializer: IReferenceType.ClassInitializer) = { inherit JExceptionObject(initializer) }
    new(initializer: IReferenceType.GlobalInitializer) = { inherit JExceptionObject(initializer) }
    new(initializer: IReferenceType.ObjectInitializer) = { inherit JExceptionObject(initializer) }

    interface IDataType<JCloneNotSupportedExceptionObject> with
        static member get_Metadata() = typeMetadata

    interface IReferenceType<JCloneNotSupportedExceptionObject> with
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JCloneNotSupportedExceptionObject(initializer)
        
    interface IClassType<JCloneNotSupportedExceptionObject> with
        static member get_Metadata() = typeMetadata
        static member Create(initializer: IReferenceType.ClassInitializer) = new JCloneNotSupportedExceptionObject(initializer)
        static member Create(initializer: IReferenceType.GlobalInitializer) = new JCloneNotSupportedExceptionObject(initializer)
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JCloneNotSupportedExceptionObject(initializer)

    interface IThrowableType<JCloneNotSupportedExceptionObject> with
        static member get_Metadata() = typeMetadata
```

### Enum class (`java.lang.Thread.State`):
Note that in this case, the enum values were not declared. However, this functionality is supported in F#.

```fsharp
[<Sealed>]
type JThreadStateObject =
    inherit JEnumObject<JThreadStateObject>

    static let typeMetadata =
        JEnumObject.TypeMetadataBuilder<JThreadStateObject>
            .Create("java/lang/Thread$State"B)
            .Build()

    new(initializer: IReferenceType.ClassInitializer) = { inherit JEnumObject<JThreadStateObject>(initializer) }
    new(initializer: IReferenceType.GlobalInitializer) = { inherit JEnumObject<JThreadStateObject>(initializer) }
    new(initializer: IReferenceType.ObjectInitializer) = { inherit JEnumObject<JThreadStateObject>(initializer) }

    interface IDataType<JThreadStateObject> with
        static member get_Metadata() = typeMetadata

    interface IReferenceType<JThreadStateObject> with
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JThreadStateObject(initializer)

    interface IEnumType<JThreadStateObject> with
        static member get_Metadata() = typeMetadata
        static member Create(initializer: IReferenceType.ClassInitializer) = new JThreadStateObject(initializer)
        static member Create(initializer: IReferenceType.GlobalInitializer) = new JThreadStateObject(initializer)
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JThreadStateObject(initializer)
```

### Interface type (`java.lang.AutoCloseable`):
```fsharp
[<Sealed>]
type JAutoCloseableObject =
    inherit JInterfaceObject<JAutoCloseableObject>

    static let typeMetadata =
        JInterfaceObject.TypeMetadataBuilder<JAutoCloseableObject>
            .Create("java/lang/AutoCloseable"B)
            .Build()

    new(initializer: IReferenceType.ObjectInitializer) = { inherit JInterfaceObject<JAutoCloseableObject>(initializer) }

    interface IDataType<JAutoCloseableObject> with
        static member get_Metadata() = typeMetadata

    interface IReferenceType<JAutoCloseableObject> with
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JAutoCloseableObject(initializer)

    interface IInterfaceType<JAutoCloseableObject> with
        static member get_Metadata() = typeMetadata
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JAutoCloseableObject(initializer)
```

### Annotation type (`java.lang.Deprecated`):
```fsharp
[<Sealed>]
type JDeprecatedObject =
    inherit JAnnotationObject<JDeprecatedObject>

    static let typeMetadata =
        JInterfaceObject.TypeMetadataBuilder<JDeprecatedObject>
            .Create("java/lang/Deprecated"B)
            .Build()

    new(initializer: IReferenceType.ObjectInitializer) = { inherit JAnnotationObject<JDeprecatedObject>(initializer) }

    interface IDataType<JDeprecatedObject> with
        static member get_Metadata() = typeMetadata

    interface IReferenceType<JDeprecatedObject> with
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JDeprecatedObject(initializer)

    interface IInterfaceType<JDeprecatedObject> with
        static member get_Metadata() = typeMetadata
        static member Create(initializer: IReferenceType.ObjectInitializer) = new JDeprecatedObject(initializer)
```