<% @ LANGUAGE="VBSCRIPT" %>
<% 	Response.Buffer = True
%>
<%
lstrTitle = "Main Screen" %>
<!-- #include file="_header.htm" -->
<% ThisPage = lstrPGMainScreen %>
Kids Mask Factory allows you to create masks using two main features.  Firstly as you'd expect by allowing the user of the program to draw a mask by freehand.  Secondly by adding predrawn face parts to your mask creation.<BR><BR>

How to draw freehand.  Click on the left mouse button to draw within the main panel. If you make a mistake click on the Undo button.<BR><BR>

How to add predrawn face parts.  Click on either of the Heads, Ears, Eyes, Mouths, Noses or Other buttons.  A small screen will appear allowing you to select the face part you require.  For more information on face part selection <a href="<% = lstrPGFacePartSelect %>">click here</a><br><br>

To delete a face part simply click the delete button then click on the piece you wish to delete.<BR><BR>

There are also two helpful features which will help you create a mask.  Firstly, the mirror feature, this allows you to draw on one side of the face and have same drawn on the other side.  Secondly, the guide feature, this simple places an oval, of an approximate size of a face.<BR><BR>

The New button will clear the current mask and give you a clean canvas<BR><BR>

The Undo feature removes the last drawn line.<BR><BR>

For information about the My Masks (Load and Save features) <a href="<% = lstrPGSlots %>">click here</a><br><br>

For more information about Printing <a href="<% = lstrPGPrinting %>">click here</a>.<br><br>

For more information about Parent Options <a href="<% = lstrPGParentOptions %>">click here</a>.
<BR><BR>
<B><<a href="<% = lstrPGSignIn %>">PREVIOUS</a>>&nbsp;&nbsp;&nbsp;<<a href="<% = lstrPGFacePartSelect %>">NEXT</a>></B>

<!-- #include file="_footer.htm" -->
