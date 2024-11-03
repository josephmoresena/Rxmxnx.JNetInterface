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
		/// Class for Java <see cref="JVoidObject"/>.
		/// </summary>
		public override JGlobal VoidObject { get; }
		/// <summary>
		/// Class for <see cref="JBooleanObject"/>.
		/// </summary>
		public override JGlobal BooleanObject { get; }
		/// <summary>
		/// Class for <see cref="JByteObject"/>.
		/// </summary>
		public override JGlobal ByteObject { get; }
		/// <summary>
		/// Class for <see cref="JCharacterObject"/>.
		/// </summary>
		public override JGlobal CharacterObject { get; }
		/// <summary>
		/// Class for <see cref="JDoubleObject"/>.
		/// </summary>
		public override JGlobal DoubleObject { get; }
		/// <summary>
		/// Class for <see cref="JFloatObject"/>.
		/// </summary>
		public override JGlobal FloatObject { get; }
		/// <summary>
		/// Class for <see cref="JIntegerObject"/>.
		/// </summary>
		public override JGlobal IntegerObject { get; }
		/// <summary>
		/// Class for <see cref="JLongObject"/>.
		/// </summary>
		public override JGlobal LongObject { get; }
		/// <summary>
		/// Class for <see cref="JShortObject"/>.
		/// </summary>
		public override JGlobal ShortObject { get; }

		/// <summary>
		/// Metadata for <see cref="JVoidObject"/>.
		/// </summary>
		protected ClassObjectMetadata VoidMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JBooleanObject"/>.
		/// </summary>
		protected ClassObjectMetadata BooleanMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JByteObject"/>.
		/// </summary>
		protected ClassObjectMetadata ByteMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JCharacterObject"/>.
		/// </summary>
		protected ClassObjectMetadata CharacterMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JDoubleObject"/>.
		/// </summary>
		protected ClassObjectMetadata DoubleMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JFloatObject"/>.
		/// </summary>
		protected ClassObjectMetadata FloatMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JIntegerObject"/>.
		/// </summary>
		protected ClassObjectMetadata IntegerMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JLongObject"/>.
		/// </summary>
		protected ClassObjectMetadata LongMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JShortObject"/>.
		/// </summary>
		protected ClassObjectMetadata ShortMetadata { get; }

		/// <summary>
		/// Metadata for <see cref="JBoolean"/>.
		/// </summary>
		protected ClassObjectMetadata BooleanPrimitiveMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JByte"/>.
		/// </summary>
		protected ClassObjectMetadata BytePrimitiveMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JChar"/>.
		/// </summary>
		protected ClassObjectMetadata CharPrimitiveMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JDouble"/>.
		/// </summary>
		protected ClassObjectMetadata DoublePrimitiveMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JFloat"/>.
		/// </summary>
		protected ClassObjectMetadata FloatPrimitiveMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JInt"/>.
		/// </summary>
		protected ClassObjectMetadata IntPrimitiveMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JLong"/>.
		/// </summary>
		protected ClassObjectMetadata LongPrimitiveMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JShort"/>.
		/// </summary>
		protected ClassObjectMetadata ShortPrimitiveMetadata { get; }

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

			this.VoidMetadata = ClassObjectMetadata.Create<JVoidObject>();
			this.VoidObject = new(vm, this.VoidMetadata, default);
			this.BooleanMetadata = ClassObjectMetadata.Create<JBooleanObject>();
			this.BooleanObject = new(vm, this.BooleanMetadata, default);
			this.ByteMetadata = ClassObjectMetadata.Create<JByteObject>();
			this.ByteObject = new(vm, this.ByteMetadata, default);
			this.CharacterMetadata = ClassObjectMetadata.Create<JCharacterObject>();
			this.CharacterObject = new(vm, this.CharacterMetadata, default);
			this.DoubleMetadata = ClassObjectMetadata.Create<JDoubleObject>();
			this.DoubleObject = new(vm, this.DoubleMetadata, default);
			this.FloatMetadata = ClassObjectMetadata.Create<JFloatObject>();
			this.FloatObject = new(vm, this.FloatMetadata, default);
			this.IntegerMetadata = ClassObjectMetadata.Create<JIntegerObject>();
			this.IntegerObject = new(vm, this.IntegerMetadata, default);
			this.LongMetadata = ClassObjectMetadata.Create<JLongObject>();
			this.LongObject = new(vm, this.LongMetadata, default);
			this.ShortMetadata = ClassObjectMetadata.Create<JShortObject>();
			this.ShortObject = new(vm, this.ShortMetadata, default);

			this.VoidPrimitive = new(vm, ClassObjectMetadata.VoidMetadata, default);
			this.BooleanPrimitiveMetadata = ClassObjectMetadata.Create<JBoolean>();
			this.BooleanPrimitive = new(vm, this.BooleanPrimitiveMetadata, default);
			this.BytePrimitiveMetadata = ClassObjectMetadata.Create<JByte>();
			this.BytePrimitive = new(vm, this.BytePrimitiveMetadata, default);
			this.CharPrimitiveMetadata = ClassObjectMetadata.Create<JChar>();
			this.CharPrimitive = new(vm, this.CharPrimitiveMetadata, default);
			this.DoublePrimitiveMetadata = ClassObjectMetadata.Create<JDouble>();
			this.DoublePrimitive = new(vm, this.DoublePrimitiveMetadata, default);
			this.FloatPrimitiveMetadata = ClassObjectMetadata.Create<JFloat>();
			this.FloatPrimitive = new(vm, this.FloatPrimitiveMetadata, default);
			this.IntPrimitiveMetadata = ClassObjectMetadata.Create<JInt>();
			this.IntPrimitive = new(vm, this.IntPrimitiveMetadata, default);
			this.LongPrimitiveMetadata = ClassObjectMetadata.Create<JLong>();
			this.LongPrimitive = new(vm, this.LongPrimitiveMetadata, default);
			this.ShortPrimitiveMetadata = ClassObjectMetadata.Create<JShort>();
			this.ShortPrimitive = new(vm, this.ShortPrimitiveMetadata, default);
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

			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.VoidObjectMainClassEnabled, env, this.VoidMetadata,
			                                   this.VoidObject);
			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.BooleanObjectMainClassEnabled, env, this.BooleanMetadata,
			                                   this.BooleanObject);
			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.ByteObjectMainClassEnabled, env, this.ByteMetadata,
			                                   this.ByteObject);
			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.CharacterObjectMainClassEnabled, env,
			                                   this.CharacterMetadata, this.CharacterObject);
			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.DoubleObjectMainClassEnabled, env, this.DoubleMetadata,
			                                   this.DoubleObject);
			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.FloatObjectMainClassEnabled, env, this.FloatMetadata,
			                                   this.FloatObject);
			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.IntegerObjectMainClassEnabled, env, this.IntegerMetadata,
			                                   this.IntegerObject);
			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.LongObjectMainClassEnabled, env, this.LongMetadata,
			                                   this.LongObject);
			GlobalMainClasses.LoadWrapperClass(JVirtualMachine.ShortObjectMainClassEnabled, env, this.ShortMetadata,
			                                   this.ShortObject);

			if (JVirtualMachine.PrimitiveMainClassesEnabled)
				this.LoadPrimitiveClasses(env);
		}
		private void LoadPrimitiveClasses(JEnvironment env)
		{
			this.VoidPrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(ClassObjectMetadata.VoidMetadata, this.VoidObject));
			this.BooleanPrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(this.BooleanPrimitiveMetadata, this.BooleanObject));
			this.BytePrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(this.BytePrimitiveMetadata, this.ByteObject));
			this.CharPrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(this.CharPrimitiveMetadata, this.CharacterObject));
			this.DoublePrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(this.DoublePrimitiveMetadata, this.DoubleObject));
			this.FloatPrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(this.FloatPrimitiveMetadata, this.FloatObject));
			this.IntPrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(this.IntPrimitiveMetadata, this.IntegerObject));
			this.LongPrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(this.LongPrimitiveMetadata, this.LongObject));
			this.ShortPrimitive.SetValue(
				env.GetPrimitiveMainClassGlobalRef(this.ShortPrimitiveMetadata, this.ShortObject));
		}

		/// <summary>
		/// Loads global wrapper class.
		/// </summary>
		/// <param name="isMainClass">Indicates current wrapper class is a main class.</param>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="classMetadata">Wrapper class metadata.</param>
		/// <param name="wGlobalClass">Current global wrapper class instance.</param>
		private static void LoadWrapperClass(Boolean isMainClass, JEnvironment env, ClassObjectMetadata classMetadata,
			JGlobal wGlobalClass)
		{
			if (isMainClass)
				wGlobalClass.SetValue(env.GetMainClassGlobalRef(classMetadata));
		}
	}
}