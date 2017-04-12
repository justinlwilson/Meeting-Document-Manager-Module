<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgendaItem.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.AgendaItem" %>
<div class="AgendaItemControl">
    <div class="AgendaItemDate">
        <div class="Month"><asp:Label runat="server" ID="lblMeetingMonth"></asp:Label></div>
        <div class="Day"><asp:Label runat="server" ID="lblMeetingDay"></asp:Label></div>
    </div>
    &nbsp;<asp:HyperLink runat="server" ID="lnkMeetingGroup"></asp:HyperLink><br />
    &nbsp;<span class="BottomLine"><asp:Label runat="server" ID="lblMeetingType"></asp:Label> @ <asp:Label runat="server" ID="lblMeetingTime"></asp:Label></span>
</div>