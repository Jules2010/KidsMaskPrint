Imports System.Drawing
Imports System.Drawing.Drawing2D

Friend Module DrawOutput
    Dim ErrCount As Integer = 1 
    Friend Sub DrawOutput(ByVal RetG As Graphics, ByVal pbooPagePrint As Boolean, ByVal PictureBox1 As PictureBox, _
        ByVal mousePath() As GraphicsPath, ByVal ReversemousePath() As GraphicsPath, _
        ByVal pSize As Single, ByVal ThisColour As Color, ByVal Mirrored As Boolean, ByVal Guided As Boolean, _
        ByVal pPieces As ArrayList, ByVal pOffSet As Point, ByVal pBrush() As PaintBrush, ByVal pReverseBrush() As PaintBrush, _
        ByVal lUserPieces As FacePartStuctureDataFile, ByVal pSortOrderForData As SortOrderForData)

        'lUserPieces added 

        Dim errpos As Integer
        Dim lintArrInc As Integer 


        Try ' error trapping

            If pbooPagePrint = True Then
                '----------------------------

                Dim m_zoom As Single = pSize 'CSng(combobox1.Text)

                Dim m_origin As Point

                errpos = 1

                ' Set the pixel offset mode. It looks better this way. 
                If (m_zoom = 1) Then '.0F) Then
                    RetG.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                Else
                    RetG.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default

                    ' Set the graphics transform to scale and/or offset drawing calls. 
                    If (m_zoom <> 1.0F) Then 'Or m_origin <> Point.Empty) Then
                        RetG.Transform = New System.Drawing.Drawing2D.Matrix(m_zoom, 0, 0, m_zoom, m_origin.X * m_zoom, m_origin.Y * m_zoom)
                    Else
                        RetG.ResetTransform()

                        ' Set the interpolation mode for best when zoomed out, 
                        ' or off when zoomed in. 
                        If (m_zoom < 1.0F) Then
                            RetG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                        Else
                            RetG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                        End If
                    End If
                End If
                '----------------------------
                errpos = 2
            End If


            errpos = 3

            ''' 
            Dim g As Graphics 
            g = RetG 
            '##############

            errpos = 4
            g.FillRectangle(Brushes.White, 0, 0, PictureBox1.Size.Width, PictureBox1.Size.Height) 

            errpos = 5

            '----- PREDRAW SO USERS CAN SEE DRAWING IN REAL TIME ---------- 
            If Not mousePath Is Nothing Then
                For lintArrInc = 0 To pBrush.GetUpperBound(0)
                    Dim ThisGraphicsPath As New GraphicsPath()
                    Try
                        ThisGraphicsPath = mousePath(lintArrInc).Clone
                    Catch
                        ThisGraphicsPath = mousePath(lintArrInc)
                    End Try

                    If pbooPagePrint = True Then
                        Try : MoveGP(pOffSet.X, pOffSet.Y, ThisGraphicsPath) : Catch : End Try
                    End If
                    Try : g.DrawPath(New Pen(pBrush(lintArrInc).BrushColour, _
                        pBrush(lintArrInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
                Next lintArrInc
            End If

            If Not ReversemousePath Is Nothing Then
                For lintArrInc = 0 To pReverseBrush.GetUpperBound(0)
                    Dim ThisGraphicsPath As New GraphicsPath()
                    Try
                        ThisGraphicsPath = ReversemousePath(lintArrInc).Clone
                    Catch
                        ThisGraphicsPath = ReversemousePath(lintArrInc)
                    End Try

                    If pbooPagePrint = True Then
                        Try : MoveGP(pOffSet.X, pOffSet.Y, ThisGraphicsPath) : Catch : End Try
                    End If

                    Try : g.DrawPath(New Pen(pReverseBrush(lintArrInc).BrushColour, _
                        pReverseBrush(lintArrInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
                Next lintArrInc
            End If
            '----- PREDRAW SO USERS CAN SEE DRAWING IN REAL TIME ---------- 

            
            Dim PieceInc As Integer
            Dim NormalGPInc As Integer
            Dim ReverseGPInc As Integer
            Dim UserPieceInc As Integer

            For lintArrInc = 0 To pSortOrderForData.DataType.Count - 1 'SO LOOP 
                Select Case CType(pSortOrderForData.DataType(lintArrInc), SortOrderForData.eDataType) 'SO CASE 
                    Case SortOrderForData.eDataType.PackPieces 'SO CASE 

                        '--- 
                        'Draw each piece 
                        Dim iPiece As Piece
                        iPiece = pPieces(PieceInc) 
                        ''For Each iPiece In pPieces
                        '--- 
                        Dim ThisPieceBounds As Rectangle
                        ThisPieceBounds = iPiece.Bounds
                        If pbooPagePrint = True Then
                            ThisPieceBounds.X += pOffSet.X
                            ThisPieceBounds.Y += pOffSet.Y
                        End If
                        g.DrawImage(iPiece.Bitmap, ThisPieceBounds)
                        '--- 
                        ''Next iPiece
                        '--- 
                        errpos = 6

                        PieceInc += 1 

                    Case SortOrderForData.eDataType.UserPieces 'SO CASE 

                        
                        Dim iUserPiece As Part
                        iUserPiece = lUserPieces.Parts(UserPieceInc) 
                        ''For Each iUserPiece In lUserPieces.Parts
                        Dim ThisPieceBounds As Rectangle ' 
                        ThisPieceBounds = iUserPiece.Bounds 
                        If pbooPagePrint = True Then
                            ThisPieceBounds.X += pOffSet.X
                            ThisPieceBounds.Y += pOffSet.Y
                        End If
                        g.DrawImage(iUserPiece.FullImage, ThisPieceBounds)
                        ''Next iUserPiece

                        UserPieceInc += 1 

                    Case SortOrderForData.eDataType.NormalGraphicsPath 'SO CASE 
                        '###################################################################################
                        '###                                                                             ###
                        '###  WARNING: After this point Null errors happen with graphics paths.          ### 
                        '###  I beleive this is caused by adding extra paths to make the undo feature    ###
                        '###  work better.  So if you ever add any new graphic path statments use a try  ###
                        '###  catch block around them. 
                        '###################################################################################

                        '--- 
                        'Put here so lines could appear over pieces.
                        If Not mousePath Is Nothing Then 
                            '--- 
                            errpos = 7
                            ''For lintArrInc = 0 To pBrush.GetUpperBound(0)
                            '--- 
                            errpos = 8
                            Dim ThisGraphicsPath As New GraphicsPath()
                            Try 
                                ThisGraphicsPath = mousePath(NormalGPInc).Clone
                            Catch 'EX As Exception 
                                'Console.WriteLine("M Clone Warning " & ex.Message & " " & lintArrInc)
                                ThisGraphicsPath = mousePath(NormalGPInc) 
                            End Try

                            errpos = 9
                            If pbooPagePrint = True Then
                                errpos = 10
                                Try 
                                    MoveGP(pOffSet.X, pOffSet.Y, ThisGraphicsPath)
                                Catch 
                                    '
                                End Try 
                            End If
                            errpos = 11
                            Try : g.DrawPath(New Pen(pBrush(NormalGPInc).BrushColour, _
                                pBrush(NormalGPInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
                            errpos = 12
                            '--- 
                            ''Next lintArrInc
                            '--- 

                            NormalGPInc += 1 
                        End If
                        errpos = 13

                    Case SortOrderForData.eDataType.ReverseGraphicsPath 'SO CASE 

                        If Not ReversemousePath Is Nothing Then 
                            '--- 
                            errpos = 14
                            ''For lintArrInc = 0 To pReverseBrush.GetUpperBound(0)
                            errpos = 15
                            '--- 
                            Dim ThisGraphicsPath As New GraphicsPath()
                            errpos = 16
                            Try 
                                ThisGraphicsPath = ReversemousePath(ReverseGPInc).Clone
                            Catch 'EX As Exception 
                                'Console.WriteLine("R Clone Warning " & ex.Message & " " & lintArrInc)
                                ThisGraphicsPath = ReversemousePath(ReverseGPInc) 
                            End Try

                            errpos = 17
                            If pbooPagePrint = True Then
                                errpos = 18
                                Try 
                                    MoveGP(pOffSet.X, pOffSet.Y, ThisGraphicsPath)
                                Catch 
                                    '
                                End Try 
                            End If
                            errpos = 19
                            Try : g.DrawPath(New Pen(pReverseBrush(ReverseGPInc).BrushColour, _
                                pReverseBrush(ReverseGPInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
                            errpos = 20
                            '--- 
                            ''Next lintArrInc
                            '--- 
                            ReverseGPInc += 1 
                        End If

                End Select 'SO CASE 
            Next lintArrInc 'SO LOOP 

            '--- 
            errpos = 21

            If pbooPagePrint = False Then
                If Mirrored = True Then
                    Dim x As Single = 195 + 60 '(MaskBitmap.Width / 2)
                    g.DrawLine(New Pen(Color.Red, 2), x, 0, x, PictureBox1.Size.Height)
                    errpos = 9
                    'Console.WriteLine("done mirror " & MaskBitmap.Height & " " & x)
                End If
                errpos = 22

                If Guided = True Then
                    GuideFace(g, PictureBox1.Size.Width - 5, PictureBox1.Size.Height - 5)
                    errpos = 23
                End If
            End If 

            '--- 
            If InStrGet((NameMe("")).ToUpper, "TRIAL") > 0 Then
                If pbooPagePrint = True Then
                    Dim NumLine As Integer = PictureBox1.Size.Height / 2 ' 15
                    Dim lintArrInc2 As Integer
                    Dim WaterFont As New Font("Arial", 14) '2)
                    Dim Waterbrush As Brush = Brushes.LightGray

                    'For lintArrInc2 = 0 To 14
                    'Dim ThisY As Integer = NumLine * lintArrInc2
                    'g.DrawString("This is a 30 day trial version of Kids Mask Print, visit our website to buy!", WaterFont, Waterbrush, 10, NumLine) 'ThisY)
                    g.DrawString("Visit www.KidsMaskPrint.com to buy and remove this text!", WaterFont, Waterbrush, 10, NumLine) ', 'ThisY)
                    'Next lintArrInc2
                End If
            End If
            '--- 

        Catch ex As Exception
            'MessageBox.Show(ex.ToString)
            Console.WriteLine(errpos & " " & ex.ToString)
            '--- 
            If ErrCount < 5 Then
                AddDebugComment("DrawOutPut.DrawOutOut - Error " & ErrCount & "/4:" & ex.ToString)
            ElseIf ErrCount = 50 Then
                AddDebugComment("DrawOutPut.DrawOutOut - Error 50:" & ex.ToString)
            End If
            ErrCount += 1
            '--- 
        End Try

    End Sub
    Private Sub GuideFace(ByVal G As Graphics, ByVal w As Single, ByVal h As Single)

        Dim OvalAdj As Integer = 40
        G.DrawEllipse(New Pen(Color.Gray, 2), 20 + OvalAdj, 20, (w - 40) - OvalAdj * 2, h - 40)

    End Sub
    Private Sub MoveGP(ByVal x As Single, ByVal y As Single, ByRef gp As GraphicsPath)
        'added 
        Dim objTranslateMatrix As New Drawing2D.Matrix()
        With objTranslateMatrix

            'move the path to zero, scale it, move the             path(back)
            If x < 0 Then
                .Translate(x, y)
            Else
                .Translate(+x, +y)
            End If

            gp.Transform(objTranslateMatrix)
            .Reset()

            ''.Scale(x, y)
            ''mobjPath.Transform(objTranslateMatrix)
            ''.Reset()

            ''.Translate(mobjRect.X + dx, mobjRect.Y + dy)
            ''mobjPath.Transform(objTranslateMatrix)

        End With
    End Sub
    Friend Function DrawDetails(ByVal PictureBox1 As PictureBox, _
        ByVal mousePath() As GraphicsPath, ByVal ReversemousePath() As GraphicsPath, ByVal pPieces As ArrayList, _
       ByVal pBrush() As PaintBrush, ByVal pReverseBrush() As PaintBrush, ByVal lUserPieces As FacePartStuctureDataFile, _
    ByVal pSortOrderForData As SortOrderForData) As Bitmap

        AddDebugComment("DrawOutPut.DrawDetails - start") 
        gstrProbDrawComtStack = " #1" 
        Dim lintArrInc As Integer

        Dim NewBitmap As Bitmap = New Bitmap(PictureBox1.Size.Width, PictureBox1.Size.Height)
        gstrProbDrawComtStack &= " #2" 
        Dim g As Graphics = Graphics.FromImage(NewBitmap)
        gstrProbDrawComtStack &= " #3" 
        g.FillRectangle(Brushes.White, 0, 0, PictureBox1.Size.Width, PictureBox1.Size.Height) 
        gstrProbDrawComtStack &= " #4" 

        '----- PREDRAW SO USERS CAN SEE DRAWING IN REAL TIME ---------- 
        If Not mousePath Is Nothing Then
            gstrProbDrawComtStack &= " #5" 
            For lintArrInc = 0 To pBrush.GetUpperBound(0)
                gstrProbDrawComtStack &= " #6" 
                Dim ThisGraphicsPath As New GraphicsPath()
                Try
                    gstrProbDrawComtStack &= " #7" 
                    ThisGraphicsPath = mousePath(lintArrInc).Clone
                    gstrProbDrawComtStack &= " #8" 
                Catch
                    gstrProbDrawComtStack &= " #9" 
                    ThisGraphicsPath = mousePath(lintArrInc)
                    gstrProbDrawComtStack &= " #10" 
                End Try
                gstrProbDrawComtStack &= " #11" 
                Try : g.DrawPath(New Pen(pBrush(lintArrInc).BrushColour, _
                    pBrush(lintArrInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
            Next lintArrInc
            gstrProbDrawComtStack &= " #12" 
        End If

        gstrProbDrawComtStack &= " #13" 

        If Not ReversemousePath Is Nothing Then
            gstrProbDrawComtStack &= " #14" 
            For lintArrInc = 0 To pReverseBrush.GetUpperBound(0)
                gstrProbDrawComtStack &= " #15" 
                Dim ThisGraphicsPath As New GraphicsPath()
                Try
                    gstrProbDrawComtStack &= " #16" 
                    ThisGraphicsPath = ReversemousePath(lintArrInc).Clone
                    gstrProbDrawComtStack &= " #17" 
                Catch
                    ThisGraphicsPath = ReversemousePath(lintArrInc)
                    gstrProbDrawComtStack &= " #18" 
                End Try

                gstrProbDrawComtStack &= " #19" 
                Try : g.DrawPath(New Pen(pReverseBrush(lintArrInc).BrushColour, _
                    pReverseBrush(lintArrInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
            Next lintArrInc
            gstrProbDrawComtStack &= " #20" 
        End If

        gstrProbDrawComtStack &= " #21" 
        '----- PREDRAW SO USERS CAN SEE DRAWING IN REAL TIME ---------- 

        
        Dim PieceInc As Integer
        Dim NormalGPInc As Integer
        Dim ReverseGPInc As Integer
        Dim UserPieceInc As Integer
        gstrProbDrawComtStack &= " #22" 

        For lintArrInc = 0 To pSortOrderForData.DataType.Count - 1 'SO LOOP 
            gstrProbDrawComtStack &= " #23" 
            Select Case CType(pSortOrderForData.DataType(lintArrInc), SortOrderForData.eDataType) 'SO CASE 
                Case SortOrderForData.eDataType.PackPieces 'SO CASE 
                    gstrProbDrawComtStack &= " #24" 
                    Dim iPiece As Piece
                    iPiece = pPieces(PieceInc) 
                    ''For Each iPiece In pPieces
                    Dim ThisPieceBounds As Rectangle
                    ThisPieceBounds = iPiece.Bounds
                    gstrProbDrawComtStack &= " #25" 
                    g.DrawImage(iPiece.Bitmap, ThisPieceBounds)
                    ''Next iPiece
                    PieceInc += 1 
                    gstrProbDrawComtStack &= " #26" 
                Case SortOrderForData.eDataType.UserPieces 'SO CASE 
                    gstrProbDrawComtStack &= " #27" 
                    
                    Dim iUserPiece As Part
                    iUserPiece = lUserPieces.Parts(UserPieceInc) 
                    ''For Each iUserPiece In lUserPieces.Parts
                    gstrProbDrawComtStack &= " #28" 
                    Dim ThisPieceBounds As Rectangle ' 
                    ThisPieceBounds = iUserPiece.Bounds 
                    g.DrawImage(iUserPiece.FullImage, ThisPieceBounds)
                    ''Next iUserPiece
                    gstrProbDrawComtStack &= " #29" 
                    UserPieceInc += 1 
                    
                    gstrProbDrawComtStack &= " #30" 
                Case SortOrderForData.eDataType.NormalGraphicsPath 'SO CASE 
                    gstrProbDrawComtStack &= " #31" 
                    If Not mousePath Is Nothing Then
                        gstrProbDrawComtStack &= " #32" 
                        ''For lintArrInc = 0 To pBrush.GetUpperBound(0)
                        Dim ThisGraphicsPath As New GraphicsPath()
                        Try 
                            ThisGraphicsPath = mousePath(NormalGPInc).Clone
                        Catch 
                            ThisGraphicsPath = mousePath(NormalGPInc) 
                        End Try
                        gstrProbDrawComtStack &= " #33" 
                        Try : g.DrawPath(New Pen(pBrush(NormalGPInc).BrushColour, _
                            pBrush(NormalGPInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
                        ''Next lintArrInc
                        gstrProbDrawComtStack &= " #34" 
                        NormalGPInc += 1 
                    End If

                Case SortOrderForData.eDataType.ReverseGraphicsPath 'SO CASE 
                    gstrProbDrawComtStack &= " #35" 
                    If Not ReversemousePath Is Nothing Then
                        ''For lintArrInc = 0 To pReverseBrush.GetUpperBound(0)
                        gstrProbDrawComtStack &= " #36" 
                        Dim ThisGraphicsPath As New GraphicsPath()
                        gstrProbDrawComtStack &= " #37" 
                        Try 
                            ThisGraphicsPath = ReversemousePath(ReverseGPInc).Clone
                            gstrProbDrawComtStack &= " #38" 
                        Catch 
                            gstrProbDrawComtStack &= " #39" 
                            ThisGraphicsPath = ReversemousePath(ReverseGPInc) 
                            gstrProbDrawComtStack &= " #40" 
                        End Try 
                        gstrProbDrawComtStack &= " #41" 
                        Try : g.DrawPath(New Pen(pReverseBrush(ReverseGPInc).BrushColour, _
                            pReverseBrush(ReverseGPInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
                        ''Next lintArrInc
                        gstrProbDrawComtStack &= " #42" 
                    End If

            End Select 'SO CASE 
        Next lintArrInc 'SO LOOP 
        gstrProbDrawComtStack &= " #43" 
        'Dim SaveBitMap As Bitmap
        'SaveBitMap = New Bitmap(PictureBox1.Size.Width, PictureBox1.Size.Height, g)
        'If IO.File.Exists(OutMask) = True Then IO.File.Delete(OutMask)
        ''NewBitmap.Save("D:\desktopnt\crapper", Imaging.ImageFormat.Bmp)
        'Console.WriteLine("written")

        AddDebugComment("DrawOutPut.DrawDetails - end") 

        gstrProbDrawComtStack &= " #44" 

        Return NewBitmap

    End Function

    Friend Sub DrawOutputBackup(ByVal RetG As Graphics, ByVal pbooPagePrint As Boolean, ByVal PictureBox1 As PictureBox, _
           ByVal mousePath() As GraphicsPath, ByVal ReversemousePath() As GraphicsPath, _
           ByVal pSize As Single, ByVal ThisColour As Color, ByVal Mirrored As Boolean, ByVal Guided As Boolean, _
           ByVal pPieces As ArrayList, ByVal pOffSet As Point, ByVal pBrush() As PaintBrush, ByVal pReverseBrush() As PaintBrush, _
           ByVal lUserPieces As FacePartStuctureDataFile)

        'lUserPieces added 

        Dim errpos As Integer
        Dim lintArrInc As Integer 

        Try ' error trapping

            If pbooPagePrint = True Then
                '----------------------------

                Dim m_zoom As Single = pSize 'CSng(combobox1.Text)

                Dim m_origin As Point

                errpos = 1

                ' Set the pixel offset mode. It looks better this way. 
                If (m_zoom = 1) Then '.0F) Then
                    RetG.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                Else
                    RetG.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default

                    ' Set the graphics transform to scale and/or offset drawing calls. 
                    If (m_zoom <> 1.0F) Then 'Or m_origin <> Point.Empty) Then
                        RetG.Transform = New System.Drawing.Drawing2D.Matrix(m_zoom, 0, 0, m_zoom, m_origin.X * m_zoom, m_origin.Y * m_zoom)
                    Else
                        RetG.ResetTransform()

                        ' Set the interpolation mode for best when zoomed out, 
                        ' or off when zoomed in. 
                        If (m_zoom < 1.0F) Then
                            RetG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                        Else
                            RetG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                        End If
                    End If
                End If
                '----------------------------
                errpos = 2
            End If


            errpos = 3

            '##############
            ''' 
            Dim g As Graphics 
            g = RetG 
            '##############

            errpos = 4
            '' Dim CurrentPen = New Pen(Color.FromArgb(255, Color.Black), 5) 'Set up the pen
            'e.Graphics.DrawPath(CurrentPen, mousePath) 'draw the path! :)
            'e.Graphics.DrawPath(New Pen(Color.Red, myPenWidth), ReversemousePath)
            'g.FillRectangle(Brushes.White, 0, 0, MaskBitmap.Width, MaskBitmap.Height)
            g.FillRectangle(Brushes.White, 0, 0, PictureBox1.Size.Width, PictureBox1.Size.Height) 

            errpos = 5
            'x'If Not PictureBox2.Image Is Nothing Then
            'x'    g.DrawImage(PictureBox2.Image, 0, 0)
            'x'    ''g.DrawImage(PictureBox2.Image, pOffSet.X, pOffSet.Y) 
            'x'    errpos = 6
            'x'End If

            'errpos = 11
            ''If Not mousePath Is Nothing Then 
            ''    Try : g.DrawPath(New Pen(ThisColour, 5), mousePath) : Catch : End Try
            ''End If

            ''errpos = 12
            '''If Mirrored = True Then 
            ''If Not ReversemousePath Is Nothing Then 
            ''    Try : g.DrawPath(New Pen(ThisColour, 5), ReversemousePath) : Catch : End Try
            ''End If
            '''End If 
            ''errpos = 13



            '''''If pbooPagePrint = False Then
            '''''    errpos = 14

            '''''    '##############
            '''''    '''
            '''''    '##############

            '''''    errpos = 15
            '''''Else
            '''''    Dim MaskBitmap As Bitmap
            '''''    MaskBitmap = New Bitmap(PictureBox1.Size.Width, PictureBox1.Size.Height, Drawing.Imaging.PixelFormat.Format24bppRgb)

            '''''    errpos = 16
            '''''    'RetG.DrawImage(MaskBitmap, 0, 0)
            '''''    RetG.DrawImage(MaskBitmap, pOffSet.X, pOffSet.Y) 
            '''''    errpos = 17
            '''''End If

            'errpos = 7

            ''--- TESTING ---
            'Dim x2 As Single = 195 + 60 '(MaskBitmap.Width / 2)
            'g.DrawLine(New Pen(Color.Blue, 2), x2, 0, x2, MaskBitmap.Height)
            ''--- TESTING ---

            'If pbooPagePrint = False Then 

            '--- 
            'Draw each piece 
            Dim iPiece As Piece
            For Each iPiece In pPieces
                '--- 
                Dim ThisPieceBounds As Rectangle
                ThisPieceBounds = iPiece.Bounds
                If pbooPagePrint = True Then
                    ThisPieceBounds.X += pOffSet.X
                    ThisPieceBounds.Y += pOffSet.Y
                End If
                g.DrawImage(iPiece.Bitmap, ThisPieceBounds)
                '--- 
                'g.DrawImage(iPiece.Bitmap, iPiece.Bounds)
            Next iPiece
            '--- 
            errpos = 6

            '############# TESTING 
            Dim iUserPiece As Part
            For Each iUserPiece In lUserPieces.Parts
                Dim ThisPieceBounds As Point
                ThisPieceBounds = iUserPiece.LeftPart
                If pbooPagePrint = True Then
                    ThisPieceBounds.X += pOffSet.X
                    ThisPieceBounds.Y += pOffSet.Y
                End If
                g.DrawImage(iUserPiece.FullImage, ThisPieceBounds)
            Next iUserPiece
            '############# TESTING 

            '###################################################################################
            '###                                                                             ###
            '###  WARNING: After this point Null errors happen with graphics paths.          ### 
            '###  I beleive this is caused by adding extra paths to make the undo feature    ###
            '###  work better.  So if you ever add any new graphic path statments use a try  ###
            '###  catch block around them. 
            '###################################################################################

            '--- 
            'Put here so lines could appear over pieces.
            If Not mousePath Is Nothing Then 
                'Try : g.DrawPath(New Pen(Color.Black, 5), mousePath(0)) : Catch : End Try
                'Try : g.DrawPath(New Pen(Color.White, 5), mousePath(1)) : Catch : End Try 
                '--- 
                errpos = 7
                For lintArrInc = 0 To pBrush.GetUpperBound(0)
                    '--- 
                    errpos = 8
                    Dim ThisGraphicsPath As New GraphicsPath()
                    Try 
                        ThisGraphicsPath = mousePath(lintArrInc).Clone
                        'ThisGraphicsPath = CType(mousePath(lintArrInc).Clone(), GraphicsPath) 

                    Catch 'EX As Exception 
                        'Console.WriteLine("M Clone Warning " & ex.Message & " " & lintArrInc)
                        ThisGraphicsPath = mousePath(lintArrInc) 
                    End Try

                    errpos = 9
                    If pbooPagePrint = True Then
                        errpos = 10
                        Try 
                            MoveGP(pOffSet.X, pOffSet.Y, ThisGraphicsPath)
                        Catch 
                            '
                        End Try 
                    End If
                    errpos = 11
                    Try : g.DrawPath(New Pen(pBrush(lintArrInc).BrushColour, _
                        pBrush(lintArrInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
                    errpos = 12
                    '--- 
                    'Try : g.DrawPath(New Pen(pBrush(lintArrInc).BrushColour, _
                    '    pBrush(lintArrInc).BrushWidth), mousePath(lintArrInc)) : Catch : End Try
                Next lintArrInc
                '--- 
            End If
            errpos = 13

            'If Mirrored = True Then 
            If Not ReversemousePath Is Nothing Then 
                'Try : g.DrawPath(New Pen(Color.Black, 5), ReversemousePath(0)) : Catch : End Try
                'Try : g.DrawPath(New Pen(Color.White, 5), ReversemousePath(1)) : Catch : End Try 
                '--- 
                errpos = 14
                For lintArrInc = 0 To pReverseBrush.GetUpperBound(0)
                    errpos = 15
                    '--- 
                    Dim ThisGraphicsPath As New GraphicsPath()
                    errpos = 16
                    Try 
                        ThisGraphicsPath = ReversemousePath(lintArrInc).Clone
                        'ThisGraphicsPath = CType(ReversemousePath(lintArrInc).Clone(), GraphicsPath) 
                    Catch 'EX As Exception 
                        'Console.WriteLine("R Clone Warning " & ex.Message & " " & lintArrInc)
                        ThisGraphicsPath = ReversemousePath(lintArrInc) 
                    End Try

                    errpos = 17
                    If pbooPagePrint = True Then
                        errpos = 18
                        Try 
                            MoveGP(pOffSet.X, pOffSet.Y, ThisGraphicsPath)
                        Catch 
                            '
                        End Try 
                    End If
                    errpos = 19
                    Try : g.DrawPath(New Pen(pReverseBrush(lintArrInc).BrushColour, _
                        pReverseBrush(lintArrInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
                    errpos = 20
                    '--- 
                    'Try : g.DrawPath(New Pen(pReverseBrush(lintArrInc).BrushColour, _
                    '    pReverseBrush(lintArrInc).BrushWidth), ReversemousePath(lintArrInc)) : Catch : End Try
                Next lintArrInc
                '--- 
            End If
            '--- 
            errpos = 21

            If pbooPagePrint = False Then

                If Mirrored = True Then
                    Dim x As Single = 195 + 60 '(MaskBitmap.Width / 2)
                    g.DrawLine(New Pen(Color.Red, 2), x, 0, x, PictureBox1.Size.Height)
                    errpos = 9
                    'Console.WriteLine("done mirror " & MaskBitmap.Height & " " & x)
                End If
                errpos = 22

                If Guided = True Then
                    GuideFace(g, PictureBox1.Size.Width - 5, PictureBox1.Size.Height - 5)
                    errpos = 23
                End If
            End If 

        Catch ex As Exception
            'MessageBox.Show(ex.ToString)
            Console.WriteLine(errpos & " " & ex.ToString)
            '--- 
            If ErrCount < 5 Then
                AddDebugComment("DrawOutPut.DrawOutOut - Error " & ErrCount & "/4:" & ex.ToString)
            ElseIf ErrCount = 50 Then
                AddDebugComment("DrawOutPut.DrawOutOut - Error 50:" & ex.ToString)
            End If
            ErrCount += 1
            '--- 
        End Try

    End Sub
    Friend Function DrawDetailsBackup(ByVal PictureBox1 As PictureBox, _
          ByVal mousePath() As GraphicsPath, ByVal ReversemousePath() As GraphicsPath, ByVal pPieces As ArrayList, _
         ByVal pBrush() As PaintBrush, ByVal pReverseBrush() As PaintBrush, ByVal lUserPieces As FacePartStuctureDataFile) As Bitmap

        AddDebugComment("DrawOutPut.DrawDetails - start") 

        Dim lintArrInc As Integer

        Dim NewBitmap As Bitmap = New Bitmap(PictureBox1.Size.Width, PictureBox1.Size.Height)
        Dim g As Graphics = Graphics.FromImage(NewBitmap)
        g.FillRectangle(Brushes.White, 0, 0, PictureBox1.Size.Width, PictureBox1.Size.Height) 

        Dim iPiece As Piece
        For Each iPiece In pPieces
            Dim ThisPieceBounds As Rectangle
            ThisPieceBounds = iPiece.Bounds

            g.DrawImage(iPiece.Bitmap, ThisPieceBounds)
        Next iPiece

        '############# TESTING 
        Dim iUserPiece As Part
        For Each iUserPiece In lUserPieces.Parts
            Dim ThisPieceBounds As Point
            ThisPieceBounds = iUserPiece.LeftPart
            g.DrawImage(iUserPiece.FullImage, ThisPieceBounds)
        Next iUserPiece
        '############# TESTING 

        If Not mousePath Is Nothing Then
            For lintArrInc = 0 To pBrush.GetUpperBound(0)
                Dim ThisGraphicsPath As New GraphicsPath()
                Try 
                    ThisGraphicsPath = mousePath(lintArrInc).Clone
                Catch 
                    ThisGraphicsPath = mousePath(lintArrInc) 
                End Try
                Try : g.DrawPath(New Pen(pBrush(lintArrInc).BrushColour, _
                    pBrush(lintArrInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
            Next lintArrInc
        End If

        If Not ReversemousePath Is Nothing Then
            For lintArrInc = 0 To pReverseBrush.GetUpperBound(0)
                Dim ThisGraphicsPath As New GraphicsPath()
                Try 
                    ThisGraphicsPath = ReversemousePath(lintArrInc).Clone
                Catch 
                    ThisGraphicsPath = ReversemousePath(lintArrInc) 
                End Try 

                Try : g.DrawPath(New Pen(pReverseBrush(lintArrInc).BrushColour, _
                    pReverseBrush(lintArrInc).BrushWidth), ThisGraphicsPath) : Catch : End Try
            Next lintArrInc

        End If

        'Dim SaveBitMap As Bitmap
        'SaveBitMap = New Bitmap(PictureBox1.Size.Width, PictureBox1.Size.Height, g)
        'If IO.File.Exists(OutMask) = True Then IO.File.Delete(OutMask)
        ''NewBitmap.Save("D:\desktopnt\crapper", Imaging.ImageFormat.Bmp)
        'Console.WriteLine("written")

        AddDebugComment("DrawOutPut.DrawDetails - end") 

        Return NewBitmap

    End Function
End Module
