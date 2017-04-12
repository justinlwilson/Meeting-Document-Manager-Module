<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditMeetingTypes.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.EditMeetingTypes" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnForm" id="panels" style="width:95%;">
    <h2 class="dnnFormSectionHead"><a href="#">Add Meeting Type</a></h2>
    <fieldset class="dnnClear">
        <div>
            <div class="dnnFormItem">
                <dnn:label runat="server" ID="lblTypeName"></dnn:label>
                <asp:TextBox runat="server" ID="txtTypeName" CssClass="dnnFormRequired"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtTypeName" ResourceKey="TypeName.Required" />
            </div>
            <div class="dnnFormItem">
                <dnn:label runat="server" ID="lblDescription"></dnn:label>
                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="dnnFormItem">
            <asp:HiddenField runat="server" ID="hdnItemID" />
            <asp:LinkButton ID="btnAddType" OnClick="btnAddType_Click" resourcekey="btnAddType" runat="server" CssClass="dnnPrimaryAction" />
            <asp:LinkButton ID="btnUpdate" OnClick="btnUpdate_Click" resourcekey="btnUpdate" runat="server" CssClass="dnnPrimaryAction" Visible="false" />
            <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" resourcekey="btnCancel" runat="server" CssClass="dnnSecondaryAction" />
        </div>
        </div>
    </fieldset>
    <h2 class="dnnFormSectionHead"><a href="#">Edit Meeting Type</a></h2>
    <fieldset class="dnnClear">
        <div>
            <asp:DataGrid runat="server" ID="dgMeetingTypes" AutoGenerateColumns="false" CssClass="table table-striped" OnEditCommand="dgMeetingTypes_EditCommand">
                <Columns>
                    <asp:BoundColumn DataField="MeetingTypeID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TypeName" HeaderText="Type Name"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ShortDescription" HeaderText="Description"></asp:BoundColumn>
                    <asp:EditCommandColumn EditText="Edit"></asp:EditCommandColumn>
                </Columns>
            </asp:DataGrid>
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