namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Stores initial global classes.
	/// </summary>
	private sealed class GlobalMainClasses : MainClasses<JGlobal>
	{
		/// <summary>
		/// Metadata for <see cref="JClassObject"/>.
		/// </summary>
		public JClassObjectMetadata ClassMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JThrowableObject"/>.
		/// </summary>
		public JClassObjectMetadata ThrowableMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JStackTraceElementObject"/>.
		/// </summary>
		public JClassObjectMetadata StackTraceElementMetadata { get; }

		/// <summary>
		/// Metadata for <see cref="JBoolean"/>.
		/// </summary>
		public JClassObjectMetadata BooleanMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JByte"/>.
		/// </summary>
		public JClassObjectMetadata ByteMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JChar"/>.
		/// </summary>
		public JClassObjectMetadata CharMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JDouble"/>.
		/// </summary>
		public JClassObjectMetadata DoubleMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JFloat"/>.
		/// </summary>
		public JClassObjectMetadata FloatMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JInt"/>.
		/// </summary>
		public JClassObjectMetadata IntMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JLong"/>.
		/// </summary>
		public JClassObjectMetadata LongMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JShort"/>.
		/// </summary>
		public JClassObjectMetadata ShortMetadata { get; }

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
			this.ClassMetadata = JClassObjectMetadata.Create<JClassObject>();
			this.ClassObject = new(this.VirtualMachine, this.ClassMetadata, false, default);
			this.ThrowableMetadata = JClassObjectMetadata.Create<JThrowableObject>();
			this.ThrowableObject = new(this.VirtualMachine, this.ThrowableMetadata, false, default);
			this.StackTraceElementMetadata = JClassObjectMetadata.Create<JStackTraceElementObject>();
			this.StackTraceElementObject = new(this.VirtualMachine, this.StackTraceElementMetadata, false, default);

			this.VoidPrimitive = new(this.VirtualMachine, JClassObjectMetadata.VoidMetadata, false, default);
			this.BooleanMetadata = JClassObjectMetadata.Create<JBoolean>();
			this.BooleanPrimitive = new(this.VirtualMachine, this.BooleanMetadata, false, default);
			this.ByteMetadata = JClassObjectMetadata.Create<JByte>();
			this.BytePrimitive = new(this.VirtualMachine, this.ByteMetadata, false, default);
			this.CharMetadata = JClassObjectMetadata.Create<JChar>();
			this.CharPrimitive = new(this.VirtualMachine, this.CharMetadata, false, default);
			this.DoubleMetadata = JClassObjectMetadata.Create<JDouble>();
			this.DoublePrimitive = new(this.VirtualMachine, this.DoubleMetadata, false, default);
			this.FloatMetadata = JClassObjectMetadata.Create<JFloat>();
			this.FloatPrimitive = new(this.VirtualMachine, this.FloatMetadata, false, default);
			this.IntMetadata = JClassObjectMetadata.Create<JInt>();
			this.IntPrimitive = new(this.VirtualMachine, this.IntMetadata, false, default);
			this.LongMetadata = JClassObjectMetadata.Create<JLong>();
			this.LongPrimitive = new(this.VirtualMachine, this.LongMetadata, false, default);
			this.ShortMetadata = JClassObjectMetadata.Create<JShort>();
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

			this.VoidPrimitive.SetValue(env.GetClassGlobalRef(JClassObjectMetadata.VoidMetadata));
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