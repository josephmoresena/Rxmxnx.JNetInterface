namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an <c>[]</c> instance.
/// </summary>
/// <typeparam name="TElement">Element type of array.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2326,
                 Justification = CommonConstants.OnlyInternalInstantiationJustification)]
public interface IArrayObject<out TElement> : ILocalObject where TElement : IObject;