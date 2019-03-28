Imports System.Drawing.Drawing2D
<DoNotObfuscateAttribute(), Serializable()> Friend Class SortOrderForData 
    Friend Enum eDataType
        PackPieces
        NormalGraphicsPath
        ReverseGraphicsPath
        UserPieces
    End Enum
    Friend DataType As New ArrayList()

    Private intPieceCount As Integer
    Private intMousePathCount As Integer
    Private intRevMousePathCount As Integer
    Private intUserPieceCount As Integer

    Friend Sub New()
        
        intPieceCount = 0
        intMousePathCount = 0
        intRevMousePathCount = 0
        intUserPieceCount = 0

    End Sub
    Friend Sub Add(ByVal pPieces As ArrayList, ByVal pMousePath() As GraphicsPath, _
        ByVal pReverseMousePath() As GraphicsPath, ByVal pUserPieces As FacePartStuctureDataFile, _
        ByVal pSortOrderForData As SortOrderForData, ByVal CodePos As String)

        Dim lintArrInc As Integer

        Dim PiecesToAdd As Integer = pPieces.Count - intPieceCount
        For lintArrInc = 1 To PiecesToAdd
            DataType.Add(eDataType.PackPieces)
            intPieceCount += 1
        Next lintArrInc

        Dim NGPToAdd As Integer = pMousePath.GetUpperBound(0) - intMousePathCount
        For lintArrInc = 1 To NGPToAdd
            DataType.Add(eDataType.NormalGraphicsPath)
            intMousePathCount += 1
        Next lintArrInc

        Dim RGPToAdd As Integer = pReverseMousePath.GetUpperBound(0) - intRevMousePathCount
        For lintArrInc = 1 To RGPToAdd
            DataType.Add(eDataType.ReverseGraphicsPath)
            intRevMousePathCount += 1
        Next lintArrInc

        Dim UsPiToAdd As Integer = pUserPieces.Parts.Count - intUserPieceCount
        For lintArrInc = 1 To UsPiToAdd '1 To UsPiToAdd
            DataType.Add(eDataType.UserPieces)
            intUserPieceCount += 1
        Next lintArrInc

        Debug(pPieces, pMousePath, pReverseMousePath, pUserPieces, pSortOrderForData, CodePos, "ADD")

    End Sub
    Friend Sub Remove(ByVal pPieces As ArrayList, ByVal pMousePath() As GraphicsPath, _
        ByVal pReverseMousePath() As GraphicsPath, ByVal pUserPieces As FacePartStuctureDataFile, _
        ByVal pSortOrderForData As SortOrderForData, ByVal CodePos As String)

        DataType.Reverse() 

        Dim lintArrInc As Integer
        Dim PiecesToRemove As Integer = intPieceCount - pPieces.Count
        For lintArrInc = 1 To PiecesToRemove
            DataType.Remove(eDataType.PackPieces)
            intPieceCount -= 1
        Next lintArrInc

        Dim NGPToRemove As Integer = intMousePathCount - pMousePath.GetUpperBound(0)
        For lintArrInc = 1 To NGPToRemove
            DataType.Remove(eDataType.NormalGraphicsPath)
            intMousePathCount -= 1
        Next lintArrInc

        Dim RGPToRemove As Integer = intRevMousePathCount - pReverseMousePath.GetUpperBound(0)
        For lintArrInc = 1 To RGPToRemove
            DataType.Remove(eDataType.ReverseGraphicsPath)
            intRevMousePathCount -= 1
        Next lintArrInc

        Dim UsPiToRemove As Integer = intUserPieceCount - pUserPieces.Parts.Count
        For lintArrInc = 1 To UsPiToRemove  '1 To UsPiToRemove
            DataType.Remove(eDataType.UserPieces)
            intUserPieceCount -= 1
        Next lintArrInc

        DataType.Reverse() 

        Debug(pPieces, pMousePath, pReverseMousePath, pUserPieces, pSortOrderForData, CodePos, "REMOVE")

    End Sub

    Private Sub Debug(ByVal pPieces As ArrayList, ByVal pMousePath() As GraphicsPath, _
       ByVal pReverseMousePath() As GraphicsPath, ByVal pUserPieces As FacePartStuctureDataFile, _
       ByVal pSortOrderForData As SortOrderForData, ByVal CodePos As String, ByVal Func As String)

        Dim lintPieceCount As Integer
        Dim lintMousePathCount As Integer
        Dim lintRevMousePathCount As Integer
        Dim lintUserPieceCount As Integer

        Dim Total As Integer
        Try : lintPieceCount = pPieces.Count : Total += pPieces.Count : Catch : Total += 0 : End Try
        Try : lintMousePathCount = pMousePath.GetUpperBound(0) : Total += pMousePath.GetUpperBound(0) : Catch : Total += 0 : End Try
        Try : lintRevMousePathCount = pReverseMousePath.GetUpperBound(0) : Total += pReverseMousePath.GetUpperBound(0) : Catch : Total += 0 : End Try
        Try : lintUserPieceCount = pUserPieces.Parts.Count : Total += pUserPieces.Parts.Count : Catch : Total += 0 : End Try

        Dim SO As Integer
        Try : SO = pSortOrderForData.DataType.Count : Catch : SO = 0 : End Try

        If Total <> SO Then
            Console.WriteLine("---")
            Console.WriteLine(SO & " = " & lintPieceCount & " " & lintMousePathCount & " " & lintRevMousePathCount & _
                " " & lintUserPieceCount & " = " & CodePos & " " & Func)

            Console.WriteLine("")
            Dim lintArrInc As Integer
            For lintArrInc = 0 To pSortOrderForData.DataType.Count - 1
                Select Case pSortOrderForData.DataType(lintArrInc)
                    Case 0
                        Console.Write("PackPieces ")
                    Case 1
                        Console.Write("NormalGraphicsPath ")
                    Case 2
                        Console.Write("ReverseGraphicsPath ")
                    Case 3
                        Console.Write("UserPieces ")
                End Select
            Next lintArrInc
            Console.WriteLine("---")
        End If

    End Sub
End Class