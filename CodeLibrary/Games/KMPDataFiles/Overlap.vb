Module Overlap
    Public Const ImgSize As Integer = 32
    Friend Function ResizeImageObj(ByVal SoureImage As Image, ByVal maxSize As Integer) As Image

        Dim inp As New IntPtr
        Dim imgHeight, imgWidth As Double

        Dim imageObj As System.Drawing.Image = SoureImage 'System.Drawing.Image.FromFile(pstrSourceFile)


        Dim bm As Bitmap = New Bitmap(imageObj)

        imgHeight = bm.Height
        imgWidth = bm.Width


        If (imgWidth > maxSize Or imgHeight > maxSize) Then
            Dim deltaWidth As Double = imgWidth - maxSize
            Dim deltaHeight As Double = imgHeight - maxSize
            Dim scaleFactor As Double

            If deltaHeight > deltaWidth Then
                'Scale by the height
                scaleFactor = maxSize / imgHeight
            Else
                'Scale by the Width
                scaleFactor = maxSize / imgWidth
            End If

            imgWidth *= scaleFactor
            imgHeight *= scaleFactor
        End If
        Try
            Dim w As Integer = Convert.ToInt32(imgWidth)
            Dim h As Integer = Convert.ToInt32(imgHeight)
            Dim bmp As System.Drawing.Image = bm.GetThumbnailImage(w, h, Nothing, inp)

            bm = New Bitmap(bmp)
            Return bm

            imageObj.Dispose()
            bmp.Dispose()
            imageObj = Nothing
            bmp = Nothing
        Catch ex As Exception

        End Try
    End Function
End Module
<DoNotObfuscateAttribute()> Friend Module Base
End Module
<System.AttributeUsage(AttributeTargets.Class Or AttributeTargets.Field _
Or AttributeTargets.Method Or AttributeTargets.Parameter Or AttributeTargets.Enum)> _
Friend Class ObfuscateAttribute
    Inherits System.Attribute
End Class 'ObfuscateAttribute

<System.AttributeUsage(AttributeTargets.Class Or AttributeTargets.Field _
Or AttributeTargets.Method Or AttributeTargets.Parameter Or AttributeTargets.Enum)> _
Friend Class DoNotObfuscateAttribute
    Inherits System.Attribute
End Class 'DoNotObfuscateAttribute

<System.AttributeUsage(AttributeTargets.Assembly, allowmultiple:=True)> _
Friend Class ObfuscateBlockAttribute
    Inherits System.Attribute
    Private BlockString As String
    Public Sub New(ByVal BlockString As String)
        MyClass.BlockString = BlockString
    End Sub
End Class
