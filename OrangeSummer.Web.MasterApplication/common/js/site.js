; (function ($) {
    "use strict"; // Start of use strict

    $.datepicker.regional["ko"] = {
        dateFormat: 'yy-mm-dd',
        showOtherMonths: true,
        selectOtherMonths: true,
        monthNames: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
        monthNamesShort: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
        dayNamesMin: ['일', '월', '화', '수', '목', '금', '토'],
        showAnim: "",
        showMonthAfterYear: true,
        currentText: '오늘',
        changeMonth: true,
        changeYear: true
    };

    $.datepicker.setDefaults($.datepicker.regional["ko"]);
    $(".datepicker").datepicker().prop("readonly", "readonly");

    $(".custom-file-input").on("change", function () {
        $(this).next("label").text($(this).prop("files").length + " 개");
    });

    $.loading = {
        show: function (message) {
            if (message === undefined) {
                message = "처리중입니다. 잠시만 기다려주세요.";
            }

            var html = "";
            html += "<div class=\"text-center font-weight-bold\" style=\"color: #6c757d;\">";
            html += "   <div class=\"spinner-border text-secondary align-middle mr-2\" role=\"status\">";
            html += "       <span class=\"sr-only\">Loading...</span>";
            html += "   </div>";
            html += "   " + message;
            html += "</div>";

            $.blockUI({
                message: html,
                css: {
                    border: "none",
                    padding: "15px",
                    backgroundColor: "#fff",
                    "-webkit-border-radius": "10px",
                    "-moz-border-radius": "10px",
                    color: "#000"
                }
            });
        },
        hide: function () {
            $.unblockUI();
        }
    };

    $.fn.process = function (message) {
        return this.each(function () {
            var $element = $(this);
            $element.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span> " + message);
            $element.prop("disabled", true).addClass("disabled");
        });
    };

    $.fn.reset = function (message) {
        return this.each(function () {
            var $element = $(this);
            $element.html(message);
            $element.prop("disabled", false).removeClass("disabled");
        });
    };

    $(document).ajaxStart($.loading.show()).ajaxStop($.loading.hide());
})(jQuery); // End of use strict