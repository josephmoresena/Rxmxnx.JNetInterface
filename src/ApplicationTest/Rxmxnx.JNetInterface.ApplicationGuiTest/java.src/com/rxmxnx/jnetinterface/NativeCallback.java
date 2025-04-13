package com.rxmxnx.jnetinterface;

public abstract class NativeCallback {
    
    protected final long lowValue;
    protected final long highValue;
    
    protected NativeCallback(long lowValue, long highValue) {
        this.lowValue = lowValue;
        this.highValue = highValue;
    }
    
    @Override
    protected final void finalize() throws Throwable {
        NativeCallback.finalize(this.lowValue, this.highValue);
        super.finalize();
    }
    
    private native static void finalize(long lowValue, long highValue);
}
