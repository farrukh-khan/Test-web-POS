'use strict';
angular.module('naut').controller('logController', ['$scope', '$location', '$modal', '$window', 'logService', 'localStorageService', 'ngAuthSettings', 'modalService', 'authService', '$filter',

    function ($scope, $location, $modal, $window, logService, localStorageService, ngAuthSettings, modalService, authService, $filter) {


        $scope.search = {
            email: "",
            to: '',
            from: '',
        };

        $scope.permissionAccessLevel = {
            isAdd: false,
            isEdit: false,
            isDelete: false,
        };


        var permLevel = localStorageService.get('permissionData');
        if (permLevel) {

            $scope.permissionAccessLevel = permLevel;
        }

        var authData = localStorageService.get('authorizationData');
        if (authData) {

            $scope.search.clientId = authData.clientId;

        }


        authService.getUsers($scope.search.clientId).then(function (response) {

            $scope.userList = response.data;

        },
    function (err) {
        $.toaster({ title: 'Error', priority: 'danger', message: err.data });
    });

        $scope.reset = function () {


            $scope.search = {
                email: "",
                to: 0,
                from: '',
            };

        };



        $scope.filter = function () {

            logService.getlogs($scope.search).then(function (response) {

                $scope.logList = response.data;

            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });


        };


        $scope.formatDate = function (date) {

            return $filter('date')(new Date(date), 'dd-MM-yyyy');

        };



    }
]);
