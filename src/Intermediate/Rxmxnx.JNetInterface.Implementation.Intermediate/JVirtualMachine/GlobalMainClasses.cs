namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Stores initial global classes.
	/// </summary>
	private abstract partial class GlobalMainClasses : MainClasses<JGlobal>
	{
		/// <inheritdoc/>
		public override JGlobal ClassObject => this._mainClasses[ClassNameHelper.ClassHash];
		/// <inheritdoc/>
		public override JGlobal ThrowableObject => this._mainClasses[ClassNameHelper.ThrowableHash];
		/// <inheritdoc/>
		public override JGlobal StackTraceElementObject => this._mainClasses[ClassNameHelper.StackTraceElementHash];
		/// <inheritdoc/>
		public override JGlobal VoidPrimitive => this._mainClasses[ClassNameHelper.VoidPrimitiveHash];
		/// <inheritdoc/>
		public override JGlobal BooleanPrimitive => this._mainClasses[ClassNameHelper.BooleanPrimitiveHash];
		/// <inheritdoc/>
		public override JGlobal BytePrimitive => this._mainClasses[ClassNameHelper.BytePrimitiveHash];
		/// <inheritdoc/>
		public override JGlobal CharPrimitive => this._mainClasses[ClassNameHelper.CharPrimitiveHash];
		/// <inheritdoc/>
		public override JGlobal DoublePrimitive => this._mainClasses[ClassNameHelper.DoublePrimitiveHash];
		/// <inheritdoc/>
		public override JGlobal FloatPrimitive => this._mainClasses[ClassNameHelper.FloatPrimitiveHash];
		/// <inheritdoc/>
		public override JGlobal IntPrimitive => this._mainClasses[ClassNameHelper.IntPrimitiveHash];
		/// <inheritdoc/>
		public override JGlobal LongPrimitive => this._mainClasses[ClassNameHelper.LongPrimitiveHash];
		/// <inheritdoc/>
		public override JGlobal ShortPrimitive => this._mainClasses[ClassNameHelper.ShortPrimitiveHash];

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		protected GlobalMainClasses(IVirtualMachine vm)
		{
			this._mainClasses = GlobalMainClasses.CreateMainClassesDictionary(vm);

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
			=> Object.ReferenceEquals(jGlobal, this._mainClasses.GetValueOrDefault(classHash));
		/// <summary>
		/// Sets the class <paramref name="jGlobal"/> as a main global class.
		/// </summary>
		/// <param name="classHash">Class hash.</param>
		/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
		public void SetMainGlobal(String classHash, JGlobal jGlobal) => this._mainClasses.TryAdd(classHash, jGlobal);
		/// <summary>
		/// Loads global classes.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		public virtual void LoadMainClasses(JEnvironment env)
		{
			this.ClassObject.SetValue(env.GetMainClassGlobalRef(this._classMetadata));
			this.ThrowableObject.SetValue(env.GetMainClassGlobalRef(this._throwableMetadata));
			this.StackTraceElementObject.SetValue(env.GetMainClassGlobalRef(this._stackTraceElementMetadata));
			this.LoadUserMainClasses(env);
			this.LoadPrimitiveMainClasses(env);
		}
		/// <summary>
		/// Loads main classes in <paramref name="classCache"/>.
		/// </summary>
		/// <param name="classCache">A <see cref="ClassCache{JGlobal}"/> instance.</param>
		protected void LoadMainClasses(ClassCache<JGlobal> classCache)
		{
			foreach (String hash in this._mainClasses.Keys)
				classCache[hash] = this._mainClasses[hash];
		}
	}
}