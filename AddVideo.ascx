<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddVideo.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.AddVideo" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="dnnForm" id="panels" style="width:95%;">
    <h2 class="dnnFormSectionHead"><a href="#">Add Video</a></h2>
    <fieldset class="dnnClear">
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblVimeo"></dnn:label>
            <asp:TextBox runat="server" ID="txtVimeo"></asp:TextBox>
        </div>
        <div class="dnnFormItem">
            <asp:LinkButton runat="server" ID="btnAddVideo" ResourceKey="btnAddVideo" OnClick="btnAddVideo_Click" CssClass="dnnPrimaryAction"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnCancel" ResourceKey="Cancel" OnClick="btnCancel_Click" CssClass="dnnSecondaryAction"></asp:LinkButton>
        </div>
    </fieldset>
    <h2 class="dnnFormSectionHead"><a href="#">Manage Bookmarks</a></h2>
    <fieldset>
        <div class="dnnFormItem">
            <dnn:label runat="server" id="lblBookmarks"></dnn:label>
            H:M:S - 
            <asp:TextBox runat="server" ID="txtHour" Text="0" Width="13"></asp:TextBox>:
            <asp:TextBox runat="server" ID="txtMin" Text="0" MaxLength="2" Width="13"></asp:TextBox>:
            <asp:TextBox runat="server" ID="txtSec" Text="0" MaxLength="2" Width="13"></asp:TextBox>
            Title: <asp:TextBox runat="server" ID="txtTitle" MaxLength="150" Width="190"></asp:TextBox>
            <asp:LinkButton runat="server" ID="btnAddBookmark" OnClick="btnAddBookmark_Click"><em class="icon icon-plus-square" style="font-size:25px;"></em></asp:LinkButton>
        </div>
        <div class="dnnFormItem">
            <telerik:RadGrid runat="server" ID="grdBookmarks" Skin="MetroTouch" AutoGenerateColumns="false" ShowHeader="false"
                OnNeedDataSource="RadGrid_NeedData" OnDeleteCommand="RadGrid1_DeleteCommand">
                <mastertableview EditMode="InPlace" DataKeyNames="MarkerID">
                    <Columns>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <%#Eval("TimeString") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <%#Eval("ShortDescription") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" />
                </Columns>
                </mastertableview>
            </telerik:RadGrid>
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