<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentDisplay.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.DocumentDisplay" %>

<div id="MDM_Document_Section" class="MDM_Section">
    <h3>Documents</h3>
    <div>
        <asp:Label runat="server" ID="lblAddDoc"></asp:Label>
        <div class="tabs">
	        <ul class="nav nav-tabs">
                <asp:Repeater runat="server" ID="Repeater1">
                <ItemTemplate>
                    <li class="<%#DataBinder.Eval(Container.DataItem, "DocumentGroup.GroupName").ToString()=="Public Notice"?"active":"" %>">
                        <a data-toggle="tab" href="#<%#DataBinder.Eval(Container.DataItem, "DocumentGroup.GroupName").ToString().Replace(" ","") %>">
                             <em class="icon icon-file"></em> <%#DataBinder.Eval(Container.DataItem, "DocumentGroup.GroupName") %> 
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
            <div class="tab-content">
                <asp:Repeater runat="server" ID="rptDocuments" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <div id="<%#DataBinder.Eval(Container.DataItem, "DocumentGroup.GroupName").ToString().Replace(" ","") %>" class="tab-pane <%#DataBinder.Eval(Container.DataItem, "DocumentGroup.GroupName").ToString()=="Public Notice"?"active":"" %>">
                            <asp:HyperLink runat="server" ID="lnkEdit" Visible="false"><em class="icon icon-pencil-square-o"></em> edit</asp:HyperLink> <%# Server.HtmlDecode(DataBinder.Eval(Container.DataItem, "Content").ToString()) %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
