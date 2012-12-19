<%@ Page 
Language="C#" 
AutoEventWireup="true" 
Inherits="VisionCritical.PanelPlus.OpenPortal.Web.Host.OpenPortalHostPage" MasterPageFile="Login.master"%>

<%@ Register 
    Assembly="VisionCritical.PanelPlus.OpenPortal.Controls" 
    Namespace="VisionCritical.PanelPlus.OpenPortal.Controls.Controls"
    TagPrefix="OpenPortalControls" %>
<%@ Register TagPrefix="scb" TagName="getscb" Src="~/_controls/scb.ascx" %>


	<asp:content id="Column2Login" contentplaceholderid="Column2Login" runat="server">
	    <div class="widgets" id="login-text">
			<!-- Static Content --><scb:getscb ID="Getscb1" getThisID="login-welcome" runat="server" operation="getStaticContent"></scb:getscb>
			
			<div id="join-now">
			<scb:getscb ID="Getscb2" getThisID="login-join-now" runat="server" operation="getStaticContent"></scb:getscb>
			</div>
			
		</div>
		
		<div class="clear"></div>
    </asp:content>
