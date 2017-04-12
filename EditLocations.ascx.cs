using System;
using DotNetNuke.Entities.Users;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using DotNetNuke.Services.Exceptions;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class EditLocations : MeetingDocumentManagerModuleBase
    {
        private LocationController ltC = new LocationController();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!UserInfo.IsInRole("MeetingAdmin"))
                        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=You do not have permission to create locations.  Please contact your System Administrator.");

                    dataLocations.DataSource = ltC.GetLocations();
                    dataLocations.DataBind();
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            ltC.AddLocation(new Location()
            {
                Latitude = Convert.ToSingle(mdm_hidden_lat.Value),
                Longitude = Convert.ToSingle(mdm_hidden_lon.Value),
                AddressOne = txtAddressOne.Text,
                AddressTwo = txtAddressTwo.Text,
                BuildingName = txtBuilding.Text,
                UserID = UserId,
                SPStatus = 0,
                ErrMsg = ""

            });
            dataLocations.DataSource = ltC.GetLocations();
            dataLocations.DataBind();
        }
    }
}