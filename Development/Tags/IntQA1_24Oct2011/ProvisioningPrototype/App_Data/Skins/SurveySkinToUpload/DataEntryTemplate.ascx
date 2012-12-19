<%@ Control %>
<%@ Register TagPrefix="inc" Namespace="Wilson.WebControls" Assembly="WilsonWebControls" %>
<%@ Import NameSpace="VisionCritical.PanelPlus"%>


<inc:contentregion id="Content" runat="server"></inc:contentregion>
<html> <!--HTML lang="<=AppSettings.Context.Culture%>"-->
	<head>
		<title>Data Entry Portal</title>
	</head>
	<body bgcolor="#ffffff" marginwidth="0" marginheight="0" leftmargin="0" onload="document.forms[0].StudyIdTextBox.focus();">
		<form id="Form1" method="post" runat="server" style="height:100%">
			
			<link href="<%=AppSettings.Context.SkinPath%>css/portalStyles.css" type="text/css" rel="stylesheet">
			<link href="<%=AppSettings.Context.SkinPath%>css/colors.css" type="text/css" rel="stylesheet">
			<link href="<%=AppSettings.Context.SkinPath%>css/<%=AppSettings.Context.Culture%>.css" type="text/css" rel="stylesheet">
			<table height="100%" width="100%" align="center" cellpadding="0" cellspacing="0" border="0">
			<tr>
			<td valign="middle">
			<table width="726" height="200" border="0" cellspacing="0" cellpadding="0" align="center">
				<tr>
					<td colspan="2">
						<!-- Instructions -->
						<inc:contentregion id="i_InstructionsLabel" runat="server"></inc:contentregion>	
					</td>
				</tr>
				
				<tr>
					<td>
						<!-- StudyId Label -->
						<inc:contentregion id="i_StudyIdLabel" runat="server"></inc:contentregion>	
					</td>
					<td>
						<!-- StudyId TextBox -->
						<inc:contentregion id="i_StudyIdTextBox" runat="server"></inc:contentregion>
					</td>
				</tr>

				<tr>
					<td>
						<!-- PanelistId Label -->
						<inc:contentregion id="i_PanelistIdLabel" runat="server"></inc:contentregion>
					</td>
					<td>
						<!-- PanelistId TextBox -->
						<inc:contentregion id="i_PanelistIdTextBox" runat="server"></inc:contentregion>
					</td>
				</tr>
				
				<tr>
					<td colspan="2">
						<!-- Login Button -->
						<inc:contentregion id="i_LoginButton" runat="server"></inc:contentregion>
					</td>
				</tr>
				
				<tr>
					<td colspan="2">
						<!-- Errors -->
						<span style="color:red;"><inc:contentregion id="i_ErrorsPlaceHolder" runat="server"></inc:contentregion>
						</span>
					</td>
				</tr>

			</table>
		</form>
	</body>
</html>



