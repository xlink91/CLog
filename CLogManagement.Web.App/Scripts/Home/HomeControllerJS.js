appSettings = {};


var dateUtil = {
    getDate: function (dateAsString, timeAsString, setDateToNow = false) {
        if (dateAsString == null || dateAsString === "") {
            if (setDateToNow) {
                let now = new Date(Date.now());
                let mm = now.getMonth() + 1;
                let dd = now.getUTCDate();
                let yyyy = now.getUTCFullYear();
                dateAsString = mm.toString() + "/" + dd.toString() + "/" + yyyy.toString();
            } else {
                dateAsString = "01/01/2020";
            }
        }

        if (timeAsString == null || timeAsString === "") {
            timeAsString = "23:59:59";
        }

        var splittedDate = dateAsString.split("/");
        var mm = splittedDate[0];
        var dd = splittedDate[1];
        var yyyy = splittedDate[2];
        let dateTimeStringToParse = yyyy + "-" + mm + "-" + dd + "T" + timeAsString;
        return new Date(dateTimeStringToParse);
    }
};

class HomeControllerJS {
    constructor(appSettings) {
        this.appSettings = appSettings;
    }

    getDateValues() {
        let fromDatePickerValue =
            $(this.appSettings.fromDatePicker).val();
        let fromTimeValue =
            $(this.appSettings.fromTime).val();
        let toDatePickerValue =
            $(this.appSettings.toDatePicker).val();
        let toTimeValue =
            $(this.appSettings.toTime).val();
        return {
            fromDateTime: dateUtil.getDate(fromDatePickerValue, fromTimeValue),
            toDateTime: dateUtil.getDate(toDatePickerValue, toTimeValue, true)
        };
    }
}


