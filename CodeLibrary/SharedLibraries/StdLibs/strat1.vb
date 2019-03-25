Imports System.Text
Imports System.IO
Imports System.Security.Cryptography
Imports AppBasic
Friend Class MyCrypto
    ' declare crypto service provider 
    'someday the class will be able to use DES TripelDES and RijndaelManaged 
    Private myCryptoService As New RijndaelManaged()
    'create the key 
    Private Function GetLegalKey(ByVal key As String) As Byte()
        Dim sTemp As String

        If myCryptoService.LegalKeySizes.Length > 0 Then
            Dim lessSize As Integer = 0
            Dim moreSize = myCryptoService.LegalKeySizes(0).MinSize

            While key.Length * 8 > moreSize
                lessSize = moreSize
                moreSize += myCryptoService.LegalKeySizes(0).SkipSize
            End While
            sTemp = key.PadRight(moreSize / 8, " ")
        Else
            sTemp = key
        End If
        Return ASCIIEncoding.ASCII.GetBytes(sTemp)
    End Function
    Friend Function Decrypt(ByVal vText As String, ByVal vkey As String) As String
        Try
            Dim msin As New System.IO.MemoryStream()
            Try
                msin.Write(Convert.FromBase64String(vText), 0, Convert.FromBase64String(vText).Length)
            Catch
                Return "Error Processing Request!"
            End Try
            msin.Position = 0

            Dim msout As New System.IO.MemoryStream()
            Dim buf() As Byte = New Byte(4096) {}
            Dim byteKey() As Byte = GetLegalKey(vkey)
            myCryptoService.Key = byteKey
            myCryptoService.IV = byteKey

            Dim trans As ICryptoTransform = myCryptoService.CreateDecryptor()
            Dim cs As CryptoStream = New CryptoStream(msin, trans, CryptoStreamMode.Read)
            Dim nLen As Integer
            nLen = cs.Read(buf, 0, buf.Length)
            While (nLen > 0)
                msout.Write(buf, 0, nLen)
                nLen = cs.Read(buf, 0, buf.Length)
            End While

            Decrypt = System.Text.Encoding.Default.GetString(msout.ToArray)

            msin.Close()
            msout.Close()
        Catch e As CryptographicException
            Return "Error Processing Request!"
        End Try
    End Function
    Friend Function Encrypt(ByVal vText As String, ByVal vKey As String) As String
        Try
            Dim msin As New System.IO.MemoryStream()
            msin.Write(System.Text.Encoding.Default.GetBytes(vText), 0, System.Text.Encoding.Default.GetBytes(vText).Length)
            msin.Position = 0

            Dim msout As New System.IO.MemoryStream()
            Dim buf() As Byte = New Byte(4096) {}
            Dim byteKey() As Byte = GetLegalKey(vKey)
            myCryptoService.Key = byteKey
            myCryptoService.IV = byteKey

            Dim trans As ICryptoTransform = myCryptoService.CreateEncryptor()
            Dim cs As CryptoStream = New CryptoStream(msout, trans, CryptoStreamMode.Write)
            Dim nLen As Integer

            nLen = msin.Read(buf, 0, buf.Length)

            While (nLen > 0)
                cs.Write(buf, 0, nLen)
                nLen = msin.Read(buf, 0, buf.Length)
            End While

            cs.FlushFinalBlock()

            Encrypt = System.Convert.ToBase64String(msout.ToArray)

            cs.Close()
            msin.Close()
        Catch e As CryptographicException
            Return e.Message & "Error Processing Request!"
        End Try
    End Function
