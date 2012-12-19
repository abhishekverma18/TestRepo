<%@ Page 
Language="C#" 
AutoEventWireup="true" 
Inherits="VisionCritical.PanelPlus.OpenPortal.Web.Host.OpenPortalHostPage" MasterPageFile="Login.master"%>

<%@ Register 
    Assembly="VisionCritical.PanelPlus.OpenPortal.Controls" 
    Namespace="VisionCritical.PanelPlus.OpenPortal.Controls.Controls"
    TagPrefix="OpenPortalControls" %>


	<asp:content id="ServerScripts" contentplaceholderid="ServerScriptsLogin" runat="server">
	<div id="tech-support-link" style="display: none;"><OpenPortalControls:StaticContentControl1_1 
		ID="StaticContentControlTS" 
		StaticContentId="DEFAULT-tech-support" 
		IncludeDefaultStyle="false" 
		runat="server" /></div>
	<script type="text/javascript" src="<%=((VisionCritical.PanelPlus.OpenPortal.Controls.IOpenPortalHost)this.Page).SkinBaseUrl%>/_js/jquery.js"></script>
	<script type="text/javascript">
		var link = $("#tech-support-link a").attr("href");
		if(link.indexOf("mailto:") == -1) {
			window.location = link;
		} else {
			window.location = link;
			window.location ="<%=((VisionCritical.PanelPlus.OpenPortal.Controls.IOpenPortalHost)this.Page).SkinBaseUrl%>/default.aspx";
		}
	</script>
    </asp:content>
