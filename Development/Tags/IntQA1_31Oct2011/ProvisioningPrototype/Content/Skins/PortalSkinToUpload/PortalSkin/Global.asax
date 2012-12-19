<%@ Import Namespace="VisionCritical.PanelPlus" %>
<%@ Import Namespace="VisionCritical.PanelPlus.Diagnostics.Logging" %>
<%@ Import namespace="VisionCritical.PanelPlus.OpenPortal.Controls.PWS"%>
<%@ Import Namespace="System.IO" %>

<script Language="C#" RunAt="server">
    private PortalLogger _mLog;
    private PortalLogger Log
    {
        get { return _mLog ?? (_mLog = new PortalLogger()); }
    }

    protected void Application_Error(object sender, EventArgs e)
    {
        Exception httpEx;
        if (HttpContext.Current != null)
        {
            for (httpEx = HttpContext.Current.Server.GetLastError(); httpEx.InnerException != null; httpEx = httpEx.InnerException) { }
            if (httpEx is HttpException)
            {
                // Log the Http Exception. 
                Log.Error("Http {0} Exception Occurred: {1}", ((HttpException)httpEx).GetHttpCode(), httpEx.GetBaseException());
                
            }
        }

        var ex = Server.GetLastError().GetBaseException();
        if (HttpContext.Current == null || HttpContext.Current.Session == null) return;
        var panelContext = (PanelContext)Session["PanelContext"];

        if (panelContext != null)
        {
            var errorLogger = new VisionCritical.PanelPlus.OpenPortal.Web.ErrorLogger(
                panelContext.PortalEventSourceName,
                panelContext.PanelName,
                panelContext.ContextName,
                panelContext.GlobalErrorsEmailAddress,
                panelContext.ReturnEmailAddress
                );

            errorLogger.LogPortalException(ex);
        }
    }
</script>