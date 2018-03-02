(function () {
    'use strict';

    angular.module('naut').controller('reportController', reportController).filter('propsFilter', propsFilter);




    function reportController($scope, $location, $modal, $window, reportService, localStorageService, ngAuthSettings, $filter, ngTableParams, modalService, flotOptions, $timeout) {
        $scope.chartBarFlotChart = flotOptions['bar'];

        var vm = this;

        $scope.template = {
            isYearMonth: false,
            isDate: false,
            isBankCustomer: false,
            isCode: false,
            isCallGapType: false,
            isAgeSlab: false,
            isFeildFeedback: false
        };

        $scope.model = {
            code1: '',
            code2: '',
            clientId: '',
            customerName: '',
            bankName: '',
            reportId: 0,
            from: '',
            to: '',
            year: new Date().getFullYear(),
            template: '',
            callGapType: '0',
            feildFeedback:1,
            ageSlab: '1',
            overallSummary:'1',
            misType:1,
            month: '1',
            pageSize: 10,
            page: 1,
            
        };


        $scope.system = {
            alertMsg: '',
            startRowNumber: 1,
            endRowNumber: 10
        };

        $scope.status = {
            isFirstOpen: false
        };



        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $scope.model.clientId = authData.clientId;
        }




        $scope.model.reportId = $location.search().id;
        if ($scope.model.reportId == 1) {
            $scope.template = {
                isYearMonth: false,
                isDate: true,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: false,
                isAgeSlab: false,
                isFeildFeedback: false,
                isOverallSummary: false,
                isMisType: false
            
            };
            $scope.model.template = 'isDate';


        }
        else if ($scope.model.reportId == 2) {
            $scope.template = {
                isYearMonth: true,
                isDate: false,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: false,
                isAgeSlab: false,
                isFeildFeedback: false,
                isOverallSummary: false,
                isMisType: false
            };
            $scope.model.template = 'isYearMonth';
        }
        else if ($scope.model.reportId == 3 || $scope.model.reportId == 10) {

            $scope.template = {
                isYearMonth: true,
                isDate: false,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: true,
                isAgeSlab: false,
                isFeildFeedback: false,
                isOverallSummary: false,
                isMisType: false
            };
            $scope.model.template = 'isYearMonth';
        }
        else if ($scope.model.reportId == 4 || $scope.model.reportId == 6) {

            $scope.template = {
                isYearMonth: false,
                isDate: false,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: false,
                isAgeSlab: true,
                isFeildFeedback: false,
                isOverallSummary: false,
                isMisType: false
            };
            $scope.model.template = '';
        }
        else if ($scope.model.reportId == 7 ) {

            $scope.template = {
                isYearMonth: false,
                isDate: true,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: false,
                isAgeSlab: false,
                isFeildFeedback: true,
                isOverallSummary: false,
                isMisType: false
            };
            $scope.model.template = 'isDate';

        }


        else if ($scope.model.reportId == 11) {

            $scope.template = {
                isYearMonth: false,
                isDate: true,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: false,
                isAgeSlab: false,
                isFeildFeedback: false,
                isOverallSummary: false,
                isMisType: false
            };
            $scope.model.template = 'isDate';
        }

        else if ($scope.model.reportId == 12) {

            $scope.template = {
                isYearMonth: false,
                isDate: true,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: false,
                isAgeSlab: false,
                isFeildFeedback: false,
                isOverallSummary: true,
                isMisType: false
            };
            $scope.model.template = 'isDate';
        }


        else if ($scope.model.reportId == 14) {

            $scope.template = {
                isYearMonth: false,
                isDate: true,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: false,
                isAgeSlab: false,
                isFeildFeedback: false,
                isOverallSummary: false,
                isMisType: false
            };
            $scope.model.template = 'isDate';
        }

        else if ($scope.model.reportId == 15) {

            $scope.template = {
                isYearMonth: false,
                isDate: true,
                isBankCustomer: false,
                isCode: false,
                isCallGapType: false,
                isAgeSlab: false,
                isFeildFeedback: false,
                isOverallSummary: false,
                isMisType:true
            };
            $scope.model.template = 'isDate';
        }



        reportService.getReportInfo($scope.model.reportId, $scope.model.clientId).then(function (response) {

            $scope.reportTitle = response.data.table0[0].title;
            $scope.code1List = response.data.table1;
            $scope.code2List = response.data.table2;

        },
     function (err) {
         $.toaster({ title: 'Error', priority: 'danger', message: err.data });
     });




        $scope.applyFilter = function () {

            $scope.status.isFirstOpen = false;
            $("#filterAccordian").toggleClass("fa-minus-square");
            $scope.getReport(1);
        };


        $scope.getReport = function (page) {
            $scope.model.page = page;
            $scope.model.pageSize = $scope.model.pageSize;

            reportService.getReport($scope.model).then(function (response) {



                if ($timeout) {
                    var data = response.data[0].table0;
                    var summery = response.data[0].table1;

                    $scope.reportDetailHeader = response.data[0].table0[0];
                    $scope.reportDetailBody = response.data[0].table0;

                    $scope.reportSummeryHeader = summery[0];
                    $scope.reportSummeryBody = summery;

                    $scope.reportTitle = response.data[1];

                    $scope.model.total = response.data[0].table2[0]["totalRows"];

                    if ($scope.reportDetailBody.length == 0) {
                        $scope.system.alertMsg = 'No records were found that match the specified search criteria';
                    }
                    else {
                        $scope.system.alertMsg = '';
                    }

                }

            },
     function (err) {



         $.toaster({ title: 'Error', priority: 'danger', message: err.data });
     });


        };





        $scope.getReportPdf = function () {


            reportService.getReportPdf($scope.model).then(function (response) {


                if ($timeout) {

                    var tabWindowId = window.open('about:blank', '_blank');
                    var blob = new Blob([response.data], { type: "application/pdf" });
                    var objectUrl = URL.createObjectURL(blob);
                    tabWindowId.location.href = objectUrl;

                }

            },
     function (err) {



         $.toaster({ title: 'Error', priority: 'danger', message: err.data });
     });

            

        }


        $scope.getReportExcel = function () {


            reportService.getReportExcel($scope.model).then(function (response) {


                if ($timeout) {
                    var tabWindowId = window.open('about:blank', '_blank');
                    var blob = new Blob([response.data], { type: "application/vnd.ms-excel" });
                    var objectUrl = URL.createObjectURL(blob);
                    tabWindowId.location.href = objectUrl;

                }

            },
     function (err) {



         $.toaster({ title: 'Error', priority: 'danger', message: err.data });
     });

        }



        $scope.filterAccordian = function () {
            $("#filterAccordian").toggleClass("fa-minus-square");
        },

        $scope.cancel = function () {
            SetModelValue($scope, localStorageService);
            $scope.status.isFirstOpen = false;
            $("#filterAccordian").toggleClass("fa-minus-square");


        };


        $scope.reset = function () {
            SetModelValue($scope, localStorageService);
        };


        $scope.getKeyHeader = function (value) {


        },

        $scope.getKeys = function (obj) {



            var key;
            var keys = [];
            for (key in obj) {
                if (key === "$$hashKey") break; //angular adds new keys to the object
                if (obj.hasOwnProperty(key)) keys.push(key);
            }



            return keys;
        };

        $scope.isDate = function (strDate) {

            var retVal = "";
            try {

                retVal = $filter('date')(strDate, 'dd/MM/yyyy');
                retVal = retVal == "01/01/1970" ? strDate : retVal;

            } catch (ex) {
                retVal = strDate;
            }

            return retVal;

        };


        // ui select

        $scope.disabled = undefined;
        $scope.filters = {
            selectedExtensions: ''
        };

        $scope.enable = function () {
            $scope.disabled = false;
        };

        $scope.disable = function () {
            $scope.disabled = true;
        };


        $scope.getColName = function (value) {


            return value == undefined ? "" : value;
        };




        $scope.ascSort = function (index) {

            var rowName = "";
            rowName = $scope.getKeys($scope.reportDetailHeader);
            rowName = rowName[index].toString()
            $scope.reportDetailBody = $filter("orderBy")($scope.reportDetailBody, rowName)

        }

        $scope.descSort = function (index) {

            var rowName = "";
            rowName = $scope.getKeys($scope.reportDetailHeader);
            rowName = rowName[index].toString()
            $scope.reportDetailBody = $filter("orderBy")($scope.reportDetailBody, "-" + rowName)

        };

        $scope.pagingAction = function (page) {
            //console.log({text, page, pageSize, total});
            $scope.system.startRowNumber = (page - 1) * 10,
        $scope.system.endRowNumber = ((page - 1) * 10) + 10;
            //BindData(id, reportService, $scope, vm, ngTableParams, $filter, $timeout, page);
            $scope.getReport(page);

        };









    }



    /**
   * AngularJS default filter with the following expression:
   * "person in people | filter: {name: $select.search, age: $select.search}"
   * performs a AND between 'name: $select.search' and 'age: $select.search'.
   * We want to perform a OR.
   */
    function propsFilter() {
        return function (items, props) {
            var out = [];

            if (angular.isArray(items)) {
                items.forEach(function (item) {
                    var itemMatches = false;

                    var keys = Object.keys(props);
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }

            return out;
        };
    }




})();


