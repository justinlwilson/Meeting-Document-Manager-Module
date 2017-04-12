<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddAlert.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.AddAlert" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm" id="locationpanels" style="width:95%;">
    <h2 class="dnnFormSectionHead"><a href="#">Add a New Meeting Document</a></h2>
    <fieldset class="dnnClear">
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblAlertText"></dnn:label>
            <asp:DropDownList runat="server" ID="ddAlerts" DataTextField="Caption" DataValueField="Caption">
            </asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <asp:LinkButton runat="server" ID="btnClear" Visible="false" ResourceKey="btnClear" OnClick="btnClear_Click" CssClass="dnnPrimaryAction"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnAddAlert" ResourceKey="btnAddAlert" OnClick="btnAddAlert_Click" CssClass="dnnPrimaryAction"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnCancel" ResourceKey="Cancel" OnClick="btnCancel_Click" CssClass="dnnSecondaryAction"></asp:LinkButton>
        </div>
    </fieldset>
</div>