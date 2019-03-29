<% @ LANGUAGE="VBSCRIPT" %>
<% 	Response.Buffer = True
%>
<%
lstrTitle = "Face Part Selection" %>
<!-- #include file="_header.htm" -->
<% ThisPage = lstrPGFacePartSelect %>
The face part selection screen is a standard type of screen which is used when the user selects either of the Heads, Ears, Eyes, Mouths, Noses or Other buttons from the main screen.<BR><BR>

Once shown the screen allows the user to select a face part.  However, when selecting parts like eyes (or other parts where two would normally be shown on a face) the user select either 'Both' to show both parts, or Left to show the left hand piece or Right to show the right hand piece.  By default Both is shown where two pieces is normal.

<BR><BR>
<B><<a href="<% = lstrPGMainScreen %>">PREVIOUS</a>>&nbsp;&nbsp;&nbsp;<<a href="<% = lstrPGSlots %>">NEXT</a>></B>
<!-- #include file="_footer.htm" -->