End Class
Friend Module strat1
    Friend Structure UnlockDetails
        Dim str1Name As String
        Dim str2Street As String
        Dim str3City As String
        Dim str4State As String
        Dim str5Zip As String
        Dim str6Country As String
        Dim str7Email As String
        Dim str8OrderDate As String
        Dim str9TransNum As String
        'Dim strCode As String
        Dim str11LicenseName As String
        Dim strSerialBlock As String
    End Structure
    Friend Function Unlock(ByVal pstrFileToCheck As String, ByRef Dets As UnlockDetails, _
        ByVal pDataFileProdNumber As String, ByVal pstrProgramLicenseHash As String, Optional ByVal RBUse As Boolean = False, Optional ByVal DontCheckGUID As Boolean = False) As Integer

        Dim lstrFileStr As String = ""

        Dim OpenFile As FileStream = New FileStream(pstrFileToCheck, FileMode.Open, FileAccess.Read, FileShare.Read)             'JM 21/08/2003

        Dim StreamReader As StreamReader = New StreamReader(OpenFile)
        lstrFileStr = StreamReader.ReadToEnd '.Read 'Line()
        StreamReader.Close()
        OpenFile.Close()

        Dim clsDec As New MyCrypto()
        Dim decstring As String

        If RBUse = False Then 'JM 12/05/2005
            decstring = clsDec.Decrypt(lstrFileStr, "bUnn1es#j*mp@thr")
        Else
            'decstring = RBDecypt(lstrFileStr) 'JM 12/05/2005
            decstring = RBDecypt(pstrFileToCheck) 'JM 07/08/2005
        End If

        Dim DelimChar As String = "#@#"
        decstring = Groovydecrypt("hfdsfodskewjiwi215209dsf9fdlkxlkds", decstring)

        Dim LastDelimPost As Integer = Occurences(decstring, DelimChar) 'JM 10/09/2004

        'Dim cpos As Integer = Microsoft.VisualBasic.InStr(decstring, RetNthStr(decstring, 11, DelimChar)) + (RetNthStr(decstring, 11, DelimChar)).Length + 3
        'JM 10/09/2004
        Dim cpos As Integer = Microsoft.VisualBasic.InStr(decstring, RetNthStr(decstring, LastDelimPost, DelimChar)) + (RetNthStr(decstring, LastDelimPost, DelimChar)).Length + 3

        Dim lstrCode As String
        Try
            'MessageBox.Show(decstring.Length + " " + cpos + " " + decstring.Length)
            lstrCode = Microsoft.VisualBasic.Mid(decstring, cpos, (decstring.Length - (cpos - 1)))
        Catch ex As System.ArgumentOutOfRangeException
            'file not long enough
            Exit Function
        End Try

        Dim lstrStr As String
        With Dets
            .str1Name = RetNthStr(decstring, 1, DelimChar)
            .str2Street = RetNthStr(decstring, 2, DelimChar)
            .str3City = RetNthStr(decstring, 3, DelimChar)
            .str4State = RetNthStr(decstring, 4, DelimChar)
            .str5Zip = RetNthStr(decstring, 5, DelimChar)
            .str6Country = RetNthStr(decstring, 6, DelimChar)
            .str7Email = RetNthStr(decstring, 7, DelimChar)
            .str8OrderDate = RetNthStr(decstring, 8, DelimChar)
            .str9TransNum = RetNthStr(decstring, 9, DelimChar)
            .str11LicenseName = RetNthStr(decstring, 11, DelimChar)

            '--- 'JM 09/12/2003 ---
            Dim lstrKeys() As String 'Dim lstrKeys(3) As String 'JM 11/12/2003
            'Dim lstrKeysCharGoodCtr(1) As Integer 'JM 11/12/2003
            Dim lstrKeysCharGoodCtr() As Integer 'JM 01/08/2004

            BlackKeys(lstrKeys) 'JM 01/08/2004

            ReDim lstrKeysCharGoodCtr(lstrKeys.GetUpperBound(0)) 'JM 01/08/2004

            Dim lbooBlackKeyFound As Boolean = False 'true 'JM 11/12/2003
            Dim lintArrInc3 As Integer 'JM 11/12/2003
            For lintArrInc3 = 0 To lstrKeys.GetUpperBound(0) 'JM 11/12/2003
                Dim lintArrInc2 As Integer
                Dim lintCharCtr As Integer = 0 'JM 11/12/2003
                'JM 07/09/2004 'You can't include new SerialBlock field in BlackKey check as it isn't present in old unlock codes.
                Dim lstrUnEncodedStr As String = .str1Name & "%@" & .str3City & "%@" & .str4State & "%@" & .str6Country & "%@" & _
                        LeftGet(.str7Email, InStrGet(.str7Email, "@") - 1) & "%@" & .str8OrderDate & "%@" & .str9TransNum & "%@" & .str11LicenseName
                '--- 'JM 21/12/2003 ---
                Dim lintMinLength As Integer
                If lstrKeys(lintArrInc3).Length > lstrUnEncodedStr.Length Then
                    lintMinLength = lstrUnEncodedStr.Length
                Else
                    'lintMinLength = lstrKeys(lintArrInc3).Length
                    'Not sure why, but an extra hash is being added to current license.
                    'this fixes the problem.
                    lintMinLength = lstrKeys(lintArrInc3).Length - 1 'JM 04/08/2004
                End If



                'For lintArrInc2 = 0 To lstrKeys(lintArrInc3).Length - 1 'lstrRetVal.Length - 1
                '--- 'JM 21/12/2003 ---
                For lintArrInc2 = 0 To lintMinLength
                    'If CharConvert(MidGet(lstrUnEncodedStr, lintArrInc2 + 1, 1)) <> MidGet(lstrRetVal, lintArrInc2 + 1, 1) Then
                    '''--- TESTING 'JM 04/08/2004 ---
                    ''Dim LookingAtCodeChar As String = CharConvert(MidGet(lstrUnEncodedStr, lintArrInc2 + 1, 1))
                    ''Dim ThisBlackKeyChar As String = MidGet(lstrKeys(lintArrInc3), lintArrInc2 + 1, 1)
                    ''Console.WriteLine(lintArrInc3 & ":=" & LookingAtCodeChar & "=" & ThisBlackKeyChar & "=")
                    '''--- TESTING 'JM 04/08/2004 ---
                    If CharConvert(MidGet(lstrUnEncodedStr, lintArrInc2 + 1, 1)) = MidGet(lstrKeys(lintArrInc3), lintArrInc2 + 1, 1) Then 'JM 11/12/2003
                        'lbooBlackKeyFound = False
                        'Exit For
                        lstrKeysCharGoodCtr(lintArrInc3) += 1 'JM 11/12/2003
                    End If
                    lintCharCtr += 1
                Next lintArrInc2
                'Console.WriteLine(lstrKeysCharGoodCtr(lintArrInc3) & " " & lintCharCtr)
                If lstrKeysCharGoodCtr(lintArrInc3) = lintCharCtr Then
                    'bad key found
                    lbooBlackKeyFound = True
                    Exit For
                End If
            Next lintArrInc3 'JM 11/12/2003

            ReDim lstrKeys(0) 'JM 11/12/2003

            'JM 07/09/2004 'Don't need to add SerialBlock here as its isn't used and would cause problems with old codes.
            lstrStr = "#@#" & .str4State & DelimChar & .str2Street & DelimChar & .str1Name & DelimChar & _
                .str8OrderDate & DelimChar & .str3City & DelimChar & .str9TransNum & DelimChar & _
                .str7Email & DelimChar & .str5Zip & DelimChar & .str11LicenseName & DelimChar & _
                .str6Country & DelimChar & RetNthStr(decstring, 10, DelimChar) & DelimChar

            If lbooBlackKeyFound = True Then
                'MessageBox.Show("Black Key Found!")
                lstrStr = ""
            End If

            '--- 'JM 09/12/2003 ---
            'encrypt peoples details who you don't want to have keys, RE above fields

            'lstrStr = "#@#" & RetNthStr(decstring, 4, DelimChar) & DelimChar & RetNthStr(decstring, 2, DelimChar) & DelimChar & RetNthStr(decstring, 1, DelimChar) & DelimChar & _
            '    RetNthStr(decstring, 8, DelimChar) & DelimChar & RetNthStr(decstring, 3, DelimChar) & DelimChar & RetNthStr(decstring, 9, DelimChar) & DelimChar & _
            '    RetNthStr(decstring, 7, DelimChar) & DelimChar & RetNthStr(decstring, 5, DelimChar) & DelimChar & RetNthStr(decstring, 11, DelimChar) & DelimChar & _
            '    RetNthStr(decstring, 6, DelimChar) & DelimChar & RetNthStr(decstring, 10, DelimChar) & DelimChar

            'JM 09/12/2003 - moved further up
            'lstrStr = "#@#" & .str4State & DelimChar & .str2Street & DelimChar & .str1Name & DelimChar & _
            '    .str8OrderDate & DelimChar & .str3City & DelimChar & .str9TransNum & DelimChar & _
            '    .str7Email & DelimChar & .str5Zip & DelimChar & .str11LicenseName & DelimChar & _
            '    .str6Country & DelimChar & RetNthStr(decstring, 10, DelimChar) & DelimChar
        End With

        'Dim lstrTest As String
        'Dim lintArrIncx As Integer

        'For lintArrIncx = 0 To lstrCode.Length - 1
        '    Dim lstrChar As String = Microsoft.VisualBasic.Mid(lstrCode, lintArrIncx + 1, 1)
        '    lstrTest &= DecodeDecoyChar(lstrChar)
        'Next lintArrIncx

        'Console.WriteLine("C" & lstrCode.Length & " " & lstrCode)
        'Console.WriteLine("T" & lstrTest.Length & " " & lstrTest)
        'Console.WriteLine("S" & lstrStr.Length & " " & lstrStr)

        lstrStr = (lstrStr).ToUpper

        Dim lintArrInc As Integer
        Dim lof As Integer = (lstrStr).Length
        Dim lintItem As Integer = lof / 22 '11
        Dim lstrDecchar As String
        Dim lstrActchar As String
        Dim lintResult As Integer = 0

        '''---- REMOVE AFTER TESTING ----
        ''Dim DecBuild As String = ""
        ''Dim AscBuild As String = ""
        ''MessageBox.Show(lstrCode.Length & " " & lstrStr.Length)

        ''Try
        ''    For lintArrInc = 1 To lstrStr.Length
        ''        lstrDecchar = (DecodeDecoyCharPart(Microsoft.VisualBasic.Mid(lstrCode, lintArrInc, 1))).Trim
        ''        lstrActchar = (Microsoft.VisualBasic.Mid(lstrStr, lintArrInc, 1)).Trim
        ''        If lstrDecchar = "" Then
        ''            lstrDecchar = "-"
        ''            lstrActchar = "-"
        ''        End If

        ''        DecBuild &= lstrDecchar
        ''        AscBuild &= lstrActchar

        ''    Next lintArrInc
        ''Catch
        ''End Try

        ''Console.WriteLine(" ")
        ''Console.WriteLine(lstrStr)
        ''Console.WriteLine(DecBuild)
        ''Console.WriteLine(AscBuild)
        '''---- REMOVE AFTER TESTING ----

        'MessageBox.Show("lintItem * 21=" & (lintItem * 21) & CR() & "lstrcode.Length=" & lstrCode.Length & CR() & "lstrStr.length=" & lstrStr.Length)
        'JM 07/09/2004 ' You can't add SerialBlock into character check as it isn't present in old codes.
        For lintArrInc = 1 To 20 '21 '10
            lstrDecchar = (DecodeDecoyCharPart(Microsoft.VisualBasic.Mid(lstrCode, lintItem * lintArrInc, 1))).Trim
            lstrActchar = (Microsoft.VisualBasic.Mid(lstrStr, lintItem * lintArrInc, 1)).Trim

            'Console.WriteLine(lintArrInc & " " & lintItem * lintArrInc & " (" & lstrDecchar & ") (" & lstrActchar & ")")
            If lstrDecchar <> "" And lstrActchar <> "" Then
                If lstrActchar = lstrDecchar Then
                    lintResult = 2
                Else
                    lintResult = 3
                    Exit For
                End If
            End If
        Next lintArrInc

        Dim objGuid As System.Runtime.InteropServices.GuidAttribute = _
            System.Runtime.InteropServices.GuidAttribute.GetCustomAttribute( _
            System.Reflection.Assembly.GetExecutingAssembly, GetType(System.Runtime.InteropServices.GuidAttribute))


        '--- 'JM 07/09/2004 ---
        Dim Guid As String = RetNthStr(decstring, 10, DelimChar)
        Select Case Guid.Length
            Case 36
                'Old license code
            Case Else
                Dim Extra As String = MidGet(Guid, 37, Guid.Length - 36)
                If Extra = "100" Then '100 is for data file selling usage
                    Dim SerialBlock As String = RetNthStr(decstring, 12, DelimChar)
                    'SerialBlock holds the number/code which ties the "data file" license key to the program licese key
                    'TODO: NEED TO TEST THIS

                    '--- 'JM 10/09/2004 ---
                    Dim RetSerialBlock As String = ""
                    Dim RetProdIdentifier As String = ""

                    GetDigs(SerialBlock, RetSerialBlock, RetProdIdentifier, Nothing)
                    'this will also check that we have he right data file identity
                    Dim ProductIdentity As String = DataFileProductIdent(pDataFileProdNumber)
                    If RetProdIdentifier <> ProductIdentity Then
                        lintResult = 0
                    End If

                    'If the Serial block from the data file equals that from program license file
                    If RetSerialBlock <> pstrProgramLicenseHash Then
                        lintResult = 0
                    End If
                    '--- 'JM 10/09/2004 ---
                End If
                Guid = LeftGet(Guid, 36)
        End Select


        'If RetNthStr(decstring, 10, DelimChar) <> objGuid.Value Then 
        If DontCheckGUID = False Then 'JM 07/08/2005
            If Guid <> objGuid.Value Then
                lintResult = 0
            End If
        End If
        '--- 'JM 07/09/2004 ---

        Select Case lintResult
            Case 0
                'characters do not match code OR
                'Shouldn't happen - char not present in function (possible!, but could would be test by MCL first!)
                'MsgBox("fail 0")
                Unlock = 256
            Case 2
                '--- 'JM 09/09/2004 ---
                'this should only be used on Product license NOT data files!!
                'MessageBox.Show(lstrCode, lstrCode.Length)
                Dets.strSerialBlock = HashData(lstrCode, True)

                'MessageBox.Show(Dets.strSerialBlock)
                'MessageBox.Show(HashData(lstrCode, False))
                '--- 'JM 09/09/2004 ---
                'MsgBox("OK!")
                Unlock = 257
            Case 3
                'MsgBox("Cracked!")
                Unlock = 268
        End Select

    End Function
    Private Function CharConvert(ByVal pstrChar As String) As String

        Dim intChar As Integer = AscGet(pstrChar) + 35
        Dim lintOver As Integer = (intChar - 255)
        If lintOver > 0 Then
            Return ChrGet(lintOver)
        Else
            Return ChrGet(intChar)
        End If

    End Function
    Friend Function DecodeDecoyCharPart(ByVal pstrChar As String) As String
        Dim lret As String = ""

        Select Case Microsoft.VisualBasic.Asc(pstrChar)
            Case 145 : lret = "0"
            Case 137 : lret = "6"
            Case 162 : lret = "A"
            Case 226 : lret = "G"
            Case 132 : lret = "K"
            Case 212 : lret = "O"
            Case 216 : lret = "S"
            Case 163 : lret = "W"
            Case 225 : lret = "&"
            Case 227 : lret = "("
        End Select

        DecodeDecoyCharPart = lret

    End Function
    Friend Function TakeCare(ByRef lintThreads As Integer, ByRef pDets As strat1.UnlockDetails)
        Dim Info As New System.IO.FileInfo(System.IO.Path.GetDirectoryName( _
        System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl")

        lintThreads = 16 'JM 21/08/2003

        If Not Info.Exists Then
            'produce message / file open dialog
            'Dim FindKey As New OpenFileDialog()
            'With FindKey
            '    .CheckFileExists = True
            '    .CheckPathExists = True
            '    .Filter = "MCL Unlock File (*.mcl)|*.mcl"
            '    .DefaultExt = "mcl"
            '    .Multiselect = False
            '    .Title = "Please locate your unlock file!"
            '    .ShowDialog()
            '    If .FileName = "" Then
            '        MxessageBox.Show("You will have been provided with the 'Unlock File' if you have purchased this product!", NameMe(""))
            '        Exit Function
            '    Else
            '        System.IO.File.Copy(.FileName, System.IO.Path.GetDirectoryName( _
            '            System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl", True)
            '    End If
            'End With
        Else
            'check keyfile
            Try
                lintThreads = Unlock(System.IO.Path.GetDirectoryName( _
                System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl", pDets, "", "") 'added ,"" 'JM 08/09/2004 'JM 21/08/2003
            Catch
                '--- 'JM 09/12/2003 ---
                Try
                    System.IO.File.Delete(System.IO.Path.GetDirectoryName( _
                    System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl")
                Catch
                    '
                End Try
                '--- 'JM 09/12/2003 ---
            End Try
        End If


        If lintThreads <> 257 Then 'JM 21/08/2003
            mintVersion = 64 'JM 21/08/2003
        Else
            mintVersion = 32 'JM 21/08/2003
        End If 'JM 21/08/2003

        'Probably not such a good idea, the program may be at fault, you don't want to have to issue millions of new keys!
        'JM 21/08/2003
        'If lintThreads <> 257 Then
        '    Try
        '        System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl")
        '    Catch e As Exception
        '    End Try
        '    MxessageBox.Show("Your 'Unlock File' has not been accepted!", NameMe(""))
        '    End 'JM 13/05/2003
        '    Exit Function
        'End If

    End Function
    Function Groovydecrypt(ByVal password As String, ByVal strtodecrypt As String) As String
        Dim ralphabet, alphabet, decrypted_string, cur_pswd_ltr As String
        Dim i, n, nn, c As Integer
        Dim pos_alpha_ary(0) As String

        strtodecrypt = PutAscBackintoChr(strtodecrypt)

        'ralphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 !,.:;?~@#$%^&*()_+-=][}{/><" & ChrGet(34) & "'`|" & ChrGet(129) & ChrGet(131) & ChrGet(132) & ChrGet(133) & ChrGet(134) & ChrGet(135) & ChrGet(136) & ChrGet(137) & ChrGet(138) & ChrGet(139) & ChrGet(140) & ChrGet(141) & ChrGet(142) & ChrGet(143) & ChrGet(144) & ChrGet(145) & ChrGet(147) & ChrGet(148) & ChrGet(149) & ChrGet(150) & ChrGet(151) & ChrGet(152) & ChrGet(153) & ChrGet(154) & ChrGet(156) & ChrGet(157) & ChrGet(160) & ChrGet(161) & ChrGet(162) & ChrGet(163) & ChrGet(165) & ChrGet(167) & ChrGet(181) & ChrGet(182) & ChrGet(183) & ChrGet(199) & ChrGet(209) & ChrGet(212) & ChrGet(216) & ChrGet(224) & ChrGet(225) & ChrGet(226) & ChrGet(227) & ChrGet(229) & ChrGet(230) & ChrGet(231) & ChrGet(233) & ChrGet(234) & ChrGet(235) & ChrGet(237)
        Dim lintArrinc As Integer
        For lintArrinc = 0 To 255
            ralphabet &= Microsoft.VisualBasic.Chr(lintArrinc)
        Next lintArrinc

        alphabet = ralphabet & ralphabet

        For i = 0 To (password.Length) - 1
            cur_pswd_ltr = Microsoft.VisualBasic.Mid(password, i + 1, 1)
            ReDim Preserve pos_alpha_ary(i)
            Dim lstrValue As String = StrStr(alphabet, cur_pswd_ltr)
            pos_alpha_ary(i) = Microsoft.VisualBasic.Mid(lstrValue, 1, ralphabet.Length)
        Next i '

        i = 0
        n = 0

        nn = password.Length
        c = strtodecrypt.Length - 1
        decrypted_string = ""

        While i < c
            Dim lstrvalue As Integer = Microsoft.VisualBasic.InStr(pos_alpha_ary(n), Microsoft.VisualBasic.Mid(strtodecrypt, i + 1, 1))
            Dim DecStr As String = Microsoft.VisualBasic.Mid(ralphabet, lstrValue, 1)
            decrypted_string &= DecStr
            'Console.Write(DecStr) 'USED IN TESTING ONLY
            n += 1
            If n = nn Then n = 0
            i += 1
        End While

        decrypted_string = str_replace("[tab]", "t", decrypted_string)
        decrypted_string = str_replace("[new]", "n", decrypted_string) '
        decrypted_string = str_replace("[ret]", "r", decrypted_string) '



        Return decrypted_string
    End Function
    Function PutAscBackintoChr(ByVal pstrString As String) As String
        Dim lstrRetVal As String
        Dim lintArrInc As Integer

        For lintArrInc = 0 To pstrString.Length Step 3
            Dim lstrpre As Integer = Val(Val(Microsoft.VisualBasic.Mid(pstrString, lintArrInc + 1, 3))) 'JM 05/09/2003
            Dim lstrValue As String = Microsoft.VisualBasic.Chr(lstrpre)
            lstrRetVal &= lstrValue
        Next lintArrInc

        Return lstrRetVal
    End Function
    Function str_replace(ByVal SearchChar As String, ByVal DestChar As String, ByVal Str As String) As String
        Return Str.Replace(SearchChar, DestChar)
    End Function
    Function StrStr(ByVal source As String, ByVal searchstr As String) As String
        Dim FindPos As Integer = Microsoft.VisualBasic.InStr(source, searchstr)
        Return Microsoft.VisualBasic.Mid(source, FindPos, (source.Length - FindPos) + 1)
    End Function
    Function RetNthStr(ByVal pstrString As String, ByVal pintPosition As Integer, ByVal pstrChar As String) As String
        Dim lintArrInc As Integer
        Dim llngPos As Long
        Dim llngLastPos As Long
        Dim lintErrPos As Integer

        'WARNING: Have discovered that this function missed first 2 chars, better to use AppBasic version!!!
        'Need to check implementation of this function!! 'JM 10/02/2005

        lintErrPos = 0
        'On Error GoTo EndFunc
        Try
            lintErrPos = 1
            Do Until lintArrInc = pintPosition
                lintErrPos = 2
                lintArrInc = lintArrInc + 1
                lintErrPos = 3
                'llngPos = InxStr(llngLastPos + 1, pstrString, pstrChar)
                llngPos = CInt(Microsoft.VisualBasic.InStr(CInt(llngLastPos + 1), pstrString, pstrChar))
                lintErrPos = 4
                RetNthStr = Microsoft.VisualBasic.Mid(pstrString, llngLastPos + 1, (llngPos - llngLastPos) - 1)
                lintErrPos = 5
                llngLastPos = llngPos + 2
                lintErrPos = 6
            Loop
            lintErrPos = 7
        Catch ex As Exception

            RetNthStr = ""
        End Try


    End Function
    Private Function HashData(ByVal str As String, ByVal pbooShort As Boolean) As String

        'added 'JM 07/09/2004
        Dim hashAlg As System.Security.Cryptography.MD5 = New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim rawBytes() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(str)
        Dim lhashData() As Byte = hashAlg.ComputeHash(rawBytes)

        Dim ret As String = BitConverter.ToString(lhashData).Replace("-", "")
        Dim out As String

        If pbooShort = True Then
            Dim lintArrInc As Integer
            For lintArrInc = 0 To ret.Length Step 4 '3
                out &= MidGet(ret, lintArrInc + 1, 1)
            Next lintArrInc
        Else
            Return BitConverter.ToString(lhashData).Replace("-", "")
        End If

        'Else
        '    Dim ret As String = BitConverter.ToString(lhashData).Replace("-", "")
        '    Return (MidGet(ret, 1, 5) & "-" & MidGet(ret, 6, 5) & "-" & MidGet(ret, 11, 5) & "-" & MidGet(ret, 16, 5) & "-" & MidGet(ret, 21, 5) & "-" & MidGet(ret, 26, 5) & "-" & MidGet(ret, 31, 2))
        'End If

        Return out

    End Function
    Private Function ShorternFullHashData(ByVal pString As String) As String
        'added 'JM 07/09/2004

        Dim out As String

        Dim lintArrInc As Integer
        For lintArrInc = 0 To pString.Length Step 3
            out &= MidGet(pString, lintArrInc + 1, 1)
        Next lintArrInc

        Return out

    End Function
    Private Sub GetDigs(ByVal pstrInput As String, ByRef SerialBlock As String, _
    ByRef RetProdIdentifier As String, ByRef CheckDigs As String)

        'Try
        '    Dim HyphPos As Integer = InStr(pstrInput, "-")
        '    SerialBlock = Left(pstrInput, HyphPos - 1)
        '    CheckDigs = Mid(pstrInput, HyphPos + 1, pstrInput.Length - HyphPos)
        'Catch
        '    SerialBlock = ""
        '    CheckDigs = ""
        'End Try

        Try
            Dim str() As String = Microsoft.VisualBasic.Split(pstrInput & "-", "-")
            SerialBlock = str(0)
            RetProdIdentifier = str(1)
            CheckDigs = str(2)
        Catch

        End Try

    End Sub
End Module
