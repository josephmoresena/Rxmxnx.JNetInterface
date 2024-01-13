/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package com.example.hellojni;

import java.nio.Buffer;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.IntBuffer;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author atem94
 */
public class HelloJni {
    
    static {
        System.loadLibrary("hello-jni");
    }
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        HelloJni jni = new HelloJni();
        // TODO code application logic here
        System.out.println("Java Version: " + System.getProperty("java.version"));  
        System.out.println(jni.stringFromJNI());
        System.out.println(jni.getClassParameter(HelloJni.class));
        try {
            java.lang.reflect.Method a = HelloJni.class.getDeclaredMethod("passClass", Class.class);
            System.out.println(a.toString());
            Class<?>[] arr2 = a.getParameterTypes();
            for (Class<?> c : arr2)
                System.out.println(c.getTypeName());
            printObjectClass(a);
            ByteBuffer buffer = ByteBuffer.allocateDirect(10); //ByteBuffer.allocate(50);
            CharBuffer charBuffer = buffer.asCharBuffer();
            IntBuffer intBuffer = buffer.asIntBuffer();
            System.out.println("Buffer class: " + buffer.getClass());
            System.out.println("Buffer class: " + charBuffer.getClass());
            System.out.println("Buffer class: " + intBuffer.getClass());
            printBufferInfo(charBuffer);
            
        } catch (NoSuchMethodException | SecurityException ex) {
            Logger.getLogger(HelloJni.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    private native String stringFromJNI();
    private native String getClassParameter(Class<?> classObj);
    private native void passClass (Class<?> classObj);
    private void printBufferClass(Buffer buffer) {
        printObjectClass(buffer);
        if (buffer instanceof ByteBuffer) {
            ByteBuffer bBuffer = (ByteBuffer)buffer;
            printObjectClass(bBuffer.asCharBuffer());
            printObjectClass(bBuffer.asDoubleBuffer());
            printObjectClass(bBuffer.asFloatBuffer());
            printObjectClass(bBuffer.asIntBuffer());
            printObjectClass(bBuffer.asLongBuffer());
            printObjectClass(bBuffer.asShortBuffer());

            printObjectClass(bBuffer.asReadOnlyBuffer());
        }
    }
    private static void printObjectClass(Object obj) {
        Class<?> classObj = obj.getClass();
        StringBuilder strBuild = new StringBuilder();
        while(classObj != null) {
            if (strBuild.length() > 0)
                strBuild.append(" -> ");
            strBuild.append(classObj.getName());
            classObj = classObj.getSuperclass();
        }
        System.out.println(strBuild);
    }
    private static native void printBufferInfo(Buffer buff);
    
    private void printClass(Class<?> classObj) {
        System.out.println(classObj);
        passClass(classObj);
    }
    
    private void printInteger(int a1, int a2, int a3) {
        System.out.println("A1: " + a1 + " A2: " + a2 + " A3: " + a3);
    }
}
