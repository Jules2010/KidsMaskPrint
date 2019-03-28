Friend Module Overlap
    '    Friend Function xIsAboveOrEqualWinXp() As Boolean 

    '        xIsAboveOrEqualWinXp = False

    '        Dim osInfo As OperatingSystem
    '        osInfo = System.Environment.OSVersion

    '        With osInfo

    '            If .Platform = PlatformID.Win32NT Then
    '                If .Version.Major >= 5 Then
    '                    If .Version.Minor >= 1 Then
    '                        xIsAboveOrEqualWinXp = True
    '                    End If
    '                End If
    '            End If
    '        End With
    '    End Function
    Function EncodeToHtml(ByVal pstrSentence As String) As String

        Dim lintArrInc As Integer
        Dim lstrOutPut As String

        pstrSentence = Web.HttpUtility.HtmlEncode(pstrSentence)

        For lintArrInc = 1 To pstrSentence.Length
            Dim lintChar As Integer = AscGet(MidGet(pstrSentence, lintArrInc, 1))
            Select Case lintChar
                Case 32 To 127, 160 To 255
                    lstrOutPut &= ChrGet(lintChar)
                Case Else
                    lstrOutPut &= "&#" & (lintChar) & ";"
            End Select
        Next lintArrInc

        Return lstrOutPut

    End Function
End Module
