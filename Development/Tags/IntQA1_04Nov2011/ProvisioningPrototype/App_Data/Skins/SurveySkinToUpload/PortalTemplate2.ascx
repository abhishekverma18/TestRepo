<%@ Control %>
<%@ Import NameSpace="VisionCritical.PanelPlus"%>
<%@ Import NameSpace="VisionCritical.PanelPlus.Portal.Web"%>
<%@ Register TagPrefix="inc" Namespace="Wilson.WebControls" Assembly="WilsonWebControls" %>
<%@ Register TagPrefix="vc" Namespace="VisionCritical.WebControls" Assembly="VisionCritical.WebControls" %>
<inc:contentregion id="Content" runat="server"></inc:contentregion>

<!doctype html public "-//w3c//dtd html 4.01 transitional//en">

<html lang="<%#AppSettings.Context.Culture%>">
	<head>
		<title> <%# AppSettings.Context.Name%></title>
		
		<link href="<%# Locator.GetRelativeSkinUrl("css/portalStyles.css") %>" type="text/css" rel="stylesheet">
		<link href="<%# Locator.GetRelativeSkinUrl("css/colors.css") %>" type="text/css" rel="stylesheet">
		<link href="<%# Locator.GetRelativeSkinUrl("css/") + AppSettings.Context.Culture %>.css" type="text/css" rel="stylesheet">
		<link href="<%# Locator.GetRelativeSkinUrl("css/ForumStyles.css") %>" type="text/css" rel="stylesheet">
		<link href="<%# Locator.GetRelativeSkinUrl("css/login.css") %>" type="text/css" rel="stylesheet">

	</head>

<body bgcolor="#ffffff" marginwidth="0" marginheight="0" topmargin="0" leftmargin="0">

<table width="723" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#FFFFFF" height="100%">

