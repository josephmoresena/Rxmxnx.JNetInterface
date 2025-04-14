package com.rxmxnx.jnetinterface;

public abstract class NativeCallback {
    
    protected final long lowValue;
    protected final long highValue;
    
    private NativeCallback(long lowValue, long highValue) {
        this.lowValue = lowValue;
        this.highValue = highValue;
        this.validate();
    }
    
    @Override
    protected final void finalize() throws Throwable {
        NativeCallback.finalize(this.lowValue, this.highValue);
        super.finalize();
    }
    
    private void validate() {
        if (this instanceof NativeRunnable)
            return;
        if (this instanceof NativeActionListener)
            return;
        
        String message = NativeCallback.getExceptionMessage();
        throw new IllegalStateException(message);
    }
    
    private native static void finalize(long lowValue, long highValue);
    private native static void runnable_run(long lowValue, long highValue);
    private native static void actionListener_actionPerformed(long lowValue, long highValue, java.awt.event.ActionEvent ae);
    private native static String getExceptionMessage();
    
    private static class NativeRunnable extends NativeCallback implements Runnable {
        
        public NativeRunnable(long lowValue, long highValue) {
            super(lowValue, highValue);
        }
        @Override
        public final void run() {
            NativeCallback.runnable_run(this.lowValue, this.highValue);
        }
    }
    private static class NativeActionListener extends NativeCallback implements java.awt.event.ActionListener {
        public NativeActionListener(long lowValue, long highValue) {
            super(lowValue, highValue);
        }
        @Override
        public final void actionPerformed(java.awt.event.ActionEvent ae) {
            NativeCallback.actionListener_actionPerformed(this.lowValue, this.highValue, ae);
        }
        
    }
}