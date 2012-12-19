<%@ Page 
Language="C#" 
AutoEventWireup="true" 
Inherits="VisionCritical.PanelPlus.OpenPortal.Web.Host.OpenPortalHostPage" MasterPageFile="Member.master"%>

<%@ Register 
    Assembly="VisionCritical.PanelPlus.OpenPortal.Controls" 
    Namespace="VisionCritical.PanelPlus.OpenPortal.Controls.Controls"
    TagPrefix="OpenPortalControls" %>


	<asp:content id="ServerScriptsMember" contentplaceholderid="ServerScriptsMember" runat="server">
    </asp:content>


	<asp:content id="Column2Member" contentplaceholderid="Column2Member" runat="server">

		<!-- PANEL : Widget Forums Full -->
		<div class="widgets" id="wid-forum-full">
			<h1>All Discussions</h1>
			<div id="forum-list">
			    <OpenPortalControls:CommunityListControl1_1     
			    ID="CommunityListControl" 
			    IncludeDefaultStyle="false" 
			    StudyStatus="AllOpen"
			    ItemsPerPage="10" 
			    DefaultForumSortOrder="Alphabetical"
    			
			    ShowSortingControls="true"
			    ShowAllButton="false"
			    ShowPaginationControls="true"
    			
			    DisplayOpenDate="true"
			    DisplayCloseDate="false"
			    DisplayForumStatus="true"
			    DisplayHotFlag="false"
			    DisplayNewFlag="false"
    			
			    Layout="Table"                     
			    runat="server" /> 
			</div>
		</div>
		<!-- END PANEL : Widget Forums Full -->
		
    </asp:content>