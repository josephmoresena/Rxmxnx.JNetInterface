package com.rxmxnx.jnetinterface;

import java.util.concurrent.Callable;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.RunnableFuture;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

public final class NativeRunnableCallable extends NativeCallback implements Runnable, Callable, RunnableFuture {
    
    public NativeRunnableCallable(long lowValue, long highValue) {
        super(lowValue, highValue);
    }

    @Override
    public final void run() {
        NativeRunnableCallable.run(this.lowValue, this.highValue);
    }
    @Override
    public final Object call() throws Exception {
        return NativeRunnableCallable.call(this.lowValue, this.highValue);
    }
    @Override
    public final boolean cancel(boolean bln) {
        return NativeRunnableCallable.cancel(this.lowValue, this.highValue, bln);
    }
    @Override
    public final boolean isCancelled() {
        return NativeRunnableCallable.isCancelled(this.lowValue, this.highValue);
    }
    @Override
    public final boolean isDone() {
       return NativeRunnableCallable.isDone(this.lowValue, this.highValue);
    }
    @Override
    public final Object get() throws InterruptedException, ExecutionException {
        return NativeRunnableCallable.get(this.lowValue, this.highValue);
    }
    @Override
    public final Object get(long l, TimeUnit tu) throws InterruptedException, ExecutionException, TimeoutException {
        return NativeRunnableCallable.get(this.lowValue, this.highValue, l, tu);
    }
    
    private static native void run(long lowValue, long highValue);
    private static native Object call(long lowValue, long highValue);
    private static native boolean cancel(long lowValue, long highValue, boolean bln);
    private static native boolean isCancelled(long lowValue, long highValue);
    private static native boolean isDone(long lowValue, long highValue);
    private static native Object get(long lowValue, long highValue);
    private static native Object get(long lowValue, long highValue, long l, TimeUnit tu);
}
