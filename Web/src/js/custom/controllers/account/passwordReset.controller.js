'use strict';
angular.module('naut').controller('passwordResetController', ['$scope', '$location', '$modal', '$window', 'authService', 'localStorageService', 'ngAuthSettings',

    function ($scope, $location, $modal, $window, authService, localStorageService, ngAuthSettings) {

        $scope.model = {
            id: "0",
            email: "",
            password: "",
            confirmPassword: "",
            code: "",
            panel1: true,
            panel2: false,
            panel3: false,
        };


        $scope.emailVarification = function () {

            authService.emailVarification($scope.model.email).then(function (response) {

                $scope.model.panel1 = false;
                $scope.model.panel2 = true;
                $scope.model.panel3 = false;

            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };




        $scope.verifyCode = function () {

            authService.verifyCode($scope.model).then(function (response) {

                $scope.model.panel1 = false;
                $scope.model.panel2 = false;
                $scope.model.panel3 = true;

            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };


        $scope.resetPassword = function () {

            authService.resetPassword($scope.model).then(function (response) {
                window.location.href = '#/login';
            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };


    }
]);
