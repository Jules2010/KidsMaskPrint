'********************* Main Module ************************************
Option Strict On

Imports System.Collections
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary


Module Module1
    Sub MainX()
        Dim c As clsArray
        Specific.SIO()
        c = Specific.DSIO()
        Console.WriteLine(c.SetTime.Capacity)
    End Sub
End Module

<Serializable()> _
Public Class clsArray
    Public Shared SetTime As New ArrayList()
End Class

Module Specific

    Public Sub SIO() ' SIO = Serialize Individual objects
        Dim myArray As New clsArray()
        Dim myFilestream As Stream = File.Create("C:\SetTime.Bin")
        Dim Serializer As New BinaryFormatter()
        Serializer.Serialize(myFilestream, myArray)
        myFilestream.Close()
    End Sub

    Public Function DSIO() As clsArray
        Dim RestoredAccount As clsArray
        Dim myFilestream As Stream = File.OpenRead("C:\SetTime.Bin")
        Dim deserializer As New BinaryFormatter()
        RestoredAccount = CType(deserializer.Deserialize(myFilestream), clsArray)
        myFilestream.Close()
        Return RestoredAccount
    End Function

End Module
'***********************************************************************



Module App

    Sub MainX()
        Serialize()
        Deserialize()
    End Sub

    Sub Serialize()

        ' Create a SortedList of values that will eventually be serialized.
        Dim addresses As New SortedList()
        addresses.Add("Jeff", "123 Main Street, Redmond, WA 98052")
        addresses.Add("Fred", "987 Pine Road, Phila., PA 19116")
        addresses.Add("Mary", "PO Box 112233, Palo Alto, CA 94301")

        ' To serialize the SortedList (and its key/value pairs),
        ' you must first open a stream for writing.
        ' In this case, use a file stream.
        Dim fs As New FileStream("DataFile.dat", FileMode.Create)

        ' Construct a BinaryFormatter and use it to serialize the data tothe stream.
        Dim formatter As New BinaryFormatter()
        Try
            formatter.Serialize(fs, addresses)
        Catch e As SerializationException
            Console.WriteLine("Failed to serialize. Reason: " & e.Message)
            Throw
        Finally
            fs.Close()
        End Try
    End Sub



    Sub Deserialize()
        ' Declare the SortedList reference.
        Dim addresses As SortedList = Nothing

        ' Open the file containing the data that you want to deserialize.
        Dim fs As New FileStream("DataFile.dat", FileMode.Open)
        Try
            Dim formatter As New BinaryFormatter()

            ' Deserialize the SortedList from the file and
            ' assign the reference to the local variable.
            addresses = DirectCast(formatter.Deserialize(fs), SortedList)
        Catch e As SerializationException
            Console.WriteLine("Failed to deserialize. Reason: " & e.Message)
            Throw
        Finally
            fs.Close()
        End Try

        ' To prove that the table deserialized correctly,
        ' display the key/value pairs.
        Dim de As DictionaryEntry
        For Each de In addresses
            Console.WriteLine("{0} lives at {1}.", de.Key, de.Value)
        Next
    End Sub
End Module