var logsReport = {
    tableFilters: {},
    urlToLoadData: "",
    dataTableId: "",
    tableWrapper: "",
    filterDataFunction: undefined,
    dataTable: null,
    tableLoaded: false,
    isNewFilter: undefined,
    timeout: null,
    selectedTerminals: [],
    reportVM: {
        init: function () {
            logsReport.dataTable =
                $(logsReport.dataTableId)
                    .on('preXhr.dt', function (e, settings, data) {
                        //showSpinner();
                    }).DataTable({
                        "dom": "<'dt-toolbar'<'col-sm-12 float-left'B>>" +
                            "t" +
                            "<'dt-toolbar-footer'<'col-md-6 col-sm-12 col-xs-12'i><'col-md-5 col-sm-11 col-xs-11'p>>",
                        "fnDrawCallback": function (oSettings) {
                            logsReport.adjustTable();
                            logsReport.isNewFilter = false;
                        },
                        "buttons": [
                            //   {
                            //        "text": "<span><i class='fa fa-check'></i></span> " + appSettings.resourcesTermGroup.JsTermialsListAllMarks,
                            //        "action": function (e, dt, node, config) {
                            //           logsReport.selectAllTerminals(true);
                            //        }
                            //   },
                            //   {
                            //       "text": "<span><i class='fa fa-times'></i></span>" + appSettings.resourcesTermGroup.JsTermialsListAllUnMarks,
                            //       "action": function (e, dt, node, config) {
                            //           logsReport.selectAllTerminals(false);
                            //       }
                            //   }
                        ],
                        "order": [[1, "asc"]],
                        "autoWitdh": true,
                        "pageLength": 25,
                        "serverSide": true,
                        "processing": true,
                        "ajax": {
                            "type": "POST",
                            "url": logsReport.urlToLoadData,
                            "contentType": 'application/json; charset=utf-8',
                            "data": function (data) {
                                let dateValues =
                                    new HomeControllerJS(appSettings)
                                        .getDateValues();
                                let newData = {
                                    fromDateTime: dateValues.fromDateTime,
                                    toDateTime: dateValues.toDateTime,
                                    filter: data
                                };
                                return JSON.stringify(newData);
                            },
                            "dataSrc": function (json) {
                                for (let idx = 0; idx < json.data.length; ++idx)
                                {
                                    let dateAsString = json.data[idx].DateTime;
                                    let s = dateAsString.substr(6, dateAsString.length - 8);
                                    let ml = Number(s);
                                    let date = new Date(ml);
                                    json.data[idx].DateTime =
                                        date.getUTCDate().toString().padStart(2, "0") + "/" +
                                        (date.getMonth() + 1).toString().padStart(2, "0") + "/" +
                                        date.getFullYear() + " " +
                                        date.getHours().toString().padStart(2, "0") + ":" +
                                        date.getMinutes().toString().padStart(2, "0") + ":" +
                                        date.getSeconds().toString().padStart(2, "0");
                                }
                                return json.data;
                            }
                        },
                        "columns": [
                        {
                            "title": 'Date',
                            "data": "DateTime",
                            "sortable": true,
                            "width": "20%"
                        },
                        {
                            "title": 'Microservice',
                            "data": "ServiceName",
                            "sortable": true,
                            "width": "20%"
                        },
                        {
                            "title": 'Severity',
                            "data": "SeverityType",
                            "sortable": true,
                            "width": "20%"
                        },
                        {
                            "title": 'Message',
                            "data": "Message",
                            "sortable": true,
                            "width": "20%"
                        },
                        {
                            "title": 'Class',
                            "data": "ClassName",
                            "sortable": true,
                            "width": "20%"
                        },              
                        {
                            "title": 'Method',
                            "data": "MethodName",
                            "sortable": true,
                            "width": "20%"
                        },
                        {
                            "title": 'Line',
                            "data": "LineNumber",
                            "sortable": true,
                            "width": "5%"
                        }
                    ],
                    "columnDefs": [
                        { "sortable": true, "targets": [0, 1, 2, 3, 4] },
                        {
                            targets: 4,
                            data: null,
                            className: 'text-center',
                            searchable: false,
                            orderable: false
                        },
                        { "className": "dt-center", "targets": "_all" }
                    ],
                    "scrollX": true,
                    initComplete: function () {
                        logsReport.addColumnFilters();
                        logsReport.tableLoaded = true;
                        logsReport.adjustTable();
                    }
                });
            },
            selectedRow: undefined,
            refresh: function () {
                selectedRow = undefined;
                logsReport.dataTable.ajax.reload();
            }
    },

    adjustTable: function () {
        logsReport.dataTable.columns.adjust().fixedColumns().relayout();
    },

    addColumnFilters: function () {
        var tableHead = $(logsReport.tableWrapper + " .dataTables_scrollHead" + " thead");
        tableHead.append("<tr class='filter-row dataTableSearchBar'>" +
            "<th></th>" +
            "<th><input type='text' class='form-control'/></th>" +
            "<th><input type='text' class='form-control'/></th>" +
            "<th><input type='text' class='form-control'/></th>" +
            "<th><input type='text' class='form-control'/></th>" +
            "<th><input type='text' class='form-control'/></th>" +
            "<th><input type='text' class='form-control'/></th>" +
            "</tr > ");
        logsReport.applySearchValues();
    },

    applySearchValues: function () {
        $(".filter-row input").on("keyup", function () {
            clearTimeout(logsReport.timeout);
            var that = this;
            logsReport.timeout = setTimeout(function () {
                var index = $(that).parents("th").index();
                var column = logsReport.dataTable.column(index);
                if (column.search() !== that.value)
                    column.search(that.value).draw();
            }, 300);
        });

        $(".filter-row select").on("change", function () {
            var index = $(this).parents("th").index();
            var column = logsReport.dataTable.column(index);
            if (column.search() !== this.value)
                column.search(this.value).draw();
        });
    },

    setSelectedRow: function (event) {
        //logsReport.reportVM.selectedRow = $(this);
        //var cbx = $(logsReport.reportVM.selectedRow[0].cells[4].firstElementChild);

        //if (event.target.id === cbx[0].id)
        //    return;

        //if (cbx.prop('checked'))
        //    cbx.prop('checked', false);
        //else
        //    cbx.prop('checked', true);
    },

    initDatatable: function () {
        logsReport.isNewFilter = true;
        if (logsReport.tableLoaded) {
            logsReport.reportVM.refresh();
        }
        else {
            $(logsReport.tableWrapper).show();
            logsReport.reportVM.init();
            $(logsReport.dataTableId + " tbody").on('click', 'tr', logsReport.setSelectedRow);
        }
    },

    selectAllTerminals: function (selected) {
        $(logsReport.dataTableId + " input[type=checkbox]").each(function () {
            var action = selected ? "check" : "uncheck";
            $(this).iCheck(action);
        });

        $('input.select-checkbox').prop('checked', true);
    },

    getSelectedItems: function () {
        var selectedTerminals = [];
        $.each($("input[name='checkTerminal']:checked"), function () {
            selectedTerminals.push($(this)[0].id);
        });
        return selectedTerminals;
    },
    init: function () {
        //sidebarToggle.addListener(function () { logsReport.adjustTable(); });
    }
}
