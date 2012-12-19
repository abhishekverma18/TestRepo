using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvisioningPrototype
{

    public class EmailTemplate
    {
        public string EmailTemplateName { get; set; }
        public string EmailFormat { get; set; }
        public string EmailSubject { get; set; }
        public string EmailLanguage { get; set; }
        public string EmailPlainText { get; set; }
        public string EmailRichText { get; set; }
        public EmailTemplate()
        {
        }
    }

}