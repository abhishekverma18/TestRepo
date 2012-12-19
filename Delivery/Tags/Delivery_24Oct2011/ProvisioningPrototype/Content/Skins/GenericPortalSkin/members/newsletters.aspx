<%@ Page 
Language="C#" 
AutoEventWireup="true" 
Inherits="VisionCritical.PanelPlus.OpenPortal.Web.Host.OpenPortalHostPage" MasterPageFile="Member.master"%>

<%@ Register 
    Assembly="VisionCritical.PanelPlus.OpenPortal.Controls" 
    Namespace="VisionCritical.PanelPlus.OpenPortal.Controls.Controls"
    TagPrefix="OpenPortalControls" %>

	<asp:content id="Column2Member" contentplaceholderid="Column2Member" runat="server">

	<!-- PANEL DETAIL -->
		<div class="lay-widget">
			<!--Newsletters -->
			<OpenPortalControls:NewslettersControl1_1 
			ID="NewslettersControl2" 
			IncludeDefaultStyle="false" 
			MaximumNewsletterCount="999"
			ShowAllButton = "false" 
			runat="server" />
			
		</div>
		<!-- END PANEL DETAIL -->
             
    </asp:content>