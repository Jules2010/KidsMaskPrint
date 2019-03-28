'Helper class representing a piece of a  
Friend Class Piece
    Private m_location As Point 'The top-left corner of the piece  
    Private m_bitmap As Bitmap 'The image used to draw the piece  
    'Private m_BitmapName As String 
    Private m_VertFlip As Boolean = False
    Private m_HorizFlip As Boolean = False
    Private m_PieceName As String 
    'Constructs a new Piece object  
    '<param name="imagePath"> The full path and filename of the image to load </param> 
    Friend Sub New()

    End Sub 'New

    Friend Sub SetImage(ByVal imagePath As String)
        m_bitmap = Nothing 
        m_bitmap = Bitmap.FromFile(imagePath)  '

        If m_VertFlip = True Then
            m_bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY)
        End If

        If m_HorizFlip = True Then
            m_bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX)
        End If

        m_location = Point.Empty
    End Sub 'New

    Friend Sub SetImageObj(ByVal pImage As Image)
        m_bitmap = Nothing 
        m_bitmap = pImage

        If m_VertFlip = True Then
            m_bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY)
        End If

        If m_HorizFlip = True Then
            m_bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX)
        End If

        m_location = Point.Empty
    End Sub 'New
    'The top-left corner of the piece  
    Friend Property Location() As Point
        Get
            Return m_location
        End Get
        Set(ByVal Value As Point)
            m_location = Value
        End Set
    End Property

    'The image used to draw the piece  
    Friend ReadOnly Property Bitmap() As Bitmap
        Get
            Return m_bitmap
        End Get
    End Property

    Friend Property PieceName() As String 
        Get
            Return m_PieceName
        End Get
        Set(ByVal Value As String)
            m_PieceName = Value
        End Set
    End Property

    '--- 
    Private m_SourceDataFileName As String
    Friend Property SourceDataFileName() As String
        Get
            Return m_SourceDataFileName
        End Get
        Set(ByVal Value As String)
            m_SourceDataFileName = Value
        End Set
    End Property
    Private m_DataFileItemNum As Integer
    Friend Property DataFileItemNum() As Integer
        Get
            Return m_DataFileItemNum
        End Get
        Set(ByVal Value As Integer)
            m_DataFileItemNum = Value
        End Set
    End Property
    '--- 

    Friend Property VertFlip() As Boolean
        Get
            Return m_VertFlip
        End Get
        Set(ByVal Value As Boolean)
            m_VertFlip = Value
        End Set
    End Property
    Friend Property HorizFlip() As Boolean
        Get
            Return m_HorizFlip
        End Get
        Set(ByVal Value As Boolean)
            m_HorizFlip = Value
        End Set
    End Property

    'The rectangle where the piece is drawn  
    Friend ReadOnly Property Bounds() As Rectangle
        Get
            Return New Rectangle(m_location, m_bitmap.Size)
        End Get
    End Property


    'Returns true if the testPoint is over a non-transparent pixel on the piece where it is drawn  
    Friend Function IsPointOverMe(ByVal testPoint As Point) As Boolean
        'Is it within the drawn rectangle? 
        Dim isOver As Boolean = Bounds.Contains(testPoint)
        If isOver Then
            'Is it non-transparent? 
            isOver = m_bitmap.GetPixel(testPoint.X - m_location.X, testPoint.Y - m_location.Y).A > 0
        End If

        Return isOver
    End Function 'IsPointOverMe
End Class 'Piece