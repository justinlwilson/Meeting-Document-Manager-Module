<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditLocations.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.EditLocations" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnForm" id="locationpanels" style="width:95%;">
    <h2 class="dnnFormSectionHead"><a href="#">Add New Location</a></h2>
    <fieldset class="dnnClear">
        <div>
            <div class="dnnFormItem dnnFormHelp dnnClear">
                <p class="dnnFormRequired"><asp:Label runat="server" ResourceKey="Required Indicator" /></p>
            </div>
            <div class="dnnFormItem">
                <dnn:label runat="server" ID="lblBuilding"></dnn:label>
                <asp:TextBox runat="server" ID="txtBuilding" CssClass="dnnFormRequired"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtBuilding" ResourceKey="Building.Required"/>
            </div>
            <div class="dnnFormItem">
                <dnn:label runat="server" ID="lblAddressOne"></dnn:label>
                <asp:TextBox runat="server" ID="txtAddressOne"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtAddressOne" ResourceKey="Address.Required"/>
            </div>
            <div class="dnnFormItem">
                <dnn:label runat="server" ID="lblAddressTwo"></dnn:label>
                <asp:TextBox runat="server" ID="txtAddressTwo"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtAddressTwo" ResourceKey="Address.Required"/>
            </div>
            <div class="dnnFormItem">
                <dnn:label runat="server" ID="lblLocation"></dnn:label>
                <div id="map-canvas" style="width:500px;height:320px;"/>
            </div>
            <div class="dnnFormItem">
                &nbsp;
            </div>
            <div class="dnnFormItem">
                <dnn:label runat="server" ID="lblXY"></dnn:label>
                <asp:TextBox runat="server" Enabled="false" ID="mdm_txtLocation" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="mdm_txtLocation" ResourceKey="Location.Required"/>
                <asp:HiddenField ClientIDMode="Static" ID="mdm_hidden_lat" Value="" runat="server" />
                <asp:HiddenField ClientIDMode="Static" ID="mdm_hidden_lon" Value="" runat="server" />
            </div>
            <div class="dnnFormItem">
                <asp:Button runat="server" CssClass="dnnRight" ID="btnAddLocation" resourceKey="btnAddLocation" OnClick="btnAddLocation_Click" />
            </div>
        </div>
    </fieldset>
    <h2 class="dnnFormSectionHead"><a href="#">Current Locations</a></h2>
    <fieldset class="dnnClear">
        <div>
            <asp:DataGrid runat="server" ID="dataLocations" AutoGenerateColumns="false" CssClass="table table-striped">
                <headerstyle cssclass="" verticalalign="Top"/>
                <itemstyle cssclass="" horizontalalign="Left" />
                <alternatingitemstyle cssclass="" />
                <footerstyle cssclass="" />
                <pagerstyle cssclass="" />
                <Columns>
                    <asp:BoundColumn DataField="LocationID" HeaderText="LocationID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="BuildingName" HeaderText="Building Name"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Address">
                        <ItemTemplate>
                            <a target="_blank" href='<%#String.Format("http://maps.google.com/?q={0} {1}", Eval("AddressOne"), Eval("AddressTwo")) %>'><em class="icon icon-map-marker"></em></a> <asp:Label runat="server" Text='<%#Eval("AddressOne") %>'></asp:Label><br />
                            <asp:Label runat="server" Text='<%#Eval("AddressTwo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Map">
                        <ItemTemplate>
                            <asp:Image runat="server" CssClass="img-thumbnail" ImageUrl='<%#String.Format("http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom=13&size=200x200&maptype=roadmap&markers=color:red%7C{0},{1}&sensor=false", Eval("Latitude"), Eval("Longitude"))%>' />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </fieldset>
</div>
<script type="text/javascript">

    (function ($, Sys) {
        function dnnEditBasicSettings() {
            $('#locationpanels').dnnPanels();
        }


        $(document).ready(function () {

            var defaultLocation = new google.maps.LatLng(34.206865, -84.139626);

            var mapOptions = {
                center: defaultLocation,
                zoom: 10
            };

            var map = new google.maps.Map(document.getElementById("map-canvas"),
                mapOptions);

            var mdm_myMarker = new google.maps.Marker({
                position: null,
                map: map
            });

            google.maps.event.addListener(map, 'click', function (event) {
                mdm_myMarker.setPosition(event.latLng);
                $('#mdm_hidden_lat').attr('value', event.latLng.lat());
                $('#mdm_hidden_lon').attr('value', event.latLng.lng());
                $('#mdm_txtLocation').val('Lat: ' + event.latLng.lat() + ' Lon: ' + event.latLng.lng());
            });

            dnnEditBasicSettings();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                dnnEditBasicSettings();
            });
        });

    }(jQuery, window.Sys));
</script>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        jQuery.RequestDnnPluginsRegistration();
    }
</script>