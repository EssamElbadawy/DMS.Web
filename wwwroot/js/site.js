var table;
var datatable;
var updatedRow;
var exportedCols = [];


//Select2
$('.js-select2').select2();

$(document).ready(function () {
    // Handle the change event of the first dropdown
    $('#firstDropdown').change(function () {
        var selectedId = $(this).val();

        // Make an AJAX call to retrieve data based on the selectedId
        $.ajax({
            url: '/Doctor/GetWorkingDays',
            type: 'GET',
            data: { selectedId: selectedId },
            success: function (result) {

                var data = result.data; // Extract the 'data' property from the result
                var doctor = result.doctor;
                var enableddates = [];
                var selectOptions = '';

                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        var dateParts = key.split('-'); // Split the key into year, month, and day parts
                        var year = parseInt(dateParts[0]);
                        var month = parseInt(dateParts[1]) - 1; // Month is zero-based in JavaScript
                        var day = parseInt(dateParts[2]);
                        var date = new Date(year, month, day);
                        enableddates.push(date);
                        var hour = doctor.startTime[0];
                        var Min = doctor.startTime[2];
                        var time = new Date()

                        var dayOfWeek = data[key];
                        selectOptions += '<option value="' + date.toString() + '">' + dayOfWeek + '</option>';
                    }

                }
                // Populate the second dropdown with options
                $('#secondDropdown').html(selectOptions);

                // Initialize Flatpickr with disabled dates
                $('.js-flatpickr').flatpickr({
                    enable: enableddates,
                    enableTime: true,
                    time_24hr: false,
                    defaultTime: doctor.startTime,
                    minTime: doctor.startTime,
                    maxTime: doctor.endTime,
                    noCalendar: true

                });
            },
            error: function (xhr, status, error) {
                // Handle the error case
                console.log(error);
            }
        });
    });
});


//Handle Cancel reservation
$(document).ready(function () {
    $('.js-toggle-status').on('click', function () {
        var btn = $(this);
        $.post({
            url: '/Schedule/ToggleStatus/' + btn.data('id'),
            success: function () {
                var status = btn.parent('tr').find('js-status');
                var newstatus = status.text().trim() === 'Canceld' ? 'Available' : 'Delted';
                status.text(newstatus).toggleClass('badge-light-success badge-light-danger')
            }
        });
    });
});



function disableSubmitButton() {
    $('body :submit').attr('disabled', 'disabled').attr('data-kt-indicator', 'on');
}

function onModalBegin() {
    disableSubmitButton();
}

function onModalSuccess(row) {
    showSuccessMessage();
    $('#Modal').modal('hide');

    if (updatedRow !== undefined) {
        datatable.row(updatedRow).remove().draw();
        updatedRow = undefined;
    }

    var newRow = $(row);
    datatable.row.add(newRow).draw();
}

function onModalComplete() {
    $('body :submit').removeAttr('disabled').removeAttr('data-kt-indicator');
}

//Select2
function applySelect2() {
    $('.js-select2').select2();
    $('.js-select2').on('select2:select', function (e) {
        $('form').not('#SignOut').validate().element('#' + $(this).attr('id'));
    });
}

//DataTables
var headers = $('th');
$.each(headers, function (i) {
    if (!$(this).hasClass('js-no-export'))
        exportedCols.push(i);
});

// Class definition
var KTDatatables = function () {
    // Private functions
    var initDatatable = function () {
        // Init datatable --- more info on datatables: https://datatables.net/manual/
        datatable = $(table).DataTable({
            'info': false,
            'pageLength': 10,
            'drawCallback': function () {
                KTMenu.createInstances();
            }
        });
    }

    // Hook export buttons
    var exportButtons = () => {
        const documentTitle = $('.js-datatables').data('document-title');
        var buttons = new $.fn.dataTable.Buttons(table, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportedCols
                    }
                },
                {
                    extend: 'excelHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportedCols
                    }
                },
                {
                    extend: 'csvHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportedCols
                    }
                },
                {
                    extend: 'pdfHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportedCols
                    }
                }
            ]
        }).container().appendTo($('#kt_datatable_example_buttons'));

        // Hook dropdown menu click event to datatable export buttons
        const exportButtons = document.querySelectorAll('#kt_datatable_example_export_menu [data-kt-export]');
        exportButtons.forEach(exportButton => {
            exportButton.addEventListener('click', e => {
                e.preventDefault();

                // Get clicked export value
                const exportValue = e.target.getAttribute('data-kt-export');
                const target = document.querySelector('.dt-buttons .buttons-' + exportValue);

                // Trigger click event on hidden datatable export buttons
                target.click();
            });
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = () => {
        const filterSearch = document.querySelector('[data-kt-filter="search"]');

        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            table = document.querySelector('.js-datatables');

            if (!table) {
                return;
            }

            initDatatable();
            exportButtons();
            handleSearchDatatable();
        }
    };
}();

$(document).ready(function () {
    //Disable submit button
    $('form').not('#SignOut').on('submit', function () {
        if ($('.js-tinymce').length > 0) {
            $('.js-tinymce').each(function () {
                var input = $(this);

                var content = tinyMCE.get(input.attr('id')).getContent();
                input.val(content);
            });
        }

        var isValid = $(this).valid();
        if (isValid) disableSubmitButton();
    });

    //TinyMCE
    if ($('.js-tinymce').length > 0) {
        var options = { selector: ".js-tinymce", height: "430" };

        if (KTThemeMode.getMode() === "dark") {
            options["skin"] = "oxide-dark";
            options["content_css"] = "dark";
        }

        tinymce.init(options);
    }

    //Select2
    applySelect2();

    //Datepicker
    $('.js-datepicker').daterangepicker({
        singleDatePicker: true,
        autoApply: true,
        drops: 'up',
        maxDate: new Date()
    });

    //SweetAlert
    var message = $('#Message').text();
    if (message !== '') {
        showSuccessMessage(message);
    }

    //DataTables
    KTUtil.onDOMContentLoaded(function () {
        KTDatatables.init();
    });

    //Handle bootstrap modal
    $('body').delegate('.js-render-modal', 'click', function () {
        var btn = $(this);
        var modal = $('#Modal');

        modal.find('#ModalLabel').text(btn.data('title'));

        if (btn.data('update') !== undefined) {
            updatedRow = btn.parents('tr');
        }

        $.get({
            url: btn.data('url'),
            success: function (form) {
                modal.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(modal);
                applySelect2();
            },
            error: function () {
                showErrorMessage();
            }
        });

        modal.modal('show');
    });


    //Handle Confirm
    $('body').delegate('.js-confirm', 'click', function () {
        var btn = $(this);

        bootbox.confirm({
            message: btn.data('message'),
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.post({
                        url: btn.data('url'),
                        data: {
                            '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                        },
                        success: function () {
                            showSuccessMessage();
                        },
                        error: function () {
                            showErrorMessage();
                        }
                    });
                }
            }
        });
    });

    //Hanlde signout
    $('.js-signout').on('click', function () {
        $('#SignOut').submit();
    });

});