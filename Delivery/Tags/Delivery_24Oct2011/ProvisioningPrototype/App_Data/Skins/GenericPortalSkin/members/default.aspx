<%@ Page 
Language="C#" 
AutoEventWireup="true" 
Inherits="VisionCritical.PanelPlus.OpenPortal.Web.Host.OpenPortalHostPage" MasterPageFile="Member.master"%>

<%@ Register TagPrefix="scb" TagName="getscb" Src="~/_controls/scb.ascx" %>
<%@ Register 
    Assembly="VisionCritical.PanelPlus.OpenPortal.Controls" 
    Namespace="VisionCritical.PanelPlus.OpenPortal.Controls.Controls"
    TagPrefix="OpenPortalControls" %>


	<asp:content id="ServerScriptsMember" contentplaceholderid="ServerScriptsMember" runat="server">
	<script language="C#" runat="server">

		protected override void OnInit(EventArgs e)
		{
			this.StudyListControl.AllStudiesRequested += new EventHandler(StudyListControl_AllFilter);
            this.CommunityListControl.AllForumsRequested += new EventHandler(CommunityListControl_AllFilter);
			base.OnInit(e);
		}

		protected void StudyListControl_AllFilter(object o, EventArgs e)
		{
			this.Response.Redirect(SkinBaseUrl+"/members/studies.aspx");
		}

        protected void CommunityListControl_AllFilter(object o, EventArgs e)
        {
            this.Response.Redirect(SkinBaseUrl + "/members/forums.aspx");
        }      
	
	</script>
    </asp:content>

	<asp:content id="cssMember" contentplaceholderid="cssMember" runat="server">
		
	</asp:content>


	<asp:content id="Column2Member" contentplaceholderid="Column2Member" runat="server">

		<!-- PANEL : Widget Welcome Message -->
		<div class="widgets" id="wid-member-welcome">
				<!-- Static Content -->
				<h1>Welcome <%=Panelist.Name%></h1>
				<scb:getscb ID="Getscb1" getThisID="member-welcome" runat="server" operation="getStaticContent"></scb:getscb>
		</div>
		<!-- END PANEL : Widget Welcome Message -->
		
      <!-- PANEL : Widget Studies List -->
		<div class="widgets" id="wid-studies-list">
				<!--Studies --> 
				<h1>Take a Survey</h1>
				<div id="studies-list">
				<OpenPortalControls:StudyListControl1_1   
				ID="StudyListControl" 
				IncludeDefaultStyle="false" 
				ItemsPerPage="5"
				StudyStatus="AllActive"
				DefaultStudySortOrder="Alphabetical"
				DefaultStudyStatusFilter="AllActive"
				DisplayPointsForCompletion="false"
				DisplayPointsEarned="false"
				DisplayCloseDate="true"
				DisplayOpenDate="false"
				DisplayStudyStatus="true"
				ShowSortingControls="false"
				ShowFilterControls="false"
				ShowInlineHeaders="true"
				ShowAllButton="true"
				Layout="List"
				runat="server" /> 
			</div>
		</div>
		<!-- END PANEL : Widget Studies List -->
		
		<!-- PANEL 6 (MAIN FORUM), if moving this to another panel, please move 'id' as well -->
		<div class="widgets" id="wid-forum-list">
		     <h1>Join a Discussion</h1>
		     <div id="forum-list">
				<OpenPortalControls:CommunityListControl1_1   
				ID="CommunityListControl" 
				IncludeDefaultStyle="false" 
				ItemsPerPage="5"
				StudyStatus="AllOpen" 
				DefaultForumSortOrder="Alphabetical"
				DefaultStudyStatusFilter="AllActive"
				ShowSortingControls="false"
				ShowFilterControls="false"
				ShowInlineHeaders="true"
				ShowAllButton="true"
				ShowPaginationControls="false"
				DisplayOpenDate="true"
				DisplayCloseDate="false"
				DisplayForumStatus="false"
				DisplayHotFlag="true"
				DisplayNewFlag="true"	
				Layout="List"                       
				runat="server" />
		    </div>
		</div>
		<!-- END PANEL : Widget Forum List -->
		
	 </asp:content>