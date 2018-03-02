'use strict';
angular.module('naut').controller('dashboardController', ['$scope', '$location', '$modal', '$window', 'dashboardService', 'localStorageService', 'ngAuthSettings', 'modalService', '$timeout', 'flotOptions', 'colors', '$rootScope', 'backendHubProxy',

    function ($scope, $location, $modal, $window, dashboardService, localStorageService, ngAuthSettings, modalService, $timeout, flotOptions, colors, $rootScope, backendHubProxy) {

        $scope.chartBarFlotChart = flotOptions['bar'];

        $scope.model = {
            external: 0,
            internal: 0,
            totalAnswered: 0,
            totalCalls: 0,
            totalIn: 0,
            totalMissed: 0,
            totalOut: 0,

        };

        $scope.pieChart = {
            external: 0,
            internal: 0,
            totalIn: 0,
            totalOut: 0,
        };

        var iCustomer = 0;
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            iCustomer = authData.iCustomer;
        }


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


        //$scope.piePercent2 = 50;
        //$scope.piePercent1 = 10;

        $scope.pieOptions = {
            animate: {
                duration: 700,
                enabled: true
            },
            barColor: colors.byName('info'),
            // trackColor: colors.byName('inverse'),
            scaleColor: false,
            lineWidth: 20,
            lineCap: 'circle'
        };



        dashboardService.getDashboard(iCustomer).then(function (response) {

            $scope.model = response.data.table0[0];
            $scope.grid = response.data.table1;

            $scope.gridAlertMsg = response.data.table1.length > 0 ? "" : 'No records were found!';


            // bar chart start

            var chartCalls = [];
            var data = response.data.table2;

            for (var i = 0; i < data.length; i++) {
                chartCalls.push([data[i]['period'], data[i]['in']]);
            }

            $scope.barChartCalls =
                [{

                    "label": "",
                    "color": "#5882FA",
                    "data": chartCalls
                }];

            // bar chart end



            $scope.pieChart.totalIn = parseInt($scope.model.totalCalls) != 0 ? parseInt(($scope.model.totalIn / $scope.model.totalCalls) * 100) : 0;
            $scope.pieChart.totalOut = parseInt($scope.model.totalOut) != 0 ? parseInt(($scope.model.totalOut / $scope.model.totalCalls) * 100) : 0;
            $scope.pieChart.internal = parseInt($scope.model.internal) != 0 ? parseInt(($scope.model.internal / $scope.model.totalCalls) * 100) : 0;
            $scope.pieChart.answered = parseInt($scope.model.totalAnswered) != 0 ? parseInt(($scope.model.totalAnswered / $scope.model.totalCalls) * 100) : 0;

            $scope.users = response.data.table3;


        },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });



        dashboardService.getActiveCalls(iCustomer).then(function (response) {
            $scope.activeCallsList = response.data;

            $scope.alertMsg = response.data.length > 0 ? "" : 'No records were found!';


        },
        function (err) {
            $.toaster({ title: 'Error', priority: 'danger', message: err.data });
        });

        console.log('trying to connect to service Hub')
        var performanceDataHub = backendHubProxy(backendHubProxy.defaultServer, 'messagesHub');
        console.log('connected to service Hub')


        performanceDataHub.on('updateActiveCalls', function (data) {

            console.log("Get Active Calls:" + data);
            $scope.$apply(function () {

                //console.log("Get Active Calls:" + data);

            ///    $scope.activeCallsList = data;

             //   $scope.alertMsg =  data != "" || data.length > 0 ? "" : 'No records were found!';


            });

        });

    }
]);
