<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.View" %>
<%@ Register Src="~/desktopmodules/MeetingDocumentManager/AgendaView.ascx" TagPrefix="uc1" TagName="AgendaView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Label ID="lblJunk" Visible="false" runat="server" />
<asp:Panel runat="server" ID="pnlAgenda">

    <div class="container">
        <asp:Panel runat="server" ID="pnlAdminAddMeeting" Visible="false">
            <asp:HyperLink runat="server" ID="lnkAddMeeting" Text="Add Meeting"></asp:HyperLink>
        </asp:Panel>
        <div style="display:block;"><span style="float:right;">
            <asp:HyperLink runat="server" id="lnkCalendar"><em class="icon icon-calendar"></em> Calendar</asp:HyperLink>
            &nbsp;&nbsp;&nbsp;<asp:HyperLink runat="server" id="lnkRSS"><em class="icon icon-rss"></em> RSS</asp:HyperLink>

                                    </span><div style="clear:both;"></div></div>
        <telerik:RadSkinManager ID="QsfSkinManager" runat="server" ShowChooser="false" />
        <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="true" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" RestoreOriginalRenderDelegate="false">
            <ajaxsettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </ajaxsettings>
        </telerik:RadAjaxManager>
        <telerik:RadGrid runat="server" OnItemDataBound="RadGrid1_DataBound" ID="RadGrid1" Skin="MetroTouch" CssClass="RadGrid_Rounded"
            AutoGenerateColumns="false" AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="true"
            OnNeedDataSource="RadGrid1_NeedDataSource">
            <groupingsettings casesensitive="false"></groupingsettings>
            <mastertableview TableLayout="fixed">
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="Meetings"  FieldName="MonthYearGroup" FormatString="{0}" HeaderValueSeparator=": "></telerik:GridGroupByField>
                       </SelectFields>
                        <GroupByFields>
                        <telerik:GridGroupByField FieldName="MonthYearGroupDT" SortOrder="Descending" ></telerik:GridGroupByField>
                    </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <telerik:GridTemplateColumn Visible="false" AllowFiltering="false" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkAddVideo"><em class="icon icon-vimeo-square"></em>&nbsp;VIDEO</asp:HyperLink><br />
                            <asp:HyperLink runat="server" ID="lnkAddDocs"><em class="icon icon-clipboard">&nbsp;</em>DOCUMENTS</asp:HyperLink><br />
                            <asp:HyperLink runat="server" ID="lnkAlert"><em class="icon icon-exclamation-circle"></em>&nbsp;ALERT</asp:HyperLink><br />
                            <asp:HyperLink runat="server" ID="lnkEditMeeting"><em class="icon icon-edit"></em>&nbsp;EDIT</asp:HyperLink>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DateString" FilterControlWidth="50px" HeaderText="Date" HeaderStyle-Width="60px"
                    SortExpression="DateString" UniqueName="DateString" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <div class="AgendaItemDate">
                                <div style="height:20px;"><%#((DateTime)Eval("Begining")).ToString("MMM")%></div>
                                <div class="Day"><%#((DateTime)Eval("Begining")).ToString("dd")%></div>
                            </div>
                        </ItemTemplate>
                  </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="MeetingGroup.GroupName" HeaderText="Meeting"
                    SortExpression="MeetingGroup.GroupName" UniqueName="MeetingGroup.GroupName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <strong><a href='<%#PortalSettings.ActiveTab.FullUrl %>/Meeting/<%#Eval("MeetingID") %>'><%#Convert.ToString(Eval("VimeoNumber")).Length > 1 ? "<em class='icon icon-youtube-play'></em> ":"" %><%#Eval("MeetingGroup.GroupName") %><%#Convert.ToString(Eval("MeetingGroup.ShortDescription")).Length > 1 ? " <a data-original-title='" + Eval("MeetingGroup.ShortDescription") + "' href='#' rel='tooltip'><em class='icon icon-info-circle'></em></a>" : "" %></a></strong>
                            <%#Convert.ToString(Eval("Flag")).Length > 1 ? "<br/><span class='label label-danger'>" + Eval("Flag") + "</span>" : "" %>
                        </ItemTemplate>
                  </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="MeetingType.TypeName" HeaderText="Type"
                    SortExpression="MeetingType.TypeName" UniqueName="MeetingType.TypeName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                    <ItemTemplate>
                        <strong><a href='<%#PortalSettings.ActiveTab.FullUrl %>/Meeting/<%#Eval("MeetingID") %>'><%#Eval("MeetingType.TypeName") %> @ <%#Eval("TimeString") %></a></strong><br />
                        <asp:Label runat="server" id="lblDocCount" Visible="false">Documents Attached: <%#Eval("Documents.Count") %></asp:Label> 
                    </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Location.BuildingName" HeaderText="Location"
                    SortExpression="Location.BuildingName" UniqueName="Location.BuildingName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                    <ItemTemplate>
                        <strong><%#Eval("Location.BuildingName") %></strong><br />
                        <a target="_blank" href='<%#String.Format("http://maps.google.com/?q={0} {1}", Eval("Location.AddressOne"), Eval("Location.AddressTwo")) %>'><em class="icon icon-map-marker"></em></a> <asp:Label runat="server" Text='<%#Eval("Location.AddressOne") %>'></asp:Label><br />
                            <asp:Label runat="server" Text='<%#Eval("Location.AddressTwo") %>'></asp:Label>
                    </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </mastertableview>
        </telerik:RadGrid>
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
</div>
<asp:Panel runat="server" ID="pnlContent"></asp:Panel>
