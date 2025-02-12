namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Stores initial global classes.
	/// </summary>
	private abstract partial class GlobalMainClasses : MainClasses<JGlobal>
	{
		/// <summary>
		/// Global cache.
		/// </summary>
		public readonly ClassCache<JGlobal> GlobalClassCache = new(JReferenceType.GlobalRefType);
		/// <inheritdoc/>
		public override JGlobal ClassObject => this.GlobalClassCache[ClassNameHelper.ClassHash];
		/// <inheritdoc/>
		public override JGlobal ThrowableObject => this.GlobalClassCache[ClassNameHelper.ThrowableHash];
		/// <inheritdoc/>
		public override JGlobal StackTraceElementObject => this.GlobalClassCache[ClassNameHelper.StackTraceElementHash];

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		protected GlobalMainClasses(IVirtualMachine vm)
		{
			this._classMetadata = ClassObjectMetadata.Create<JClassObject>();
			this._throwableMetadata = ClassObjectMetadata.Create<JThrowableObject>();
			this._stackTraceElementMetadata = ClassObjectMetadata.Create<JStackTraceElementObject>();
			this._booleanMetadata = ClassObjectMetadata.Create<JBoolean>();
			this._byteMetadata = ClassObjectMetadata.Create<JByte>();
			this._charMetadata = ClassObjectMetadata.Create<JChar>();
			this._doubleMetadata = ClassObjectMetadata.Create<JDouble>();
			this._floatMetadata = ClassObjectMetadata.Create<JFloat>();
			this._intMetadata = ClassObjectMetadata.Create<JInt>();
			this._longMetadata = ClassObjectMetadata.Create<JLong>();
			this._shortMetadata = ClassObjectMetadata.Create<JShort>();

			this.AppendGlobal(vm, this._classMetadata);
			this.AppendGlobal(vm, this._throwableMetadata);
			this.AppendGlobal(vm, this._stackTraceElementMetadata);

			this.AppendGlobal(vm, ClassObjectMetadata.VoidMetadata);
			this.AppendGlobal(vm, this._booleanMetadata);
			this.AppendGlobal(vm, this._byteMetadata);
			this.AppendGlobal(vm, this._charMetadata);
			this.AppendGlobal(vm, this._doubleMetadata);
			this.AppendGlobal(vm, this._floatMetadata);
			this.AppendGlobal(vm, this._intMetadata);
			this.AppendGlobal(vm, this._longMetadata);
			this.AppendGlobal(vm, this._shortMetadata);
		}

		/// <summary>
		/// Indicates whether <paramref name="jGlobal"/> is a main global class.
		/// </summary>
		/// <param name="classHash">Class hash.</param>
		/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="jGlobal"/> is main global class; otherwise;
		/// <see langword="false"/>.
		/// </returns>
		public Boolean IsMainGlobal(String classHash, JGlobal jGlobal)
			=> classHash switch
			{
				// Fundamental classes
				ClassNameHelper.ClassHash => true,
				ClassNameHelper.ThrowableHash => true,
				ClassNameHelper.StackTraceElementHash => true,
				// Primitive classes
				ClassNameHelper.VoidPrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				ClassNameHelper.BooleanPrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				ClassNameHelper.BytePrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				ClassNameHelper.CharPrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				ClassNameHelper.DoublePrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				ClassNameHelper.FloatPrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				ClassNameHelper.IntPrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				ClassNameHelper.LongPrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				ClassNameHelper.ShortPrimitiveHash => MainClasses.PrimitiveMainClassesEnabled,
				// User main classes
				_ => JVirtualMachine.userMainClasses.ContainsKey(classHash),
			} && Object.ReferenceEquals(jGlobal, this.GlobalClassCache[classHash]);
		/// <summary>
		/// Loads global classes.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		public void LoadMainClasses(JEnvironment env)
		{
			this.ClassObject.SetValue(env.GetMainClassGlobalRef(this._classMetadata));
			this.ThrowableObject.SetValue(env.GetMainClassGlobalRef(this._throwableMetadata));
			this.StackTraceElementObject.SetValue(env.GetMainClassGlobalRef(this._stackTraceElementMetadata));
			this.LoadUserMainClasses(env);
			if (MainClasses.PrimitiveMainClassesEnabled)
				this.LoadPrimitiveMainClasses(env);
			this.GlobalClassCache.RefreshAccess();
		}
	}
}