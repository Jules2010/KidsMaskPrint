Imports System.Drawing.Drawing2D
Friend Class Drawings
    Private Class UndoRedo
        Friend CurColIdx As Integer
        Friend mousePath() As Drawing2D.GraphicsPath
        Friend PB() As PaintBrush
    End Class

    Public mousePath(0) As GraphicsPath
    Public ReversemousePath(0) As GraphicsPath

    Dim lCurColIdx As Integer = 0
    Dim lCurReverseColIdx As Integer = 0

    Dim lLastRevColour As Color = Color.AliceBlue

    Public lPaintBrush(0) As PaintBrush
    Public lPaintReverseBrush(0) As PaintBrush

    Dim RedoNormalArr As New Collections.Stack()
    Dim RedoReverseArr As New Collections.Stack()

    Dim lintDrawingInProgress As Integer

    Dim lintLastNormalXCoord As Integer
    Dim lintLastNormalYCoord As Integer
    Dim lintLastReverseXCoord As Integer
    Dim lintLastReverseYCoord As Integer
    Friend Sub New(ByVal CurrentBrushColour As Color, ByVal CurrentBrushWidth As Integer)

        mousePath(0) = New GraphicsPath()
        ReversemousePath(0) = New GraphicsPath()

        lPaintBrush(0) = New PaintBrush()
        lPaintBrush(0).BrushColour = CurrentBrushColour 
        lPaintBrush(0).BrushWidth = CurrentBrushWidth 

        lPaintReverseBrush(0) = New PaintBrush()
        lPaintReverseBrush(0).BrushColour = CurrentBrushColour 
        lPaintReverseBrush(0).BrushWidth = CurrentBrushWidth 

        lLastRevColour = lPaintBrush(lCurColIdx).BrushColour

    End Sub
    Friend Sub MouseDownClick(ByVal withMirror As Boolean, ByVal CurrentBrushWidth As Integer)
        Try : mousePath(lCurColIdx).StartFigure() : Catch : End Try
        If withMirror = True Then
            If Color.op_Equality(lLastRevColour, lPaintBrush(lCurColIdx).BrushColour) = False Then
                lLastRevColour = lPaintBrush(lCurColIdx).BrushColour

                lCurReverseColIdx += 1
                ReDim Preserve lPaintReverseBrush(lCurReverseColIdx)
                lPaintReverseBrush(lCurReverseColIdx) = New PaintBrush()

                lPaintReverseBrush(lCurReverseColIdx).BrushColour = lLastRevColour
                lPaintReverseBrush(lCurReverseColIdx).BrushWidth = CurrentBrushWidth 

                ReDim Preserve ReversemousePath(lCurReverseColIdx)
                ReversemousePath(lCurReverseColIdx) = New GraphicsPath()
            End If
            Try : ReversemousePath(lCurReverseColIdx).StartFigure() : Catch : End Try
        End If
    End Sub
    Friend Sub MouseMoveClick(ByVal e As System.Windows.Forms.MouseEventArgs, ByVal withMirror As Boolean)
        Try
            lintDrawingInProgress = 1
            mousePath(lCurColIdx).AddLine(e.X, e.Y, e.X, e.Y)
            lintLastNormalXCoord = e.X : lintLastNormalYCoord = e.Y 

            If withMirror Then 
                Dim x As Single = ((e.X - (e.X * 2)) + 509)
                lintDrawingInProgress = 2
                ReversemousePath(lCurReverseColIdx).AddLine(x, e.Y, x, (e.Y))
                lintLastReverseXCoord = e.X : lintLastReverseYCoord = e.Y 

            End If 
        Catch : End Try
    End Sub
    Friend Sub MouseUP(ByVal CurrentBrushWidth As Integer)

        If lintDrawingInProgress > 0 Then
            lLastRevColour = lPaintBrush(lCurColIdx).BrushColour

            lCurColIdx += 1
            ReDim Preserve mousePath(lCurColIdx)
            mousePath(lCurColIdx) = New GraphicsPath()
            mousePath(lCurColIdx).StartFigure()
            ReDim Preserve lPaintBrush(lCurColIdx)
            lPaintBrush(lCurColIdx) = New PaintBrush()
            lPaintBrush(lCurColIdx).BrushColour = lLastRevColour
            lPaintBrush(lCurColIdx).BrushWidth = CurrentBrushWidth 

            lCurReverseColIdx += 1
            ReDim Preserve ReversemousePath(lCurReverseColIdx)
            ReversemousePath(lCurReverseColIdx) = New GraphicsPath()
            ReversemousePath(lCurReverseColIdx).StartFigure()
            ReDim Preserve lPaintReverseBrush(lCurReverseColIdx)
            lPaintReverseBrush(lCurReverseColIdx) = New PaintBrush()
            lPaintReverseBrush(lCurReverseColIdx).BrushColour = lLastRevColour
            lPaintReverseBrush(lCurReverseColIdx).BrushWidth = CurrentBrushWidth 

            lintDrawingInProgress = 0
        End If

    End Sub
    Friend Sub Undo(ByVal CurrentBrushWidth As Integer)

        Try : mousePath(lCurColIdx).Reset() : Catch : End Try
        Try : ReversemousePath(lCurReverseColIdx).Reset() : Catch : End Try

        If lCurColIdx > 0 Then
            '  Try : RedoArr.Dequeue() : Catch : End Try
            Dim l_UndoRedo As UndoRedo
            l_UndoRedo = New UndoRedo()
            With l_UndoRedo
                .CurColIdx = lCurColIdx
                .mousePath = mousePath '.Clone
                .PB = lPaintBrush
            End With

            RedoNormalArr.Push(l_UndoRedo)

            lLastRevColour = lPaintBrush(lCurColIdx).BrushColour
            lCurColIdx -= 1

            ReDim Preserve mousePath(lCurColIdx)
            mousePath(lCurColIdx) = New GraphicsPath()
            ReDim Preserve lPaintBrush(lCurColIdx)
            lPaintBrush(lCurColIdx) = New PaintBrush()
            lPaintBrush(lCurColIdx).BrushColour = lLastRevColour
            lPaintBrush(lCurColIdx).BrushWidth = CurrentBrushWidth 

        End If

        If lCurReverseColIdx > 0 Then

            Dim l_UndoRedo As UndoRedo
            l_UndoRedo = New UndoRedo()
            With l_UndoRedo
                .CurColIdx = lCurReverseColIdx
                .mousePath = ReversemousePath '.Clone
                .PB = lPaintReverseBrush
            End With

            RedoReverseArr.Push(l_UndoRedo)

            'CanRedo = True

            lCurReverseColIdx -= 1
            ReDim Preserve ReversemousePath(lCurReverseColIdx)
            ReversemousePath(lCurReverseColIdx) = New GraphicsPath()
            ReDim Preserve lPaintReverseBrush(lCurReverseColIdx)
            lPaintReverseBrush(lCurReverseColIdx) = New PaintBrush()
            lPaintReverseBrush(lCurReverseColIdx).BrushColour = lLastRevColour
            lPaintReverseBrush(lCurReverseColIdx).BrushWidth = CurrentBrushWidth 
        End If

        'ChangeUndoRedoStatus(CanUndo, CanRedo)

    End Sub
    Friend Sub ChangeUndoRedoStatus(ByRef CanUndo As Boolean, ByRef CanRedo As Boolean)

        If lCurColIdx = 0 And lCurReverseColIdx = 0 Then
            CanUndo = False
        Else
            CanUndo = True
        End If

        If RedoNormalArr.Count = 0 And RedoReverseArr.Count = 0 Then
            CanRedo = False
        Else
            CanRedo = True
        End If

    End Sub
    Friend Function Redo() As Boolean

        Try 
            Dim l_UndoRedo As UndoRedo
            l_UndoRedo = GetLastRedo(True)

            With l_UndoRedo
                lCurColIdx = .CurColIdx
                mousePath = .mousePath
                lPaintBrush = .PB
            End With

            l_UndoRedo = Nothing
        Catch 
        End Try

        Try 
            Dim l_RevUndoRedo As UndoRedo
            l_RevUndoRedo = GetLastRedo(False)

            With l_RevUndoRedo
                lCurReverseColIdx = .CurColIdx
                ReversemousePath = .mousePath
                lPaintReverseBrush = .PB
            End With

            l_RevUndoRedo = Nothing

        Catch 
        End Try

        'ChangeUndoRedoStatus(CanUndo, CanRedo)

    End Function
    Private Function GetLastRedo(ByVal pbooNormal As Boolean) As UndoRedo

        Dim lRempt As New UndoRedo()

        Dim EnumerShowNow As System.Collections.IEnumerator
        If pbooNormal = True Then
            EnumerShowNow = RedoNormalArr.GetEnumerator()
        Else
            EnumerShowNow = RedoReverseArr.GetEnumerator()
        End If

        EnumerShowNow.MoveNext()
        lRempt = EnumerShowNow.Current

        If pbooNormal = True Then
            RedoNormalArr.Pop()
        Else
            RedoReverseArr.Pop()
        End If

        Return lRempt

    End Function
    Friend Sub SetColour(ByVal pColour As Color, ByVal CurrentBrushWidth As Integer)

        lCurColIdx += 1
        ReDim Preserve mousePath(lCurColIdx)
        mousePath(lCurColIdx) = New GraphicsPath()
        ReDim Preserve lPaintBrush(lCurColIdx)
        lPaintBrush(lCurColIdx) = New PaintBrush()
        lPaintBrush(lCurColIdx).BrushColour = pColour
        lPaintBrush(lCurColIdx).BrushWidth = CurrentBrushWidth 

    End Sub
    Friend Sub PreSave(ByVal CurrentBrushColour As Color, ByVal CurrentBrushWidth As Integer)

        lCurColIdx += 1
        ReDim Preserve mousePath(lCurColIdx)
        mousePath(lCurColIdx) = New GraphicsPath()
        ReDim Preserve lPaintBrush(lCurColIdx)
        lPaintBrush(lCurColIdx) = New PaintBrush()
        lPaintBrush(lCurColIdx).BrushColour = CurrentBrushColour 

        lPaintBrush(lCurColIdx).BrushWidth = CurrentBrushWidth 

        mousePath(lCurColIdx).StartFigure()
        mousePath(lCurColIdx).AddLine(lintLastNormalXCoord, lintLastNormalYCoord, lintLastNormalXCoord, lintLastNormalYCoord)

        lCurReverseColIdx += 1
        ReDim Preserve ReversemousePath(lCurReverseColIdx)
        ReversemousePath(lCurReverseColIdx) = New GraphicsPath()
        ReDim Preserve lPaintReverseBrush(lCurReverseColIdx)
        lPaintReverseBrush(lCurReverseColIdx) = New PaintBrush()
        lPaintReverseBrush(lCurReverseColIdx).BrushColour = CurrentBrushColour 

        lPaintReverseBrush(lCurReverseColIdx).BrushWidth = CurrentBrushWidth 

        ReversemousePath(lCurReverseColIdx).StartFigure()
        ReversemousePath(lCurReverseColIdx).AddLine(lintLastReverseXCoord, lintLastReverseYCoord, lintLastReverseXCoord, lintLastReverseYCoord)

    End Sub
    Friend Sub setCountersAfterLoad(ByVal CurrentBrushColour As Color, ByVal CurrentBrushWidth As Integer)

        lCurColIdx = mousePath.GetUpperBound(0)
        lCurReverseColIdx = ReversemousePath.GetUpperBound(0)

        'add brush stroke here
        If mousePath(lCurColIdx) Is Nothing Then
            'MessageBox.Show("mousePath(lCurColIdx) Is Nothing " & lCurColIdx)
            lCurColIdx -= 1
            If lCurColIdx = -1 Then
                lCurColIdx = 0
                ReDim mousePath(lCurColIdx)
                mousePath(lCurColIdx) = New GraphicsPath()
                ReDim lPaintBrush(lCurColIdx)
            End If
            lPaintBrush(lCurColIdx) = New PaintBrush()
            lPaintBrush(lCurColIdx).BrushColour = CurrentBrushColour 
            lPaintBrush(lCurColIdx).BrushWidth = CurrentBrushWidth 

        End If
        If ReversemousePath(lCurReverseColIdx) Is Nothing Then
            'MessageBox.Show("ReversemousePath(lCurReverseColIdx) Is Nothing " & lCurReverseColIdx)
            lCurReverseColIdx -= 1
            If lCurReverseColIdx = -1 Then
                lCurReverseColIdx = 0
                ReDim ReversemousePath(lCurReverseColIdx)
                ReversemousePath(lCurReverseColIdx) = New GraphicsPath()
                ReDim lPaintReverseBrush(lCurReverseColIdx)
            End If
            lPaintReverseBrush(lCurReverseColIdx) = New PaintBrush()
            lPaintReverseBrush(lCurReverseColIdx).BrushColour = CurrentBrushColour 

            lPaintReverseBrush(lCurReverseColIdx).BrushWidth = CurrentBrushWidth 

        End If

    End Sub
    Friend Sub Clear(ByVal CurrentBrushColour As Color, ByVal CurrentBrushWidth As Integer)

        lCurColIdx = 0
        lCurReverseColIdx = 0
        ReDim mousePath(0)
        mousePath(0) = New GraphicsPath()
        ReDim ReversemousePath(0)
        ReversemousePath(0) = New GraphicsPath()
        ReDim lPaintBrush(0)
        ReDim lPaintReverseBrush(0)
        lPaintBrush(0) = New PaintBrush()
        lPaintBrush(0).BrushColour = CurrentBrushColour 

        lPaintBrush(0).BrushWidth = CurrentBrushWidth 

        lPaintReverseBrush(0) = New PaintBrush()
        lPaintReverseBrush(0).BrushColour = CurrentBrushColour 
        lPaintReverseBrush(0).BrushWidth = CurrentBrushWidth 

    End Sub
End Class
