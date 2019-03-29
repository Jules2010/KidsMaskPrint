<% @ LANGUAGE="VBSCRIPT" %>
<% 	Response.Buffer = True
%>
<%
lstrTitle = "Sign In" %>
<!-- #include file="_header.htm" -->
<% ThisPage = lstrPGSignIn %>
When the program first starts you are asked to enter a name, this is used to identify ownership of masks.  The next time the program is run the name is available from a button.  It is possible to have several users of the program.<br><br>

The user of the program MUST provide the system with a name!<BR><BR>

Providing the system with a name makes it possible to use the slot features.  <br><br>


Slots are entries which make it possible to load / save mask files, without having to worry about file names and directories. For for information about slots <a href="<% = lstrPGSlots %>">click here<a>.<br><br>

The second time the program runs you can choose a colour scheme for the program.<BR><BR>


<B><<a href="<% = lstrPGQuickStart %>">PREVIOUS</a>>&nbsp;&nbsp;&nbsp;<<a href="<% = lstrPGMainScreen %>">NEXT</a>></B>

<!-- #include file="_footer.htm" -->
