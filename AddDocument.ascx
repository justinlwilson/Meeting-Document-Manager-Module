<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDocument.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.AddDocument" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<%@ Register Src="~/desktopmodules/MeetingDocumentManager/MeetingQuickSelect.ascx" TagPrefix="dnn" TagName="MeetingQuickSelect" %>

<div class="dnnForm" id="locationpanels" style="width:95%;">
    <h2 class="dnnFormSectionHead"><a href="#">Add a New Meeting Document</a></h2>
    <fieldset class="dnnClear">
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblDocumentType"></dnn:label>
            <asp:DropDownList runat="server" ID="ddDocumentType" DataTextField="GroupName" DataValueField="DocumentGroupID"></asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblContent"></dnn:label>
            <dnn:TextEditor ID="txtContent" runat="server" Height="500" Width="100%" />
        </div>
        <div class="dnnFormItem">
            <asp:LinkButton runat="server" ID="btnAddDocument" ResourceKey="btnAddDocument" OnClick="btnAddDocument_Click" CssClass="dnnPrimaryAction"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnCancel" ResourceKey="Cancel" OnClick="btnCancel_Click" CssClass="dnnSecondaryAction"></asp:LinkButton>
        </div>
    </fieldset>
</div>