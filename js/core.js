var moduleid = null;
var editor = false;

var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
var monthsFull = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
var meetingGroupList = [];
var meetingTypeList = [];
var meetingYearList = [];
var typeFilter = null;
var groupFilter = null;
var monthFilter = null;
var yearFilter = null;
var filtersVis = true;
var page = null;
var host = null;
var pageSize = 20;
var startOffset = 0;
var uri = window.location.origin;

var editAlert;
var editVimeo;
var editDocument;
var editMeeting;

$(function () {
    page = window.location.href;
    host = window.location.hostname;

    console.log('ready');
    moduleid = $('#hdnModID')[0].value;
    editor = $('#hdnIsEditor')[0].value;

    editAlert = $('#hdnEditAlert').val();
    editVimeo = $('#hdnEditVideo').val();
    editDocument = $('#hdnEditDoc').val();
    editMeeting = $('#hdnEditMeeting').val();

    console.log(moduleid);
    LoadMeetingGroups();
    LoadMeetingTypes();
    LoadMeetingYears();
    LoadMeetingData(startOffset, pageSize);

    if ($('#mgm-btn-small-filters').css('display') == 'block') {
        filtersVis = true;
        HideAllFilters(filtersVis);
    }

    $('#mdm-btn-dateFilter').on('click', function () {
        $('#mdm-grp-filers').slideUp();
        $('#mdm-date-filers').slideDown();
    });

    $('#mdm-btn-loadmore').on('click', function () {
        startOffset += pageSize;
        LoadMeetingData(startOffset, pageSize);
    });

    $('#mdm-btn-groupFilter').on('click', function () {
        $('#mdm-grp-filers').slideDown();
        $('#mdm-date-filers').slideUp();
    });

    $('#mgm-btn-small-filters').on('click', function () {
        if (filtersVis) {
            filtersVis = false;
        } else {
            filtersVis = true;
        }
        HideAllFilters(filtersVis);
    })

    $('#mdm-btn-clear').on('click', function () {
        HideMeetingBodies(false);
        ClearDateTypeFilter();
        ClearGroupTypeFilter();
        LoadMeetingData(0, pageSize);
        ShowLoadMore(true);
    });

    $('#btnEditToggle').on('click', function () {
        if ($('#hdnIsEditor')[0].value == "true") {
            $('#hdnIsEditor')[0].value = 'false';
            $('#btnEditToggle').removeClass('btn-primary')
            clearEditMode();
        } else {
            $('#btnEditToggle').addClass('btn-primary');
            $('#mdm-editor')[0].value = 'true';
        }

    });

    $.each(monthsFull, function (k, v) {
        $('#mdm-filter-month').append($('<a></a>').addClass('list-group-item col-md-4 col-sm-4 col-xs-4').attr('id', 'mdm-month-' + v).attr('mid', k + 1).html(v).on('click', SelectMonth));
    });
});

function ShowLoadMore(flag) {
    if (flag) {
        $('#mdm-btn-loadmore').slideDown();
    } else {
        $('#mdm-btn-loadmore').slideUp();
    }
}

function HideAllFilters(flag) {
    if (flag) {
        $('#mdm-filters').slideUp();
    } else {
        $('#mdm-filters').slideDown();
    }
}

function ClearGroupTypeFilter() {
    $('#mdm-filter-group').children().each(function (k, v) {
        if ($(v).hasClass('active')) {
            $(v).removeClass('active');
            groupFilter = null;
        }
    });
    $('#mdm-filter-type').children().each(function (k, v) {
        if ($(v).hasClass('active')) {
            $(v).removeClass('active');
            typeFilter = null;
        }
    });
}

function ClearDateTypeFilter() {
    $('#mdm-filter-month').children().each(function (k, v) {
        if ($(v).hasClass('active')) {
            $(v).removeClass('active');
            monthFilter = null;
        }
    });
    $('#mdm-filter-year').children().each(function (k, v) {
        if ($(v).hasClass('active')) {
            $(v).removeClass('active');
            yearFilter = null;
        }
    });
}

function HideMeetingBodies(flag) {
    $('#mdm-filter-group').children().each(function (k, v) {
        if (!$(v).hasClass('active')) {
            if (flag) {
                $(v).slideUp();
            } else {
                $(v).slideDown();
            }
        }
    });
}