<tr valign="top">
<td>
	<table width="723" border="0" cellspacing="0" cellpadding="0" height="100%" bgcolor="#eeeeee">
	
	<tr style="background:#99CCFF;">
		<td height="110"><span style="font-size:18pt;font-weight:bolder;padding-left:1em;font-family:'Trebuchet MS', verdana, lucida, arial, helvetica, sans-serif">Blank Template2</span></td>
	</tr>
	<tr>
	<td>
		<table cellspacing="0" cellpadding="0" width="705" border="0" id="Table1">
		<tr valign="top">
			<td class="topCurve"></td>
			<td class="topGradiant" align="right">
				<span class="helpTitle" enableviewstate="False" runat="server" key="Skin_NeedHelpLabel" id="Span1"></span>
				<span class="helpText" enableviewstate="False" runat="server" key="Skin_EmailLink" id="Span2"></span>
				<span class="helpText"><a href="mailto:<%# AppSettings.Context.TechSupportEmail%>"><%# AppSettings.Context.TechSupportEmail%></a></span>
			</td>
		</tr>
		</table>
	</td>
	</tr>
	<tr height="100%" valign="top">
	<td class="contentBox">

		<form id="Form1" method="post" runat="server">
			<table cellspacing="0" width="100%" height="100%" border="1" cellpadding="0">
				<tr>
				<td width="100%" colspan="3">
					<div id="lg" runat="server" Visible="<%# ((Portal)Page).ShowLoginModule %>">
						<div id="lgInst" class="clearfix">
							<inc:contentregion id="i_UserPrompt" runat="server"></inc:contentregion>
						</div>
						<div id="lgEml" class="clearfix">
							<inc:contentregion id="i_EmailAddressLabel" runat="server"></inc:contentregion>
							<inc:contentregion id="i_EmailText" runat="server"></inc:contentregion>
						</div>

						<div id="lgPwd" class="clearfix">
							<inc:contentregion id="i_PasswordLabel" runat="server"></inc:contentregion>
							<inc:contentregion id="i_PasswordText" runat="server"></inc:contentregion>
						</div>
						<div id="lgRem" class="clearfix">
							<inc:contentregion id="i_SetCookieCheckBox" runat="server"></inc:contentregion>
						</div>
						<div id="btns" class="clearfix">
							<div id="lgSi">
								<inc:contentregion id="i_LoginButton" runat="server"></inc:contentregion>
							</div>
							<div id="lgFp">
								<inc:contentregion id="i_ForgotPasswordButton" runat="server"></inc:contentregion>
							</div>		
						</div>
						<div id="lgTechAssistText" class="clearfix">
							<inc:contentregion id="i_TechAssistanceLabel" runat="server"></inc:contentregion>
						</div>
						<div id="lgTechAssistEmail" class="clearfix">
							<inc:contentregion id="i_EmailLabel" runat="server"></inc:contentregion>
							<a href="mailto:<%#AppSettings.Context.TechSupportEmail%>"><%#AppSettings.Context.TechSupportEmail%></a>
						</div>
						<div id="lgErr" class="clearfix">
							<inc:contentregion id="i_MessageLabel" runat="server"></inc:contentregion>
						</div>
					</div>		
				</td>
				</tr>
				<tr>
				<td width="100%" colspan="3">
					<div runat="server" id="HomeControls" Visible="<%# ((Portal)Page).ShowHomeModule %>">
						<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tbody>
								<tr valign="top">
									<td width="320">
										<!-- left-hand column -->
										<table cellspacing="0" cellpadding="0" width="320" border="0">
											<tr>
												<td style="BORDER-BOTTOM: #969792 1px solid" width="320">
													<table cellspacing="0" cellpadding="0" width="320" border="0">
														<tr>
															<td>
																<span class="welcomeTitle">
																	<inc:contentregion id="i_WelcomeLabel" runat="server"></inc:contentregion>
																</span>
																<span class="welcomeName">
																	<inc:contentregion id="i_WelcomeNameLabel" runat="server"></inc:contentregion>
																</span>
															</td>
															<td align="right">
																<p class="welcomeName">
																	<inc:contentregion id="i_LogoutButton" runat="server"></inc:contentregion>
																</p>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td>
													<img height=5 src="<%# Locator.GetRelativeSkinUrl("images/shim.gif") %>" width=320>
												</td>
											</tr>
										</table>
										<div class="welcomeText">
											<inc:contentregion id="i_PrePanelNameWelcomeLabel" runat="server"></inc:contentregion>
										</div>
										<br>
										<table cellspacing="0" cellpadding="0" width="320" border="0">
											<tr>
												<td>
													<img height=5 src="<%# Locator.GetRelativeSkinUrl("images/shim.gif") %>" width=1>
												</td>
												<td width="23" rowspan="2">
													<img height=28 src="<%# Locator.GetRelativeSkinUrl("images/studies_page.gif") %>" width=23>
												</td>
												<td>
													<img height=5 src="<%# Locator.GetRelativeSkinUrl("images/shim.gif") %>" width=1 >
												</td>
											</tr>
											<tr>
												<td class="studyListOpenTitle">
													<img height=5 src="<%# Locator.GetRelativeSkinUrl("images/shim.gif") %>" width=1>
												</td>
												<td class="studyListOpenTitle">
													<span>
														<inc:contentregion id="i_Default_OpenStudies" runat="server"></inc:contentregion>
													</span>
												</td>
											</tr>
											<tr>
												<td class="studyListOpenTitle" colspan="3"><img height=1 src="<%# Locator.GetRelativeSkinUrl("images/shim.gif") %>" width=1></td>
											</tr>
										</table>
										<inc:contentregion id="i_StudyGrid" runat="server"></inc:contentregion>
										<inc:contentregion id="i_NoOpenStudiesLabel" runat="server"></inc:contentregion>
										
										<br>
										<table class="ForumHeaderTable" cellpadding="0" cellspacing="1">
											<tr>
												<td colspan="2">
													<table cellspacing="0" cellpadding="0" width="320" border="0">
														<tr>
															<td>
																<img height=5 src="<%# Locator.GetRelativeSkinUrl("images/shim.gif") %>" width=1>
															</td>
															<td width="23" rowspan="2">
																<img height=28 src="<%# Locator.GetRelativeSkinUrl("images/studies_page.gif") %>" width=23>
															</td>
															<td>
																<img height=5 src="<%# Locator.GetRelativeSkinUrl("images/shim.gif") %>" width=1 >
															</td>
														</tr>
														<tr>
															<td class="studyListOpenTitle">
																<img height=5 runat="server" id="shim3" width=1>
															</td>
															<td class="studyListOpenTitle">
																<inc:contentregion id="i_ForumsHeaderLabel" runat="server"></inc:contentregion>
															</td>
														</tr>
														<tr>
															<td class="studyListOpenTitle" colspan="3"><img height=1 src="<%# Locator.GetRelativeSkinUrl("images/shim.gif") %>" width=1></td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td class="studyListOpenTitle"><asp:label runat="server" id="ForumListHeaderName"></asp:label> </td>
												<td class="studyListOpenTitle"><asp:label runat="server" id="ForumListHeaderTitle"></asp:label></td>
											</tr>
										</table>
										<inc:contentregion id="i_ForumRepeater" runat="server"></inc:contentregion>
										
										<div id="IncentiveInfoPanel" style="width:100%">
											<br>
											<div id="incentiveInfo">
												<div class="userInfoTitle">
													<inc:contentregion id="i_IncentiveInfoTitleLabel" runat="server"></inc:contentregion>
												</div>
												<table class="userInfoText" cellspacing="0" cellpadding="0" width="100%" border="0">
													<tr>
														<td>
															<inc:contentregion id="i_TotalPointsTitleLabel" runat="server"></inc:contentregion>
														</td>
														<td>
															<inc:contentregion id="i_TotalPointsLabel" runat="server"></inc:contentregion>
														</td>
													</tr>
													<tr>
														<td>
															<inc:contentregion id="i_TotalPointsRedeemedTitleLabel" runat="server"></inc:contentregion>
														</td>
														<td>
															<inc:contentregion id="i_TotalPointsRedeemedLabel" runat="server"></inc:contentregion>
														</td>
													</tr>
													<tr>
														<td>
															<inc:contentregion id="i_RedeemPointsLinkButton" runat="server"></inc:contentregion>
														</td>
														<td>
															<inc:contentregion id="i_GetPointsHistoryLinkButton" runat="server"></inc:contentregion>
														</td>
													</tr>
												</table>
											</div>
										</div>
										<br>
										
										<div id="userInfo">
											<div class="userInfoTitle">
												<inc:contentregion id="i_UserProfileLabel" runat="server"></inc:contentregion>
											</div>
											<div class="userInfoText">
												<inc:contentregion id="i_PanelistNameLabel" runat="server"></inc:contentregion><br>
												<inc:contentregion id="i_PanelistUserNameLabel" runat="server"></inc:contentregion><br>
												<inc:contentregion id="i_PanelistEmailLabel" runat="server"></inc:contentregion>
											</div>
											<div class="changeButtons" align="right">
												<inc:contentregion id="i_ChangeEmailButton" runat="server"></inc:contentregion>
												<inc:contentregion id="i_ChangeUserNameButton" runat="server"></inc:contentregion>
												<inc:contentregion id="i_ChangePasswordButton" runat="server"></inc:contentregion>
											</div>
										</div>
										<!-- end left-hand column -->
									</TD>
									<td>
										<img height="1" src="collateral/images/shim.gif" width="15">
									</td>
									<td valign="top" align="left" width="370">
										<div id="resultMessageDiv">
											<inc:contentregion id="i_ResultMessage" runat="server"></inc:contentregion>
										</div>
										<!-- right-hand column -->
										<div id="changeEmail">
											<inc:contentregion id="i_ChangeEmailLabel" runat="server"></inc:contentregion>
											<p>
												<b>
													<inc:contentregion id="i_OldEmailLabel" runat="server"></inc:contentregion>
												</b>
												<br>
												<inc:contentregion id="i_OldEmailTextBox" runat="server"></inc:contentregion>
											</p>
											<p>
												<b>
													<inc:contentregion id="i_NewEmailLabel" runat="server"></inc:contentregion>
												</b>
												<br>
												<inc:contentregion id="i_NewEmailTextBox" runat="server"></inc:contentregion>
											</p>
											<p>
												<b>
													<inc:contentregion id="i_ConfirmEmailLabel" runat="server"></inc:contentregion>
												</b>
												<br>
												<inc:contentregion id="i_ConfirmEmailTextBox" runat="server"></inc:contentregion>
												<br>
												<br>
												<div>
													<inc:contentregion id="i_SubmitEmailAddressButton" runat="server"></inc:contentregion>
													<inc:contentregion id="i_CancelEmailButton" runat="server"></inc:contentregion>
												</div>
											</p>
										</div>
										<div id="changePassword">
											<inc:contentregion id="i_ChangePasswordLabel" runat="server"></inc:contentregion>
											<p>
												<b>
													<inc:contentregion id="i_OldPasswordLabel" runat="server"></inc:contentregion>
												</b>
												<br>
												<inc:contentregion id="i_OldPasswordTextBox" runat="server"></inc:contentregion>
											</p>
											<p>
												<b>
													<inc:contentregion id="i_NewPasswordLabel" runat="server"></inc:contentregion>
												</b>
												<br>
												<inc:contentregion id="i_NewPasswordTextBox" runat="server"></inc:contentregion>
											</p>
											<p>
												<b>
													<inc:contentregion id="i_ConfirmPasswordLabel" runat="server"></inc:contentregion>
												</b>
												<br>
												<inc:contentregion id="i_ConfirmPasswordTextBox" runat="server"></inc:contentregion>
												<br>
												<br>
												<div>
													<inc:contentregion id="i_SubmitPasswordButton" runat="server"></inc:contentregion>
													<inc:contentregion id="i_CancelPasswordButton" runat="server"></inc:contentregion>
												</div>
											</p>
										</div>
										<div id="changeUserName">
											<inc:contentregion id="i_ChangeUserNameLabel" runat="server"></inc:contentregion>
											<p>
												<b>
													<inc:contentregion id="i_NewUserNameLabel" runat="server"></inc:contentregion>
												</b>
												<br>
												<inc:contentregion id="i_NewUserNameTextBox" runat="server"></inc:contentregion>
												<br>
												<br>
												<div>
													<inc:contentregion id="i_SubmitUserNameButton" runat="server"></inc:contentregion>
													<inc:contentregion id="i_CancelUserNameButton" runat="server"></inc:contentregion>
												</div>
											</p>
										</div>
										<!-- end right-hand column -->
									</td>
								</TR>
							</TBODY>
						</TABLE>
					</div>
				</td>
			</tr>
			<tr>
				<td valign=top height="270px" width="350px">
					<div runat="server" Visible="<%# ((Portal)Page).ShowQuickPollModule %>" id="qContainer">
						<div id="quickpollInfo">
							<div class="quickpollTitle">
								<inc:contentregion id="i_QuickPollLabel" runat="server"></inc:contentregion>
							</div>
						</div>
						<div id="quickpollArea">
							<inc:contentregion id="i_QuickPollPlaceHolder" runat="server"></inc:contentregion>
							<inc:contentregion id="i_QuickPollSubmitButton" runat="server"></inc:contentregion>
						</div>
					</div>
				</td>
				<td valign=top height="270px" width="320px">
					<div runat="server" visible="<%# ((Portal)Page).ShowNewsLettersModule %>" id="nContainer">
						<div id="newsletterInfo">
							<div class="newsletterTitle">
								<inc:contentregion id="i_NewsletterLabel" runat="server"></inc:contentregion>
							</div>
						</div>
						<div id="newsletterArea">
							<inc:contentregion id="i_NewslettersRepeater" runat="server"></inc:contentregion>
						</div>
					</div>
				</td>
				<td valign=top height="270px" width="320px">
					<div runat="server" visible="<%# ((Portal)Page).ShowStudyLinksModule %>" id="Div1">
						<div id="studylinkInfo">
							<div class="studylinkTitle">
								<inc:contentregion id="i_StudyLinkLabel" runat="server"></inc:contentregion>
							</div>
						</div>
						<div id="studylinkArea">
							<inc:contentregion id="i_StudyLinksRepeater" runat="server"></inc:contentregion>
						</div>
					</div>
				</td>
			</tr>
		</table>	
	</form>
	
	</td>
	</tr>	
	
	<tr>
	<td>
		<table cellpadding="0" cellspacing="0" border="0" width="100%">
		<tr>
		<td><div class="footer"><a id="PrivacyLink" class="footerLink" href="http://www.visioncritical.com/VC_PrivacyPolicy_<%# AppSettings.Context.Culture%>.pdf" target="_blank">&raquo;<span id="privacyLinkText" runat="server" key="Skin_PrivacyPolicyLink"></span></a></div></td>
		<td align="right"><a href="http://www.visioncritical.com" target="_blank" onmouseover="document.getElementById('vcLogo').src = '<%# Locator.GetRelativeSkinUrl("images/vc_logo_over2.gif") %>';" onmouseout="document.getElementById('vcLogo').src = '<%# Locator.GetRelativeSkinUrl("images/vc_logo2.gif") %>';"><img id="vcLogo" border="0" src="<%# Locator.GetRelativeSkinUrl("images/vc_logo2.gif") %>" hspace="15" vspace="5"></a></td>
		</tr>
		</table></td>
	</tr>
	
	</table></td>
</tr>
</table>
</body>
</html>