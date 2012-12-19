<%@ Page 
Language="C#" 
AutoEventWireup="true" 
Inherits="VisionCritical.PanelPlus.OpenPortal.Web.Host.OpenPortalHostPage" MasterPageFile="Member.master"%>

<%@ Register 
    Assembly="VisionCritical.PanelPlus.OpenPortal.Controls" 
    Namespace="VisionCritical.PanelPlus.OpenPortal.Controls.Controls"
    TagPrefix="OpenPortalControls" %>

	<asp:content id="Column2Member" contentplaceholderid="Column2Member" runat="server">


		<!-- PANEL : Widget Profile -->
		<div class="widgets" id="wid-profile">
					<!--Profile  -->   
					<OpenPortalControls:ProfileInformationControl1_1 
					ID="ProfileInformationControl" 
					IncludeDefaultStyle="false" 
					runat="server" />
		</div>
		<!-- END PANEL : Widget Profile -->   
    </asp:content>