'use strict';
angular.module('naut').controller('loginController', ['$scope', '$location', '$modal', '$window', 'authService', 'localStorageService', 'ngAuthSettings', '$remember', '$timeout', 'modalService', '$forget',


    function ($scope, $location, $modal, $window, authService, localStorageService, ngAuthSettings, $remember, $timeout, modalService, $forget) {

        $scope.loginData = {
            userName: "",
            password: "",
            rememberMe: false,
            useRefreshTokens: false,
            isLogOffUser: false,
            message: ""
        };


        if (authService.authentication.isAuth) {
            
            var authData = localStorageService.get('authorizationData');

            if (authData.userType == 1) {
                $location.path('/app/companies');
            }
            else {
                $location.path('/app/dashboard');
            }


            

            //if ($location.$$path != '/dashboard ') {
            //    $location.path('/dashboard');
            //} else {
            //    $location.path('/login');
            //}
        }
        else {

            var loginData = $remember.get('login');


            if (loginData) {
                $scope.loginData.rememberMe = Boolean(loginData.split(',')[2]);
                if ($scope.loginData.rememberMe) {

                    $scope.loginData.userName = loginData.split(',')[0]
                    $scope.loginData.password = loginData.split(',')[1]

                }
                else {
                    $forget('login');
                    $scope.loginData.userName = "";
                    $scope.loginData.password = "";
                    $scope.loginData.rememberMe = false;
                }

            }
        }


        $scope.message = "";

        $scope.login = function () {
            localStorageService.remove('authorizationData');
            if ($scope.loginData.userName != '' && $scope.loginData.userName != '') {
                $scope.loginData.isLogOffUser = false;



                authService.login($scope.loginData).then(function (response) {


                    if ($scope.loginData.rememberMe) {
                        
                        $remember.set('login', $scope.loginData.userName + ',' + $scope.loginData.password + ',' + $scope.loginData.rememberMe);
                    }
                    else {
                        $forget('login');
                    }

                    var authData = localStorageService.get('authorizationData');
                    if (authData.userType == 1) {
                        $location.path('/app/companies');
                    }
                    else {
                        $location.path('/app/dashboard');
                    }


                },
             function (err) {

                 console.log(err);

                 $.toaster({ title: 'Error', priority: 'danger', message: err.error_description });

             });
            }
        };



    }
]);
