namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JConstructorDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	protected JConstructorDefinition(
#if !NET9_0_OR_GREATER
		params JArgumentMetadata[] metadata
#else
		JArgumentMetadata[] metadata
#endif
	) : this(metadata.AsReadOnlySpan()) { }

	/// <inheritdoc cref="JConstructorDefinition.New(JClassObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected JLocalObject New(JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.New(jClass, args.AsReadOnlySpan());
	/// <inheritdoc cref="JConstructorDefinition.New{TObject}(IEnvironment, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TObject New<TObject>(IEnvironment env,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	) where TObject : JLocalObject, IClassType<TObject>
		=> this.New<TObject>(env, args.AsReadOnlySpan());
	/// <inheritdoc cref="JConstructorDefinition.NewReflected(JConstructorObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected JLocalObject NewReflected(JConstructorObject jConstructor,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.NewReflected(jConstructor, args.AsReadOnlySpan());
	/// <inheritdoc cref="JConstructorDefinition.NewReflected{TObject}(JConstructorObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TObject NewReflected<TObject>(JConstructorObject jConstructor,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	) where TObject : JLocalObject, IClassType<TObject>
		=> this.NewReflected<TObject>(jConstructor, args.AsReadOnlySpan());
}