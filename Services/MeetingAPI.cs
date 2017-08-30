using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using DotNetNuke.Web.Api;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using System.ServiceModel.Syndication;
using Newtonsoft.Json;

namespace ForsythCo.Modules.MeetingDocumentManager.Services
{
    public class MeetingAPIController : DnnApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Location> GetLocations()
        {
            return new LocationController().GetLocations();
        }

        [AllowAnonymous]
        [HttpGet]
        public string GetUpcomingMeetingsJSON()
        {
            MeetingController conM = new MeetingController();
            return Newtonsoft.Json.JsonConvert.SerializeObject(conM.GetUpcomingMeetings());
        }

        [AllowAnonymous]
        [HttpGet]
        public string GetActiveMeetingsJSON()
        {
            MeetingController conM = new MeetingController();
            return Newtonsoft.Json.JsonConvert.SerializeObject(conM.GetAllActiveMeetings());
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Meeting> GetMeetings()
        {
            return new MeetingController().GetUpcomingMeetings();
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeetingsJSON()
        {

            MeetingController conM = new MeetingController();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(conM.GetAllMeetings()), System.Text.Encoding.UTF8, "application/json")
            };
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeetingsByOffset(string offset, string pagesize)
        {
            HttpResponseMessage msg = new HttpResponseMessage();

            MeetingController conM = new MeetingController();
            msg.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(conM.GetByOffset(Convert.ToInt32(offset), Convert.ToInt32(pagesize))), System.Text.Encoding.UTF8, "application/json");
            return msg;
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeetingsByGroupType(string group, string type)
        {
            MeetingController con = new MeetingController();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(con.GetFilteredByGT(Convert.ToInt32(group), Convert.ToInt32(type))), System.Text.Encoding.UTF8, "application/json")
            };
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeetingsByDate(string month, string year)
        {
            HttpResponseMessage msg = new HttpResponseMessage();

            MeetingController con = new MeetingController();
            msg.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(con.GetFilteredByDate(Convert.ToInt32(month), Convert.ToInt32(year))), System.Text.Encoding.UTF8, "application/json");
            return msg;
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeetingYears()
        {
            HttpResponseMessage msg = new HttpResponseMessage();

            MeetingYearController con = new MeetingYearController();
            msg.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(con.GetMeetingYears()), System.Text.Encoding.UTF8, "application/json");
            return msg;
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeetingGroupsJSON()
        {
            MeetingGroupController con = new MeetingGroupController();
            HttpResponseMessage msg = new HttpResponseMessage();
            msg.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(con.GetMeetingGroups()), System.Text.Encoding.UTF8, "application/json");

            return msg;
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeetingTypesJSON()
        {
            MeetingTypeController con = new MeetingTypeController();
            HttpResponseMessage msg = new HttpResponseMessage();
            msg.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(con.GetMeetingTypes()), System.Text.Encoding.UTF8, "application/json");

