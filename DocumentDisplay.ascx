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
                            <asp:HyperLink runat="server" ID="lnkEdit" Visible="false"><em class="icon icon-pencil-square-o"></em> edit</asp:HyperLink> <div class="text-right"><asp:HyperLink runat="server" ID="HyperLink1" Visible="false"><button type="button" class="button button-warning"><em class="icon icon-pencil-square-o"></em> Edit</button></asp:HyperLink><a target="_blank"  href="/DesktopModules/MeetingDocumentManager/api/meetingapi/Document?docID=<%#DataBinder.Eval(Container.DataItem, "DocumentID").ToString()%>"><button type="button" class="btn btn-success"><em class="icon icon-print"></em> Print</button></a></div><%# Server.HtmlDecode(DataBinder.Eval(Container.DataItem, "Content").ToString()) %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
