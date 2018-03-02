'use strict';
angular.module('naut').controller('reportCatalogueController', ['$scope', '$location', '$modal', '$window', 'reportService', 'localStorageService', 'ngAuthSettings',

    function ($scope, $location, $modal, $window, reportService, localStorageService, ngAuthSettings) {

        
        reportService.getReportCatalogues().then(function (response) {
            $scope.list = response.data;
        },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });




    }
]);
