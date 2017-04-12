<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgendaView.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.AgendaView" %>
<%@ Register Src="~/desktopmodules/MeetingDocumentManager/AgendaItem.ascx" TagPrefix="uc1" TagName="AgendaItem" %>

    <!--div class="MDM_Calendar_Content">
        <asp:Calendar ClientIDMode="Static" runat="server" CssClass="MDM_Calendar" ID="calMDMMeetings" OnVisibleMonthChanged="calSelect_VisibleMonthChanged" SelectionMode="None">
            <SelectedDayStyle CssClass="MDM_Calendar_Highlight" BackColor="#0096FF" />
        </asp:Calendar>
    </!--div-->
    <div class="MDM_AgendaView_Content">
        <h2><asp:Label runat="server" ID="lblAgendaTitle" CssClass="TitleH2">Upcoming Meetings</asp:Label></h2>
        <asp:Repeater runat="server" ID="rptMeetings" OnItemCreated="rptMeetings_ItemCreated" OnItemDataBound="rptMeetings_ItemDataBound">
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate>
                <uc1:AgendaItem runat="server" id="AgendaItem" 
                    MeetingType='<%#DataBinder.Eval(Container.DataItem, "MeetingType.TypeName") %>' 
                    MeetingGroup='<%#DataBinder.Eval(Container.DataItem, "MeetingGroup.GroupName") %>'
                    MeetingDay='<%#DataBinder.Eval(Container.DataItem, "Begining", "{0:dd}") %>'
                    MeetingMonth='<%#DataBinder.Eval(Container.DataItem, "Begining", "{0:MMM}") %>' 
                    MeetingTime='<%#DataBinder.Eval(Container.DataItem, "Begining", "{0:t}") %>'
                    MeetingID='<%#DataBinder.Eval(Container.DataItem, "MeetingID") %>'/>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
            <FooterTemplate></FooterTemplate>
        </asp:Repeater>    
    </div>

