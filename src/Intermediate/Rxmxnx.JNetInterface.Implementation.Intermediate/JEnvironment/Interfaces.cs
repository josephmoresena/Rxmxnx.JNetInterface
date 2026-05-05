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
	Boolean IEquatable<JEnvironment>.Equals(JEnvironment? other) => other is not null && this._core.Equals(other._core);
	Boolean IEnvironment.NoProxy => true;
	Int32? IEnvironment.LocalCapacity
	{
		get => this._core.Capacity;
		set => this._core.EnsureLocalCapacity(value.GetValueOrDefault());
	}
	IAccessFeature IEnvironment.AccessFeature => this._core;
	IClassFeature IEnvironment.ClassFeature => this._core;
	IReferenceFeature IEnvironment.ReferenceFeature => this._core;
	IStringFeature IEnvironment.StringFeature => this._core;
	IArrayFeature IEnvironment.ArrayFeature => this._core;
	INioFeature IEnvironment.NioFeature => this._core;
	NativeFunctionSet IEnvironment.FunctionSet => NativeFunctionSetImpl.Instance;

	Boolean IEnvironment.IsValidationAvoidable(JGlobalBase jGlobal)
		=> JEnvironment.IsValidationAvoidable(this._core, jGlobal);
	JReferenceType IEnvironment.GetReferenceType(JObject jObject)
	{
		if (jObject is not JReferenceObject jRefObj || jRefObj.IsDefault || jRefObj.IsProxy)
			return JReferenceType.InvalidRefType;
		using INativeTransaction jniTransaction = this._core.Host.MemoryManager.CreateTransaction(1);
		JObjectLocalRef localRef = jniTransaction.Add(jRefObj);
		JReferenceType result = this.GetReferenceType(localRef);
		if (result == JReferenceType.InvalidRefType)
		{
			if (jRefObj is JGlobalBase jGlobal) this._core.Host.GlobalManager.Remove(jGlobal);
			else this._core.Remove(jRefObj as JLocalObject);
			jRefObj.ClearValue();
		}
		else if (this._core.IsSame(jRefObj.As<JObjectLocalRef>(), default))
		{
			if (jRefObj is JGlobalBase jGlobal)
				this._core.Unload(jGlobal);
			else
				this._core.Unload(jRefObj as JLocalObject ?? ILocalViewObject.GetObject(jRefObj as ILocalViewObject));
		}

		return result;
	}
	Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther)
	{
		if (Object.ReferenceEquals(jObject, jOther)) return true;
		if (jObject is not JReferenceObject jRefObj || jOther is not JReferenceObject jRefOther)
			return JEnvironment.EqualEquatable(jObject as IEquatable<IPrimitiveType>, jOther as IPrimitiveType) ??
				JEnvironment.EqualEquatable(jOther as IEquatable<IPrimitiveType>, jObject as IPrimitiveType) ??
				JEnvironment.EqualEquatable(jObject as IEquatable<JPrimitiveObject>, jOther as JPrimitiveObject) ??
				JEnvironment.EqualEquatable(jOther as IEquatable<JPrimitiveObject>, jObject as JPrimitiveObject) ??
				false;

		ImplementationValidationUtilities.ThrowIfProxy(jRefObj);
		ImplementationValidationUtilities.ThrowIfProxy(jRefOther);
		using INativeTransaction jniTransaction = this._core.Host.MemoryManager.CreateTransaction(2);
		JObjectLocalRef localRef = jniTransaction.Add(jRefObj);
		JObjectLocalRef otherLocalRef = jniTransaction.Add(jRefOther);
		return this._core.IsSame(localRef, otherLocalRef);
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
}