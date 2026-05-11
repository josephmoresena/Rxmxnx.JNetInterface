namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS4035,
                 Justification = CommonConstants.InternalInheritanceJustification)]
#endif
partial class JEnvironment : IEquatable<IEnvironment>, IEquatable<JEnvironment>
{
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	Boolean IEquatable<IEnvironment>.Equals(IEnvironment? other)
		=> other is not null && this.Reference == other.Reference && (this as IEnvironment).NoProxy == other.NoProxy;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	Boolean IEquatable<JEnvironment>.Equals(JEnvironment? other) => other is not null && this._m.Core.Equals(other._m);
	Boolean IEnvironment.NoProxy => true;
	Int32? IEnvironment.LocalCapacity
	{
		get => this._m.LocalCapacity;
		set => this._m.LocalCapacity = value;
	}
	IAccessFeature IEnvironment.AccessFeature => this._m.Core;
	IClassFeature IEnvironment.ClassFeature => this._m.Core;
	IReferenceFeature IEnvironment.ReferenceFeature => this._m.Core;
	IStringFeature IEnvironment.StringFeature => this._m.Core;
	IArrayFeature IEnvironment.ArrayFeature => this._m.Core;
	INioFeature IEnvironment.NioFeature => this._m.Core;
	NativeFunctionSet IEnvironment.FunctionSet => NativeFunctionSetImpl.Instance;

	Boolean IEnvironment.IsValidationAvoidable(JGlobalBase jGlobal)
		=> EnvironmentCore.IsValidationAvoidable(this._m.Core, jGlobal);
	JReferenceType IEnvironment.GetReferenceType(JObject jObject) => this._m.GetReferenceType(jObject);
	Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther) => this._m.IsSameObject(jObject, jOther);
	TResult IEnvironment.WithFrame<TResult>(Int32 capacity, Func<TResult> func)
		=> EnvironmentValue.WithFrame(this, capacity, func);
	TResult IEnvironment.WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
		=> EnvironmentValue.WithFrame(this, capacity, state, func);
}