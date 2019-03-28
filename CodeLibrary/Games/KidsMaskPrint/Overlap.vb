Friend Module Overlap
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