            return msg;
        }


        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage RSS(string s)
        {

            string feedLink = s + "/desktopmodules/MeetingDocumentManager/api/meetingapi/RSS?s=" + s;

            string xmlStr = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            xmlStr += "<rss version=\"2.0\">";
            xmlStr += "<channel>";
            xmlStr += "<title>Forsyth County Meetings (RSS)</title>";
            xmlStr += "<description>This feed contains all public meetings that take place for Forsyth County Georgia.</description>";
            xmlStr += "<link>" + feedLink + "</link>";
            xmlStr += "<language>en-US</language>";
            xmlStr += "<generator>Forsyth County Georgia</generator>";

            foreach (Meeting m in new MeetingController().GetUpcomingMeetings())
            {
                xmlStr += "<item>";
                xmlStr += String.Format("<title>{0} {1} on {2}</title>", m.MeetingGroup.GroupName, m.MeetingType.TypeName, m.Begining.ToString("U"));

                string desc = "";

                if (!String.IsNullOrEmpty(m.Flag))
                    desc += "<h2>ALERT: " + m.Flag + "</h2>";
                foreach (Document d in m.Documents)
                {
                    if (d.DocumentGroup.GroupName == "Public Notice")
                    {
                        desc += HttpUtility.HtmlDecode(d.Content);
                    }
                }

                xmlStr += "<description><![CDATA[" + desc + "]]></description>";
                xmlStr += "<link>" + s + "/meeting/" + m.MeetingID + "</link>";
                xmlStr += "<guid>Meeting" + m.MeetingID.ToString() + "</guid>";
                xmlStr += "<pubDate>" + m.RecordCreated.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000</pubDate>";
                xmlStr += "</item>";
            }


            xmlStr += "</channel>";
            xmlStr += "</rss>";


            HttpResponseMessage msg = new HttpResponseMessage();
                msg.Content = new StringContent(xmlStr, System.Text.Encoding.UTF8, "application/rss+xml");
            return msg;
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeetingGroups()
        {
            try
            {
                string output = "{ \"response\" : [";

                foreach (MeetingGroup g in new MeetingGroupController().GetMeetingGroups())
                {
                    output += "{\"groupid\" : " + g.MeetingGroupID.ToString() + ", \"groupname\" : \"" + g.GroupName + "\", \"description\" : \"" + g.ShortDescription + "\"},";
                }
                output = output.Remove(output.Length - 1, 1);
                output += "]}";

                return new HttpResponseMessage()
                {
                    Content = new StringContent(output, System.Text.Encoding.UTF8, "application/json")
                };          
            }
            catch(Exception ex)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"error\" : \"" + ex.Message + "\"}", System.Text.Encoding.UTF8, "application/json"),
                };
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetMeeting(string mid)
        {
            try
            {
                Meeting m = new MeetingController().GetMeetingByID(Convert.ToInt32(mid));
                string vim = "null";
                if (!Object.Equals(m.VimeoNumber, null))
                    vim = m.VimeoNumber;
                string output = "{ \"response\" : ";
                string upcoming = (m.Begining > DateTime.Now ? "true" : "false");
                output += "{\"upcoming\" : "+ upcoming + ", \"meetingid\" : " + m.MeetingID + ", \"link\" : \"http://www.forsythco.com/meetings/meeting/" + m.MeetingID.ToString() + "\", \"group\" : \"" + m.MeetingGroup.GroupName + "\", \"type\" : \"" + m.MeetingType.TypeName + "\", \"building\" : \"" + m.Location.BuildingName + "\", \"location\" : \"" + m.Location.AddressOne + " " + m.Location.AddressTwo + "\", \"date\" : \"" + m.Begining.ToShortDateString() + "\", \"time\" : \"" + m.Begining.ToShortTimeString() + "\", \"vimeoid\" : " + vim + ", \"documents\" : [";
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                
                foreach (Document d in m.Documents)
                {
                    byte[] doc = enc.GetBytes(HttpUtility.HtmlDecode(d.Content));
                    
                    output += "{\"type\" : \""+ d.DocumentGroup.GroupName +"\", \"content\" : \"" + Convert.ToBase64String(doc, Base64FormattingOptions.None) + "\"},";
                }
                if(output[output.Length-1].ToString() == ",")
                    output = output.Remove(output.Length - 1, 1);
                output += "]}}";

                return new HttpResponseMessage()
                {
                    Content = new StringContent(output, System.Text.Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"error\" : \"" + ex.Message + "\"}", System.Text.Encoding.UTF8, "application/json"),
                };
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetByType(string t)
        {
            try{
            string output = "{ \"response\" : [";

            foreach (Meeting m in new MeetingController().GetByMeetingGroup(Convert.ToInt32(t)))
            {
                string vim = "null";
                if (!Object.Equals(m.VimeoNumber, null))
                    vim = m.VimeoNumber;
                if(m.Begining > DateTime.Now)
                {
                    output += "{\"upcoming\" : true, \"meetingid\" : " + m.MeetingID + ", \"link\" : \"http://www.forsythco.com/meetings/meeting/" + m.MeetingID.ToString() + "\", \"group\" : \"" + m.MeetingGroup.GroupName + "\", \"type\" : \"" + m.MeetingType.TypeName + "\", \"building\" : \"" + m.Location.BuildingName + "\", \"location\" : \"" + m.Location.AddressOne + " " + m.Location.AddressTwo + "\", \"date\" : \"" + m.Begining.ToShortDateString() + "\", \"time\" : \"" + m.Begining.ToShortTimeString() + "\", \"vimeoid\" : " + vim + "}";
                }
                else
                {
                    output += "{\"upcoming\" : false, \"meetingid\" : " + m.MeetingID + ", \"link\" : \"http://www.forsythco.com/meetings/meeting/" + m.MeetingID.ToString() + "\", \"group\" : \"" + m.MeetingGroup.GroupName + "\", \"type\" : \"" + m.MeetingType.TypeName + "\", \"building\" : \"" + m.Location.BuildingName + "\", \"location\" : \"" + m.Location.AddressOne + " " + m.Location.AddressTwo + "\", \"date\" : \"" + m.Begining.ToShortDateString() + "\", \"time\" : \"" + m.Begining.ToShortTimeString() + "\", \"vimeoid\" : " + vim + "}";
                }
                output += ",";
            }
            if(output[output.Length-1].ToString() == ",")
                output = output.Remove(output.Length -1, 1);
            output += "]}";

            return new HttpResponseMessage()
            {
                Content = new StringContent(output, System.Text.Encoding.UTF8, "application/json")
            };
                }
            catch(Exception ex)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Message + "|" + ex.StackTrace, System.Text.Encoding.UTF8, "text/plain")
                };
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage Calendar()
        {
            string output = "BEGIN:VCALENDAR" + Environment.NewLine;
            output += "METHOD:PUBLISH" + Environment.NewLine;
            output += "VERSION:2.0" + Environment.NewLine;
            output += "PRODID:-//Forsyth County Georgia - Meetings//EN" + Environment.NewLine;
            foreach (Meeting m in new MeetingController().GetUpcomingMeetings())
            {
                if (string.IsNullOrEmpty(m.Flag))
                {
                    string stamp = string.Format("{0}{1}{2}T{3}Z", m.RecordCreated.ToUniversalTime().ToString("yyyy"), m.RecordCreated.ToUniversalTime().ToString("MM"), m.RecordCreated.ToUniversalTime().ToString("dd"), m.RecordCreated.ToUniversalTime().ToString("HHmmss"));
                    string stD = string.Format("{0}{1}{2}T{3}Z", m.Begining.ToUniversalTime().ToString("yyyy"), m.Begining.ToUniversalTime().ToString("MM"), m.Begining.ToUniversalTime().ToString("dd"), m.Begining.ToUniversalTime().ToString("HHmmss"));
                    output += "BEGIN:VEVENT" + Environment.NewLine;
                    output += "ORGANIZER:forsythpr@forsythco.com" + Environment.NewLine;
                    output += "DTSTAMP:" + stamp + Environment.NewLine;
                    output += "ORGANIZER:ForsythCounty:MAILTO:forsythpr@forsythco.com" + Environment.NewLine;
                    output += "DTSTART:" + stD + Environment.NewLine;
                    output += "UID:meeting" + m.MeetingID + "@forsythco.com" + Environment.NewLine;
                    output += "DURATION:PT1H0M0S" + Environment.NewLine;
                    output += "LOCATION:" + String.Format("{0} {1}", m.Location.AddressOne, m.Location.AddressTwo) + Environment.NewLine;
                    output += "SUMMARY:" + m.FormattedDescription + Environment.NewLine;
                    output += "END:VEVENT" + Environment.NewLine;
                }
            }

            output += "END:VCALENDAR" + Environment.NewLine;
            output += "" + Environment.NewLine;
            return new HttpResponseMessage()
            {
                Content = new StringContent(output, System.Text.Encoding.UTF8, "text/calendar")              
            };
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage Calendar(string id)
        {
            string output = "BEGIN:VCALENDAR" + Environment.NewLine; ;
            output += "METHOD:PUBLISH" + Environment.NewLine;
            output += "VERSION:2.0" + Environment.NewLine;
            output += "PRODID:-//Forsyth County Georgia - Meetings//EN" + Environment.NewLine;
            Meeting m = new MeetingController().GetMeetingByID(Convert.ToInt32(id));
            
            if (string.IsNullOrEmpty(m.Flag))
            {
                string stamp = string.Format("{0}{1}{2}T{3}Z", m.RecordCreated.ToUniversalTime().ToString("yyyy"), m.RecordCreated.ToUniversalTime().ToString("MM"), m.RecordCreated.ToUniversalTime().ToString("dd"), m.RecordCreated.ToUniversalTime().ToString("HHmmss"));
                string stD = string.Format("{0}{1}{2}T{3}Z", m.Begining.ToUniversalTime().ToString("yyyy"), m.Begining.ToUniversalTime().ToString("MM"), m.Begining.ToUniversalTime().ToString("dd"), m.Begining.ToUniversalTime().ToString("HHmmss"));
                output += "BEGIN:VEVENT" + Environment.NewLine;
                output += "ORGANIZER:forsythpr@forsythco.com" + Environment.NewLine;
                output += "DTSTAMP:" + stamp + Environment.NewLine;
                output += "ORGANIZER:ForsythCounty:MAILTO:forsythpr@forsythco.com" + Environment.NewLine;
                output += "DTSTART:" + stD + Environment.NewLine;
                output += "UID:meeting" + m.MeetingID + "@forsythco.com" + Environment.NewLine;
                output += "DURATION:PT1H0M0S" + Environment.NewLine;
                output += "LOCATION:" + String.Format("{0} {1}", m.Location.AddressOne, m.Location.AddressTwo) + Environment.NewLine;
                output += "SUMMARY:" + m.FormattedDescription + Environment.NewLine;
                output += "END:VEVENT" + Environment.NewLine;
            }
            

            output += "END:VCALENDAR" + Environment.NewLine;
            output += "" + Environment.NewLine;
            return new HttpResponseMessage()
            {
                //
                Content = new StringContent(output, System.Text.Encoding.UTF8, "text/calendar")

            };
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage Document(string docID)
        {
            Document d = new DocumentController().GetDocumentByID(Convert.ToInt32(docID));
            Meeting m = new MeetingController().GetMeetingByID(d.MeetingID);

            string result = "<h1>" + m.MeetingGroup.GroupName + "</h1>";
            result += HttpUtility.HtmlDecode(d.Content);
            return new HttpResponseMessage()
            {
                Content = new StringContent(result, System.Text.Encoding.UTF8, "text/html")
            };

        }
    }

    

    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("MeetingDocumentManager", "default", "{controller}/{action}", new[] { "ForsythCo.Modules.MeetingDocumentManager.Services" });
        }
    }
}