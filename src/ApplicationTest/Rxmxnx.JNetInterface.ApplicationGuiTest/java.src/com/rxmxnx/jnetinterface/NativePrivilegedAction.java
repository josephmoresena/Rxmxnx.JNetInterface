package com.rxmxnx.jnetinterface;

import java.security.PrivilegedAction;
import java.security.PrivilegedExceptionAction;

public final class NativePrivilegedAction extends NativeCallback implements PrivilegedAction, PrivilegedExceptionAction {

    public NativePrivilegedAction(long lowValue, long highValue) {
        super(lowValue, highValue);
    }
    
    @Override
    public final Object run() {
        return NativePrivilegedAction.run(this.lowValue, this.highValue);
    }
    
    private static native Object run(long lowValue, long highValue);
}
