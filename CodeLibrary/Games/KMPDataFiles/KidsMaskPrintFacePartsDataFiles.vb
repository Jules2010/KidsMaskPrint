Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Security.Cryptography

Public Module KidsMaskPrintFacePartsDataFiles

    Enum DataFileName
        Basic
        Halloween2004
        Jungle
    End Enum
    Public Sub ProducePack(ByVal Which As DataFileName)

        Const ImgSize As Integer = 32

        Dim strPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName())

        'Dim Dirs() As String = {"D:\CodeLibrary\Games\KidsMaskPrint\bin\FaceParts\", _
        '    "D:\Build\KMP\FaceParts\", "C:\Program Files\Mindwarp Consultancy Ltd\KidsMaskPrint\FaceParts\"}
        Dim Dirs() As String = {strPath + "\FaceParts\"}

        Dim FileName As String
        Dim FPs As New FacePartStuctureDataFile
        Select Case Which
            Case DataFileName.Basic
                FPs = BasicPack.BasicPack
                FileName = "Basic.dat"
            Case DataFileName.Halloween2004
                FPs = Halloween2004.Halloween2004
                FileName = "Halloween.dat"
            Case DataFileName.Jungle
                FPs = JunglePack.JunglePack
                FileName = "Jungle.dat"
        End Select


        Try : File.Delete(Dirs(0) & FileName) : Catch : End Try
        Try : File.Delete(Dirs(1) & FileName) : Catch : End Try
        Try : File.Delete(Dirs(2) & FileName) : Catch : End Try

        Dim rijndael As New RijndaelManaged

        Dim key As Byte() = {89, 128, 147, 49, 7, 196, 76, 194, 33, 225, 176, 205, 207, 127, 137, 108, 200, 32, 234, 189, 212, 82, 152, 112, 25, 150, 91, 95, 10, 117, 248, 209}
        Dim iv As Byte() = {228, 63, 134, 217, 160, 206, 233, 198, 194, 17, 158, 98, 122, 16, 193, 216}

        Dim encryptor As ICryptoTransform = rijndael.CreateEncryptor(key, iv)

        Dim formatter As New BinaryFormatter
        Dim output As Stream = File.Open(Dirs(0) & FileName, FileMode.Create)
        Dim cryptoOutput As New CryptoStream(output, encryptor, CryptoStreamMode.Write)
        formatter.Serialize(cryptoOutput, FPs)
        cryptoOutput.FlushFinalBlock()
        cryptoOutput.Close()
        output.Close()

        'File.Copy(Dirs(0) & FileName, Dirs(1) & FileName)
        'File.Copy(Dirs(0) & FileName, Dirs(2) & FileName)

        Console.WriteLine("Pack " & FileName & " Updated!")
    End Sub

End Module
