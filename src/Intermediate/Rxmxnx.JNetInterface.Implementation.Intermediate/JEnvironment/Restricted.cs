namespace Rxmxnx.JNetInterface;

partial class JEnvironment : ILocalCacheOwner, IAccessibleManager
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JFieldId IAccessibleManager.GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> this.GetFieldId(definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JFieldId IAccessibleManager.GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> this.GetStaticFieldId(definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JMethodId IAccessibleManager.GetMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> this.GetMethodId(definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JMethodId IAccessibleManager.GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> this.GetStaticMethodId(definition, classRef);
	LocalCache ILocalCacheOwner.LocalCache
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.LocalCache;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => this.SetObjectCache(value);
	}
}