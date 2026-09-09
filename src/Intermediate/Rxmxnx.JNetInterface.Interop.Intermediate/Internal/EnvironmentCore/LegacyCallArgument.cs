using StringBuilder = System.Text.StringBuilder;

namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class EnvironmentCore
{
#if !NET9_0_OR_GREATER
	private readonly struct LegacyCallArgument : ICallArgument
	{
		private readonly ReadOnlyValPtr<IObject?> _ptr;
		private readonly Int32 _length;

		private ReadOnlySpan<IObject?> Values
			=> this._ptr.GetUnsafeFixedContext(this._length, FixedPointerValue.UnsafeDisposable).Values;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="args">A read-only span of <see cref="IObject"/> instances.</param>
		// ReSharper disable once ConvertToPrimaryConstructor
		public LegacyCallArgument(ReadOnlyFixedContextValue<IObject?> args)
		{
			this._ptr = args.ValuePointer;
			this._length = args.Values.Length;
		}
#else
	private readonly ref struct LegacyCallArgument : ICallArgument
	{
		private ReadOnlySpan<IObject?> Values { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="args">A read-only span of <see cref="IObject"/> instances.</param>
		// ReSharper disable once ConvertToPrimaryConstructor
		public LegacyCallArgument(ReadOnlySpan<IObject?> args) => this.Values = args;

		String ICallArgument.ToTraceText() => this.ToString();
#endif
		void ICallArgument.Configure<TSlot>(TSlot slot, JCallDefinition callDefinition)
		{
			ReadOnlySpan<IObject?> values = this.Values;
			for (Int32 index = 0; index < values.Length; index++)
			{
				IObject? arg = values[index];
				slot.SetParameterValue((Byte)index, arg);
			}
		}

		/// <inheritdoc/>
		public override String ToString()
		{
			ReadOnlySpan<IObject?> values = this.Values;
			StringBuilder sb = new();
			for (Int32 i = 0; i < values.Length; i++)
			{
				switch (values[i])
				{
					case IPrimitiveType jPrimitive:
						sb.AppendLine($"{i}: {jPrimitive.ObjectSignature} {jPrimitive}");
						break;
					case JReferenceObject jObject:
						sb.AppendLine($"{i}: {jObject.ToTraceText()}");
						break;
					default:
						sb.AppendLine($"{i}: null");
						break;
				}
			}
			return sb.ToString();
		}
	}
#if !NET9_0_OR_GREATER
	/// <summary>
	/// Struct used to call funcional interface-based JNI constructor.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	private readonly struct LegacyConstructorCall<TObject>(
		EnvironmentCore core,
		JClassObject jClass,
		JConstructorDefinition definition) : IReadOnlyFixedContextFunction<IObject?, TObject>
		where TObject : JLocalObject, IDataType<TObject>
	{
		TObject IReadOnlyFixedContextFunction<IObject?, TObject>.Apply(
			scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			return core.CallConstructor<TObject, LegacyCallArgument>(jClass, definition, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI constructor.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	private readonly struct LegacyReflectedConstructorCall<TObject>(
		EnvironmentCore core,
		JConstructorObject jConstructor,
		JConstructorDefinition definition) : IReadOnlyFixedContextFunction<IObject?, TObject>
		where TObject : JLocalObject, IClassType<TObject>
	{
		TObject IReadOnlyFixedContextFunction<IObject?, TObject>.Apply(
			scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			return core.CallConstructor<TObject, LegacyCallArgument>(jConstructor, definition, in args);
		}
	}
#endif
}