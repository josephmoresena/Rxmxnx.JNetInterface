namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	private sealed partial class VirtualMachineCache
	{
		/// <inheritdoc/>
		public override void LoadMainClasses(JEnvironment env)
		{
			base.LoadMainClasses(env);

			this.GlobalClassCache[this.ClassMetadata.Hash] = this.ClassObject;
			this.GlobalClassCache[this.ThrowableMetadata.Hash] = this.ThrowableObject;
			this.GlobalClassCache[this.StackTraceElementMetadata.Hash] = this.StackTraceElementObject;

			this.GlobalClassCache[this.VoidMetadata.Hash] = this.VoidObject;
			this.GlobalClassCache[this.BooleanMetadata.Hash] = this.BooleanObject;
			this.GlobalClassCache[this.ByteMetadata.Hash] = this.ByteObject;
			this.GlobalClassCache[this.CharacterMetadata.Hash] = this.CharacterObject;
			this.GlobalClassCache[this.DoubleMetadata.Hash] = this.DoubleObject;
			this.GlobalClassCache[this.FloatMetadata.Hash] = this.FloatObject;
			this.GlobalClassCache[this.IntegerMetadata.Hash] = this.IntegerObject;
			this.GlobalClassCache[this.LongMetadata.Hash] = this.LongObject;
			this.GlobalClassCache[this.ShortMetadata.Hash] = this.ShortObject;

			this.GlobalClassCache[ClassObjectMetadata.VoidMetadata.Hash] = this.VoidPrimitive;
			this.GlobalClassCache[this.BooleanPrimitiveMetadata.Hash] = this.BooleanPrimitive;
			this.GlobalClassCache[this.BytePrimitiveMetadata.Hash] = this.BytePrimitive;
			this.GlobalClassCache[this.CharPrimitiveMetadata.Hash] = this.CharPrimitive;
			this.GlobalClassCache[this.DoublePrimitiveMetadata.Hash] = this.DoublePrimitive;
			this.GlobalClassCache[this.FloatPrimitiveMetadata.Hash] = this.FloatPrimitive;
			this.GlobalClassCache[this.IntPrimitiveMetadata.Hash] = this.IntPrimitive;
			this.GlobalClassCache[this.LongPrimitiveMetadata.Hash] = this.LongPrimitive;
			this.GlobalClassCache[this.ShortPrimitiveMetadata.Hash] = this.ShortPrimitive;
		}
	}
}