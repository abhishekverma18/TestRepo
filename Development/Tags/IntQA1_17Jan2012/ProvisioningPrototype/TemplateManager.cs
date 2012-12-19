using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;

namespace ProvisioningPrototype
{
    // Added for Dynamic Template List module for loading templates
    public class TemplateManager
    {
        #region Fields/Attributes
        /// <summary>
        /// For check instance of class
        /// </summary>
        private static TemplateManager _instance;

        /// <summary>
        /// Object for thread safety
        /// </summary>
        private static readonly object _objLock = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the TemplateManager Object after checking its instance
        /// </summary>
        /// <returns>TemplateManager Object</returns>
        public static TemplateManager Instance
        {
            get
            {
                lock (_objLock)
                {
                    return _instance ?? (_instance = new TemplateManager());
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        private TemplateManager()
        {
        }
        #endregion

        #region Private Methods
        private GuiTemplate ImageLoad(DirectoryInfo SkinPackageToUpload, string templatePath)
        {
            var guiTemplate = new GuiTemplate();
            var imageFilePath = new StringBuilder();
            imageFilePath.Append(templatePath);
            imageFilePath.Append(SkinPackageToUpload.Name);
            imageFilePath.Append(Res.LoginImageFileName);
            guiTemplate.Image1 = imageFilePath.ToString();
            imageFilePath.Clear();
            imageFilePath.Append(templatePath);
            imageFilePath.Append(SkinPackageToUpload.Name);
            imageFilePath.Append(Res.PortalImageFileName);
            guiTemplate.Image2 = imageFilePath.ToString();

            imageFilePath.Clear();
            imageFilePath.Append(templatePath);
            imageFilePath.Append(SkinPackageToUpload.Name);
            imageFilePath.Append(Res.SurveyImageFileName);
            guiTemplate.Image3 = imageFilePath.ToString();
            return guiTemplate;
        }

        // Added by K.G(16-JAN-2012) to complete load from zip functionality
        private GuiTemplate GetStaticVariables(GuiTemplate guiTemplate, string filePath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath + Res.SubstitutionXmlFileName);
            XmlNode QuestionnaireName = xmlDoc.SelectSingleNode(Res.SubstitutionQuestionnaireNamePath);
            XmlNode CompanyName = xmlDoc.SelectSingleNode(Res.SubstitutionCompanyNamePath);
            XmlNode ContactEmail = xmlDoc.SelectSingleNode(Res.SubstitutionContactEmailPath);
            XmlNode Language = xmlDoc.SelectSingleNode(Res.SubstitutionLanguagePath);
            guiTemplate.QuestionnaireName =(QuestionnaireName!=null)?QuestionnaireName.InnerText:string.Empty;
            guiTemplate.CompanyName = (CompanyName!=null)?CompanyName.InnerText:string.Empty;
            guiTemplate.ContactEmail = (ContactEmail!=null)?ContactEmail.InnerText:string.Empty;
            guiTemplate.Language = (Language != null) ? Language.InnerText : string.Empty;
            return guiTemplate;
        }
        private GuiTemplate GetGuiTemplate(GuiTemplate guiTemplate, string filePath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath + Res.SubstitutionXmlFileName);

            XmlNode groupingsNode = xmlDoc.SelectSingleNode(Res.SubstitutonGroupsPath);
            XmlNode substitutionNode = xmlDoc.SelectSingleNode(Res.SubstitutionPath);

            guiTemplate.TemplateName = (substitutionNode.Attributes[Res.TemplateNameAttribute] != null)
                ? substitutionNode.Attributes[Res.TemplateNameAttribute].Value
                : string.Empty;

            if (groupingsNode != null)
            {
                guiTemplate.VariableGroups = new List<GuiVariableGroup>();
                foreach (XmlNode groupingNode in groupingsNode.ChildNodes)
                {
                    var guiGroup = new GuiVariableGroup
                    {
                        GroupName = (groupingNode.Attributes[Res.NameAttribute] != null)
                                        ? groupingNode.Attributes[Res.NameAttribute].Value
                                        : string.Empty,
                        GroupLabel =
                            (groupingNode.Attributes[Res.GroupLabelAttribute] != null)
                                ? groupingNode.Attributes[Res.GroupLabelAttribute].Value
                                : string.Empty
                    };
                    guiTemplate.VariableGroups.Add(guiGroup);
                }
            }

            XmlNode variablesNode = xmlDoc.SelectSingleNode(Res.SubstitutionVariablesPath);

            if (variablesNode != null)
            {
                foreach (XmlNode variableNode in variablesNode.ChildNodes)
                {
                    var group = (variableNode.Attributes[Res.GroupingAttribute] != null) ?
                        variableNode.Attributes[Res.GroupingAttribute].Value : string.Empty;
                    foreach (GuiVariableGroup guiVarGroup in guiTemplate.VariableGroups)
                    {
                        if (guiVarGroup.GroupName.Equals(group))
                        {
                            if (guiVarGroup.Variables == null)
                            {
                                guiVarGroup.Variables = new List<GuiVariable>();
                            }

                            var guiVariable = new GuiVariable();
                            guiVariable.Substitution = (variableNode.Attributes[Res.SubstituionAttribute] != null) ?
                                variableNode.Attributes[Res.SubstituionAttribute].Value : string.Empty;
                            guiVariable.PropertyName = (variableNode.Attributes[Res.PropertyNameAttribute] != null) ?
                                variableNode.Attributes[Res.PropertyNameAttribute].Value : string.Empty;
                            guiVariable.GuiName = (variableNode.Attributes[Res.UiNameAttribute] != null) ?
                                variableNode.Attributes[Res.UiNameAttribute].Value : string.Empty;
                            guiVariable.AltText = (variableNode.Attributes[Res.AltTextAttribute] != null) ?
                                variableNode.Attributes[Res.AltTextAttribute].Value : string.Empty;
                            guiVariable.UiComponent = (variableNode.Attributes[Res.UIComponentAttribute] != null) ?
                                variableNode.Attributes[Res.UIComponentAttribute].Value : string.Empty;
                            guiVariable.Default = (variableNode.Attributes[Res.DefaultAttribute] != null) ?
                                variableNode.Attributes[Res.DefaultAttribute].Value : string.Empty;
                            guiVariable.ComponentName = (variableNode.Attributes[Res.ComponentNameAttribute] != null) ?
                                variableNode.Attributes[Res.ComponentNameAttribute].Value : string.Empty;
                            guiVariable.ReplacementDirectory = (variableNode.Attributes[Res.ReplacementDirectoryAttribute] != null) ?
                                variableNode.Attributes[Res.ReplacementDirectoryAttribute].Value : string.Empty;
                            guiVariable.PathToUpload = (variableNode.Attributes[Res.PathToUploadAttribute] != null) ?
                                variableNode.Attributes[Res.PathToUploadAttribute].Value : string.Empty;
                            var valueLst = (variableNode.Attributes[Res.ComponentValueListAttribute] != null) ?
                                variableNode.Attributes[Res.ComponentValueListAttribute].Value : string.Empty;
                            if (!String.IsNullOrEmpty(valueLst))
                            {
                                guiVariable.ComponentValueList = new List<string>();
                                string tempString = valueLst.Replace("\',\'", ".").Replace("\'", "");           // TODO needs to be more forgiving
                                guiVariable.ComponentValueList.AddRange(tempString.Split('.'));
                            }
                            else
                            {
                                guiVariable.ComponentValueList = null;
                            }
                            guiVarGroup.Variables.Add(guiVariable);
                        }
                    }
                }
            }
            return guiTemplate;
        }
        #endregion


        #region Public Methods

        // Added by K.G for Phase 2 task 'Allow the UI to load previously generated zip files in order to pre-populate the form' & Some refactoring of code has been done. Commonly used code has been shifted to private functions.
        public GuiTemplate LoadPrevTemp()
        {
            var guiTemplate = new GuiTemplate();
            var SkinPackageToUpload = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + Res.PreviouslyGeneratedPackagePath);
            var filePath = SkinPackageToUpload.FullName;
            guiTemplate = ImageLoad(SkinPackageToUpload, Res.PrevGeneratedTemplatePath);
            guiTemplate = GetGuiTemplate(guiTemplate, filePath);
            guiTemplate = GetStaticVariables(guiTemplate, filePath);
            return guiTemplate;
        }

        public DynamicGuiTemplates LoadTemplates()
        {
            var guiModel = new DynamicGuiTemplates();
            var mainDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + Res.LocalTemplatePath);
            int templateCount = mainDir.GetDirectories().Count(dir => (dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden);
            guiModel.GuiTemplates = new GuiTemplate[templateCount];
            int count = 0;
            foreach (var subDir in mainDir.EnumerateDirectories())
            {
                if (!subDir.Name.Equals(Constants.SVN))
                {
                    var guiTemplate = new GuiTemplate();

                    var filePath = subDir.FullName;
                    guiTemplate = ImageLoad(subDir, Res.TemplateImagePath);
                    guiModel.GuiTemplates[count] = GetGuiTemplate(guiTemplate, filePath);

                    count++;
                }
            }
            return guiModel;
        }
        #endregion

    }
}