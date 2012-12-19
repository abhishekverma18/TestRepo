<%@ Control %>
<%@ Register TagPrefix="inc" Namespace="Wilson.WebControls" Assembly="WilsonWebControls" %>
<%@ Import NameSpace="VisionCritical.PanelPlus"%>
<%@ Register TagPrefix="vc" Namespace="VisionCritical.WebControls" Assembly="VisionCritical.WebControls" %>

<inc:ContentRegion id="Content" runat="server"></inc:ContentRegion>

<inc:ContentRegion id="PortalHeader" runat="server"></inc:ContentRegion>

<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
<form id="Form1" method="post" runat="server">	
	<table cellSpacing="0" width="100%" border="0" cellpadding="0" height="100%">
		<tr vAlign="top">
			<td width="320">
				<!-- left-hand column -->
				<table cellSpacing="0" width="320" border="0" cellpadding="0">
					<tr>
						<td width="320" style="border-bottom:1px solid #969792;">
							<table border="0" cellpadding="0" cellspacing="0" width="320">
							
							<tr>
							<td><span class="welcomeTitle"><asp:Label runat="server" id="WelcomeLabel" key="Default_Welcome"></asp:Label></span> <span class="welcomeName"><inc:ContentRegion id="i_WelcomeNameLabel" runat="server"></inc:ContentRegion></span></td>
							<td align="right"><p class="welcomeName"><inc:ContentRegion id="i_LogoutButton" runat="server"></inc:ContentRegion></p></td>
							</tr>
							
							</table>
						</td>
					</tr>
					<tr>
						<td><IMG height="5" src="<%= Locator.GetRelativeSkinUrl("images/shim.gif") %>" width="320"></td>
					</tr>
				</table>
				<div class="welcomeText">
					<inc:ContentRegion id="i_PanelWelcome" runat="server"></inc:ContentRegion>
					
				</div>
				<br>
				<table cellSpacing="0" width="300" border="0" cellpadding="0">
				
				<tr>
				<td><IMG height="5" src="<%= Locator.GetRelativeSkinUrl("images/shim.gif") %>" width="1"></td>
				<td rowspan="2" width="23"><IMG height="28" src="<%= Locator.GetRelativeSkinUrl("images/studies_page.gif") %>" width="23"></td>
				<td><IMG height="5" src="<%= Locator.GetRelativeSkinUrl("images/shim.gif") %>" width="1"></td>
				</tr>
				
				<tr>
				<td class="studyListOpenTitle"><IMG height="5" src="<%= Locator.GetRelativeSkinUrl("images/shim.gif") %>" width="1"></td>
				<td class="studyListOpenTitle"><span><asp:Label runat="server" id="Default_OpenStudies" key="Default_OpenStudies"></asp:Label></span></td>
				</tr>
				
				<tr>
				<td colspan="3" class="studyListOpenTitle"><IMG height="1" src="<%= Locator.GetRelativeSkinUrl("images/shim.gif") %>" width="1"></td>
				</tr>			
				
				</table>

				<inc:ContentRegion id="i_StudyGrid" runat="server"></inc:ContentRegion>
				<br>
				<div id="userInfo">
					<div class="userInfoTitle"><inc:ContentRegion id="i_UserProfileLabel" runat="server"></inc:ContentRegion></div>
					<div class="userInfoText">
						<inc:ContentRegion id="i_PanelistNameLabel" runat="server"></inc:ContentRegion><br>					
					<inc:ContentRegion id="i_PanelistUserNameLabel" runat="server"></inc:ContentRegion><br>
						<inc:ContentRegion id="i_PanelistEmailLabel" runat="server"></inc:ContentRegion>
					</div>
					<div class="changeButtons" align="right">
						<inc:ContentRegion id="i_ChangeEmailButton" runat="server"></inc:ContentRegion>
						<inc:ContentRegion id="i_ChangeUserNameButton" runat="server"></inc:ContentRegion>
						<inc:ContentRegion id="i_ChangePasswordButton" runat="server"></inc:ContentRegion>
					</div></div>
				<!-- end left-hand column --></td>
			<td><IMG height="1" src="collateral/images/shim.gif" width="15"></td>
			<td vAlign="top" align="left" width="370">
				<div id="resultMessageDiv">
					<inc:ContentRegion id="i_ResultMessageLabel" runat="server"></inc:ContentRegion>
				</div>
				<!-- right-hand column -->				
				<div id="changeEmail"><inc:ContentRegion id="i_ChangeEmailLabel" runat="server"></inc:ContentRegion>
					<p><b><inc:ContentRegion id="i_OldEmailLabel" runat="server"></inc:ContentRegion></b><br>
						<inc:ContentRegion id="i_OldEmailTexBox" runat="server"></inc:ContentRegion>
					<p><b><inc:ContentRegion id="i_NewEmailLabel" runat="server"></inc:ContentRegion></b><br>
						<inc:ContentRegion id="i_NewEmailTextBox" runat="server"></inc:ContentRegion>
					<p><b><inc:ContentRegion id="i_ConfirmEmailLabel" runat="server"></inc:ContentRegion></b><br>
						<inc:ContentRegion id="i_ConfirmEmailTextBox" runat="server"></inc:ContentRegion>
						<br>
						<br>						
						<div>
							<inc:ContentRegion id="i_SubmitEmailAddressImageButton" runat="server"></inc:ContentRegion>
							<inc:ContentRegion id="i_CancelEmailButton" runat="server"></inc:ContentRegion>
						</div>
						
				</div>
				<div id="changePassword"><inc:ContentRegion id="i_ChangePasswordLabel" runat="server"></inc:ContentRegion>
					<p><b><inc:ContentRegion id="i_OldPasswordLabel" runat="server"></inc:ContentRegion></b><br>
						<inc:ContentRegion id="i_OldPasswordTextBox" runat="server"></inc:ContentRegion>
					<p><b><inc:ContentRegion id="i_NewPasswordLabel" runat="server"></inc:ContentRegion></b><br>
						<inc:ContentRegion id="i_NewPasswordTextBox" runat="server"></inc:ContentRegion>
					<p><b><inc:ContentRegion id="i_ConfirmPasswordLabel" runat="server"></inc:ContentRegion></b><br>
						<inc:ContentRegion id="i_ConfirmPasswordTextBox" runat="server"></inc:ContentRegion>						
						<br>
						<br> 
						<div>
							<inc:ContentRegion id="i_SubmitPasswordImageButton" runat="server"></inc:ContentRegion>
							<inc:ContentRegion id="i_CancelPasswordButton" runat="server"></inc:ContentRegion>
						</div>
				</div>
				<div id="changeUserName"><inc:ContentRegion id="i_ChangeUserNameLabel" runat="server"></inc:ContentRegion>
					<p><b><inc:ContentRegion id="i_NewUserNameLabel" runat="server"></inc:ContentRegion></b><br>
						<inc:ContentRegion id="i_NewUserNameTextBox" runat="server"></inc:ContentRegion>
						<br>
						<br> 
						<div>
							<inc:ContentRegion id="i_SubmitUserNameImageButton" runat="server"></inc:ContentRegion>
							<inc:ContentRegion id="i_CancelUserNameButton" runat="server"></inc:ContentRegion>
						</div>
				</div>
				<!-- end right-hand column -->
			</td>
		</tr>
	</table>
</form>
<inc:ContentRegion id="PortalFooter" runat="server"></inc:ContentRegion>