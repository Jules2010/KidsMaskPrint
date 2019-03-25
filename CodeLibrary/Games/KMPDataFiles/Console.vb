Module ConsoleMod
    Sub Main()

        ProducePack(DataFileName.Basic)

        ProducePack(DataFileName.Halloween2004)

        ProducePack(DataFileName.Jungle)

        'Produce CD.Dat file posibilities
        Dim CustomerOrderJunglePackOnly As New CDDataIncluded()
        With CustomerOrderJunglePackOnly
            .NumofPacks = 1
            .Pack1 = "Jungle Pack"
        End With
        'WriteCDDataIncluded("D:\Build\KMP\CD.Dat\CustomerOrderJunglePackOnly.dat", CustomerOrderJunglePackOnly)

        Dim CustomerOrderHalloweenPackOnly As New CDDataIncluded()
        With CustomerOrderHalloweenPackOnly
            .NumofPacks = 1
            .Pack1 = "Halloween Pack"
        End With
        'WriteCDDataIncluded("D:\Build\KMP\CD.Dat\CustomerOrderHalloweenPackOnly.dat", CustomerOrderHalloweenPackOnly)

        'Produce Pack.Dat file possibilities
        Dim JunglePack As New CDPack()
        JunglePack.PackName = "Jungle Pack"
        JunglePack.KeyFileName = "Jungle.key"
        'WriteCDPack("D:\Build\KMP\Pack.Dat\JunglePack.dat", JunglePack)

        Dim HalloweenPack As New CDPack()
        HalloweenPack.PackName = "Halloween Pack"
        HalloweenPack.KeyFileName = "Halloween.key"
        'WriteCDPack("D:\Build\KMP\Pack.Dat\HalloweenPack.dat", HalloweenPack)

    End Sub
    'Private Sub WriteCDPack(ByVal pstrFileName As String, ByVal PackClass As CDPack)

    '    Try : IO.File.Delete(pstrFileName) : Catch : End Try
    '    Dim FileStream As IO.Stream = IO.File.Open(pstrFileName, IO.FileMode.Create)
    '    Dim FileFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
    '    FileFormatter.Serialize(FileStream, PackClass)
    '    FileStream.Close()

    'End Sub
    'Private Sub WriteCDDataIncluded(ByVal pstrFileName As String, ByVal CDDataIncludedClass As CDDataIncluded)

    '    Try : IO.File.Delete(pstrFileName) : Catch : End Try
    '    Dim FileStream As IO.Stream = IO.File.Open(pstrFileName, IO.FileMode.Create)
    '    Dim FileFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
    '    FileFormatter.Serialize(FileStream, CDDataIncludedClass)
    '    FileStream.Close()

    'End Sub
End Module
