Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.Marshal

Friend Class FloodFill
    'Private Declare Function LoadImage Lib "user32" Alias "LoadImageA" (ByVal hInst As Integer, ByVal lpsz As String, ByVal uType As Integer, ByVal cxDesired As Integer, ByVal cyDesired As Integer, ByVal fuLoad As Integer) As IntPtr
    'Private Declare Function BitBlt Lib "gdi32" Alias "BitBlt" (ByVal hDestDC As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As IntPtr, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
    'Private Declare Function DeleteObject Lib "gdi32" Alias "DeleteObject" (ByVal hObject As IntPtr) As Integer
    'Private Declare Function CreateCompatibleDC Lib "gdi32" Alias "CreateCompatibleDC" (ByVal hdc As IntPtr) As IntPtr
    'Private Declare Function CreateCompatibleBitmap Lib "gdi32" Alias "CreateCompatibleBitmap" (ByVal hdc As IntPtr, ByVal nWidth As Integer, ByVal nHeight As Integer) As IntPtr
    'Private Declare Function SelectObject Lib "gdi32" Alias "SelectObject" (ByVal hdc As IntPtr, ByVal hObject As IntPtr) As IntPtr
    'Private Declare Function DeleteDC Lib "gdi32" Alias "DeleteDC" (ByVal hdc As IntPtr) As Integer

    'Private Const SRCAND = &H8800C6
    'Private Const SRCCOPY = &HCC0020
    'Private Const SRCERASE = &H440328
    'Private Const SRCINVERT = &H660046
    'Private Const SRCPAINT = &HEE0086
    'Private Const IMAGE_BITMAP = 0
    'Private Const LR_LOADFROMFILE = &H10

    Function FloodFillIt(ByRef raster As Bitmap, ByVal iX As Integer, ByVal iY As Integer, ByVal iFColor As Color, _
        ByVal iBColor As Color, ByRef ClipTop As Integer, ByRef ClipLeft As Integer) As Bitmap

        'raster.Save("D:\desktopnt\before.bmp")

        ''Dim FullImage As Image = DrawDetails(PictureBox1, m_Drawings.mousePath, m_Drawings.ReversemousePath, mPieces, m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush)  ' DrawingWithoutHelpers()

        ''Dim Before As New Bitmap(FullImage)
        ''Dim l_ActiveBitmap As New Bitmap(FullImage)

        ''ff.BoundaryFillBitmap(l_ActiveBitmap, curX, curY, Color.Red, Color.Black)
        '''ff.compare(FullImage, l_ActiveBitmap)
        ''PictureBox2.Image = ff.FloodFillClipImage(FullImage, l_ActiveBitmap)


        Dim Bitmapbefore As Bitmap = raster.Clone
        ''  BoundaryFillBitmap(raster, iX, iY, iFColor, iBColor)'commented 'JM 15/10/2004

        Dim mf As New MapFill() 'JM 15/10/2004
        raster = mf.Fill(raster, New Point(iX, iY), iFColor) 'JM 15/10/2004

        'Return FloodFillClipImage(Bitmapbefore, raster, ClipTop, ClipLeft, iFColor)
        Return FloodFillClipImage(Bitmapbefore, raster, ClipTop, ClipLeft, iFColor) 'JM 15/10/2004

    End Function
    '    Private Sub BoundaryFillBitmap(ByRef raster As Bitmap, ByVal iX As Integer, ByVal iY As Integer, ByVal iFColor As Color, ByVal iBColor As Color)

    '        ' Allocate the maximum amount of space needed.  If you prefer less, you
    '        ' need to modify this data structure to allow for dynamic reallocation
    '        ' when it is needed.  An empty stack has iTop == -1.
    '        Dim iQuantity As Integer = (raster.Width + 1) * (raster.Height + 1)
    '        Dim iXMax As Integer = raster.Width
    '        Dim iYMax As Integer = raster.Height
    '        Dim akXStack(iQuantity) As Integer
    '        Dim akYStack(iQuantity) As Integer
    '        Dim FColorARGB As Integer = iFColor.ToArgb()
    '        Dim BColorARGB As Integer = iBColor.ToArgb()
    '        Dim tempARGB As Integer

    '        ' Push seed point onto stack if it has the background color.  All points
    '        ' pushed onto stack have background color iBColor.
    '        Dim iTop As Integer = 0
    '        tempARGB = raster.GetPixel(iX, iY).ToArgb()
    '        If tempARGB = FColorARGB OrElse tempARGB = BColorARGB OrElse tempARGB = 0 Then
    '            Return
    '        End If

    '        akXStack(iTop) = iX
    '        akYStack(iTop) = iY

    '        While iTop >= 0 ' stack is not empty
    '            ' Read top of stack.  Do not pop since we need to return to this
    '            ' top value later to restart the fill in a different direction.
    '            iX = akXStack(iTop)
    '            iY = akYStack(iTop)
    '            ' fill the pixel
    '            raster.SetPixel(iX, iY, iFColor)

    '            Dim iXp1 As Integer = iX + 1

    '            If iXp1 < iXMax Then
    '                tempARGB = raster.GetPixel(iXp1, iY).ToArgb()
    '                If tempARGB <> FColorARGB AndAlso tempARGB <> BColorARGB AndAlso tempARGB <> 0 Then
    '                    ' push pixel with background color
    '                    iTop += 1
    '                    akXStack(iTop) = iXp1
    '                    akYStack(iTop) = iY
    '                    GoTo ContinueWhile1
    '                End If
    '            End If

    '            Dim iXm1 As Integer = iX - 1

    '            If 0 <= iXm1 Then
    '                tempARGB = raster.GetPixel(iXm1, iY).ToArgb()
    '                If tempARGB <> FColorARGB AndAlso tempARGB <> BColorARGB AndAlso tempARGB <> 0 Then
    '                    ' push pixel with background color
    '                    iTop += 1
    '                    akXStack(iTop) = iXm1
    '                    akYStack(iTop) = iY
    '                    GoTo ContinueWhile1
    '                End If
    '            End If

    '            Dim iYp1 As Integer = iY + 1

    '            If iYp1 < iYMax Then
    '                tempARGB = raster.GetPixel(iX, iYp1).ToArgb()
    '                If tempARGB <> FColorARGB AndAlso tempARGB <> BColorARGB AndAlso tempARGB <> 0 Then
    '                    ' push pixel with background color
    '                    iTop += 1
    '                    akXStack(iTop) = iX
    '                    akYStack(iTop) = iYp1
    '                    GoTo ContinueWhile1
    '                End If
    '            End If

    '            Dim iYm1 As Integer = iY - 1

    '            If 0 <= iYm1 Then
    '                tempARGB = raster.GetPixel(iX, iYm1).ToArgb()
    '                If tempARGB <> FColorARGB AndAlso tempARGB <> BColorARGB AndAlso tempARGB <> 0 Then
    '                    ' push pixel with background color
    '                    iTop += 1
    '                    akXStack(iTop) = iX
    '                    akYStack(iTop) = iYm1
    '                    GoTo ContinueWhile1
    '                End If
    '            End If

    '            ' done in all directions, pop and return to search other directions
    '            iTop -= 1

    'ContinueWhile1:

    '        End While

    '    End Sub 'BoundaryFillBitmap
    'Function compare(ByVal pimgBeforeFill As Image, ByVal pimgAfterFill As Image) As Image
    '    Dim hBmp1, hBmp2 As IntPtr
    '    Dim memDC1, memDC2, picDC As IntPtr
    '    Dim oldBmp1, oldBmp2 As IntPtr
    '    'Dim picGraphics As Graphics
    '    'Dim img As Image
    '    'img = Image.FromFile("d:\1.bmp")

    '    Dim TempFile1 As String = Path.GetTempFileName
    '    'Dim myFileInfo As New FileInfo(TempFile1)
    '    'myFileInfo.Attributes = FileAttributes.Temporary
    '    pimgBeforeFill.Save(TempFile1, Imaging.ImageFormat.Bmp)

    '    'hBmp1 = LoadImage(0, "d:\1.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE)
    '    hBmp1 = LoadImage(0, TempFile1, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE)

    '    Dim TempFile2 As String = Path.GetTempFileName
    '    'Dim myFileInfo2 As New FileInfo(TempFile2)
    '    'myFileInfo2.Attributes = FileAttributes.Temporary
    '    pimgAfterFill.Save(TempFile2, Imaging.ImageFormat.Bmp)

    '    'hBmp2 = LoadImage(0, "d:\2.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE)
    '    hBmp2 = LoadImage(0, TempFile2, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE)

    '    memDC1 = CreateCompatibleDC(New IntPtr(0))
    '    memDC2 = CreateCompatibleDC(New IntPtr(0))
    '    oldBmp1 = SelectObject(memDC1, hBmp1)
    '    oldBmp2 = SelectObject(memDC2, hBmp2)

    '    Dim NewBitmap As Bitmap = New Bitmap(pimgBeforeFill.Size.Width, pimgBeforeFill.Size.Height)
    '    Dim picGraphics As Graphics = Graphics.FromImage(NewBitmap)

    '    BitBlt(memDC1, 0, 0, pimgBeforeFill.Width, pimgBeforeFill.Height, memDC2, 0, 0, SRCINVERT)
    '    'picGraphics = picBoxOut.CreateGraphics

    '    picDC = picGraphics.GetHdc()
    '    BitBlt(picDC, 0, 0, pimgBeforeFill.Width, pimgBeforeFill.Height, memDC1, 0, 0, SRCPAINT)

    '    picGraphics.ReleaseHdc(picDC)

    '    oldBmp1 = SelectObject(memDC1, oldBmp1)
    '    oldBmp2 = SelectObject(memDC1, oldBmp2)

    '    DeleteDC(memDC1)
    '    DeleteDC(memDC2)
    '    DeleteObject(hBmp1)
    '    DeleteObject(hBmp2)

    '    File.Delete(TempFile1)
    '    File.Delete(TempFile2)

    '    Return NewBitmap

    'End Function
    Private Function FloodFillClipImage(ByVal pimgBeforeFill As Bitmap, ByVal pimgAfterFill As Bitmap, _
        ByRef Top As Integer, ByRef Left As Integer, ByVal iFColor As Color) As Bitmap

        Dim RetBitmap As New Bitmap(pimgBeforeFill)

        Dim i As Bitmap = pimgBeforeFill
        If (i.PixelFormat = Imaging.PixelFormat.Format24bppRgb Or i.PixelFormat = Imaging.PixelFormat.Format32bppArgb) Then
            'we can fiddle with a 24 or 32 bit image
            'To use LockBits in VB we can use the Marshal class to access image

            Dim bmd As BitmapData = i.LockBits(New Rectangle(0, 0, i.Width, i.Height), ImageLockMode.ReadWrite, i.PixelFormat)
            Dim bmdFilled As BitmapData = pimgAfterFill.LockBits(New Rectangle(0, 0, pimgAfterFill.Width, pimgAfterFill.Height), ImageLockMode.ReadWrite, pimgAfterFill.PixelFormat)
            Dim bmdOut As BitmapData = RetBitmap.LockBits(New Rectangle(0, 0, RetBitmap.Width, RetBitmap.Height), ImageLockMode.ReadWrite, RetBitmap.PixelFormat)


            Dim x As Integer
            Dim y As Integer

            Dim itop As Integer = i.Height : Dim ibottom As Integer = -1 : Dim ileft As Integer = i.Width : Dim iright As Integer = -1

            Dim advance As Integer = Microsoft.VisualBasic.IIf(i.PixelFormat = PixelFormat.Format24bppRgb, 3, 4)

            For y = 0 To i.Height - 1
                'calculate the offset to the beginning of the row
                Dim offset As Integer = y * bmd.Stride
                For x = 0 To i.Width - 1

                    Dim white As Byte = 255
                    If Color.op_Equality(iFColor, Color.White) = True Then 'JM 15/10/2004
                        white = 254 'JM 15/10/2004
                    End If
                    Dim r As Byte = Marshal.ReadByte(bmd.Scan0, offset + (x * advance) + 2)
                    Dim g As Byte = Marshal.ReadByte(bmd.Scan0, offset + (x * advance) + 1)
                    Dim b As Byte = Marshal.ReadByte(bmd.Scan0, offset + (x * advance))

                    Dim rFild As Byte = Marshal.ReadByte(bmdFilled.Scan0, offset + (x * advance) + 2)
                    Dim gFild As Byte = Marshal.ReadByte(bmdFilled.Scan0, offset + (x * advance) + 1)
                    Dim bFild As Byte = Marshal.ReadByte(bmdFilled.Scan0, offset + (x * advance))

                    If r = rFild And g = gFild And b = bFild Then
                        Marshal.WriteByte(bmdOut.Scan0, offset + (x * advance) + 2, white)
                        Marshal.WriteByte(bmdOut.Scan0, offset + (x * advance) + 1, white)
                        Marshal.WriteByte(bmdOut.Scan0, offset + (x * advance), white)
                    Else
                        If y < itop Then itop = y
                        If y > ibottom Then ibottom = y
                        If x < ileft Then ileft = x
                        If x > iright Then iright = x
                        Marshal.WriteByte(bmdOut.Scan0, offset + (x * advance) + 2, rFild)
                        Marshal.WriteByte(bmdOut.Scan0, offset + (x * advance) + 1, gFild)
                        Marshal.WriteByte(bmdOut.Scan0, offset + (x * advance), bFild)

                    End If

                Next
            Next

            i.UnlockBits(bmd)
            pimgAfterFill.UnlockBits(bmd)
            RetBitmap.UnlockBits(bmd)

            Dim Width As Integer = (iright - ileft) + 4
            Dim Height As Integer = (ibottom - itop) + 4

            If Width < 0 Or Height < 0 Then 'JM 15/10/2004
                Width = (ileft - iright) + 4
                Height = (itop - ibottom) + 4
            End If

            Dim CroppedBitmap As New Bitmap(Width, Height)
            Dim gr As Graphics = Graphics.FromImage(CroppedBitmap)
            gr.DrawImage(RetBitmap, New Rectangle(0, 0, Width, Height), New Rectangle(ileft, itop, Width, Height), GraphicsUnit.Pixel)

            Top = itop 'JM 13/10/2004
            Left = ileft 'JM 13/10/2004

            If Color.op_Equality(iFColor, Color.White) = True Then 'JM 15/10/2004
                CroppedBitmap.MakeTransparent(Color.FromArgb(254, 254, 254)) 'JM 14/10/2004
            Else
                CroppedBitmap.MakeTransparent(Color.White)
            End If


            Return CroppedBitmap 'RetBitmap

        End If

    End Function
End Class

'/ <summary>
'/ Fills a bitmap using a non-recursive flood-fill.
'/ </summary>

Public Class MapFill

    Public Sub New()
    End Sub 'New
    Private Shared stack As New Stack()

    '/ <summary>
    '/ Checks to make sure a pixel is in an image.
    '/ </summary>
    '/ <param name="pos">The position to check</param>
    '/ <param name="bmd">The BitmapData from which the bounds are determined</param>
    '/ <returns>True if the point is in the image</returns>
    Private Shared Function CheckPixel(ByVal pos As Point, ByVal bmd As BitmapData) As Boolean
        Return pos.X > -1 AndAlso pos.Y > -1 AndAlso pos.X < bmd.Width AndAlso pos.Y < bmd.Height
    End Function 'CheckPixel

    '/ <summary>
    '/ Returns the color at a specific pixel
    '/ </summary>
    '/ <param name="pos">The position of the pixel</param>
    '/ <param name="bmd">The locked bitmap data</param>
    '/ <returns>The color of the pixel under the nominated point</returns>
    Private Shared Function GetPixel(ByVal pos As Point, ByVal bmd As BitmapData) As Color
        If CheckPixel(pos, bmd) Then
            'always assumes 32 bit per pixels
            Dim offset As Integer = pos.Y * bmd.Stride + 4 * pos.X
            Return Color.FromArgb(Marshal.ReadByte(bmd.Scan0, offset + 2), Marshal.ReadByte(bmd.Scan0, offset + 1), Marshal.ReadByte(bmd.Scan0, offset))
        Else
            Return Color.FromArgb(0, 0, 0, 0)
        End If
    End Function 'GetPixel
    '/ <summary>
    '/ Sets a pixel at a nominated point to a specified color
    '/ </summary>
    '/ <param name="pos">The coordinate of the pixel to set</param>
    '/ <param name="bmd">The locked bitmap data</param>
    '/ <param name="c">The color to set</param>
    Private Shared Sub SetPixel(ByVal pos As Point, ByVal bmd As BitmapData, ByVal c As Color)
        If CheckPixel(pos, bmd) Then
            'always assumes 32 bit per pixels
            Dim white As Byte = 255
            Dim offset As Integer = pos.Y * bmd.Stride + 4 * pos.X
            Marshal.WriteByte(bmd.Scan0, offset + 2, c.R)
            Marshal.WriteByte(bmd.Scan0, offset + 1, c.G)
            Marshal.WriteByte(bmd.Scan0, offset, c.B)
            Marshal.WriteByte(bmd.Scan0, offset + 3, white) ' 255)
        End If
    End Sub 'SetPixel

    '/ <summary>
    '/ Fills a pixel and its un-filled neigbors with a specified color
    '/ </summary>
    '/ <param name="pos">The position at which to begin</param>
    '/ <param name="bmd">The locked bitmap data</param>
    '/ <param name="c">The color with which to fill the area</param>
    '/ <param name="org">The original colour of the point. Filling stops when all connected pixels of this color are exhausted</param>
    Private Shared Sub FillPixel(ByVal pos As Point, ByVal bmd As BitmapData, ByVal c As Color, ByVal org As Color)
        Dim currpos As New Point(0, 0)
        stack.Push(pos)
        Do
            currpos = CType(stack.Pop(), Point)
            SetPixel(currpos, bmd, c)
            ''If Color.op_Equality(GetPixel(New Point(currpos.X + 1, currpos.Y), bmd), org) = True Then
            ''    stack.Push(New Point(currpos.X + 1, currpos.Y))
            ''End If
            ''If Color.op_Equality(GetPixel(New Point(currpos.X, currpos.Y - 1), bmd), org) = True Then
            ''    stack.Push(New Point(currpos.X, currpos.Y - 1))
            ''End If
            ''If Color.op_Equality(GetPixel(New Point(currpos.X - 1, currpos.Y), bmd), org) = True Then
            ''    stack.Push(New Point(currpos.X - 1, currpos.Y))
            ''End If
            ''If Color.op_Equality(GetPixel(New Point(currpos.X, currpos.Y + 1), bmd), org) = True Then
            ''    stack.Push(New Point(currpos.X, currpos.Y + 1))
            ''End If

            If GetPixel(New Point(currpos.X + 1, currpos.Y), bmd).Equals(org) Then
                stack.Push(New Point(currpos.X + 1, currpos.Y))
            End If
            If GetPixel(New Point(currpos.X, currpos.Y - 1), bmd).Equals(org) Then
                stack.Push(New Point(currpos.X, currpos.Y - 1))
            End If
            If GetPixel(New Point(currpos.X - 1, currpos.Y), bmd).Equals(org) Then
                stack.Push(New Point(currpos.X - 1, currpos.Y))
            End If
            If GetPixel(New Point(currpos.X, currpos.Y + 1), bmd).Equals(org) Then
                stack.Push(New Point(currpos.X, currpos.Y + 1))
            End If
        Loop While stack.Count > 0
    End Sub 'FillPixel
    '/ <summary>
    '/ Fills a bitmap with color.
    '/ </summary>
    '/ <remarks>If a non 32-bit image is passed to this routine and only 32 bit image will be created, the original image will be copied to the new image and filling will take place on the new image which will be handed back when complete. </remarks>
    '/ <param name="img">The image to fill</param>
    '/ <param name="pos">The position to begin filling at</param>
    '/ <param name="color">The color to fill</param>
    '/ <returns>A Bitmap object with the filled area.</returns>
    Public Shared Function Fill(ByVal img As Image, ByVal pos As Point, ByVal color As Color) As Bitmap
        'Ensure the bitmap is in the right format
        Dim bm As Bitmap = CType(img, Bitmap)
        If img.PixelFormat <> PixelFormat.Format32bppArgb Then
            'if it isn't, convert it.
            bm = New Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb)
            Dim g As Graphics = Graphics.FromImage(bm)
            g.InterpolationMode = InterpolationMode.NearestNeighbor
            g.DrawImage(img, New Rectangle(0, 0, bm.Width, bm.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel)
            g.Dispose()
        End If
        'Lock the bitmap data
        Dim bmd As BitmapData = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, bm.PixelFormat)
        'get the color under the point. This is the original.
        Dim org As Color = GetPixel(pos, bmd)
        'Fill the first pixel and recursively fill all it's neighbors
        FillPixel(pos, bmd, color, org)
        'unlock the bitmap
        bm.UnlockBits(bmd)
        Return bm
    End Function 'Fill
End Class 'MapFill