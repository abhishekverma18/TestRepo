<%@ Control %>
<%@ Register TagPrefix="inc" Namespace="Wilson.WebControls" Assembly="WilsonWebControls" %>
<%@ Import NameSpace="VisionCritical.PanelPlus"%>
<inc:ContentRegion id="Content" runat="server"></inc:ContentRegion>
<HTML> <!--HTML lang="<=AppSettings.Context.Culture%>"-->
	<HEAD>
		<title>Login</title>
		<script>
		function fnTrapKD(btnID, event){
			btn = findObj(btnID);
			if (document.all){
				if (event.keyCode == 13){
					event.returnValue=false;
					event.cancel = true;
					btn.click();
				}
			}else if (document.getElementById){
				if (event.which == 13){
					event.returnValue=false;
					event.cancel = true;
					btn.focus();
					btn.click();
				}
			}
			else if(document.layers){
				if(event.which == 13){
					event.returnValue=false;
					event.cancel = true;
					btn.focus();
					btn.click();
				}
			}
		}
		
		function findObj(n, d) { 
			var p,i,x;  
			if(!d) 
				d=document; 
			if((p=n.indexOf("?"))>0 && parent.frames.length) {
				d=parent.frames[n.substring(p+1)].document; 
				n=n.substring(0,p);
			}
			if(!(x=d[n])&&d.all) 
				x=d.all[n]; 
			for (i=0;!x&&i<d.forms.length;i++) 
				x=d.forms[i][n];
			for(i=0;!x&&d.layers&&i<d.layers.length;i++) 
				x=findObj(n,d.layers[i].document);
			if(!x && d.getElementById) 
				x=d.getElementById(n); 
			return x;
		}
		
		</script>	
		
	</HEAD>
	<body bgcolor="#ffffff" marginwidth="0" marginheight="0" leftmargin="0" onload="document.forms[0].EmailText.focus();">
		<form id="Form1" method="post" runat="server" style="height:100%" target="_parent">
			<LINK href="<%= Locator.GetRelativeSkinUrl("css/portalStyles.css") %>" type="text/css" rel="stylesheet">
			<LINK href="<%= Locator.GetRelativeSkinUrl("css/colors.css") %>" type="text/css" rel="stylesheet">
			<LINK href="<%= Locator.GetRelativeSkinUrl("css/") + AppSettings.Context.Culture %>.css" type="text/css" rel="stylesheet">
			<table height="100%" width="100%" align="center" cellpadding="0" cellspacing="0" border="0">
			<tr>
			<td valign="middle">
			<table width="726" height="400" border="0" cellspacing="0" cellpadding="0" align="center">
				<TR class="headerBlock">
					<td height="110"><span style="font-size:18pt;font-weight:bolder;padding-left:1em;font-family:'Trebuchet MS', verdana, lucida, arial, helvetica, sans-serif">Blank Template</span></td>
				</TR>
				<tr class="contentBlock">
					<td valign="top" style="padding-left:45px;padding-top:10px;">								
							<table border="0" cellspacing="0" width="600" class="mainLoginUI" style="table-layout:auto;">							
							
							<TR>
								<TD colspan="3" class="loginTitle" width="600"><inc:ContentRegion id="i_Login_UserPrompt" runat="server"></inc:ContentRegion></TD>
							</TR>
							<TR valign="middle">
								<TD class="loginLabel" width="92"><inc:ContentRegion id="i_EmailAddressLabel" runat="server"></inc:ContentRegion></TD>
								<TD align="right" width="263"><inc:ContentRegion id="i_EmailTextBox" runat="server"></inc:ContentRegion></TD>
								<TD rowSpan="5" vAlign="top" width="245"><inc:ContentRegion id="i_InstructionsLabel" runat="server"></inc:ContentRegion></TD>
							</TR>
							<TR>
								<TD class="loginLabel" width="92"><inc:ContentRegion id="i_PasswordLabel" runat="server"></inc:ContentRegion></TD>
								<TD align="right" width="263"><inc:ContentRegion id="i_PasswordTextBox" runat="server"></inc:ContentRegion></TD>
							</TR>
							<tr>
								<TD width="92">&nbsp;</TD>	
								<TD width="263">
									<table width="263"  cellPadding="0" cellspacing="0" border="0">
									<tr>
									<TD valign="middle" nowrap="nowrap"><inc:ContentRegion id="i_CookieCheckBox" runat="server"></inc:ContentRegion></TD>
									</tr>
									<tr>
									<td align="right" width="263" style="padding-top:15px;">
										<table width="0" cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td align="right">
											
												<table border=0 cellpadding=0 cellspacing=0>
													<tr>
														<td><inc:ContentRegion id="i_ForgotPasswordButton" runat="server"></inc:ContentRegion></td>
														<td><inc:ContentRegion id="i_LoginButton" runat="server"></inc:ContentRegion></td>
													</tr>
												</table>
												
												
											</td>
										</tr>
										</table>
									</td>
									</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td id="LoginSpacer" colspan="2"></td>
							</tr>
							<tr>
								<td colSpan="3" width="600">
									<table cellPadding="2" border="0">
										<tr>
											<td colSpan="2"><span class="helpTitle"><inc:ContentRegion id="i_TechAssistanceLabel" runat="server"></inc:ContentRegion></span></td>
										</tr>

										<tr>
											<td class="helpText"><inc:ContentRegion id="i_EmailLabel" runat="server"></inc:ContentRegion> <a href="mailto:<%=AppSettings.Context.TechSupportEmail%>"><%=AppSettings.Context.TechSupportEmail%></a></td>
											<td align="right"></td>
										</tr>

										<tr>
											<td style="padding-top:4px;"><a Class="footerLink" Href="http://www.visioncritical.com/VC_PrivacyPolicy_<%=AppSettings.Context.Culture%>.pdf" target="_blank">&raquo;<span id="privacyLinkText" EnableViewState="False" runat="server" key="Skin_PrivacyPolicyLink"></span></a> &nbsp; <!-- <a href='<%= AppSettings.Context.Culture == "en-CA" ? "http://fr.periscope.visioncritical.com/faq.pdf" : "http://en.periscope.visioncritical.com/faq.pdf" %>' Class="footerLink"><inc:ContentRegion id="i_Login_FAQ" runat="server"></inc:ContentRegion></a> &nbsp; <a href='<%= AppSettings.Context.Culture == "en-CA" ? "http://fr.periscope.visioncritical.com" : "http://en.periscope.visioncritical.com" %>' Class="footerLink"><inc:ContentRegion id="i_Login_AlternateCulture" runat="server"></inc:ContentRegion></a> --></td>
											<td></td>
										</tr>

										<tr>
											<td height="5" colspan="2"><IMG src="<%= Locator.GetRelativeSkinUrl("images/shim.gif") %>" width="1" height="5"></td>
										</tr>
									</table>
								</td>
							</tr>
							</table>								
						
					</td>
				</tr>
				<tr>
					<td align="right"><a href="http://www.visioncritical.com" target="_blank" onMouseOver="document.getElementById('vcLogo').src = '<%= Locator.GetRelativeSkinUrl("images/vc_logo_over.gif") %>';" onMouseOut="document.getElementById('vcLogo').src = '<%= Locator.GetRelativeSkinUrl("images/vc_logo.gif")%>';"><img id="vcLogo" border="0" src="<%= Locator.GetRelativeSkinUrl("images/vc_logo.gif")%>"></a></td>
				</tr>
			</table>

			

			<div style="display:none">
				<span class="welcome"><inc:ContentRegion id="i_WelcomeLabel" runat="server"></inc:ContentRegion></span>
				<span class="to"><inc:ContentRegion id="i_IntroLabel" runat="server"></inc:ContentRegion></span>
				<span class="panel"><inc:ContentRegion id="i_PanelLabel" runat="server"></inc:ContentRegion></span>
			</div>

			</td>
			</tr>
			</table>
			
		</form>
	</body>
</HTML>



