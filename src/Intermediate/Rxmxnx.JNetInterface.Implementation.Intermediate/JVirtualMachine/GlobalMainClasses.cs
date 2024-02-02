namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Stores initial global classes.
	/// </summary>
	private sealed record GlobalMainClasses : MainClasses<JGlobal>
	{
		/// <summary>
		/// Metadata for <see cref="JClassObject"/>.
		/// </summary>
		public ClassObjectMetadata ClassMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JThrowableObject"/>.
		/// </summary>
		public ClassObjectMetadata ThrowableMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JStackTraceElementObject"/>.
		/// </summary>
		public ClassObjectMetadata StackTraceElementMetadata { get; }

		/// <summary>
		/// Metadata for <see cref="JBoolean"/>.
		/// </summary>
		public ClassObjectMetadata BooleanMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JByte"/>.
		/// </summary>
		public ClassObjectMetadata ByteMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JChar"/>.
		/// </summary>
		public ClassObjectMetadata CharMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JDouble"/>.
		/// </summary>
		public ClassObjectMetadata DoubleMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JFloat"/>.
		/// </summary>
		public ClassObjectMetadata FloatMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JInt"/>.
		/// </summary>
		public ClassObjectMetadata IntMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JLong"/>.
		/// </summary>
		public ClassObjectMetadata LongMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JShort"/>.
		/// </summary>
		public ClassObjectMetadata ShortMetadata { get; }

		/// <summary>
		/// A <see cref="JVirtualMachine"/> instance.
		/// </summary>
		public JVirtualMachine VirtualMachine { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		public GlobalMainClasses(JVirtualMachine vm)
		{
			this.VirtualMachine = vm;
			this.ClassMetadata = ClassObjectMetadata.Create<JClassObject>();
			this.ClassObject = new(this.VirtualMachine, this.ClassMetadata, false, default);
			this.ThrowableMetadata = ClassObjectMetadata.Create<JThrowableObject>();
			this.ThrowableObject = new(this.VirtualMachine, this.ThrowableMetadata, false, default);
			this.StackTraceElementMetadata = ClassObjectMetadata.Create<JStackTraceElementObject>();
			this.StackTraceElementObject = new(this.VirtualMachine, this.StackTraceElementMetadata, false, default);

			this.VoidPrimitive = new(this.VirtualMachine, ClassObjectMetadata.VoidMetadata, false, default);
			this.BooleanMetadata = ClassObjectMetadata.Create<JBoolean>();
			this.BooleanPrimitive = new(this.VirtualMachine, this.BooleanMetadata, false, default);
			this.ByteMetadata = ClassObjectMetadata.Create<JByte>();
			this.BytePrimitive = new(this.VirtualMachine, this.ByteMetadata, false, default);
			this.CharMetadata = ClassObjectMetadata.Create<JChar>();
			this.CharPrimitive = new(this.VirtualMachine, this.CharMetadata, false, default);
			this.DoubleMetadata = ClassObjectMetadata.Create<JDouble>();
			this.DoublePrimitive = new(this.VirtualMachine, this.DoubleMetadata, false, default);
			this.FloatMetadata = ClassObjectMetadata.Create<JFloat>();
			this.FloatPrimitive = new(this.VirtualMachine, this.FloatMetadata, false, default);
			this.IntMetadata = ClassObjectMetadata.Create<JInt>();
			this.IntPrimitive = new(this.VirtualMachine, this.IntMetadata, false, default);
			this.LongMetadata = ClassObjectMetadata.Create<JLong>();
			this.LongPrimitive = new(this.VirtualMachine, this.LongMetadata, false, default);
			this.ShortMetadata = ClassObjectMetadata.Create<JShort>();
			this.ShortPrimitive = new(this.VirtualMachine, this.ShortMetadata, false, default);
		}

		/// <summary>
		/// Loads global classes.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		public void Load(JEnvironment env)
		{
			this.ClassObject.SetValue(env.GetClassGlobalRef(this.ClassMetadata));
			this.ThrowableObject.SetValue(env.GetClassGlobalRef(this.ThrowableMetadata));
			this.StackTraceElementObject.SetValue(env.GetClassGlobalRef(this.StackTraceElementMetadata));

			this.VoidPrimitive.SetValue(env.GetClassGlobalRef(ClassObjectMetadata.VoidMetadata));
			this.BooleanPrimitive.SetValue(env.GetClassGlobalRef(this.BooleanMetadata));
			this.BytePrimitive.SetValue(env.GetClassGlobalRef(this.ByteMetadata));
			this.CharPrimitive.SetValue(env.GetClassGlobalRef(this.CharMetadata));
			this.DoublePrimitive.SetValue(env.GetClassGlobalRef(this.DoubleMetadata));
			this.FloatPrimitive.SetValue(env.GetClassGlobalRef(this.FloatMetadata));
			this.IntPrimitive.SetValue(env.GetClassGlobalRef(this.IntMetadata));
			this.LongPrimitive.SetValue(env.GetClassGlobalRef(this.LongMetadata));
			this.ShortPrimitive.SetValue(env.GetClassGlobalRef(this.ShortMetadata));
		}

		/// <summary>
		/// Registers current instance in <paramref name="cache"/>.
		/// </summary>
		/// <param name="cache">A <see cref="JVirtualMachineCache"/> instance.</param>
		public void Register(JVirtualMachineCache cache)
		{
			cache.GlobalClassCache[this.ClassMetadata.Hash] = this.ClassObject;
			cache.GlobalClassCache[this.ThrowableMetadata.Hash] = this.ThrowableObject;
			cache.GlobalClassCache[this.StackTraceElementMetadata.Hash] = this.StackTraceElementObject;

			cache.GlobalClassCache[this.BooleanMetadata.Hash] = this.VoidPrimitive;
			cache.GlobalClassCache[this.BooleanMetadata.Hash] = this.BooleanPrimitive;
			cache.GlobalClassCache[this.ByteMetadata.Hash] = this.BytePrimitive;
			cache.GlobalClassCache[this.CharMetadata.Hash] = this.CharPrimitive;
			cache.GlobalClassCache[this.DoubleMetadata.Hash] = this.DoublePrimitive;
			cache.GlobalClassCache[this.FloatMetadata.Hash] = this.FloatPrimitive;
			cache.GlobalClassCache[this.IntMetadata.Hash] = this.IntPrimitive;
			cache.GlobalClassCache[this.LongMetadata.Hash] = this.LongPrimitive;
			cache.GlobalClassCache[this.ShortMetadata.Hash] = this.ShortPrimitive;
		}
	}
}