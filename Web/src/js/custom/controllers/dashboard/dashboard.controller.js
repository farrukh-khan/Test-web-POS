/**=========================================================
 * Module: DashboardController.js
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('naut')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$rootScope', '$scope', 'colors', 'flotOptions', '$timeout', 'localStorageService', 'dashboardService', 'initService'];

    function DashboardController($rootScope, $scope, colors, flotOptions, $timeout, localStorageService, dashboardService, initService) {


        $scope.sys = {
            fsYear: '2016_2017_BE',
            grantId: 1,
            month: new Date().getMonth(),
            type: 1,
            status:1,
        };

        $scope.model = {
            receiptsTotal: 0,
            receiptsUsed: 0,
            receiptPercentage: 0,

            salaryTotal: 0,
            salaryUsed: 0,
            salaryPercentage: 0,


            capitalTotal: 0,
            capitalUsed: 0,
            capitalPercentage: 0,

            roeTotal: 0,
            roeUsed: 0,
            roePercentage: 0,
            transfered: 0,


            post: 0,
            filled: 0,
            diffPost: 0,

        };
             


        // Some numbers for demo
        $scope.loadProgressValues = function () {
            $scope.progressVal = [0, 0, 0, 0];
            // helps to show animation when values change
            $timeout(function () {
                $scope.progressVal[0] = 60;
                $scope.progressVal[1] = 34;
                $scope.progressVal[2] = 22;
                $scope.progressVal[3] = 76;
            });
        };

        $scope.chartBarStackedFlotChart = flotOptions['bar-stacked'];
        $scope.chartBarFlotChart = flotOptions['bar'];


        $scope.pieOptions = {
            animate: {
                duration: 700,
                enabled: true
            },
            barColor: colors.byName('info'),
            // trackColor: colors.byName('inverse'),
            scaleColor: false,
            lineWidth: 10,
            lineCap: 'circle'
        };

        /*
        // get fs year list 


        // fucntions start
        $scope.dashboardValulesFunc = function () {
            /// dashboardValules

            /// Dashbaord 
            dashboardService.getDashboard($scope.sys).then(function (response) {

                $scope.model = response.data[0];

            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };



        $scope.RoeChartFunc = function () {
            /// Roe Chart

            $scope.sys.status = 1;
            dashboardService.getChartReport($scope.sys).then(function (response) {


                var arr = [];

                $scope.roeChart = [];
                var dataset = response.data;
                for (var i = 0; i < dataset.length; i++) {

                    var obj = [];
                    obj.push(dataset[i].district);
                    obj.push(dataset[i].amount);
                    arr.push(obj);
                }

                $scope.roeChart = [{
                    "label": "ROE",
                    "color": "#4caf50",
                    "data": arr
                }];


                //var arr = [];

                //$scope.roeChart = [];
                //var dataset = response.data;
                //for (var i = 0; i < dataset.length; i++) {

                //    var detail = [];

                //    for (var j = 0; j < dataset[i].data.length; j++) {
                //        var obj = [];
                //        obj.push(dataset[i].data[j].key);
                //        obj.push(dataset[i].data[j].value);
                //        detail.push(obj);
                //    }

                //    var master = {
                //        "label": dataset[i].label,
                //        "resize": true,
                //        "color": dataset[i].color,
                //        "data": detail
                //    };

                //    arr.push(master);
                //}

                //$scope.roeChart = arr;

            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        };



        $scope.capitalChartFunc = function () {
            /// capital Chart

            $scope.sys.status = 2;
            dashboardService.getChartReport($scope.sys).then(function (response) {

                var arr = [];

                $scope.capitalChart = [];
                var dataset = response.data;
                for (var i = 0; i < dataset.length; i++) {

                    var obj = [];
                    obj.push(dataset[i].district);
                    obj.push(dataset[i].amount);
                    arr.push(obj);
                }

                $scope.capitalChart = [{
                    "label": "Capital",
                    "color": "#ec008c",
                    "data": arr
                }];

            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        };


        $scope.polChartFunc = function () {

            // pol
            $scope.sys.status = 3;
            dashboardService.getChartReport($scope.sys).then(function (response) {

                var arr = [];

                $scope.polChart = [];
                var dataset = response.data;
                for (var i = 0; i < dataset.length; i++) {

                    var obj = [];
                    obj.push(dataset[i].district);
                    obj.push(dataset[i].amount);
                    arr.push(obj);
                }

                $scope.polChart = [{
                    "label": "POL",
                    "color": "#FFBE41",
                    "data": arr
                }];



            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        };



        $scope.getPettyCashChartFunc = function () {


            // pettycash
            $scope.sys.status = 4;
            dashboardService.getChartReport($scope.sys).then(function (response) {

                var arr = [];

                $scope.pettyCashChart = [];
                var dataset = response.data;
                for (var i = 0; i < dataset.length; i++) {

                    var obj = [];
                    obj.push(dataset[i].district);
                    obj.push(dataset[i].amount);
                    arr.push(obj);
                }

                $scope.pettyCashChart = [{
                    "label": "Petty Cash",
                    "color": "#F57035",
                    "data": arr
                }];






            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        };




        $scope.getTadaChartChartFunc = function () {


            // tada
            $scope.sys.status = 5;
            dashboardService.getChartReport($scope.sys).then(function (response) {

                var arr = [];

                $scope.tadaChart = [];
                var dataset = response.data;
                for (var i = 0; i < dataset.length; i++) {

                    var obj = [];
                    obj.push(dataset[i].district);
                    obj.push(dataset[i].amount);
                    arr.push(obj);
                }

                $scope.tadaChart = [{
                    "label": "TADA",
                    "color": "#ff3e43",
                    "data": arr
                }];


            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        };





        $scope.getDistrictDetailFunc = function () {


            districtDetailList
            dashboardService.getDistrictDetail($scope.sys).then(function (response) {

                $scope.districtDetailList = response.data;
            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        };





        // functions end



        $scope.pageLoad = function () {

            $scope.dashboardValulesFunc();
            $scope.RoeChartFunc();
            $scope.capitalChartFunc();
            $scope.polChartFunc();
            $scope.getPettyCashChartFunc();
            $scope.getTadaChartChartFunc();
            //     $scope.getDistrictDetailFunc();

        };



        $scope.$watch('fsYear', function (newValue, oldValue) {
            $scope.sys.fsYear = newValue;
            $scope.pageLoad();
        });

        $scope.$watch('grantId', function (newValue, oldValue) {
            $scope.sys.grantId = newValue;
            $scope.pageLoad();
        });


        $scope.$watch('month', function (newValue, oldValue) {
            $scope.sys.month = newValue;
            $scope.pageLoad();



        });


        $scope.$watch('type', function (newValue, oldValue) {
            $scope.sys.type = newValue;
            $scope.pageLoad();
        });

        */

        // Dashboard charts
        // ----------------------------------- 

        // Spline chart
        $scope.splineChartOpts = angular.extend({}, flotOptions['default'], {
            series: {
                lines: {
                    show: false
                },
                splines: {
                    show: true,
                    tension: 0.4,
                    lineWidth: 2,
                    fill: 0.5
                },
            },
            yaxis: { max: 50 }
        });
        $scope.splineData = getSplineData();

        function getSplineData() {
            return [{
                'label': 'Capital',
                'color': colors.byName($scope.app.theme.name),
                'data':

               [
    ["Jan", 121203234],
    ["Feb", 121203234],
    ["Mar", 111203234]
               ]




            }



            ];
        }

        $scope.$watch('app.theme.name', function (val) {
            $scope.splineData = getSplineData();
        });


    }
})();

