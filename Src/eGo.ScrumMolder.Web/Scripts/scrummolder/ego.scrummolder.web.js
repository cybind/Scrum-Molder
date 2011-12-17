// **** Namespace declaration **** //
Type.registerNamespace('eGo.ScrumMolder.Web');

// **** Namespace implementation **** //
eGo.ScrumMolder.Web = function () {

    function init() {

        initAjaxSetup();

    };

    function initAjaxSetup() {

        $.ajaxSetup({
            cache: false,
            async: true,
            beforeSend: function () {

                $.blockUI({
                    message: $('div.preloader'),
                    fadeIn: 700,
                    fadeOut: 700,
                    showOverlay: false,
                    centerY: false,
                    css: {
                        width: '270px',
                        height: '80px',
                        top: '10px',
                        left: '',
                        right: '10px',
                        border: 'none',
                        backgroundColor: '#000',
                        'border-radius': '10px',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .6,
                        color: '#fff'
                    }
                });

            },
            complete: function () {
                $.unblockUI();
            },
            error: function () {
                $.unblockUI();
            }

        });

    };

    return {
        Init: init
    };

};

eGo.ScrumMolder.Web.registerClass('eGo.ScrumMolder.Web');

// **** Usages **** //
var website = new eGo.ScrumMolder.Web();
website.Init();