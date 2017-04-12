using System.Collections;
using System.Collections.Generic;
using System;
using System.Web;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using DotNetNuke.Services.Search.Entities;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for MeetingDocumentManager
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController : ModuleSearchBase//: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<MeetingDocumentManagerInfo> colMeetingDocumentManagers = GetMeetingDocumentManagers(ModuleID);
        //if (colMeetingDocumentManagers.Count != 0)
        //{
        //    strXML += "<MeetingDocumentManagers>";

        //    foreach (MeetingDocumentManagerInfo objMeetingDocumentManager in colMeetingDocumentManagers)
        //    {
        //        strXML += "<MeetingDocumentManager>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objMeetingDocumentManager.Content) + "</content>";
        //        strXML += "</MeetingDocumentManager>";
        //    }
        //    strXML += "</MeetingDocumentManagers>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlMeetingDocumentManagers = DotNetNuke.Common.Globals.GetContent(Content, "MeetingDocumentManagers");
        //foreach (XmlNode xmlMeetingDocumentManager in xmlMeetingDocumentManagers.SelectNodes("MeetingDocumentManager"))
        //{
        //    MeetingDocumentManagerInfo objMeetingDocumentManager = new MeetingDocumentManagerInfo();
        //    objMeetingDocumentManager.ModuleId = ModuleID;
        //    objMeetingDocumentManager.Content = xmlMeetingDocumentManager.SelectSingleNode("content").InnerText;
        //    objMeetingDocumentManager.CreatedByUser = UserID;
        //    AddMeetingDocumentManager(objMeetingDocumentManager);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<MeetingDocumentManagerInfo> colMeetingDocumentManagers = GetMeetingDocumentManagers(ModInfo.ModuleID);

        //foreach (MeetingDocumentManagerInfo objMeetingDocumentManager in colMeetingDocumentManagers)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objMeetingDocumentManager.Content, 
        //        objMeetingDocumentManager.CreatedByUser, objMeetingDocumentManager.CreatedDate, ModInfo.ModuleID, 
        //       objMeetingDocumentManager.ItemId.ToString(), objMeetingDocumentManager.Content, "ItemId=" + objMeetingDocumentManager.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

        public override IList<SearchDocument> GetModifiedSearchDocuments(ModuleInfo modInfo, DateTime beginDate)
        {
            var searchDocuments = new List<SearchDocument>();

            IEnumerable<Meeting> meetingList = new ForsythCo.Modules.MeetingDocumentManager.Components.MeetingController().GetAllMeetings();

            foreach (Meeting m in meetingList)
            {
                if (m.RecordUpdated >= beginDate)
                {
                    Dictionary<string, string> keywords = new Dictionary<string, string>();

                    keywords.Add("Date", m.Begining.ToLongDateString());
                    keywords.Add("Description", m.FormattedDescription);
                    keywords.Add("Building", m.Location.BuildingName);
                    keywords.Add("Group", m.MeetingGroup.GroupName);
                    keywords.Add("Type", m.MeetingType.TypeName);

                    foreach (Document d in m.Documents)
                    {
                        IEnumerable<string> tags = new string[] { m.MeetingType.TypeName, m.MeetingGroup.GroupName, d.DocumentGroup.GroupName };
                        
                        SearchDocument doc = new SearchDocument()
                        {
                            UniqueKey = "MDM" + m.MeetingID.ToString() + "_DOC" + d.DocumentID.ToString(),
                            ModuleId = modInfo.ModuleID,
                            Description = HttpUtility.HtmlDecode(d.Content).Substring(0, d.Content.Length > 250 ? 250 : d.Content.Length),
                            Body = HttpUtility.HtmlDecode(d.Content).Replace("<br />",""),
                            Tags = tags,
                            ModifiedTimeUtc = m.RecordUpdated.ToUniversalTime(),
                            Title = String.Format("{1}: {0} ({2})", m.FormattedDescription, m.Begining.ToShortDateString(), d.DocumentGroup.GroupName),
                            PortalId = modInfo.PortalID,
                            QueryString = String.Format("Meeting/{0}", m.MeetingID),
                        };

                        searchDocuments.Add(doc);
                    }
                }
            }

            return searchDocuments;
        }
    }
}
