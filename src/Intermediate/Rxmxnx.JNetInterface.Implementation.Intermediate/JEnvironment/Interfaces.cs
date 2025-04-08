namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS4035,
                 Justification = CommonConstants.InternalInheritanceJustification)]
#endif
partial class JEnvironment : IEquatable<IEnvironment>, IEquatable<JEnvironment>
{
	Boolean IEnvironment.NoProxy => true;
	Int32? IEnvironment.LocalCapacity
	{
		get => this._cache.Capacity;
		set => this._cache.EnsureLocalCapacity(value.GetValueOrDefault());
	}
	IAccessFeature IEnvironment.AccessFeature => this._cache;
	IClassFeature IEnvironment.ClassFeature => this._cache;
	IReferenceFeature IEnvironment.ReferenceFeature => this._cache;
	IStringFeature IEnvironment.StringFeature => this._cache;
	IArrayFeature IEnvironment.ArrayFeature => this._cache;
	INioFeature IEnvironment.NioFeature => this._cache;
	NativeFunctionSet IEnvironment.FunctionSet => NativeFunctionSetImpl.Instance;

	Boolean IEnvironment.IsValidationAvoidable(JGlobalBase jGlobal)
		=> JEnvironment.IsValidationAvoidable(this._cache, jGlobal);
	JReferenceType IEnvironment.GetReferenceType(JObject jObject)
	{
		if (jObject is not JReferenceObject jRefObj || jRefObj.IsDefault || jRefObj.IsProxy)
			return JReferenceType.InvalidRefType;
		using INativeTransaction jniTransaction = this._cache.VirtualMachine.CreateTransaction(1);
		JObjectLocalRef localRef = jniTransaction.Add(jRefObj);
		JReferenceType result = this.GetReferenceType(localRef);
		if (result == JReferenceType.InvalidRefType)
		{
			if (jRefObj is JGlobalBase jGlobal) (this.VirtualMachine as JVirtualMachine)!.Remove(jGlobal);
			else this._cache.Remove(jRefObj as JLocalObject);
			jRefObj.ClearValue();
		}
		else if (this.IsSame(jRefObj.As<JObjectLocalRef>(), default))
		{
			if (jRefObj is JGlobalBase jGlobal)
				this._cache.Unload(jGlobal);
			else
				this._cache.Unload(jRefObj as JLocalObject ?? ILocalViewObject.GetObject(jRefObj as ILocalViewObject));
		}

		return result;
	}
	Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther)
	{
		if (jObject.Equals(jOther)) return true;
		if (jObject is not JReferenceObject jRefObj || jOther is not JReferenceObject jRefOther)
			return JEnvironment.EqualEquatable(jObject as IEquatable<IPrimitiveType>, jOther as IPrimitiveType) ??
				JEnvironment.EqualEquatable(jOther as IEquatable<IPrimitiveType>, jObject as IPrimitiveType) ??
				JEnvironment.EqualEquatable(jObject as IEquatable<JPrimitiveObject>, jOther as JPrimitiveObject) ??
				JEnvironment.EqualEquatable(jOther as IEquatable<JPrimitiveObject>, jObject as JPrimitiveObject) ??
				false;

		ImplementationValidationUtilities.ThrowIfProxy(jRefObj);
		ImplementationValidationUtilities.ThrowIfProxy(jRefOther);
		using INativeTransaction jniTransaction = this._cache.VirtualMachine.CreateTransaction(2);
		JObjectLocalRef localRef = jniTransaction.Add(jRefObj);
		JObjectLocalRef otherLocalRef = jniTransaction.Add(jRefOther);
		return this.IsSame(localRef, otherLocalRef);
	}
	TResult IEnvironment.WithFrame<TResult>(Int32 capacity, Func<TResult> func)
	{
		using LocalFrame localFrame = new(this, capacity);
		TResult result = func();
		localFrame.SetResult(result);
		return result;
	}
	TResult IEnvironment.WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
	{
		using LocalFrame localFrame = new(this, capacity);
		TResult result = func(state);
		localFrame.SetResult(result);
		return result;
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	Boolean IEquatable<IEnvironment>.Equals(IEnvironment? other)
		=> other is not null && this.Reference == other.Reference && (this as IEnvironment).NoProxy == other.NoProxy;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	Boolean IEquatable<JEnvironment>.Equals(JEnvironment? other)
		=> other is not null && this._cache.Equals(other._cache);
}