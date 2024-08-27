Imports Rxmxnx.JNetInterface.Io
Imports Rxmxnx.JNetInterface.Lang
Imports Rxmxnx.JNetInterface.Lang.Annotation
Imports Rxmxnx.JNetInterface.Lang.Reflect
Imports Rxmxnx.JNetInterface.Native
Imports Rxmxnx.JNetInterface.Primitives
Imports Rxmxnx.JNetInterface.Types
Imports Rxmxnx.JNetInterface.Types.Metadata

Partial Module Program
    Private Sub PrintMetadataInfo()
        PrintBuiltInMetadata()
        PrintArrayMetadata(JArrayObject (Of JBoolean).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JByte).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JChar).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JDouble).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JFloat).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JInt).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JLong).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JShort).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JLocalObject).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JClassObject).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JThrowableObject).Metadata, 5)
        PrintArrayMetadata(JArrayObject (Of JStringObject).Metadata, 5)
    End Sub

    Private Sub PrintArrayMetadata(arrMetadata As JArrayTypeMetadata, dimension As Integer)
        Console.WriteLine(arrMetadata.ElementMetadata.Signature)
        Dim stopMetadata = False

        For i = 0 To dimension - 1
            Console.WriteLine(arrMetadata.Signature)
            Dim arrMet2 As JArrayTypeMetadata = arrMetadata.GetArrayMetadata()
            If arrMet2 Is Nothing Then
                stopMetadata = True
                Goto FinalPrint
            End If
            arrMetadata = arrMet2
        Next

        FinalPrint:
        If Not stopMetadata Then
            Console.WriteLine(arrMetadata.Signature)
        End If

        PrintNestedArrayMetadata(arrMetadata)
    End Sub

    Private Sub PrintNestedArrayMetadata(arrMetadata As JArrayTypeMetadata,
                                         Optional ByVal printCurrent As Boolean = False)
        If printCurrent AndAlso arrMetadata IsNot Nothing Then
            Console.WriteLine(arrMetadata.Signature)
        End If

        While arrMetadata IsNot Nothing
            Console.WriteLine(arrMetadata.ElementMetadata.Signature)
            arrMetadata = TryCast(arrMetadata.ElementMetadata, JArrayTypeMetadata)
        End While
    End Sub

    Private Sub PrintBuiltInMetadata()
        Console.WriteLine("====== Primitive types ======")
        Console.WriteLine(JPrimitiveTypeMetadata.VoidMetadata)
        Console.WriteLine(IDataType.GetMetadata (Of JBoolean)())
        Console.WriteLine(IDataType.GetMetadata (Of JByte)())
        Console.WriteLine(IDataType.GetMetadata (Of JChar)())
        Console.WriteLine(IDataType.GetMetadata (Of JShort)())
        Console.WriteLine(IDataType.GetMetadata (Of JInt)())
        Console.WriteLine(IDataType.GetMetadata (Of JLong)())
        Console.WriteLine(IDataType.GetMetadata (Of JFloat)())
        Console.WriteLine(IDataType.GetMetadata (Of JDouble)())

        Console.WriteLine("====== Reference types ======")
        Console.WriteLine(IDataType.GetMetadata (Of JLocalObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JClassObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JStringObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JEnumObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JNumberObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JThrowableObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JStackTraceElementObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JExceptionObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JRuntimeExceptionObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JErrorObject)())

        Console.WriteLine("====== Wrapper types ======")
        Console.WriteLine(IDataType.GetMetadata (Of JVoidObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JBooleanObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JByteObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JDoubleObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JFloatObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JIntegerObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JCharacterObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JLongObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JShortObject)())

        Console.WriteLine("====== Array types ======")
        Console.WriteLine(IDataType.GetMetadata (Of JArrayObject(Of JBoolean))())
        Console.WriteLine(IDataType.GetMetadata (Of JArrayObject(Of JByte))())
        Console.WriteLine(IDataType.GetMetadata (Of JArrayObject(Of JChar))())
        Console.WriteLine(IDataType.GetMetadata (Of JArrayObject(Of JShort))())
        Console.WriteLine(IDataType.GetMetadata (Of JArrayObject(Of JInt))())
        Console.WriteLine(IDataType.GetMetadata (Of JArrayObject(Of JLong))())
        Console.WriteLine(IDataType.GetMetadata (Of JArrayObject(Of JFloat))())
        Console.WriteLine(IDataType.GetMetadata (Of JArrayObject(Of JDouble))())

        Console.WriteLine("====== Interfaces types ======")
        Console.WriteLine(IDataType.GetMetadata (Of JCharSequenceObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JCloneableObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JComparableObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JSerializableObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JAnnotatedElementObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JGenericDeclarationObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JTypeObject)())
        Console.WriteLine(IDataType.GetMetadata (Of JAnnotationObject)())
    End Sub
End Module