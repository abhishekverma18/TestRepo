namespace ProvisioningPrototype
{
    public class ContextList
    {
       public  string ContextName{get;set;}
       public  ContextInfo ContextInfo { get; set; }
        
        public ContextList()
        {
        }
        public ContextList(string contextName, ContextInfo contextInfo)
        {
            ContextName = contextName;
            ContextInfo = contextInfo;
        }
        
    }
}