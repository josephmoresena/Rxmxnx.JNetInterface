namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Stores initial global classes.
	/// </summary>
	private abstract class GlobalMainClasses : MainClasses<JGlobal>
	{
		/// <summary>
		/// Metadata for <see cref="JClassObject"/>.
		/// </summary>
		protected ClassObjectMetadata ClassMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JThrowableObject"/>.
		/// </summary>
		protected ClassObjectMetadata ThrowableMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JStackTraceElementObject"/>.
		/// </summary>
		protected ClassObjectMetadata StackTraceElementMetadata { get; }

		/// <summary>
		/// Metadata for <see cref="JBoolean"/>.
		/// </summary>
		protected ClassObjectMetadata BooleanMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JByte"/>.
		/// </summary>
		protected ClassObjectMetadata ByteMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JChar"/>.
		/// </summary>
		protected ClassObjectMetadata CharMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JDouble"/>.
		/// </summary>
		protected ClassObjectMetadata DoubleMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JFloat"/>.
		/// </summary>
		protected ClassObjectMetadata FloatMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JInt"/>.
		/// </summary>
		protected ClassObjectMetadata IntMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JLong"/>.
		/// </summary>
		protected ClassObjectMetadata LongMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JShort"/>.
		/// </summary>
		protected ClassObjectMetadata ShortMetadata { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		protected GlobalMainClasses(IVirtualMachine vm)
		{
			this.ClassMetadata = ClassObjectMetadata.Create<JClassObject>();
			this.ClassObject = new(vm, this.ClassMetadata, default);
			this.ThrowableMetadata = ClassObjectMetadata.Create<JThrowableObject>();
			this.ThrowableObject = new(vm, this.ThrowableMetadata, default);
			this.StackTraceElementMetadata = ClassObjectMetadata.Create<JStackTraceElementObject>();
			this.StackTraceElementObject = new(vm, this.StackTraceElementMetadata, default);

			this.VoidPrimitive = new(vm, ClassObjectMetadata.VoidMetadata, default);
			this.BooleanMetadata = ClassObjectMetadata.Create<JBoolean>();
			this.BooleanPrimitive = new(vm, this.BooleanMetadata, default);
			this.ByteMetadata = ClassObjectMetadata.Create<JByte>();
			this.BytePrimitive = new(vm, this.ByteMetadata, default);
			this.CharMetadata = ClassObjectMetadata.Create<JChar>();
			this.CharPrimitive = new(vm, this.CharMetadata, default);
			this.DoubleMetadata = ClassObjectMetadata.Create<JDouble>();
			this.DoublePrimitive = new(vm, this.DoubleMetadata, default);
			this.FloatMetadata = ClassObjectMetadata.Create<JFloat>();
			this.FloatPrimitive = new(vm, this.FloatMetadata, default);
			this.IntMetadata = ClassObjectMetadata.Create<JInt>();
			this.IntPrimitive = new(vm, this.IntMetadata, default);
			this.LongMetadata = ClassObjectMetadata.Create<JLong>();
			this.LongPrimitive = new(vm, this.LongMetadata, default);
			this.ShortMetadata = ClassObjectMetadata.Create<JShort>();
			this.ShortPrimitive = new(vm, this.ShortMetadata, default);
		}

		/// <summary>
		/// Loads global classes.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		public virtual void LoadMainClasses(JEnvironment env)
		{
			this.ClassObject.SetValue(env.GetMainClassGlobalRef(this.ClassMetadata));
			this.ThrowableObject.SetValue(env.GetMainClassGlobalRef(this.ThrowableMetadata));
			this.StackTraceElementObject.SetValue(env.GetMainClassGlobalRef(this.StackTraceElementMetadata));

			this.VoidPrimitive.SetValue(env.GetMainClassGlobalRef(ClassObjectMetadata.VoidMetadata));
			this.BooleanPrimitive.SetValue(env.GetMainClassGlobalRef(this.BooleanMetadata));
			this.BytePrimitive.SetValue(env.GetMainClassGlobalRef(this.ByteMetadata));
			this.CharPrimitive.SetValue(env.GetMainClassGlobalRef(this.CharMetadata));
			this.DoublePrimitive.SetValue(env.GetMainClassGlobalRef(this.DoubleMetadata));
			this.FloatPrimitive.SetValue(env.GetMainClassGlobalRef(this.FloatMetadata));
			this.IntPrimitive.SetValue(env.GetMainClassGlobalRef(this.IntMetadata));
			this.LongPrimitive.SetValue(env.GetMainClassGlobalRef(this.LongMetadata));
			this.ShortPrimitive.SetValue(env.GetMainClassGlobalRef(this.ShortMetadata));
		}
	}
}