package com.rxmxnx.jnetinterface;

import java.awt.AWTEvent;
import java.awt.datatransfer.FlavorEvent;
import java.awt.datatransfer.FlavorListener;
import java.awt.dnd.*;
import java.awt.event.*;

public class NativeAwtListener extends NativeCallback implements 
        ActionListener, AdjustmentListener, AWTEventListener, ComponentListener, ContainerListener, 
        FocusListener, HierarchyBoundsListener, HierarchyListener, InputMethodListener, ItemListener,
        KeyListener, MouseListener, MouseMotionListener, MouseWheelListener, 
        TextListener, WindowFocusListener, WindowListener, WindowStateListener, 
        DragGestureListener, DragSourceListener, DragSourceMotionListener, DropTargetListener,
        FlavorListener {
    
    public NativeAwtListener(long lowValue, long highValue) {
        super(lowValue, highValue);
    }

    @Override
    public final void actionPerformed(ActionEvent ae) {
        NativeAwtListener.actionPerformed(this.lowValue, this.highValue, ae);
    }
    @Override
    public final void adjustmentValueChanged(AdjustmentEvent ae) {
        NativeAwtListener.adjustmentValueChanged(this.lowValue, this.highValue, ae);
    }
    @Override
    public final void eventDispatched(AWTEvent awte) {
        NativeAwtListener.eventDispatched(this.lowValue, this.highValue, awte);
    }
    @Override
    public final void componentResized(ComponentEvent ce) {
       NativeAwtListener.componentResized(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void componentMoved(ComponentEvent ce) {
        NativeAwtListener.componentMoved(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void componentShown(ComponentEvent ce) {
        NativeAwtListener.componentShown(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void componentHidden(ComponentEvent ce) {
        NativeAwtListener.componentHidden(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void componentAdded(ContainerEvent ce) {
        NativeAwtListener.componentAdded(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void componentRemoved(ContainerEvent ce) {
           NativeAwtListener.componentRemoved(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void dragGestureRecognized(DragGestureEvent dge) {
        NativeAwtListener.dragGestureRecognized(this.lowValue, this.highValue, dge);
    }
    @Override
    public final void dragEnter(DragSourceDragEvent dsde) {
        NativeAwtListener.dragEnter(this.lowValue, this.highValue, dsde);
    }
    @Override
    public final void dragOver(DragSourceDragEvent dsde) {
        NativeAwtListener.dragOver(this.lowValue, this.highValue, dsde);
    }
    @Override
    public final void dropActionChanged(DragSourceDragEvent dsde) {
        NativeAwtListener.dropActionChanged(this.lowValue, this.highValue, dsde);
    }
    @Override
    public final void dragExit(DragSourceEvent dse) {
        NativeAwtListener.dragExit(this.lowValue, this.highValue, dse);
    }
    @Override
    public final void dragDropEnd(DragSourceDropEvent dsde) {
        NativeAwtListener.dragDropEnd(this.lowValue, this.highValue, dsde);
    }
    @Override
    public final void dragMouseMoved(DragSourceDragEvent dsde) {
        NativeAwtListener.dragMouseMoved(this.lowValue, this.highValue, dsde);
    }
    @Override
    public final void dragEnter(DropTargetDragEvent dtde) {
        NativeAwtListener.dragEnter(this.lowValue, this.highValue, dtde);
    }
    @Override
    public final void dragOver(DropTargetDragEvent dtde) {
        NativeAwtListener.dragOver(this.lowValue, this.highValue, dtde);
    }
    @Override
    public final void dropActionChanged(DropTargetDragEvent dtde) {
        NativeAwtListener.dropActionChanged(this.lowValue, this.highValue, dtde);
    }
    @Override
    public final void dragExit(DropTargetEvent dte) {
        NativeAwtListener.dragExit(this.lowValue, this.highValue, dte);
    }
    @Override
    public final void drop(DropTargetDropEvent dtde) {
        NativeAwtListener.drop(this.lowValue, this.highValue, dtde);
    }
    @Override
    public final void flavorsChanged(FlavorEvent fe) {
        NativeAwtListener.flavorsChanged(this.lowValue, this.highValue, fe);
    }
    @Override
    public final void focusGained(FocusEvent fe) {
        NativeAwtListener.focusGained(this.lowValue, this.highValue, fe);
    }
    @Override
    public final void focusLost(FocusEvent fe) {
        NativeAwtListener.focusLost(this.lowValue, this.highValue, fe);
    }
    @Override
    public final void inputMethodTextChanged(InputMethodEvent ime) {
        NativeAwtListener.inputMethodTextChanged(this.lowValue, this.highValue, ime);
    }
    @Override
    public final void caretPositionChanged(InputMethodEvent ime) {
        NativeAwtListener.caretPositionChanged(this.lowValue, this.highValue, ime);
    }
    @Override
    public final void mouseClicked(MouseEvent me) {
        NativeAwtListener.mouseClicked(this.lowValue, this.highValue, me);
    }
    @Override
    public final void mousePressed(MouseEvent me) {
        NativeAwtListener.mousePressed(this.lowValue, this.highValue, me);
    }
    @Override
    public final void mouseReleased(MouseEvent me) {
        NativeAwtListener.mouseReleased(this.lowValue, this.highValue, me);
    }
    @Override
    public final void mouseEntered(MouseEvent me) {
        NativeAwtListener.mouseEntered(this.lowValue, this.highValue, me);
    }
    @Override
    public final void mouseExited(MouseEvent me) {
        NativeAwtListener.mouseExited(this.lowValue, this.highValue, me);
    }
    @Override
    public final void mouseDragged(MouseEvent me) {
        NativeAwtListener.mouseDragged(this.lowValue, this.highValue, me);
    }
    @Override
    public final void mouseMoved(MouseEvent me) {
        NativeAwtListener.mouseMoved(this.lowValue, this.highValue, me);
    }
    @Override
    public final void mouseWheelMoved(MouseWheelEvent mwe) {
        NativeAwtListener.mouseWheelMoved(this.lowValue, this.highValue, mwe);
    }
    @Override
    public final void textValueChanged(TextEvent te) {
        NativeAwtListener.textValueChanged(this.lowValue, this.highValue, te);
    }
    @Override
    public final void windowGainedFocus(WindowEvent we) {
           NativeAwtListener.windowGainedFocus(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowLostFocus(WindowEvent we) {
        NativeAwtListener.windowLostFocus(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowOpened(WindowEvent we) {
        NativeAwtListener.windowOpened(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowClosing(WindowEvent we) {
        NativeAwtListener.windowClosing(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowClosed(WindowEvent we) {
        NativeAwtListener.windowClosed(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowIconified(WindowEvent we) {
        NativeAwtListener.windowIconified(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowDeiconified(WindowEvent we) {
        NativeAwtListener.windowDeiconified(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowActivated(WindowEvent we) {
        NativeAwtListener.windowActivated(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowDeactivated(WindowEvent we) {
        NativeAwtListener.windowDeactivated(this.lowValue, this.highValue, we);
    }
    @Override
    public final void windowStateChanged(WindowEvent we) {
        NativeAwtListener.windowStateChanged(this.lowValue, this.highValue, we);
    }
    @Override
    public final void ancestorMoved(HierarchyEvent he) {
        NativeAwtListener.ancestorMoved(this.lowValue, this.highValue, he);
    }
    @Override
    public final void ancestorResized(HierarchyEvent he) {
        NativeAwtListener.ancestorResized(this.lowValue, this.highValue, he);
    }
    @Override
    public final void hierarchyChanged(HierarchyEvent he) {
        NativeAwtListener.hierarchyChanged(this.lowValue, this.highValue, he);
    }
    @Override
    public final void itemStateChanged(ItemEvent ie) {
        NativeAwtListener.itemStateChanged(this.lowValue, this.highValue, ie);
    }
    @Override
    public final void keyTyped(KeyEvent ke) {
        NativeAwtListener.keyTyped(this.lowValue, this.highValue, ke);
    }
    @Override
    public final void keyPressed(KeyEvent ke) {
        NativeAwtListener.keyPressed(this.lowValue, this.highValue, ke);
    }
    @Override
    public final void keyReleased(KeyEvent ke) {
        NativeAwtListener.keyReleased(this.lowValue, this.highValue, ke);
    }

    private static native void actionPerformed(long lowValue, long highValue, ActionEvent ae);
    private static native void adjustmentValueChanged(long lowValue, long highValue, AdjustmentEvent ae);
    private static native void eventDispatched(long lowValue, long highValue, AWTEvent awte);
    private static native void componentResized(long lowValue, long highValue, ComponentEvent ce);
    private static native void componentMoved(long lowValue, long highValue, ComponentEvent ce);
    private static native void componentShown(long lowValue, long highValue, ComponentEvent ce);
    private static native void componentHidden(long lowValue, long highValue, ComponentEvent ce);
    private static native void componentAdded(long lowValue, long highValue, ContainerEvent ce);
    private static native void componentRemoved(long lowValue, long highValue, ContainerEvent ce);
    private static native void dragGestureRecognized(long lowValue, long highValue, DragGestureEvent dge);
    private static native void dragEnter(long lowValue, long highValue, DragSourceDragEvent dsde);
    private static native void dragOver(long lowValue, long highValue, DragSourceDragEvent dsde);
    private static native void dropActionChanged(long lowValue, long highValue, DragSourceDragEvent dsde);
    private static native void dragExit(long lowValue, long highValue, DragSourceEvent dse);
    private static native void dragDropEnd(long lowValue, long highValue, DragSourceDropEvent dsde);
    private static native void dragMouseMoved(long lowValue, long highValue, DragSourceDragEvent dsde);
    private static native void dragEnter(long lowValue, long highValue, DropTargetDragEvent dtde);
    private static native void dragOver(long lowValue, long highValue, DropTargetDragEvent dtde);
    private static native void dropActionChanged(long lowValue, long highValue, DropTargetDragEvent dtde);
    private static native void dragExit(long lowValue, long highValue, DropTargetEvent dte);
    private static native void drop(long lowValue, long highValue, DropTargetDropEvent dtde);
    private static native void flavorsChanged(long lowValue, long highValue, FlavorEvent fe);
    private static native void focusGained(long lowValue, long highValue, FocusEvent fe);
    private static native void focusLost(long lowValue, long highValue, FocusEvent fe);
    private static native void inputMethodTextChanged(long lowValue, long highValue, InputMethodEvent ime);
    private static native void caretPositionChanged(long lowValue, long highValue, InputMethodEvent ime);
    private static native void mouseClicked(long lowValue, long highValue, MouseEvent me);
    private static native void mousePressed(long lowValue, long highValue, MouseEvent me);
    private static native void mouseReleased(long lowValue, long highValue, MouseEvent me);
    private static native void mouseEntered(long lowValue, long highValue, MouseEvent me);
    private static native void mouseExited(long lowValue, long highValue, MouseEvent me);
    private static native void mouseDragged(long lowValue, long highValue, MouseEvent me);
    private static native void mouseMoved(long lowValue, long highValue, MouseEvent me);
    private static native void mouseWheelMoved(long lowValue, long highValue, MouseWheelEvent mwe);
    private static native void textValueChanged(long lowValue, long highValue, TextEvent te);
    private static native void windowGainedFocus(long lowValue, long highValue, WindowEvent we);
    private static native void windowLostFocus(long lowValue, long highValue, WindowEvent we);
    private static native void windowOpened(long lowValue, long highValue, WindowEvent we);
    private static native void windowClosing(long lowValue, long highValue, WindowEvent we);
    private static native void windowClosed(long lowValue, long highValue, WindowEvent we);
    private static native void windowIconified(long lowValue, long highValue, WindowEvent we);
    private static native void windowDeiconified(long lowValue, long highValue, WindowEvent we);
    private static native void windowActivated(long lowValue, long highValue, WindowEvent we);
    private static native void windowDeactivated(long lowValue, long highValue, WindowEvent we);
    private static native void windowStateChanged(long lowValue, long highValue, WindowEvent we);
    private static native void ancestorMoved(long lowValue, long highValue, HierarchyEvent he);
    private static native void ancestorResized(long lowValue, long highValue, HierarchyEvent he);
    private static native void hierarchyChanged(long lowValue, long highValue, HierarchyEvent he);
    private static native void itemStateChanged(long lowValue, long highValue, ItemEvent ie);
    private static native void keyTyped(long lowValue, long highValue, KeyEvent ke);
    private static native void keyPressed(long lowValue, long highValue, KeyEvent ke);
    private static native void keyReleased(long lowValue, long highValue, KeyEvent ke);
}
