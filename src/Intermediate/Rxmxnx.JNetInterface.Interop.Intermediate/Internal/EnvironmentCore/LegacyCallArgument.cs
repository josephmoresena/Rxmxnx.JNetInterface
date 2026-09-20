using StringBuilder = System.Text.StringBuilder;

namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class EnvironmentCore
{
	/// <summary>
	/// The <see cref="ICallArgument"/> implementation for legacy call arguments.
	/// </summary>
#if !NET9_0_OR_GREATER
	private readonly struct LegacyCallArgument : ICallArgument
	{
		/// <summary>
		/// Internal pointer to the values.
		/// </summary>
		private readonly ReadOnlyValPtr<IObject?> _ptr;
		/// <summary>
		/// Count of the values.
		/// </summary>
		private readonly Int32 _length;

		/// <summary>
		/// Internal values.
		/// </summary>
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
		/// <summary>
		/// Internal values.
		/// </summary>
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

	/// <summary>
	/// Struct used to call funcional interface-based JNI primitive static function.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	private readonly struct LegacyStaticPrimitiveFunctionCall(
		EnvironmentCore core,
		JClassObject jClass,
		JFunctionDefinition definition) : IFixedPointerListAction
	{
		void IFixedPointerListAction.Accept(scoped FixedPointerValueList list)
		{
			ReadOnlyFixedContextValue<Byte> byteContext = (ReadOnlyFixedContextValue<Byte>)list[0].Value;
			LegacyCallArgument args = new((ReadOnlyFixedContextValue<IObject?>)list[1].Value);
			// Although the list is read-only, byteContext is not.
			Span<Byte> bytes =
				MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(byteContext.Values), byteContext.Values.Length);
			core.CallStaticPrimitiveFunction(bytes, jClass, definition, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI static function.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	private readonly struct LegacyStaticFunctionCall<TResult>(
		EnvironmentCore core,
		JClassObject jClass,
		JFunctionDefinition definition) : IReadOnlyFixedContextFunction<IObject?, TResult?>
		where TResult : IDataType<TResult>
	{
		TResult? IReadOnlyFixedContextFunction<IObject?, TResult?>.Apply(
			scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			return core.CallStaticFunction<TResult, LegacyCallArgument>(jClass, definition, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI static function.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	private readonly struct LegacyReflectedStaticFunctionCall<TObject>(
		EnvironmentCore core,
		JMethodObject jMethod,
		JFunctionDefinition definition) : IReadOnlyFixedContextFunction<IObject?, TObject?>
		where TObject : IDataType<TObject>
	{
		TObject? IReadOnlyFixedContextFunction<IObject?, TObject?>.Apply(
			scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			return core.CallStaticFunction<TObject, LegacyCallArgument>(jMethod, definition, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI static method.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	private readonly struct LegacyStaticMethodCall(
		EnvironmentCore core,
		JClassObject jClass,
		JMethodDefinition definition) : IReadOnlyFixedContextAction<IObject?>
	{
		void IReadOnlyFixedContextAction<IObject?>.Accept(scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			core.CallStaticMethod(jClass, definition, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI static method.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	private readonly struct LegacyReflectedStaticMethodCall(
		EnvironmentCore core,
		JMethodObject jMethod,
		JMethodDefinition definition) : IReadOnlyFixedContextAction<IObject?>
	{
		void IReadOnlyFixedContextAction<IObject?>.Accept(scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			core.CallStaticMethod(jMethod, definition, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI primitive static function.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	private readonly struct LegacyInstancePrimitiveFunctionCall(
		EnvironmentCore core,
		JLocalObject jLocal,
		JClassObject jClass,
		JFunctionDefinition definition,
		Boolean nonVirtual) : IFixedPointerListAction
	{
		public void Accept(scoped FixedPointerValueList list)
		{
			ReadOnlyFixedContextValue<Byte> byteContext = (ReadOnlyFixedContextValue<Byte>)list[0].Value;
			LegacyCallArgument args = new((ReadOnlyFixedContextValue<IObject?>)list[1].Value);
			Span<Byte> bytes =
				MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(byteContext.Values), byteContext.Values.Length);
			core.CallPrimitiveFunction(bytes, jLocal, jClass, definition, nonVirtual, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI instance function.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	private readonly struct LegacyInstanceFunctionCall<TResult>(
		EnvironmentCore core,
		JLocalObject jLocal,
		JClassObject jClass,
		JFunctionDefinition definition,
		Boolean nonVirtual) : IReadOnlyFixedContextFunction<IObject?, TResult?> where TResult : IDataType<TResult>
	{
		TResult? IReadOnlyFixedContextFunction<IObject?, TResult?>.Apply(
			scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			return core.CallFunction<TResult, LegacyCallArgument>(jLocal, jClass, definition, nonVirtual, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI instance function.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	private readonly struct LegacyReflectedInstanceFunctionCall<TObject>(
		EnvironmentCore core,
		JMethodObject jMethod,
		JLocalObject jLocal,
		JFunctionDefinition definition,
		Boolean nonVirtual) : IReadOnlyFixedContextFunction<IObject?, TObject?> where TObject : IDataType<TObject>
	{
		TObject? IReadOnlyFixedContextFunction<IObject?, TObject?>.Apply(
			scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			return core.CallFunction<TObject, LegacyCallArgument>(jMethod, jLocal, definition, nonVirtual, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI instance method.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	private readonly struct LegacyInstanceMethodCall(
		EnvironmentCore core,
		JLocalObject jLocal,
		JClassObject jClass,
		JMethodDefinition definition,
		Boolean nonVirtual) : IReadOnlyFixedContextAction<IObject?>
	{
		void IReadOnlyFixedContextAction<IObject?>.Accept(scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			core.CallMethod(jLocal, jClass, definition, nonVirtual, in args);
		}
	}

	/// <summary>
	/// Struct used to call funcional interface-based JNI instance method.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	private readonly struct LegacyReflectedInstanceMethodCall(
		EnvironmentCore core,
		JMethodObject jMethod,
		JLocalObject jLocal,
		JMethodDefinition definition,
		Boolean nonVirtual) : IReadOnlyFixedContextAction<IObject?>
	{
		void IReadOnlyFixedContextAction<IObject?>.Accept(scoped ReadOnlyFixedContextValue<IObject?> fixedContext)
		{
			LegacyCallArgument args = new(fixedContext);
			core.CallMethod(jMethod, jLocal, definition, nonVirtual, in args);
		}
	}
#endif
}