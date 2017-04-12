<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoDisplay.ascx.cs" Inherits="ForsythCo.Modules.MeetingDocumentManager.VideoDisplay" %>
<div id="MDM_Video_Section" class="MDM_Section">
	<h3>Meeting Video</h3>
	<div id="MediaArea">
		<div id="MDM_Video" class="MDM_video">
            <asp:Repeater runat="server" ID="vimeo">
                <ItemTemplate>
			        <iframe allowfullscreen="" frameborder="0" height="281" width="500" mozallowfullscreen="" src='http://player.vimeo.com/video/<%#DataBinder.Eval(Container.DataItem, "VimeoSrc") %>?api=1' webkitallowfullscreen=""></iframe>
                </ItemTemplate>
            </asp:Repeater>
		</div>
		<div id="MDM_MarkerTitle" class="markertitle">&nbsp;&nbsp;<asp:Label runat="server" ID="lblBookmarks" Visible="false"><em class="icon icon-list"></em> BOOKMARKS</asp:Label></div>
		<div id="MarkerGroup" class="marker">
            <asp:Repeater runat="server" ID="rptMarkers">
                <ItemTemplate>
                    <div id="Keyframe" class="keyframe" time="<%#DataBinder.Eval(Container.DataItem, "KeyTime") %>">
				        <div class="time"><%#DataBinder.Eval(Container.DataItem, "TimeString") %></div>
				        <div class="title"><%#DataBinder.Eval(Container.DataItem, "ShortDescription") %></div>
			        </div>
                </ItemTemplate>
            </asp:Repeater>
		</div>
	</div>
</div>
<div style="display: block; clear: both; height: 20px;"></div>
<script type="text/javascript">

    var f;

    (function ($) {
        $(window).load(function () {
            $("#MarkerGroup").mCustomScrollbar({
                horizontalScroll: false,
                theme: "dark"
            });
            $('.keyframe').click(function () {
                console.log($(this).attr('time'));
            });
            $('.keyframe').mouseover(function () {
                $(this).addClass('keyframe_on');
            });
            $('.keyframe').mouseleave(function () {
                $(this).removeClass('keyframe_on');
            });

            $('[id="Keyframe"]').each(function (index) {
                $(this).bind("click", function () {
                    seekTo($(this).attr('time'));
                });
            });
            f = $('iframe'),
          url = f.attr('src').split('?')[0],
          status = $('.status');
        });
    })(jQuery, window.Sys);



    function seekTo(seconds) {
        var data = {
            method: 'seekTo'
        };
        data.value = seconds;
        f[0].contentWindow.postMessage(JSON.stringify(data), url);

        post('play');
    }


</script>