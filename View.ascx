<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.View" %>
<%@ Register Src="~/desktopmodules/MeetingDocumentManager/AgendaView.ascx" TagPrefix="uc1" TagName="AgendaView" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/MeetingDocumentManager/js/core.js" />
<asp:Label ID="lblJunk" Visible="false" runat="server" />
<asp:Panel runat="server" ID="pnlAgenda">

    <div class="container">
        <asp:Panel runat="server" ID="pnlAdminAddMeeting" Visible="false">
            <asp:HyperLink runat="server" ID="lnkAddMeeting" CssClass="btn btn-primary btn-lg" Text="Add Meeting"></asp:HyperLink>
        </asp:Panel>
        <div style="display:block;"><span style="float:right;">
            <asp:HyperLink runat="server" id="lnkCalendar"><em class="icon icon-calendar"></em> Calendar</asp:HyperLink>
            &nbsp;&nbsp;&nbsp;<asp:HyperLink runat="server" id="lnkRSS"><em class="icon icon-rss"></em> RSS</asp:HyperLink>

                                    </span><div style="clear:both;"></div></div>

        <div class="col-md-12">
            <a id="mgm-btn-small-filters" class="btn btn-default btn-lg btn-block visible-xs visible-sm"><span class="glyphicon glyphicon-sort
"></span> Show/Hide Filters</a><br/>
						<div class="row">
							<div id="mdm-filters" class="col-md-4">
								<div class="btn-group btn-group-justified" role="group" aria-label="...">
									<a id="mdm-btn-dateFilter" type="button" class="btn btn-default btn-lg"><span class="icon icon-filter"></span> Date</a>
									<a id="mdm-btn-groupFilter" type="button" class="btn btn-default btn-lg"><span class="icon icon-filter"></span> Group</a>
									<a id="mdm-btn-clear" type="button" class="btn btn-default btn-lg"><span class="icon icon-filter"></span> Clear</a>
									</div><hr />
								<div id="mdm-date-filers" class="">
									<h4>Month</h4>
									<div id="mdm-filter-month" class="list-group col-md-12">
									</div>
									<h4>Year</h4>
									<div id="mdm-filter-year" class="list-group col-md-12">
									</div></div>
								<div id="mdm-grp-filers" class="collapse">
									<h4>Meeting Body</h4>
									<div id="mdm-filter-group" class="list-group col-md-12">
									</div>
									<h4>Meeting Type</h4>
									<div id="mdm-filter-type" class="list-group col-md-12">
									</div>
								</div>
							</div>
							<div class="col-md-8">
								<div class="panel panel-default">
									<div class="panel-heading">
										<h3 class="panel-title">Forsyth County Meetings</h3>
									</div>
									<div id="mdm-itemlist" class="panel-body">

									</div>
								</div>
								<a id="mdm-btn-loadmore" class="btn btn-default btn-lg btn-block">Load More...</a>
							</div>
						</div>
        </div>
        <hr />
    </div>
</asp:Panel>

<div style="margin-top: 20px;">
    <asp:Repeater ID="rptItemList" Visible="false" runat="server" OnItemDataBound="rptItemListOnItemDataBound" OnItemCommand="rptItemListOnItemCommand">
        <HeaderTemplate>
        </HeaderTemplate>

        <ItemTemplate>
            <div class="toggle">
                <section class="toggle active">
                    <label>
                        <asp:Label ID="lblitemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GroupName").ToString() %>' /></label>
                    <div class="toggle-content">
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortDescription").ToString() %>' />

                        <ul class="simple-post-list">

                            <asp:Repeater runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "MeetingList") %>'>
                                <ItemTemplate>
                                    <li>
                                        <div class="post-info">
                                            <%#DataBinder.Eval(Container.DataItem, "MeetingType.TypeName").ToString() %>: <%#DataBinder.Eval(Container.DataItem, "Begining", "{0:d}").ToString() %>
                                            <div class="post-meta"><%#DataBinder.Eval(Container.DataItem, "Begining", "{0:t}").ToString() %></div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </section>
            </div>
            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                <asp:HyperLink ID="lnkEdit" runat="server" ResourceKey="EditItem.Text" Visible="false" Enabled="false" />
                <asp:LinkButton ID="lnkDelete" runat="server" ResourceKey="DeleteItem.Text" Visible="false" Enabled="false" CommandName="Delete" />
            </asp:Panel>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
    <asp:HiddenField ID="hdnEditVideo" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnEditAlert" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnEditDoc" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnEditMeeting" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnModID" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnIsEditor" ClientIDMode="Static" runat="server" Value="false" />
</div>
<asp:Panel runat="server" ID="pnlContent"></asp:Panel>
