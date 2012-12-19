<%@ Control Language="C#" AutoEventWireup="true"%>
<%@ Import Namespace="VisionCritical.PanelPlus.OpenPortal.Controls" %>
<%@ Import Namespace="VisionCritical.PanelPlus.OpenPortal.Controls.Controls" %>

<script language="C#" runat="server">
    private string theenv;
    private string thebox;
    public string getThisID;
	public string operation;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        switch (operation)
		{
			case "getStaticContent":
				theenv = ((IOpenPortalHost)this.Page).PanelContext.Environment;
				thebox = getStaticContent(getThisID, theenv);
				
			break;
			case "getLocalString":
					thebox = getLocalString(getThisID);
			break;
		default:
		break;
		}
    }
	protected string getStaticContent(string theID, string env)
	{
		string temp="";
		try
		{
			temp = ((IOpenPortalHost)this.Page).StaticContentManager.GetStaticContent(env + "-" + theID);
			if(temp == null)
			{
				temp = ((IOpenPortalHost)this.Page).StaticContentManager.RequestStaticContent(env + "-" + theID);
				if(temp == null)
				{
					//temp = ((IOpenPortalHost)this.Page).StaticContentManager.RequestStaticContent(env + "-" + theID);
					temp = ((IOpenPortalHost)this.Page).StaticContentManager.GetStaticContent(env + "-" + theID);
				}
			}
				
		}
		catch
		{
			temp = "";
		}
		return temp;
	}
	protected string getLocalString(string theID)
	{
		string temp="";
		try
		{
			temp = ((IOpenPortalHost)this.Page).LocalizedResourceOverridesManager.GetLocalizedStringOverride(theID);
		}
		catch
		{
			temp = "";
		}
		return temp;
	}
</script>
<%=thebox %>