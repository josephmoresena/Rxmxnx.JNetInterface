package com.rxmxnx.jnetinterface;

import java.beans.PropertyChangeListener;
import javax.swing.Action;
import javax.swing.event.*;
import javax.swing.text.Document;
import javax.swing.text.Element;
import javax.swing.tree.ExpandVetoException;

public final class NativeSwingListener extends NativeAwtListener implements
        Action, AncestorListener, CaretListener, CellEditorListener, ChangeListener, 
        DocumentEvent, DocumentEvent.ElementChange, DocumentListener, HyperlinkListener, 
        InternalFrameListener, ListDataListener, ListSelectionListener, MenuDragMouseListener, 
        MenuKeyListener, MenuListener, MouseInputListener, PopupMenuListener, RowSorterListener, 
        TableColumnModelListener, TableModelListener, TreeExpansionListener, TreeModelListener, 
        TreeSelectionListener, TreeWillExpandListener, UndoableEditListener {
    
    public NativeSwingListener(long lowValue, long highValue) {
        super(lowValue, highValue);
    }

    @Override
    public final Object getValue(String string) {
        return NativeSwingListener.getValue(this.lowValue, this.highValue, string);
    }
    @Override
    public final void putValue(String string, Object o) {
        NativeSwingListener.putValue(this.lowValue, this.highValue, string, o);
    }
    @Override
    public final void setEnabled(boolean bln) {
        NativeSwingListener.setEnabled(this.lowValue, this.highValue, bln);
    }
    @Override
    public final boolean isEnabled() {
        return NativeSwingListener.isEnabled(this.lowValue, this.highValue);
    }
    @Override
    public final void addPropertyChangeListener(PropertyChangeListener pl) {
        NativeSwingListener.addPropertyChangeListener(this.lowValue, this.highValue, pl);
    }
    @Override
    public final void removePropertyChangeListener(PropertyChangeListener pl) {
        NativeSwingListener.removePropertyChangeListener(this.lowValue, this.highValue, pl);
    }
    @Override
    public final void ancestorAdded(AncestorEvent ae) {
        NativeSwingListener.ancestorAdded(this.lowValue, this.highValue, ae);
    }
    @Override
    public final void ancestorRemoved(AncestorEvent ae) {
        NativeSwingListener.ancestorRemoved(this.lowValue, this.highValue, ae);
    }
    @Override
    public final void ancestorMoved(AncestorEvent ae) {
        NativeSwingListener.ancestorMoved(this.lowValue, this.highValue, ae);
    }
    @Override
    public final void caretUpdate(CaretEvent ce) {
        NativeSwingListener.caretUpdate(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void editingStopped(ChangeEvent ce) {
        NativeSwingListener.editingStopped(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void editingCanceled(ChangeEvent ce) {
        NativeSwingListener.editingCanceled(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void stateChanged(ChangeEvent ce) {
        NativeSwingListener.stateChanged(this.lowValue, this.highValue, ce);
    }
    @Override
    public final ElementChange getChange(Element elmnt) {
        return NativeSwingListener.getChange(this.lowValue, this.highValue, elmnt);
    }
    @Override
    public final void insertUpdate(DocumentEvent de) {
        NativeSwingListener.insertUpdate(this.lowValue, this.highValue, de);
    }
    @Override
    public final void removeUpdate(DocumentEvent de) {
        NativeSwingListener.removeUpdate(this.lowValue, this.highValue, de);
    }
    @Override
    public final void changedUpdate(DocumentEvent de) {
        NativeSwingListener.changedUpdate(this.lowValue, this.highValue, de);
    }
    @Override
    public final void hyperlinkUpdate(HyperlinkEvent he) {
        NativeSwingListener.hyperlinkUpdate(this.lowValue, this.highValue, he);
    }
    @Override
    public final void internalFrameOpened(InternalFrameEvent ife) {
        NativeSwingListener.internalFrameOpened(this.lowValue, this.highValue, ife);
    }
    @Override
    public final void internalFrameClosing(InternalFrameEvent ife) {
        NativeSwingListener.internalFrameClosing(this.lowValue, this.highValue, ife);
    }
    @Override
    public final void internalFrameClosed(InternalFrameEvent ife) {
        NativeSwingListener.internalFrameClosed(this.lowValue, this.highValue, ife);
    }
    @Override
    public final void internalFrameIconified(InternalFrameEvent ife) {
        NativeSwingListener.internalFrameIconified(this.lowValue, this.highValue, ife);
    }
    @Override
    public final void internalFrameDeiconified(InternalFrameEvent ife) {
        NativeSwingListener.internalFrameDeiconified(this.lowValue, this.highValue, ife);
    }
    @Override
    public final void internalFrameActivated(InternalFrameEvent ife) {
        NativeSwingListener.internalFrameActivated(this.lowValue, this.highValue, ife);
    }
    @Override
    public final void internalFrameDeactivated(InternalFrameEvent ife) {
        NativeSwingListener.internalFrameDeactivated(this.lowValue, this.highValue, ife);
    }
    @Override
    public final void intervalAdded(ListDataEvent lde) {
        NativeSwingListener.intervalAdded(this.lowValue, this.highValue, lde);
    }
    @Override
    public final void intervalRemoved(ListDataEvent lde) {
        NativeSwingListener.intervalRemoved(this.lowValue, this.highValue, lde);
    }
    @Override
    public final void contentsChanged(ListDataEvent lde) {
        NativeSwingListener.contentsChanged(this.lowValue, this.highValue, lde);
    }
    @Override
    public final void valueChanged(ListSelectionEvent lse) {
        NativeSwingListener.valueChanged(this.lowValue, this.highValue, lse);
    }
    @Override
    public final void menuDragMouseEntered(MenuDragMouseEvent mdme) {
        NativeSwingListener.menuDragMouseEntered(this.lowValue, this.highValue, mdme);
    }
    @Override
    public final void menuDragMouseExited(MenuDragMouseEvent mdme) {
        NativeSwingListener.menuDragMouseExited(this.lowValue, this.highValue, mdme);
    }
    @Override
    public final void menuDragMouseDragged(MenuDragMouseEvent mdme) {
        NativeSwingListener.menuDragMouseDragged(this.lowValue, this.highValue, mdme);
    }
    @Override
    public final void menuDragMouseReleased(MenuDragMouseEvent mdme) {
        NativeSwingListener.menuDragMouseReleased(this.lowValue, this.highValue, mdme);
    }
    @Override
    public final void menuKeyTyped(MenuKeyEvent mke) {
        NativeSwingListener.menuKeyTyped(this.lowValue, this.highValue, mke);
    }
    @Override
    public final void menuKeyPressed(MenuKeyEvent mke) {
        NativeSwingListener.menuKeyPressed(this.lowValue, this.highValue, mke);
    }
    @Override
    public final void menuKeyReleased(MenuKeyEvent mke) {
        NativeSwingListener.menuKeyReleased(this.lowValue, this.highValue, mke);
    }
    @Override
    public final void menuSelected(MenuEvent me) {
        NativeSwingListener.menuSelected(this.lowValue, this.highValue, me);
    }
    @Override
    public final void menuDeselected(MenuEvent me) {
        NativeSwingListener.menuDeselected(this.lowValue, this.highValue, me);
    }
    @Override
    public final void menuCanceled(MenuEvent me) {
        NativeSwingListener.menuCanceled(this.lowValue, this.highValue, me);
    }
    @Override
    public final void popupMenuWillBecomeVisible(PopupMenuEvent pme) {
        NativeSwingListener.popupMenuWillBecomeVisible(this.lowValue, this.highValue, pme);
    }
    @Override
    public final void popupMenuWillBecomeInvisible(PopupMenuEvent pme) {
        NativeSwingListener.popupMenuWillBecomeInvisible(this.lowValue, this.highValue, pme);
    }
    @Override
    public final void popupMenuCanceled(PopupMenuEvent pme) {
        NativeSwingListener.popupMenuCanceled(this.lowValue, this.highValue, pme);
    }
    @Override
    public final void sorterChanged(RowSorterEvent rse) {
        NativeSwingListener.sorterChanged(this.lowValue, this.highValue, rse);
    }
    @Override
    public final void columnAdded(TableColumnModelEvent tcme) {
        NativeSwingListener.columnAdded(this.lowValue, this.highValue, tcme);
    }
    @Override
    public final void columnRemoved(TableColumnModelEvent tcme) {
        NativeSwingListener.columnRemoved(this.lowValue, this.highValue, tcme);
    }
    @Override
    public final void columnMoved(TableColumnModelEvent tcme) {
        NativeSwingListener.columnMoved(this.lowValue, this.highValue, tcme);
    }
    @Override
    public final void columnMarginChanged(ChangeEvent ce) {
        NativeSwingListener.columnMarginChanged(this.lowValue, this.highValue, ce);
    }
    @Override
    public final void columnSelectionChanged(ListSelectionEvent lse) {
        NativeSwingListener.columnSelectionChanged(this.lowValue, this.highValue, lse);
    }
    @Override
    public final void tableChanged(TableModelEvent tme) {
        NativeSwingListener.tableChanged(this.lowValue, this.highValue, tme);
    }
    @Override
    public final void treeExpanded(TreeExpansionEvent tee) {
        NativeSwingListener.treeExpanded(this.lowValue, this.highValue, tee);
    }
    @Override
    public final void treeCollapsed(TreeExpansionEvent tee) {
        NativeSwingListener.treeCollapsed(this.lowValue, this.highValue, tee);
    }
    @Override
    public final void treeNodesChanged(TreeModelEvent tme) {
        NativeSwingListener.treeNodesChanged(this.lowValue, this.highValue, tme);
    }
    @Override
    public final void treeNodesInserted(TreeModelEvent tme) {
        NativeSwingListener.treeNodesInserted(this.lowValue, this.highValue, tme);
    }
    @Override
    public final void treeNodesRemoved(TreeModelEvent tme) {
        NativeSwingListener.treeNodesRemoved(this.lowValue, this.highValue, tme);
    }
    @Override
    public final void treeStructureChanged(TreeModelEvent tme) {
        NativeSwingListener.treeStructureChanged(this.lowValue, this.highValue, tme);
    }
    @Override
    public final void valueChanged(TreeSelectionEvent tse) {
        NativeSwingListener.valueChanged(this.lowValue, this.highValue, tse);
    }
    @Override
    public final void treeWillExpand(TreeExpansionEvent tee) throws ExpandVetoException {
        NativeSwingListener.treeWillExpand(this.lowValue, this.highValue, tee);
    }
    @Override
    public final void treeWillCollapse(TreeExpansionEvent tee) throws ExpandVetoException {
        NativeSwingListener.treeWillCollapse(this.lowValue, this.highValue, tee);
    }
    @Override
    public final void undoableEditHappened(UndoableEditEvent uee) {
        NativeSwingListener.undoableEditHappened(this.lowValue, this.highValue, uee);
    }
    @Override
    public final int getOffset() {
        return NativeSwingListener.getOffset(this.lowValue, this.highValue);
    }
    @Override
    public final int getLength() {
        return NativeSwingListener.getLength(this.lowValue, this.highValue);
    }
    @Override
    public final Document getDocument() {
        return NativeSwingListener.getDocument(this.lowValue, this.highValue);
    }
    @Override
    public final EventType getType() {
        return NativeSwingListener.getType(this.lowValue, this.highValue);
    }
    @Override
    public final Element getElement() {
        return NativeSwingListener.getElement(this.lowValue, this.highValue);
    }
    @Override
    public final int getIndex() {
        return NativeSwingListener.getIndex(this.lowValue, this.highValue);
    }
    @Override
    public final Element[] getChildrenRemoved() {
        return NativeSwingListener.getChildrenRemoved(this.lowValue, this.highValue);
    }
    @Override
    public final Element[] getChildrenAdded() {
        return NativeSwingListener.getChildrenAdded(this.lowValue, this.highValue);
    }
    
    private static native Object getValue(long lowValue, long highValue, String string);
    private static native void putValue(long lowValue, long highValue, String string, Object o);
    private static native void setEnabled(long lowValue, long highValue, boolean bln);
    private static native boolean isEnabled(long lowValue, long highValue);
    private static native void addPropertyChangeListener(long lowValue, long highValue, PropertyChangeListener pl);
    private static native void removePropertyChangeListener(long lowValue, long highValue, PropertyChangeListener pl);
    private static native void ancestorAdded(long lowValue, long highValue, AncestorEvent ae);
    private static native void ancestorRemoved(long lowValue, long highValue, AncestorEvent ae);
    private static native void ancestorMoved(long lowValue, long highValue, AncestorEvent ae);
    private static native void caretUpdate(long lowValue, long highValue, CaretEvent ce);
    private static native void editingStopped(long lowValue, long highValue, ChangeEvent ce);
    private static native void editingCanceled(long lowValue, long highValue, ChangeEvent ce);
    private static native void stateChanged(long lowValue, long highValue, ChangeEvent ce);
    private static native ElementChange getChange(long lowValue, long highValue, Element elmnt);
    private static native void insertUpdate(long lowValue, long highValue, DocumentEvent de);
    private static native void removeUpdate(long lowValue, long highValue, DocumentEvent de);
    private static native void changedUpdate(long lowValue, long highValue, DocumentEvent de);
    private static native void hyperlinkUpdate(long lowValue, long highValue, HyperlinkEvent he);
    private static native void internalFrameOpened(long lowValue, long highValue, InternalFrameEvent ife);
    private static native void internalFrameClosing(long lowValue, long highValue, InternalFrameEvent ife);
    private static native void internalFrameClosed(long lowValue, long highValue, InternalFrameEvent ife);
    private static native void internalFrameIconified(long lowValue, long highValue, InternalFrameEvent ife);
    private static native void internalFrameDeiconified(long lowValue, long highValue, InternalFrameEvent ife);
    private static native void internalFrameActivated(long lowValue, long highValue, InternalFrameEvent ife);
    private static native void internalFrameDeactivated(long lowValue, long highValue, InternalFrameEvent ife);
    private static native void intervalAdded(long lowValue, long highValue, ListDataEvent lde);
    private static native void intervalRemoved(long lowValue, long highValue, ListDataEvent lde);
    private static native void contentsChanged(long lowValue, long highValue, ListDataEvent lde);
    private static native void valueChanged(long lowValue, long highValue, ListSelectionEvent lse);
    private static native void menuDragMouseEntered(long lowValue, long highValue, MenuDragMouseEvent mdme);
    private static native void menuDragMouseExited(long lowValue, long highValue, MenuDragMouseEvent mdme);
    private static native void menuDragMouseDragged(long lowValue, long highValue, MenuDragMouseEvent mdme);
    private static native void menuDragMouseReleased(long lowValue, long highValue, MenuDragMouseEvent mdme);
    private static native void menuKeyTyped(long lowValue, long highValue, MenuKeyEvent mke);
    private static native void menuKeyPressed(long lowValue, long highValue, MenuKeyEvent mke);
    private static native void menuKeyReleased(long lowValue, long highValue, MenuKeyEvent mke);
    private static native void menuSelected(long lowValue, long highValue, MenuEvent me);
    private static native void menuDeselected(long lowValue, long highValue, MenuEvent me);
    private static native void menuCanceled(long lowValue, long highValue, MenuEvent me);
    private static native void popupMenuWillBecomeVisible(long lowValue, long highValue, PopupMenuEvent pme);
    private static native void popupMenuWillBecomeInvisible(long lowValue, long highValue, PopupMenuEvent pme);
    private static native void popupMenuCanceled(long lowValue, long highValue, PopupMenuEvent pme);
    private static native void sorterChanged(long lowValue, long highValue, RowSorterEvent rse);
    private static native void columnAdded(long lowValue, long highValue, TableColumnModelEvent tcme);
    private static native void columnRemoved(long lowValue, long highValue, TableColumnModelEvent tcme);
    private static native void columnMoved(long lowValue, long highValue, TableColumnModelEvent tcme);
    private static native void columnMarginChanged(long lowValue, long highValue, ChangeEvent ce);
    private static native void columnSelectionChanged(long lowValue, long highValue, ListSelectionEvent lse);
    private static native void tableChanged(long lowValue, long highValue, TableModelEvent tme);
    private static native void treeExpanded(long lowValue, long highValue, TreeExpansionEvent tee);
    private static native void treeCollapsed(long lowValue, long highValue, TreeExpansionEvent tee);
    private static native void treeNodesChanged(long lowValue, long highValue, TreeModelEvent tme);
    private static native void treeNodesInserted(long lowValue, long highValue, TreeModelEvent tme);
    private static native void treeNodesRemoved(long lowValue, long highValue, TreeModelEvent tme);
    private static native void treeStructureChanged(long lowValue, long highValue, TreeModelEvent tme);
    private static native void valueChanged(long lowValue, long highValue, TreeSelectionEvent tse);
    private static native void treeWillExpand(long lowValue, long highValue, TreeExpansionEvent tee) throws ExpandVetoException;
    private static native void treeWillCollapse(long lowValue, long highValue, TreeExpansionEvent tee) throws ExpandVetoException;
    private static native void undoableEditHappened(long lowValue, long highValue, UndoableEditEvent uee);
    private static native int getOffset(long lowValue, long highValue);
    private static native int getLength(long lowValue, long highValue);
    private static native Document getDocument(long lowValue, long highValue);
    private static native EventType getType(long lowValue, long highValue);
    private static native Element getElement(long lowValue, long highValue);
    private static native int getIndex(long lowValue, long highValue);
    private static native Element[] getChildrenRemoved(long lowValue, long highValue);
    private static native Element[] getChildrenAdded(long lowValue, long highValue);
}
