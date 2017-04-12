<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditDocumentTypes.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.EditDocumentTypes" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnForm" id="panels" style="width:95%;">
    <h2 class="dnnFormSectionHead">Add Document Type</h2>
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
            <asp:LinkButton runat="server" ID="btnAddGroup" ResourceKey="btnAddGroup" CssClass="dnnPrimaryAction" OnClick="btnAddGroup_Click"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnCancel" ResourceKey="btnCancel" CssClass="dnnSecondaryAction" OnClick="btnCancel_Click"></asp:LinkButton>
        </div>
    </fieldset>
    <h2 class="dnnFormSectionHead">Existing Documents</h2>
    <fieldset class="dnnFormItem">
        <div>
            <asp:DataList runat="server" ID="lstDocGroups">
                <HeaderTemplate>
                    <ol>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><em class="icon icon-arrow-circle-o-right"></em> <%#Eval("GroupName") %>: <%#Eval("ShortDescription") %></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ol>
                </FooterTemplate>
            </asp:DataList>
        </div>
    </fieldset>
</div>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        jQuery.RequestDnnPluginsRegistration();
    }
</script>