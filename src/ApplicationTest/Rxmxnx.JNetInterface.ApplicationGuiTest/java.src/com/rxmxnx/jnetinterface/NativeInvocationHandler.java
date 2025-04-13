package com.rxmxnx.jnetinterface;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;

public final class NativeInvocationHandler extends NativeCallback implements InvocationHandler {
    
    public NativeInvocationHandler(long lowValue, long highValue) {
        super(lowValue, highValue);
    }

    @Override
    public final Object invoke(Object o, Method method, Object[] os) throws Throwable {
        return NativeInvocationHandler.invoke(lowValue, highValue, o, method, os);
    }
    
    private static native Object invoke(long lowValue, long highValue, Object o, Method method, Object[] os) throws Throwable;
}
