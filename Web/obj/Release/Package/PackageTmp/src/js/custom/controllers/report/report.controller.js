

(function () {
    'use strict';

    angular.module('naut').controller('reportController', reportController).filter('propsFilter', propsFilter);




    function reportController($scope, $location, $modal, $window, reportService, localStorageService, ngAuthSettings, $filter, ngTableParams, modalService, flotOptions, $timeout) {

        $scope.chartBarFlotChart = flotOptions['bar'];

        var vm = this;

        SetModelValue($scope, localStorageService);

        $scope.system = {
            alertMsg: '',
            startRowNumber: 1,
            endRowNumber: 10
        };


        $scope.attr = {
            isStartDateDisable: true,
            isEndDateDisable: true,
            isStartTimeDisable: true,
            isEndTimeDisable: true,

            isExtensionRangeDisable: true,
            isSelectedExtensionsDisable: true,

            isDivisionDisable: true,
            isDepartmentDisable: true,

            isCostCentreDisable: true,

            isSiteDisable: true,

            isAccountDisable: true,

            isAccountGroupDisable: true,

            isSelectRestrictCampaignsDisable: true,

            isSelectedDDIsDisable: true,

            isIncomingCallsDisable: true,
            isCheckOutgoingcallsDisable: true,

            isCheckInternalcallsDisable: true,

            isCheckRestrictDisable: true,

            isRestrictStartsDisable: true,
            isRestrictSelectFromDisable: true,
            isSelectFromDirectoryDisable: true,

            isIncomingCallDurationRangeDisable: true,

            isOutgoingCallDurationRangeDisable: true,

            isIncomingAnsweredRingTimeDisable: true,

            isIncomingUnansweredRingTimeDisable: true,


        };

        $scope.status = {
            isFirstOpen: false
        };



        var id = $location.search().id;

        BindData(id, reportService, $scope, vm, ngTableParams, $filter, $timeout, 1);

        //$scope.BindData = function (id) {
        //    $scope.model.isLoadPage = false;
        //    BindData(id, reportService, $scope, vm, ngTableParams, $filter, $timeout);

        //}

        $scope.applyFilter = function () {


            $scope.status.isFirstOpen = false;
            $("#filterAccordian").toggleClass("fa-minus-square");
            var id = $location.search().id;
            $scope.model.isLoadPage = false;

            BindData(id, reportService, $scope, vm, ngTableParams, $filter, $timeout, 1);

        };

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

        $scope.clear = function () {
            $scope.person.selected = undefined;
            $scope.address.selected = undefined;
            $scope.country.selected = undefined;
        };




        $scope.availableExtentions = [];

        // get filters data

        reportService.getReportFilters($scope.model.customerId, $scope.model.clientId).then(function (response) {



            for (var i = 0; i < response.data.table0.length; i++) {
                $scope.availableExtentions.push(response.data.table0[i].extension);
            }


            $scope.divisionList = response.data.table1;
            $scope.departmentList = response.data.table2;
            $scope.costCentreList = response.data.table3;

            $scope.ddiList = [];
            for (var i = 0; i < response.data.table4.length; i++) {

                $scope.ddiList.push(response.data.table4[i].account);

            }

            $scope.ddiGroupNameList = response.data.table5;


        },
     function (err) {

         $.toaster({ title: 'Error', priority: 'danger', message: err.data });
     });

        $scope.divisions = [];
        $scope.divisions.selected = 1;

        $scope.departments = [];
        $scope.departments.selected = 1;

        $scope.costcentres = [];
        $scope.costcentres.selected = 1;

        $scope.accounts = [];
        $scope.accounts.selected = 1;

        $scope.account = [];
        $scope.account.selected = 1;




        /// check box and racio button change enable and disabled

        $scope.funcSelectedDateTime = function (value) {
            console.log(value);
            if (value == "daterange") {
                $scope.attr.isStartDateDisable = false;
                $scope.attr.isEndDateDisable = false;

            }
            else {
                $scope.attr.isStartDateDisable = true;
                $scope.attr.isEndDateDisable = true;
            }
        };


        $scope.funcTimerange = function () {


            if ($scope.model.timeRange) {

                $scope.attr.isStartTimeDisable = false;
                $scope.attr.isEndTimeDisable = false;

            }
            else {

                $scope.attr.isStartTimeDisable = true;
                $scope.attr.isEndTimeDisable = true;
            }
        };


        // Extension and agent tab
        $scope.funcExtensions = function (value) {

            console.log(value);
            if (value == "allextention") {

                $scope.attr.isExtensionRangeDisable = true;
                $scope.attr.isSelectedExtensionsDisable = true;

            }
            else if (value == "extensionrange") {
                $scope.attr.isExtensionRangeDisable = false;
                $scope.attr.isSelectedExtensionsDisable = true;
            }
            else {
                $scope.attr.isExtensionRangeDisable = true;
                $scope.attr.isSelectedExtensionsDisable = false;
            }
        };


        // Group tab

        $scope.funcDivision = function () {


            if ($scope.model.division) {


                $scope.attr.isDivisionDisable = true;
            }

            else {
                $scope.attr.isDivisionDisable = false;
            }
        };

        $scope.funcDepartment = function () {


            if ($scope.model.department) {

                $scope.attr.isDepartmentDisable = true

            }

            else {
                $scope.attr.isDepartmentDisable = false;
                ;
            }
        };

        $scope.funcCostCentre = function () {


            if ($scope.model.costCentre) {

                $scope.attr.isCostCentreDisable = true

            }

            else {
                $scope.attr.isCostCentreDisable = false;
                ;
            }
        };

        $scope.funcSite = function () {


            if ($scope.model.site) {

                $scope.attr.isSiteDisable = true

            }

            else {
                $scope.attr.isSiteDisable = false;
                ;
            }
        };

        $scope.funcAccount = function () {


            if ($scope.model.acccount) {

                $scope.attr.isAccountDisable = true

            }

            else {
                $scope.attr.isAccountDisable = false;
                ;
            }
        };

        $scope.funcAccountGroup = function () {


            if ($scope.model.accountGroup) {

                $scope.attr.isAccountGroupDisable = true

            }

            else {
                $scope.attr.isAccountGroupDisable = false;
                ;
            }
        };


        // DDI/Campaign

        $scope.funcDDI = function (value) {


            if (value == "allddi") {

                $scope.attr.isSelectRestrictCampaignsDisable = true;
                $scope.attr.isSelectedDDIsDisable = true;

            }
            else if (value == "restrictcampaigns") {
                $scope.attr.isSelectRestrictCampaignsDisable = false;
                $scope.attr.isSelectedDDIsDisable = true;
            }
            else {
                $scope.attr.isSelectRestrictCampaignsDisable = true;
                $scope.attr.isSelectedDDIsDisable = false;
            }
        };


        // Call Types


        $scope.funcIncomingCalls = function () {


            if ($scope.model.incomingCalls) {

                $scope.attr.isIncomingCallsDisable = false

            }

            else {
                $scope.attr.isIncomingCallsDisable = true;

            }
        };

        $scope.funcCheckOutgoingcalls = function () {


            if ($scope.model.checkOutgoingcalls) {

                $scope.attr.isCheckOutgoingcallsDisable = false

            }

            else {
                $scope.attr.isCheckOutgoingcallsDisable = true;
                ;
            }
        };

        $scope.funcCheckInternalcalls = function () {


            if ($scope.model.checkInternalcalls) {

                $scope.attr.isCheckInternalcallsDisable = false

            }

            else {
                $scope.attr.isCheckInternalcallsDisable = true;

            }
        };


        // Restrictions
        $scope.funcCheckRestrict = function () {

            if ($scope.model.checkRestrict) {

                $scope.attr.isCheckRestrictDisable = false

            }
            else {
                $scope.attr.isCheckRestrictDisable = true;

            }
        };

        $scope.funcRestrictradio = function (value) {

            if (value == "starts") {
                $scope.attr.isRestrictStartsDisable = false;
                $scope.attr.isRestrictSelectFromDisable = true;
                $scope.attr.isSelectFromDirectoryDisable = true;

            }
            else if (value == "selectfrom") {

                $scope.attr.isRestrictStartsDisable = true;
                $scope.attr.isRestrictSelectFromDisable = false;
                $scope.attr.isSelectFromDirectoryDisable = true;
            }
            else {

                $scope.attr.isRestrictStartsDisable = true;
                $scope.attr.isRestrictSelectFromDisable = true;
                $scope.attr.isSelectFromDirectoryDisable = false;
            }
        };


        $scope.funcIncomingCallDurationRange = function () {


            if ($scope.model.incomingCallDurationRange) {

                $scope.attr.isIncomingCallDurationRangeDisable = false

            }

            else {
                $scope.attr.isIncomingCallDurationRangeDisable = true;

            }
        };

        $scope.funcOutgoingCallDurationRange = function () {


            if ($scope.model.outgoingCallDurationRange) {

                $scope.attr.isOutgoingCallDurationRangeDisable = false

            }

            else {
                $scope.attr.isOutgoingCallDurationRangeDisable = true;

            }
        };


        $scope.funcIncomingAnsweredRingTime = function () {


            if ($scope.model.incomingAnsweredRingTime) {

                $scope.attr.isIncomingAnsweredRingTimeDisable = false

            }

            else {
                $scope.attr.isIncomingAnsweredRingTimeDisable = true;

            }
        };


        $scope.funcIncomingUnansweredRingTime = function () {


            if ($scope.model.incomingUnansweredRingTime) {

                $scope.attr.isIncomingUnansweredRingTimeDisable = false

            }

            else {
                $scope.attr.isIncomingUnansweredRingTimeDisable = true;

            }
        };


        $scope.getColName = function (value) {


            return value == undefined ? "" : value;
        };


        // Time Picker start

        $scope.hstep = 1;
        $scope.mstep = 15;

        $scope.options = {
            hstep: [1, 2, 3],
            mstep: [1, 5, 10, 15, 25, 30]
        };

        $scope.ismeridian = true;
        $scope.toggleMode = function () {
            $scope.ismeridian = !$scope.ismeridian;
        };


        $scope.changed = function () {
            //console.log('Time changed to: ' + $scope.model.timeRangeStartTime);
            //console.log('Time changed to: ' + $scope.model.timeRangeEndTime);
        };

        // Time Picker End

        // sorting

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
            $scope.system.startRowNumber = (page-1) * 10,
        $scope.system.endRowNumber = ((page-1) * 10) + 10;
            BindData(id, reportService, $scope, vm, ngTableParams, $filter, $timeout, page);

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


    function SetModelValue($scope, localStorageService) {
        $scope.model = {
            reportId: 0,
            customerId: 0,
            selectedDateTime: 'thisweek',

            selectedStartDate: '',
            selectedEndDate: '',


            timeRange: false,
            timeRangeStartTime: new Date(),
            timeRangeEndTime: new Date(),

            extension: 'allextention',

            extensionRange: '',
            selectedExtensions: [],


            divisionId: 0,
            division: true,
            departmentId: 0,
            department: true,
            costCentreId: 0,
            costCentre: true,
            siteId: 0,
            site: true,
            accountId: 0,
            account: true,
            accountGroupId: 0,
            accountGroup: true,
            ddiCampaign: 'allddi',
            selectRestrictCampaigns: '',
            restrictDDI: '',
            selectedDDIs: [],
            incomingCalls: '',
            incomingCallsAnsStatus: 'any',
            routing: 'any',
            bouncedCalls: 'any',
            checkOutgoingcalls: false,
            outgoingcallsLocal: false,
            outgoingcallsNational: '',
            outgoingcallsInternational: false,
            outgoingcallsOther: '',
            radioOutgoingcalls: 'any',
            checkInternalcalls: '',
            radioInternalcalls: 'any',
            checkRestrict: false,
            radioRestrict: true,
            restrictStarts: '',
            restrictSelectFrom: '',
            selectFromDirectory: '',
            incomingCallDurationRange: false,
            incomingminimum: new Date(2015, 1, 1, 0, 0, 0, 0),
            incomingmuximum: new Date(2015, 1, 1, 0, 0, 0, 0),
            outgoingCallDurationRange: false,
            outgoingminimum: new Date(2015, 1, 1, 0, 0, 0, 0),
            outgoingmuximum: new Date(2015, 1, 1, 0, 0, 0, 0),
            incomingAnsweredRingTime: false,
            incomingAnsweredRingTimeMinimum: new Date(2015, 1, 1, 0, 0, 0, 0),
            incomingAnsweredRingTimeMaximum: new Date(2015, 1, 1, 0, 0, 0, 0),
            incomingUnansweredRingTime: false,
            incomingUnansweredRingTimeMinimum: new Date(2015, 1, 1, 0, 0, 0, 0),
            incomingUnansweredRingTimeMaximum: new Date(2015, 1, 1, 0, 0, 0, 0),

            // is load page
            isLoadPage: true,
            clientId: 0,
            page: 1,
            pageSize: 10,
            total: 0,
        };

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $scope.model.customerId = authData.iCustomer;
            $scope.model.clientId = authData.clientId;

        }


    }



    function BindData(id, reportService, $scope, vm, ngTableParams, $filter, $timeout, page) {


        $scope.model.page = page;
        $scope.model.pageSize = $scope.model.pageSize;


        $scope.model.reportId = id;


        $scope.model.timeRangeStartTime = $filter('date')(new Date($scope.model.timeRangeStartTime), 'MM/dd/yyyy HH:mm:ss');

        $scope.model.timeRangeEndTime = $filter('date')($scope.model.timeRangeEndTime, 'MM/dd/yyyy HH:mm:ss');


        $scope.model.incomingminimum = $filter('date')($scope.model.incomingminimum, 'MM/dd/yyyy HH:mm:ss');
        $scope.model.incomingmuximum = $filter('date')($scope.model.incomingmuximum, 'MM/dd/yyyy HH:mm:ss');


        $scope.model.outgoingminimum = $filter('date')($scope.model.outgoingminimum, 'MM/dd/yyyy HH:mm:ss');
        $scope.model.outgoingmuximum = $filter('date')($scope.model.outgoingmuximum, 'MM/dd/yyyy HH:mm:ss');

        $scope.model.incomingAnsweredRingTimeMinimum = $filter('date')($scope.model.incomingAnsweredRingTimeMinimum, 'MM/dd/yyyy HH:mm:ss');
        $scope.model.incomingAnsweredRingTimeMaximum = $filter('date')($scope.model.incomingAnsweredRingTimeMaximum, 'MM/dd/yyyy HH:mm:ss');


        $scope.model.incomingUnansweredRingTimeMinimum = $filter('date')($scope.model.incomingUnansweredRingTimeMinimum, 'MM/dd/yyyy HH:mm:ss');
        $scope.model.incomingUnansweredRingTimeMaximum = $filter('date')($scope.model.incomingUnansweredRingTimeMaximum, 'MM/dd/yyyy HH:mm:ss');




        console.log($scope.model);

        reportService.getReport($scope.model).then(function (response) {



            if ($timeout) {



                vm.tableParams = null;

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

                




                // Chart Start


                if (response.data[1] == "Call Response Analysis") {

                    var callOffered = [];

                    for (var i = 0; i < data.length; i++) {
                        callOffered.push([data[i]['period'], data[i]['calls Offered']]);
                    }


                    $scope.callOffered =
                  [{

                      "label": "",
                      "color": "#5882FA",
                      "data": callOffered
                  }];



                    var answered = [];

                    for (var i = 0; i < data.length; i++) {
                        answered.push([data[i]['period'], data[i]['answered']]);
                    }


                    $scope.answered =
                  [{

                      "label": "",
                      "color": "#B45F04",
                      "data": answered
                  }];


                    var answeredInTarget = [];

                    for (var i = 0; i < data.length; i++) {
                        answeredInTarget.push([data[i]['period'], data[i]['answered In Target']]);
                    }


                    $scope.answeredInTarget =
                  [{

                      "label": "",
                      "color": "#04B45F",
                      "data": answeredInTarget
                  }];




                    var gos = [];

                    for (var i = 0; i < data.length; i++) {
                        gos.push([data[i]['period'], data[i]['gos']]);
                    }


                    $scope.gos =
                  [{

                      "label": "",
                      "color": "#FA8258",
                      "data": gos
                  }];


                }

                else if (response.data[1] == "Overall Activity by DDI") {



                    var callOffered = [];

                    for (var i = 0; i < data.length; i++) {
                        callOffered.push([data[i]['ddi'], data[i]['offered']]);
                    }


                    $scope.callOffered =
                  [{

                      "label": "",
                      "color": "#5882FA",
                      "data": callOffered
                  }];



                    var answered = [];

                    for (var i = 0; i < data.length; i++) {
                        answered.push([data[i]['ddi'], data[i]['answered']]);
                    }


                    $scope.answered =
                  [{

                      "label": "",
                      "color": "#B45F04",
                      "data": answered
                  }];


                    var answeredInTarget = [];

                    for (var i = 0; i < data.length; i++) {
                        answeredInTarget.push([data[i]['ddi'], data[i]['answered In Target']]);
                    }


                    $scope.answeredInTarget =
                  [{

                      "label": "",
                      "color": "#04B45F",
                      "data": answeredInTarget
                  }];




                    var gos = [];

                    for (var i = 0; i < data.length; i++) {
                        gos.push([data[i]['ddi'], data[i]['gos']]);
                    }


                    $scope.gos =
                  [{

                      "label": "",
                      "color": "#FA8258",
                      "data": gos
                  }];


                }
                else if (response.data[1] == "Division Call Activity") {


                    var totalCalls = [];

                    for (var i = 0; i < data.length; i++) {
                        totalCalls.push([data[i]['division'], data[i]['total Calls']]);
                    }

                    $scope.totalCalls =
                  [{

                      "label": "",
                      "color": "#5882FA",
                      "data": totalCalls
                  }];



                    var out = [];

                    for (var i = 0; i < data.length; i++) {
                        out.push([data[i]['division'], data[i]['out']]);
                    }


                    $scope.out =
                  [{

                      "label": "",
                      "color": "#B45F04",
                      "data": out
                  }];


                    var inCalls = [];

                    for (var i = 0; i < data.length; i++) {
                        inCalls.push([data[i]['division'], data[i]['in']]);
                    }


                    $scope.inCalls =
                  [{

                      "label": "",
                      "color": "#04B45F",
                      "data": inCalls
                  }];




                    var missed = [];

                    for (var i = 0; i < data.length; i++) {
                        missed.push([data[i]['division'], data[i]['missed']]);
                    }


                    $scope.missed =
                  [{

                      "label": "",
                      "color": "#FA8258",
                      "data": missed
                  }];


                }
            }


        },
       function (err) {
           $.toaster({ title: 'Error', priority: 'danger', message: err.data });
       });
    }

})();
//}]);

