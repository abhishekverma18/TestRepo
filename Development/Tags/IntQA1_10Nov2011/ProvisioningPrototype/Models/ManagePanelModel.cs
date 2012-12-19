using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProvisioningPrototype.Models
{
    public class ManagePanelModel
    {

        public int ContextIndex { get; set; }
        public string Name { get; set; }
        public string PortalUrl { get; set; }
        public string Environment { get; set; }
        public string Culture { get; set; }
        public List<ManagePanelModel> PanelList { get; set; }
    }
}