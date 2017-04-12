<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeetingItem.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.MeetingItem" %>
<%@ Register Src="~/desktopmodules/MeetingDocumentManager/VideoDisplay.ascx" TagPrefix="uc1" TagName="VideoDisplay" %>
<%@ Register Src="~/desktopmodules/MeetingDocumentManager/DocumentDisplay.ascx" TagPrefix="uc1" TagName="DocumentDisplay" %>
<%@ Register Src="~/desktopmodules/MeetingDocumentManager/GoogleMap.ascx" TagPrefix="uc1" TagName="GoogleMap" %>

<div style="display: block;">
    <asp:Label runat="server" ID="lblCal" Visible="false"></asp:Label> <asp:Label runat="server" ID="lblMeetingDate"></asp:Label></div>
<h2><asp:Label CssClass="TitleH2" runat="server" ID="lblMeetingHead"></asp:Label></h2>
<div class="row">
<div class="col-md-3">
    <uc1:GoogleMap runat="server" id="GoogleMap" />
</div>
<div class="col-md-9">
    <asp:Label ID="lblCancelled" runat="server" Visible="false"></asp:Label>
    <uc1:VideoDisplay runat="server" id="VideoDisplay" />
    <uc1:DocumentDisplay runat="server" id="DocumentDisplay" />
</div>
</div>

