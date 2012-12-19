<%@ Page 
Language="C#" 
AutoEventWireup="true" 
Inherits="VisionCritical.PanelPlus.OpenPortal.Web.Host.OpenPortalHostPage" MasterPageFile="Login.master"%>

<%@ Register 
    Assembly="VisionCritical.PanelPlus.OpenPortal.Controls" 
    Namespace="VisionCritical.PanelPlus.OpenPortal.Controls.Controls"
    TagPrefix="OpenPortalControls" %>


	<asp:content id="ServerScripts" contentplaceholderid="ServerScriptsLogin" runat="server">
	<script language="C#" runat="server">

		protected override void OnInit(EventArgs e)
		{
            this.Response.Redirect(GetLink());
		}

        protected string GetLink()
        {

            string link = ((VisionCritical.PanelPlus.OpenPortal.Controls.IOpenPortalHost)this.Page).StaticContentManager.RequestStaticContent("DEFAULT-web-terms");
            string link2 = ((VisionCritical.PanelPlus.OpenPortal.Controls.IOpenPortalHost)this.Page).StaticContentManager.GetStaticContent("DEFAULT-web-terms");

            //look for link
            string re1 = "((?:http|https)(?::\\/{2}[\\w]+)(?:[\\/|\\.]?)(?:[^\\s\"]*))";
            Regex r = new Regex(re1, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            
            return  r.Match(link).ToString();
        }  
	
	</script>
    </asp:content>

	
