Imports System.Security.Cryptography
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

'--- parts data file ---
<DoNotObfuscateAttribute(), Serializable()> Friend Class FacePartStuctureDataFile
    Friend Description As String
    Friend DescImage As Image
    Friend Parts As New ArrayList
    Friend ProductNumber As String
    Friend VersionNum As String
    '--- Reserved for future use ---
    Friend AdditionalS1 As String
    Friend AdditionalS2 As String
    Friend AdditionalS3 As String
    Friend AdditionalO1 As Object
    Friend AdditionalO2 As Object
    Friend AdditionalO3 As Object
    Friend AdditionalI1 As Image
    Friend AdditionalI2 As Image
    Friend AdditionalA1 As New ArrayList
    Friend AdditionalA2 As New ArrayList
    '--- Reserved for future use ---
End Class
<DoNotObfuscateAttribute(), Serializable()> Public Class Part

    'Friend Structure SubPart
    '    Dim sngPos As Point
    '    Dim VertFlip As Boolean
    '    Dim HorizFlip As Boolean
    'End Structure
    Friend PartType As FacePartEnums.ePartType
    Friend FaceMaster As String
    Friend ThumbImage As Image
    Friend FullImage As Image
    Friend BothParts As Boolean
    Friend LeftPart As New Point
    Friend RightPart As New Point
    '--- Reserved for future use ---
    Friend AdditionalS1 As String
    Friend AdditionalS2 As String
    Friend AdditionalO1 As Object
    Friend AdditionalO2 As Object
    Friend AdditionalI1 As Image
    Friend AdditionalI2 As Image
    '--- Reserved for future use ---

    Friend ReadOnly Property Bounds() As Rectangle
        Get
            Return New Rectangle(LeftPart.X, LeftPart.Y, FullImage.Width, FullImage.Height)
        End Get
    End Property

End Class
Public Class FacePartEnums
    Public Enum ePartType
        Eye
        Ear
        Mouth
        Nose
        Outline
        Misc
    End Enum
    Public Enum ePositionSelection
        Left
        Both
        Right
    End Enum
End Class

Friend Class PaintBrush
    Friend BrushColour As Color
    Friend BrushWidth As Integer
    '--- Reserved for future use ---
    Friend AdditionalS1 As String
    Friend AdditionalS2 As String
    Friend AdditionalS3 As String
    '--- Reserved for future use ---
End Class
Friend Class GPArr
    Friend PointArray() As PointF
    Friend TypeArray() As Byte
End Class

<DoNotObfuscateAttribute(), Serializable()> Friend Class CDDataIncluded
    Friend NumofPacks As Integer
    Friend Pack1 As String
    Friend Pack2 As String
    Friend Pack3 As String
End Class
<DoNotObfuscateAttribute(), Serializable()> Friend Class CDPack
    Friend PackName As String
    Friend KeyFileName As String
End Class
