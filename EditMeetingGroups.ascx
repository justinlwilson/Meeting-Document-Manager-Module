<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditMeetingGroups.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.EditMeetingGroups" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnForm" id="locationpanels" style="width:95%;">
    <h2 class="dnnFormSectionHead"><a href="#">Add/Edit Meeting Group</a></h2>
    <fieldset class="dnnClear">
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblGroupName"></dnn:label>
            <asp:TextBox runat="server" ID="txtGroupName"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtGroupName" CssClass="dnnFormMessage dnnFormError" ResourceKey="GroupName.Required"></asp:RequiredFieldValidator>
        </div>
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblDescription"></dnn:label>
            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="dnnFormItem">
            <asp:HiddenField runat="server" ID="hdnGroupID" />
            <asp:LinkButton runat="server" ID="btnAddGroup" ResourceKey="btnAddGroup" CssClass="dnnPrimaryAction" OnClick="btnAddGroup_Click"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnUpdate" ResourceKey="btnUpdate" CssClass="dnnPrimaryAction" OnClick="btnUpdate_Click" Visible="false"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnCancel" ResourceKey="btnCancel" CssClass="dnnSecondaryAction" OnClick="btnCancel_Click"></asp:LinkButton>
        </div>
    </fieldset>
    <h2 class="dnnFormSectionHead"><a href="#">Existing Meeting Groups</a></h2>
    <fieldset class="dnnClear">
        <asp:DataGrid ID="dgMeetingGroups" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" OnEditCommand="dgMeetingGroups_EditCommand">
            <Columns>
                <asp:BoundColumn DataField="MeetingGroupID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="GroupName" HeaderText="Group Name"></asp:BoundColumn>
                <asp:BoundColumn DataField="ShortDescription" HeaderText="Description"></asp:BoundColumn>
                <asp:EditCommandColumn EditText="Edit"></asp:EditCommandColumn>
            </Columns>
        </asp:DataGrid>
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