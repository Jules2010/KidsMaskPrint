Module DataFileAcceptLicense
    Friend Function AcceptDataFileLicense(ByVal pDataFileDesc As String, ByVal pProdNum As String, _
        ByVal pform As Form, ByVal pstrProposedKeyFile As String, ByRef pstrDataFileState As String)

        Busy(pform, True)

        '---- This part uses program license to derive serial block which data file license will need ----
        Dim Dets As strat1.UnlockDetails
        Try
            'ProdNum must be used here, as it won't fail the function, as it doesn't inclde the GUID 100 flag.
            Unlock(System.IO.Path.GetDirectoryName( _
                System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl", Dets, pProdNum, "")
        Catch
            '
        End Try
        '---- This part uses program license to derive serial block which data file license will need ----

        Dim lSerialCode As String = Dets.strSerialBlock & "-" & DataFileProductIdent(pProdNum) & "-" & _
            ProduceCheckDigs(Dets.strSerialBlock & "-" & DataFileProductIdent(pProdNum))

        Dim dlgResult As DialogResult

        Dim DFAR As New DataFileAcceptReg
        With DFAR
            .Owner = pform
            .Caption = NameMe("")
            .DataFileDesc = pDataFileDesc
            .SerialCode = lSerialCode
            .ButtonType = DataFileAcceptReg.eButtonType.BevelRed
            dlgResult = .ShowDialog()

            If dlgResult = DialogResult.OK Then
                'create temp license file
                Dim lstrTemp As String
                Dim clsEnc As New MyCrypto

                If x(.LicenseCode) = "" Then
                    Busy(pform, False)
                    MessageBox.Show("Your license code was not accepted!", NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Function
                End If

                lstrTemp = clsEnc.Encrypt(x(.LicenseCode), "bUnn1es#j*mp@thr")
                clsEnc = Nothing

                Dim lstrEncFile As String = System.IO.Path.GetDirectoryName( _
                    System.Reflection.Assembly.GetEntryAssembly.Location.ToString()) & "\Temp-" & _
                    Date.Now.ToString("dddd-dd-MMM-yyyy-HH-mm-ss") & ".tmp"

                ReDim Preserve lstrTempFiles(lstrTempFiles.GetUpperBound(0) + 1)
                lstrTempFiles(lstrTempFiles.GetUpperBound(0)) = lstrEncFile

                Dim lintFreeFile As Integer = Microsoft.VisualBasic.FreeFile()
                Microsoft.VisualBasic.FileOpen(lintFreeFile, lstrEncFile, Microsoft.VisualBasic.OpenMode.Output)
                Microsoft.VisualBasic.Print(lintFreeFile, lstrTemp)
                Microsoft.VisualBasic.FileClose(lintFreeFile)

                'check license
                Dim lintCheck As Integer = 16
                Try
                    lintCheck = Unlock(lstrEncFile, Nothing, pProdNum, Dets.strSerialBlock)
                Catch

                End Try

                If lintCheck <> 245 + 12 Then
                    Busy(pform, False)
                    MessageBox.Show("Your license code was not accepted!", NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Try
                        System.IO.File.Delete(lstrEncFile)
                    Catch
                    End Try
                Else

                    Try
                        System.IO.File.Delete(pstrProposedKeyFile)
                    Catch
                    End Try

                    System.IO.File.Copy(lstrEncFile, pstrProposedKeyFile)
                    Busy(pform, False)
                    MessageBox.Show("Your license code was accepted!", NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Information)
                    pstrDataFileState = "1"
                End If
            End If

        End With

        Busy(pform, False)
    End Function
    Private Function ProduceCheckDigs(ByVal pstrInput As String) As String

        Dim Out As Integer = 0

        Try
            Dim lintArrInc As Integer

            For lintArrInc = 0 To pstrInput.Length
                Out += AscGet(MidGet(pstrInput, lintArrInc + 1, 1))
            Next lintArrInc
        Catch
            '
        End Try

        Return Out.ToString

    End Function
End Module
