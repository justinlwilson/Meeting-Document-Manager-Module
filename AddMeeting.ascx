<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeeting.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.AddMeeting" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnForm" id="locationpanels" style="width:95%;">
    <h2 class="dnnFormSectionHead"><a href="#">Add a New Meeting</a></h2>
    <fieldset class="dnnClear">
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblMeetingGroup"></dnn:label>
            <asp:DropDownList runat="server" ID="ddMeetingGroup" DataTextField="GroupName" DataValueField="MeetingGroupID"></asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblMeetingType"></dnn:label>
            <asp:DropDownList runat="server" ID="ddMeetingType" DataTextField="TypeName" DataValueField="MeetingTypeID"></asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblLocation"></dnn:label>
            <asp:DropDownList runat="server" ID="ddLocation" DataTextField="BuildingName" DataValueField="LocationID"></asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblDate"></dnn:label>
            <asp:textbox runat="server" ID="datePicker" CssClass="dnnFormInput"></asp:textbox>
        </div>
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblTime"></dnn:label>
            <asp:textbox runat="server" ID="txtHour" CssClass="dnnFormInput" MaxLength="2" Width="20"></asp:textbox> : <asp:textbox runat="server" ID="txtMin" CssClass="dnnFormInput" Width="20" MaxLength="2"></asp:textbox>
            <asp:DropDownList runat="server" ID="ddAMPM" Width="60">
                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <asp:LinkButton runat="server" ID="btnAddMeeting" ResourceKey="btnAddMeeting" OnClick="btnAddMeeting_Click" CssClass="dnnPrimaryAction"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnCancel" ResourceKey="Cancel" OnClick="btnCancel_Click" CssClass="dnnSecondaryAction"></asp:LinkButton>
        </div>
    </fieldset>
</div>
<script type="text/javascript">
    (function ($, Sys) {
        function dnnPanels() {
            $('#panels').dnnPanels();
        }
        $(document).ready(function () {
            dnnPanels();
        });
    }(jQuery, window.Sys));
</script>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        jQuery.RequestDnnPluginsRegistration();
    }
</script>