function SelectMeetingGroup() {
    if (monthFilter != null || yearFilter != null) {
        ClearDateTypeFilter();
    }
    ShowLoadMore(false);
    if ($(this).hasClass('active')) {
        $(this).removeClass('active');
        HideMeetingBodies(false);
        groupFilter = null;
    } else {
        groupFilter = this.id.split('-')[2];
        ResetButtons('mdm-filter-group');
        $(this).addClass('active');
        HideMeetingBodies(true);
    }
    LoadGTFilter(groupFilter, typeFilter);
}

function SelectMeetingType() {
    if (monthFilter != null || yearFilter != null) {
        ClearDateTypeFilter();
    }
    ShowLoadMore(false);
    if ($(this).hasClass('active')) {
        $(this).removeClass('active');
        typeFilter = null;
    } else {
        typeFilter = this.id.split('-')[2];
        ResetButtons('mdm-filter-type');
        $(this).addClass('active');
    }
    LoadGTFilter(groupFilter, typeFilter);
}

function SelectMonth() {
    if (groupFilter != null || typeFilter != null) {
        ClearGroupTypeFilter();
    }
    ShowLoadMore(false);
    if ($(this).hasClass('active')) {
        $(this).removeClass('active');
        monthFilter = null;
    } else {
        monthFilter = $(this).attr('mid');
        ResetButtons('mdm-filter-month');
        $(this).addClass('active');
    }
    LoadDateFilter(monthFilter, yearFilter);
}

function SelectYear() {
    if (groupFilter != null || typeFilter != null) {
        ClearGroupTypeFilter();
    }
    ShowLoadMore(false);
    if ($(this).hasClass('active')) {
        $(this).removeClass('active');
        yearFilter = null;
    } else {
        yearFilter = $(this).html();
        ResetButtons('mdm-filter-year');
        $(this).addClass('active');
    }
    LoadDateFilter(monthFilter, yearFilter);
}

function ShowAllMeetings() {
    $('#mdm-itemlist > .row').each(function (k, v) {
        if ($(v).css('display') != 'block') {
            $(v).slideDown();
        }
    });
}

function ResetButtons(id) {
    $('#' + id).children().each(function (k, v) {
        if ($(v).hasClass('active')) {
            $(v).removeClass('active');
        }
    });
}

function LoadGTFilter(g, t) {
    if (g == null && t == null) {
        LoadMeetingData(0, pageSize);
    } else {
        if (g == null) {
            g = '';
        }
        if (t == null) {
            t = '';
        }
        $.getJSON(uri + '/desktopmodules/meetingdocumentmanager/api/meetingapi/getmeetingsbygrouptype?group=' + g + '&type=' + t, function (data) {
            DrawMeetingRecords(data, true);
        });
    }
}

function LoadDateFilter(m, y) {
    if (m == null && y == null) {
        LoadMeetingData(0, pageSize);
    } else {
        if (m == null) {
            m = '';
        }
        if (y == null) {
            y = '';
        }
        $.getJSON(uri + '/desktopmodules/meetingdocumentmanager/api/meetingapi/getmeetingsbydate?month=' + m + '&year=' + y, function (data) {
            DrawMeetingRecords(data, true);
        });
    }
}

