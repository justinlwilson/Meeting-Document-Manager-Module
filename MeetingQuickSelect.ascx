<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeetingQuickSelect.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.MeetingQuickSelect" %>
<div>
    <div style="float:left;">
        <asp:Calendar ID="cal" runat="server" OnSelectionChanged="cal_SelectionChanged"></asp:Calendar>
    </div>
    <div>
        <asp:RadioButtonList runat="server" ID="rblMeetings" OnSelectedIndexChanged="rblMeetings_SelectedIndexChanged" DataTextField="FormattedDescription" DataValueField="MeetingID"></asp:RadioButtonList>
    </div>
    <div class="dnnClear"></div>
</div>
