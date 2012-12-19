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


	<asp:content id="cssMember" contentplaceholderid="cssMember" runat="server">
		<!-- IF NO POINTS, disable DisplayPointsForCompletion & DisplayPointsEarned in control and show override css //-->
	</asp:content>

	<asp:content id="Column2Member" contentplaceholderid="Column2Member" runat="server">

		<!-- PANEL : Widget Studies FULL -->
		<div class="widgets subpage" id="wid-studies-full">
		
		<h1>Take a Survey</h1>
		<div id="studies-list">
			<OpenPortalControls:StudyListControl1_1 
			ID="StudyListControl" 
			IncludeDefaultStyle="false" 
			ItemsPerPage="10"
			StudyStatus="AllActive"
			
			DefaultStudySortOrder="Alphabetical"
			DefaultStudyStatusFilter="AllActive" 
			
			ShowIncompleteClosedResponses="true"  
			ShowPaginationControls="true"
			ShowSortingControls="true"
			ShowFilterControls="true"
			
			DisplayPointsForCompletion="false"
			DisplayPointsEarned="false"	
			DisplayCloseDate="true"
			DisplayOpenDate="false" 
			DisplayStudyStatus="true"
			
			
			Layout="Table"    
			runat="server" /> 
		
			</div>
		</div>
		<!-- END PANEL : Widget Studies Full -->
             
    </asp:content>