function DrawMeetingRecords(data, isNew) {
    if (isNew) {
        ClearMeetingRecords();
    }
    var c = 0;
    var dateNow = Date.now();
    $.each(data, function (key, value) {
        var m = (c / 2) % 1;
        var editRow = $('<div></div>').attr('id', 'mtg_' + value.MeetingID + '_edit').addClass('mdm-edit-collapse bg-success');

        $(editRow).append($('<a></a>').attr('href', editAlert.replace('%2520', value.MeetingID)).addClass('btn').html('<span class="icon icon-exclamation-circle"></span> Alert'));
        $(editRow).append($('<a></a>').attr('href', editVimeo.replace('%2520', value.MeetingID)).addClass('btn').html('<span class="icon icon-vimeo-square"></span> Vimeo'));
        $(editRow).append($('<a></a>').attr('href', editDocument.replace('%2520', value.MeetingID)).addClass('btn').html('<span class="icon icon-clipboard"></span> Documents'));
        $(editRow).append($('<a></a>').attr('href', editMeeting.replace('%2520', value.MeetingID)).addClass('btn').html('<span class="icon icon-edit"></span> Edit'));
        $(editRow).append($('<a></a>').attr('href', window.location.href + '/Meeting/' + value.MeetingID).addClass('btn pull-right').html('View Meeting Page <span class="icon icon-chevron-right"></span> '));
        var meetingDate = new Date(value.Begining);
        var month = monthsFull[meetingDate.getMonth()];
        var row = $('<div></div>').attr('id', 'mtg_' + value.MeetingID).addClass('row').attr('group-id', value.MeetingGroupID).attr('type-id', value.MeetingTypeID).attr('month-name', meetingDate.getMonth()).attr('mtg-year', meetingDate.getFullYear());



        if (m == 0) {
            $(row).addClass('other');
        }

        var dateItem = $('<div></div>').addClass('text-center mdm-date').append($('<div></div>').addClass('mdm-month').html(month[0] + month[1] + month[2] + ' ' + meetingDate.getDate())).append($('<div></div>').addClass('mdm-day').html(meetingDate.getFullYear()));
        var dateCol = $('<div></div>').addClass('col-md-2 col-sm-4 col-xs-4').append(dateItem);

        if (meetingDate >= dateNow) {
            $(dateItem).addClass('mdm-date-upcoming');
        }

        var infoCol = $('<div></div>').addClass('col-md-6 col-xs-8 mdm-board');

        $(infoCol).html(' ' + value.MeetingGroup.name + '<br/>' + value.MeetingType.name + ' @ ' + value.TimeString);

        if (value.VimeoNumber != null || value.VimeoNumber != '') {
            $(infoCol).prepend($('<em></em>').addClass('icon icon-youtube-play').html(value.Flag));
        }

        if (value.Flag != null) {
            $(infoCol).prepend($('<span></span>').addClass('label label-danger').html(value.Flag));
        }

        var addrCol = $('<div></div>').addClass('col-md-4 mdm-address hidden-sm hidden-xs').append($('<address></address>').html('<strong>' + value.Location.BuildingName + '</strong><br/>' + value.Location.AddressOne + '<br/>' + value.Location.AddressTwo));

        $(row).on('click', function () {
            if ($('#hdnIsEditor')[0].value == "true") {
                clearEditMode();
                $('#' + this.id + '_edit').slideDown(200);
            } else {
                //alert('Item Selected');
                window.location = window.location.href + '/Meeting/' + this.id.split('_')[1];
            }
        });

        $(".panel-body").append($(row).append(dateCol).append(infoCol).append(addrCol));
        $(".panel-body").append($(editRow));
        c += 1;

    });
}

function ClearMeetingRecords() {
    $('#mdm-itemlist').empty();
}

function LoadMeetingData(o, ps) {
    $.getJSON(uri + '/desktopmodules/meetingdocumentmanager/api/meetingapi/getmeetingsbyoffset?offset=' + o + '&pagesize=' + ps, function (data) {
        DrawMeetingRecords(data, (o == 0));
    });
}

function LoadMeetingGroups() {
    $.getJSON(uri + '/desktopmodules/meetingdocumentmanager/api/meetingapi/getmeetinggroupsjson', function (data) {
        $.each(data, function (k, v) {
            $('#mdm-filter-group').append($('<a></a>').addClass('list-group-item').attr('id', 'mdm-group-' + v.id).html(v.name).on('click', SelectMeetingGroup));
        });
    });
}

function LoadMeetingTypes() {
    $.getJSON(uri + '/desktopmodules/meetingdocumentmanager/api/meetingapi/getmeetingtypesjson', function (data) {
        $.each(data, function (k, v) {
            $('#mdm-filter-type').append($('<a></a>').addClass('list-group-item').attr('id', 'mdm-type-' + v.id).html(v.name).on('click', SelectMeetingType));
        });
    });
}

function LoadMeetingYears() {
    $.getJSON(uri + '/desktopmodules/meetingdocumentmanager/api/meetingapi/getmeetingyears', function (data) {
        $.each(data, function (k, v) {
            $('#mdm-filter-year').append($('<a></a>').addClass('list-group-item').attr('id', 'mdm-year-' + v.year).html(v.year).on('click', SelectYear));
        });
    });
}

function clearEditMode() {
    $('.mdm-edit-collapse').each(function (k, v) {
        if ($(v).css('display') == 'block') {
            $(v).slideUp(200);
        }
    });
}
