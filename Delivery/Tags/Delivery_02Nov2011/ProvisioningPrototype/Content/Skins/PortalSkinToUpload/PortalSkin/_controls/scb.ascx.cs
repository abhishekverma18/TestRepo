using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisionCritical.PanelPlus.OpenPortal.Controls;

namespace BonTon._controls
{
    public partial class Scb : System.Web.UI.UserControl
    {
        private string _theenv;
        protected string Thebox;
        public string GetThisId;
        public string Operation;

        protected void Page_Load(object sender, EventArgs e)
        {

            switch (Operation)
            {
                case "getStaticContent":
                    _theenv = ((IOpenPortalHost)this.Page).PanelContext.Environment;
                    Thebox = GetStaticContent(GetThisId, _theenv);

                    break;
                case "getLocalString":
                    Thebox = GetLocalString(GetThisId);
                    break;
                default:
                    break;
            }
        }
        
        protected string GetStaticContent(string theId, string env)
        {
            string temp = "";
            try
            {
               // temp = ((IOpenPortalHost)this.Page).StaticContentManager.RequestStaticContent(env + "-" + theId);
                temp = ((IOpenPortalHost)this.Page).StaticContentManager.RequestStaticContent(theId);
            }
            catch
            {
                temp = "";
            }
            return temp;
        }
        
        protected string GetLocalString(string theId)
        {
            string temp = "";
            try
            {
                temp = ((IOpenPortalHost)this.Page).LocalizedResourceOverridesManager.GetLocalizedStringOverride(theId);
            }
            catch
            {
                temp = "";
            }
            return temp;
        }
      
    }
}