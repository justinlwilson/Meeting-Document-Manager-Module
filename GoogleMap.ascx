<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.GoogleMap" %>
<asp:Repeater runat="server" ID="rptMap">
    <ItemTemplate>
        <div class="col-md-12">
            <h3>Location</h3>
            <div class="team-item thumbnail">
                <a target="_blank" href='<%#String.Format("http://maps.google.com/?q={0} {1}", DataBinder.Eval(Container.DataItem, "AddressOne"), DataBinder.Eval(Container.DataItem, "AddressTwo")) %>' class="thumb-info team">
                    <asp:Image runat="server" CssClass="img-responsive" ImageUrl='<%#String.Format("http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom=13&size=300x300&maptype=roadmap&markers=color:red%7C{0},{1}&sensor=false", DataBinder.Eval(Container.DataItem, "Latitude"), DataBinder.Eval(Container.DataItem, "Longitude"))%>' />
                </a>
                <span class="thumb-info-caption">
                    <p>
                    <em class="icon icon-map-marker"></em> <asp:Label runat="server" Font-Bold="true" ID="Label1" Text='<%#DataBinder.Eval(Container.DataItem, "BuildingName") %>'></asp:Label><br />
                    <a target="_blank" href='<%#String.Format("http://maps.google.com/?q={0} {1}", DataBinder.Eval(Container.DataItem, "AddressOne"), DataBinder.Eval(Container.DataItem, "AddressTwo")) %>'><asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AddressOne") %>'></asp:Label><br />
                    <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AddressTwo") %>'></asp:Label></a></p>
                </span>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
