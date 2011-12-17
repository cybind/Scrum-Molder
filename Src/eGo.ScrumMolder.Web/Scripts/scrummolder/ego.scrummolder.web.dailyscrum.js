// **** Namespace declaration **** //

// **** Namespace implementation **** //
eGo.ScrumMolder.Web.DailyScrum = function () {

    var addNewDailyScrumButton = $("#addNewDailyScrumButton");
    var dailyScrumEntryEditor = $("#dailyScrumEntryEditor");

    var clientsDropDown = $('.clients');
    var projetcsClass = "projects";

    var stepper1Class = 'ns_1';
    var stepper2Class = 'ns_2';

    function init(getDailyProjectScrumUrl, getClientProjectsUrl) {
        initAddDailyScrumButton(getDailyProjectScrumUrl);
        //initStepper();
        initClientDropDown(getClientProjectsUrl);
    };

    function initAddDailyScrumButton(getDailyProjectScrumUrl) {

        addNewDailyScrumButton.click(function () {
            var url = getDailyProjectScrumUrl;
            $.get(url, function (template) {
                dailyScrumEntryEditor.append(template);
                initStepper();
            });
            return false;
        });

    };

    function initStepper() {

        // activate stepper
        $("." + stepper1Class).stepper({
            min: 0,
            max: 8,
            step: 1,
            start: 0
        });
        $("." + stepper2Class).stepper({
            min: 0,
            max: 55,
            step: 5,
            start: 0
        });

    };

    function initClientDropDown(getClientProjectsUrl) {

        clientsDropDown.each(function () {
            var projectList = $(this).parent().parent().find("." + projetcsClass);
            var selectedProjectId = $(projectList).val();
            $(projectList).attr("disabled", "disabled");
            if ($(this).val() != "") {
                var url = getClientProjectsUrl;
                $.getJSON(url, { clientId: $(this).val() }, function (data) {
                    $(projectList).removeAttr("disabled");
                    $(projectList).empty();
                    $(projectList).append('<option value=""> -- select project -- </option>');
                    $.each(data, function (key, val) {
                        if (selectedProjectId == val.Id)
                            $(projectList).append('<option value="' + val.Id + '" selected="selected">' + val.Name + '</option>');
                        else
                            $(projectList).append('<option value="' + val.Id + '">' + val.Name + '</option>');
                    });

                });
            }
        });

        clientsDropDown.live("change", function () {
            var projectList = $(this).parent().parent().find("." + projetcsClass);
            $(projectList).attr("disabled", "disabled");
            if ($(this).val() != "") {
                var url = getClientProjectsUrl;
                $.getJSON(url, { clientId: $(this).val() }, function (data) {
                    $(projectList).removeAttr("disabled");
                    $(projectList).empty();
                    $(projectList).append('<option value=""> -- select project -- </option>');
                    $.each(data, function (key, val) {
                        $(projectList).append('<option value="' + val.Id + '">' + val.Name + '</option>');
                    });

                });
            }
        });

    };

    return {
        Init: init
    };

};

eGo.ScrumMolder.Web.DailyScrum.registerClass('eGo.ScrumMolder.Web.DailyScrum');

// **** Usages **** //
