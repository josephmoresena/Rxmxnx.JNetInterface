namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// JNI Delegate dictionary.
		/// </summary>
		private static readonly Dictionary<Type, DelegateInfo> delegateIndex = new()
		{
			{ typeof(DefineClassDelegate), new() { Index = 0, Name = nameof(DefineClassDelegate), } },
			{ typeof(FindClassDelegate), new() { Index = 1, Name = nameof(FindClassDelegate), } },
			{
				typeof(FromReflectedMethodDelegate),
				new() { Index = 2, Name = nameof(FromReflectedMethodDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(FromReflectedFieldDelegate),
				new() { Index = 3, Name = nameof(FromReflectedFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{ typeof(ToReflectedMethodDelegate), new() { Index = 4, Name = nameof(ToReflectedMethodDelegate), } },
			{ typeof(GetSuperclassDelegate), new() { Index = 5, Name = nameof(GetSuperclassDelegate), } },
			{ typeof(IsAssignableFromDelegate), new() { Index = 6, Name = nameof(IsAssignableFromDelegate), } },
			{ typeof(ToReflectedFieldDelegate), new() { Index = 7, Name = nameof(ToReflectedFieldDelegate), } },
			{
				typeof(ThrowDelegate),
				new() { Index = 8, Name = nameof(ThrowDelegate), Level = JniSafetyLevels.ErrorSafe, }
			},
			{ typeof(ThrowNewDelegate), new() { Index = 9, Name = nameof(ThrowNewDelegate), } },
			{
				typeof(ExceptionOccurredDelegate),
				new() { Index = 10, Name = nameof(ExceptionOccurredDelegate), Level = JniSafetyLevels.ErrorSafe, }
			},
			{
				typeof(ExceptionDescribeDelegate),
				new()
				{
					Index = 11,
					Name = nameof(ExceptionDescribeDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ExceptionClearDelegate),
				new()
				{
					Index = 12,
					Name = nameof(ExceptionClearDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{ typeof(FatalErrorDelegate), new() { Index = 13, Name = nameof(FatalErrorDelegate), } },
			{ typeof(PushLocalFrameDelegate), new() { Index = 14, Name = nameof(PushLocalFrameDelegate), } },
			{
				typeof(PopLocalFrameDelegate),
				new()
				{
					Index = 15,
					Name = nameof(PopLocalFrameDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{ typeof(NewGlobalRefDelegate), new() { Index = 16, Name = nameof(NewGlobalRefDelegate), } },
			{
				typeof(DeleteGlobalRefDelegate),
				new()
				{
					Index = 17,
					Name = nameof(DeleteGlobalRefDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(DeleteLocalRefDelegate),
				new()
				{
					Index = 18,
					Name = nameof(DeleteLocalRefDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(IsSameObjectDelegate),
				new() { Index = 19, Name = nameof(IsSameObjectDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{ typeof(NewLocalRefDelegate), new() { Index = 20, Name = nameof(NewLocalRefDelegate), } },
			{
				typeof(EnsureLocalCapacityDelegate),
				new() { Index = 21, Name = nameof(EnsureLocalCapacityDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{ typeof(NewObjectADelegate), new() { Index = 25, Name = nameof(NewObjectADelegate), } },
			{ typeof(GetObjectClassDelegate), new() { Index = 26, Name = nameof(GetObjectClassDelegate), } },
			{
				typeof(IsInstanceOfDelegate),
				new() { Index = 27, Name = nameof(IsInstanceOfDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetMethodIdDelegate),
				new() { Index = 28, Name = nameof(IsInstanceOfDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{ typeof(CallObjectMethodADelegate), new() { Index = 31, Name = nameof(CallObjectMethodADelegate), } },
			{ typeof(CallBooleanMethodADelegate), new() { Index = 34, Name = nameof(CallBooleanMethodADelegate), } },
			{ typeof(CallByteMethodADelegate), new() { Index = 37, Name = nameof(CallByteMethodADelegate), } },
			{ typeof(CallCharMethodADelegate), new() { Index = 40, Name = nameof(CallCharMethodADelegate), } },
			{ typeof(CallShortMethodADelegate), new() { Index = 43, Name = nameof(CallShortMethodADelegate), } },
			{ typeof(CallIntMethodADelegate), new() { Index = 46, Name = nameof(CallIntMethodADelegate), } },
			{ typeof(CallLongMethodADelegate), new() { Index = 49, Name = nameof(CallLongMethodADelegate), } },
			{ typeof(CallFloatMethodADelegate), new() { Index = 52, Name = nameof(CallFloatMethodADelegate), } },
			{ typeof(CallDoubleMethodADelegate), new() { Index = 55, Name = nameof(CallFloatMethodADelegate), } },
			{ typeof(CallVoidMethodADelegate), new() { Index = 58, Name = nameof(CallVoidMethodADelegate), } },
			{
				typeof(CallNonVirtualObjectMethodADelegate),
				new() { Index = 61, Name = nameof(CallNonVirtualObjectMethodADelegate), }
			},
			{
				typeof(CallNonVirtualBooleanMethodADelegate),
				new() { Index = 64, Name = nameof(CallNonVirtualBooleanMethodADelegate), }
			},
			{
				typeof(CallNonVirtualByteMethodADelegate),
				new() { Index = 67, Name = nameof(CallNonVirtualByteMethodADelegate), }
			},
			{
				typeof(CallNonVirtualCharMethodADelegate),
				new() { Index = 70, Name = nameof(CallNonVirtualCharMethodADelegate), }
			},
			{
				typeof(CallNonVirtualShortMethodADelegate),
				new() { Index = 73, Name = nameof(CallNonVirtualShortMethodADelegate), }
			},
			{
				typeof(CallNonVirtualIntMethodADelegate),
				new() { Index = 76, Name = nameof(CallNonVirtualIntMethodADelegate), }
			},
			{
				typeof(CallNonVirtualLongMethodADelegate),
				new() { Index = 79, Name = nameof(CallNonVirtualLongMethodADelegate), }
			},
			{
				typeof(CallNonVirtualFloatMethodADelegate),
				new() { Index = 82, Name = nameof(CallNonVirtualFloatMethodADelegate), }
			},
			{
				typeof(CallNonVirtualDoubleMethodADelegate),
				new() { Index = 85, Name = nameof(CallNonVirtualDoubleMethodADelegate), }
			},
			{
				typeof(CallNonVirtualVoidMethodADelegate),
				new() { Index = 88, Name = nameof(CallNonVirtualVoidMethodADelegate), }
			},
			{
				typeof(GetFieldIdDelegate),
				new() { Index = 89, Name = nameof(GetFieldIdDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{ typeof(GetObjectFieldDelegate), new() { Index = 90, Name = nameof(GetFieldIdDelegate), } },
			{
				typeof(GetBooleanFieldDelegate),
				new() { Index = 91, Name = nameof(GetBooleanFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetByteFieldDelegate),
				new() { Index = 92, Name = nameof(GetBooleanFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetCharFieldDelegate),
				new() { Index = 93, Name = nameof(GetCharFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetShortFieldDelegate),
				new() { Index = 94, Name = nameof(GetShortFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetIntFieldDelegate),
				new() { Index = 95, Name = nameof(GetIntFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetLongFieldDelegate),
				new() { Index = 96, Name = nameof(GetLongFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetFloatFieldDelegate),
				new() { Index = 97, Name = nameof(GetFloatFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetDoubleFieldDelegate),
				new() { Index = 98, Name = nameof(GetDoubleFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{ typeof(SetObjectFieldDelegate), new() { Index = 99, Name = nameof(SetObjectFieldDelegate), } },
			{
				typeof(SetBooleanFieldDelegate),
				new() { Index = 100, Name = nameof(SetBooleanFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetByteFieldDelegate),
				new() { Index = 101, Name = nameof(SetByteFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetCharFieldDelegate),
				new() { Index = 102, Name = nameof(SetCharFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetShortFieldDelegate),
				new() { Index = 103, Name = nameof(SetShortFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetIntFieldDelegate),
				new() { Index = 104, Name = nameof(SetIntFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetLongFieldDelegate),
				new() { Index = 105, Name = nameof(SetLongFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetFloatFieldDelegate),
				new() { Index = 106, Name = nameof(SetFloatFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetDoubleFieldDelegate),
				new() { Index = 107, Name = nameof(SetDoubleFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStaticMethodIdDelegate),
				new() { Index = 108, Name = nameof(GetStaticMethodIdDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(CallStaticObjectMethodADelegate),
				new() { Index = 111, Name = nameof(CallStaticObjectMethodADelegate), }
			},
			{
				typeof(CallStaticBooleanMethodADelegate),
				new() { Index = 114, Name = nameof(CallStaticBooleanMethodADelegate), }
			},
			{
				typeof(CallStaticByteMethodADelegate),
				new() { Index = 117, Name = nameof(CallStaticByteMethodADelegate), }
			},
			{
				typeof(CallStaticCharMethodADelegate),
				new() { Index = 120, Name = nameof(CallStaticCharMethodADelegate), }
			},
			{
				typeof(CallStaticShortMethodADelegate),
				new() { Index = 123, Name = nameof(CallStaticShortMethodADelegate), }
			},
			{
				typeof(CallStaticIntMethodADelegate),
				new() { Index = 126, Name = nameof(CallStaticIntMethodADelegate), }
			},
			{
				typeof(CallStaticLongMethodADelegate),
				new() { Index = 129, Name = nameof(CallStaticLongMethodADelegate), }
			},
			{
				typeof(CallStaticFloatMethodADelegate),
				new() { Index = 132, Name = nameof(CallStaticFloatMethodADelegate), }
			},
			{
				typeof(CallStaticDoubleMethodADelegate),
				new() { Index = 135, Name = nameof(CallStaticDoubleMethodADelegate), }
			},
			{
				typeof(CallStaticVoidMethodADelegate),
				new() { Index = 138, Name = nameof(CallStaticVoidMethodADelegate), }
			},
			{
				typeof(GetStaticFieldIdDelegate),
				new() { Index = 139, Name = nameof(GetStaticFieldIdDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStaticObjectFieldDelegate),
				new() { Index = 140, Name = nameof(GetStaticObjectFieldDelegate), }
			},
			{
				typeof(GetStaticBooleanFieldDelegate),
				new()
				{
					Index = 141, Name = nameof(GetStaticBooleanFieldDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetStaticByteFieldDelegate),
				new() { Index = 142, Name = nameof(GetStaticByteFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStaticCharFieldDelegate),
				new() { Index = 143, Name = nameof(GetStaticCharFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStaticShortFieldDelegate),
				new() { Index = 144, Name = nameof(GetStaticShortFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStaticIntFieldDelegate),
				new() { Index = 145, Name = nameof(GetStaticIntFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStaticLongFieldDelegate),
				new() { Index = 146, Name = nameof(GetStaticLongFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStaticFloatFieldDelegate),
				new() { Index = 147, Name = nameof(GetStaticFloatFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStaticDoubleFieldDelegate),
				new()
				{
					Index = 148, Name = nameof(GetStaticDoubleFieldDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(SetStaticObjectFieldDelegate),
				new() { Index = 149, Name = nameof(SetStaticObjectFieldDelegate), }
			},
			{
				typeof(SetStaticBooleanFieldDelegate),
				new()
				{
					Index = 150, Name = nameof(SetStaticBooleanFieldDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(SetStaticByteFieldDelegate),
				new() { Index = 151, Name = nameof(SetStaticByteFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetStaticCharFieldDelegate),
				new() { Index = 152, Name = nameof(SetStaticCharFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetStaticShortFieldDelegate),
				new() { Index = 153, Name = nameof(SetStaticShortFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetStaticIntFieldDelegate),
				new() { Index = 154, Name = nameof(SetStaticIntFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetStaticLongFieldDelegate),
				new() { Index = 115, Name = nameof(SetStaticLongFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetStaticFloatFieldDelegate),
				new() { Index = 156, Name = nameof(SetStaticFloatFieldDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetStaticDoubleFieldDelegate),
				new()
				{
					Index = 157, Name = nameof(SetStaticDoubleFieldDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{ typeof(NewStringDelegate), new() { Index = 158, Name = nameof(NewStringDelegate), } },
			{
				typeof(GetStringLengthDelegate),
				new() { Index = 159, Name = nameof(GetStringLengthDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStringCharsDelegate),
				new() { Index = 160, Name = nameof(GetStringCharsDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(ReleaseStringCharsDelegate),
				new()
				{
					Index = 161,
					Name = nameof(ReleaseStringCharsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{ typeof(NewStringUtfDelegate), new() { Index = 162, Name = nameof(NewStringUtfDelegate), } },
			{
				typeof(GetStringUtfLengthDelegate),
				new() { Index = 163, Name = nameof(GetStringUtfLengthDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStringUtfCharsDelegate),
				new() { Index = 164, Name = nameof(GetStringUtfCharsDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(ReleaseStringUtfCharsDelegate),
				new()
				{
					Index = 165,
					Name = nameof(ReleaseStringUtfCharsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetArrayLengthDelegate),
				new() { Index = 166, Name = nameof(GetArrayLengthDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{ typeof(NewObjectArrayDelegate), new() { Index = 167, Name = nameof(NewObjectArrayDelegate), } },
			{
				typeof(GetObjectArrayElementDelegate),
				new() { Index = 168, Name = nameof(GetObjectArrayElementDelegate), }
			},
			{
				typeof(SetObjectArrayElementDelegate),
				new() { Index = 169, Name = nameof(SetObjectArrayElementDelegate), }
			},
			{ typeof(NewBooleanArrayDelegate), new() { Index = 170, Name = nameof(NewBooleanArrayDelegate), } },
			{ typeof(NewByteArrayDelegate), new() { Index = 171, Name = nameof(NewByteArrayDelegate), } },
			{ typeof(NewCharArrayDelegate), new() { Index = 172, Name = nameof(NewCharArrayDelegate), } },
			{ typeof(NewShortArrayDelegate), new() { Index = 173, Name = nameof(NewShortArrayDelegate), } },
			{ typeof(NewIntArrayDelegate), new() { Index = 174, Name = nameof(NewIntArrayDelegate), } },
			{ typeof(NewLongArrayDelegate), new() { Index = 175, Name = nameof(NewLongArrayDelegate), } },
			{ typeof(NewFloatArrayDelegate), new() { Index = 176, Name = nameof(NewFloatArrayDelegate), } },
			{ typeof(NewDoubleArrayDelegate), new() { Index = 177, Name = nameof(NewDoubleArrayDelegate), } },
			{
				typeof(GetBooleanArrayElementsDelegate),
				new()
				{
					Index = 178,
					Name = nameof(GetBooleanArrayElementsDelegate),
					Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetByteArrayElementsDelegate),
				new()
				{
					Index = 179, Name = nameof(GetByteArrayElementsDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetCharArrayElementsDelegate),
				new()
				{
					Index = 180, Name = nameof(GetCharArrayElementsDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetShortArrayElementsDelegate),
				new()
				{
					Index = 181, Name = nameof(GetShortArrayElementsDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetIntArrayElementsDelegate),
				new() { Index = 182, Name = nameof(GetIntArrayElementsDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetLongArrayElementsDelegate),
				new()
				{
					Index = 183, Name = nameof(GetLongArrayElementsDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetFloatArrayElementsDelegate),
				new()
				{
					Index = 184, Name = nameof(GetFloatArrayElementsDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetDoubleArrayElementsDelegate),
				new()
				{
					Index = 185,
					Name = nameof(GetDoubleArrayElementsDelegate),
					Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleaseBooleanArrayElementsDelegate),
				new()
				{
					Index = 186,
					Name = nameof(ReleaseBooleanArrayElementsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleaseByteArrayElementsDelegate),
				new()
				{
					Index = 187,
					Name = nameof(ReleaseByteArrayElementsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleaseCharArrayElementsDelegate),
				new()
				{
					Index = 188,
					Name = nameof(ReleaseCharArrayElementsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleaseShortArrayElementsDelegate),
				new()
				{
					Index = 189,
					Name = nameof(ReleaseShortArrayElementsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleaseIntArrayElementsDelegate),
				new()
				{
					Index = 190,
					Name = nameof(ReleaseIntArrayElementsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleaseLongArrayElementsDelegate),
				new()
				{
					Index = 191,
					Name = nameof(ReleaseLongArrayElementsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleaseFloatArrayElementsDelegate),
				new()
				{
					Index = 192,
					Name = nameof(ReleaseFloatArrayElementsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleaseDoubleArrayElementsDelegate),
				new()
				{
					Index = 193,
					Name = nameof(ReleaseDoubleArrayElementsDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetBooleanArrayRegionDelegate),
				new()
				{
					Index = 194, Name = nameof(GetBooleanArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetByteArrayRegionDelegate),
				new() { Index = 195, Name = nameof(GetByteArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetCharArrayRegionDelegate),
				new() { Index = 196, Name = nameof(GetCharArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetShortArrayRegionDelegate),
				new() { Index = 197, Name = nameof(GetShortArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetIntArrayRegionDelegate),
				new() { Index = 198, Name = nameof(GetIntArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetLongArrayRegionDelegate),
				new() { Index = 199, Name = nameof(GetLongArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetFloatArrayRegionDelegate),
				new() { Index = 200, Name = nameof(GetFloatArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetDoubleArrayRegionDelegate),
				new()
				{
					Index = 201, Name = nameof(GetDoubleArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(SetBooleanArrayRegionDelegate),
				new()
				{
					Index = 202, Name = nameof(SetBooleanArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(SetByteArrayRegionDelegate),
				new() { Index = 203, Name = nameof(SetByteArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetCharArrayRegionDelegate),
				new() { Index = 204, Name = nameof(SetCharArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetShortArrayRegionDelegate),
				new() { Index = 205, Name = nameof(SetShortArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetIntArrayRegionDelegate),
				new() { Index = 206, Name = nameof(SetIntArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetLongArrayRegionDelegate),
				new() { Index = 207, Name = nameof(SetLongArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetFloatArrayRegionDelegate),
				new() { Index = 208, Name = nameof(SetFloatArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(SetDoubleArrayRegionDelegate),
				new()
				{
					Index = 209, Name = nameof(SetDoubleArrayRegionDelegate), Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{ typeof(RegisterNativesDelegate), new() { Index = 210, Name = nameof(RegisterNativesDelegate), } },
			{ typeof(UnregisterNativesDelegate), new() { Index = 211, Name = nameof(UnregisterNativesDelegate), } },
			{ typeof(MonitorEnterDelegate), new() { Index = 212, Name = nameof(MonitorEnterDelegate), } },
			{
				typeof(MonitorExitDelegate),
				new()
				{
					Index = 213,
					Name = nameof(MonitorEnterDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetVirtualMachineDelegate),
				new() { Index = 214, Name = nameof(MonitorEnterDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStringRegionDelegate),
				new() { Index = 215, Name = nameof(GetStringRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetStringUtfRegionDelegate),
				new() { Index = 216, Name = nameof(GetStringUtfRegionDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(GetPrimitiveArrayCriticalDelegate),
				new()
				{
					Index = 217,
					Name = nameof(GetPrimitiveArrayCriticalDelegate),
					Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ReleasePrimitiveArrayCriticalDelegate),
				new()
				{
					Index = 218,
					Name = nameof(ReleasePrimitiveArrayCriticalDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetStringCriticalDelegate),
				new() { Index = 219, Name = nameof(GetStringCriticalDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			{
				typeof(ReleaseStringCriticalDelegate),
				new()
				{
					Index = 220,
					Name = nameof(ReleaseStringCriticalDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{ typeof(NewWeakGlobalRefDelegate), new() { Index = 221, Name = nameof(NewWeakGlobalRefDelegate), } },
			{
				typeof(DeleteWeakGlobalRefDelegate),
				new()
				{
					Index = 222,
					Name = nameof(DeleteWeakGlobalRefDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(ExceptionCheckDelegate),
				new()
				{
					Index = 223,
					Name = nameof(ExceptionCheckDelegate),
					Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
				}
			},
			{ typeof(NewDirectByteBufferDelegate), new() { Index = 224, Name = nameof(NewDirectByteBufferDelegate), } },
			{
				typeof(GetDirectBufferAddressDelegate),
				new()
				{
					Index = 225,
					Name = nameof(GetDirectBufferAddressDelegate),
					Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetDirectBufferCapacityDelegate),
				new()
				{
					Index = 226,
					Name = nameof(GetDirectBufferCapacityDelegate),
					Level = JniSafetyLevels.CriticalSafe,
				}
			},
			{
				typeof(GetObjectRefTypeDelegate),
				new() { Index = 227, Name = nameof(GetObjectRefTypeDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			// JNI 0x00090000
			{
				typeof(GetModuleDelegate),
				new() { Index = 228, Name = nameof(GetModuleDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
			// JNI 0x00130000
			{
				typeof(IsVirtualThreadDelegate),
				new() { Index = 229, Name = nameof(GetModuleDelegate), Level = JniSafetyLevels.CriticalSafe, }
			},
		};
	}
}