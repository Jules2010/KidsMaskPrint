Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.Marshal

Friend Class FloodFill

    Function FloodFillIt(ByRef raster As Bitmap, ByVal iX As Integer, ByVal iY As Integer, ByVal iFColor As Color, _
        ByVal iBColor As Color, ByRef ClipTop As Integer, ByRef ClipLeft As Integer) As Bitmap

        Dim Bitmapbefore As Bitmap = raster.Clone

        Dim mf As New MapFill
        raster = mf.Fill(raster, New Point(iX, iY), iFColor)

        Return FloodFillClipImage(Bitmapbefore, raster, ClipTop, ClipLeft, iFColor)

    End Function

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
                    If Color.op_Equality(iFColor, Color.White) = True Then
                        white = 254
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

            If Width < 0 Or Height < 0 Then
                Width = (ileft - iright) + 4
                Height = (itop - ibottom) + 4
            End If

            Dim CroppedBitmap As New Bitmap(Width, Height)
            Dim gr As Graphics = Graphics.FromImage(CroppedBitmap)
            gr.DrawImage(RetBitmap, New Rectangle(0, 0, Width, Height), New Rectangle(ileft, itop, Width, Height), GraphicsUnit.Pixel)

            Top = itop
            Left = ileft

            If Color.op_Equality(iFColor, Color.White) = True Then
                CroppedBitmap.MakeTransparent(Color.FromArgb(254, 254, 254))
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
    Private Shared stack As New stack

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