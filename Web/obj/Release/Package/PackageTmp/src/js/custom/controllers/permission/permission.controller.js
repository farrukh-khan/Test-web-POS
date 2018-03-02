'use strict';
angular.module('naut').controller('permissionController', ['$scope', '$location', '$modal', '$window', 'permissionService', 'localStorageService', 'ngAuthSettings', 'modalService',

    function ($scope, $location, $modal, $window, permissionService, localStorageService, ngAuthSettings, modalService) {

        $scope.model = {
            roleId: 1,
            parameters: []
        };


        permissionService.getPermissions().then(function (response) {

            $scope.roleList = response.data[0];
            $scope.permissionList = response.data[1];


        },
         function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });





        $scope.permissionSubmit = function () {

            permissionService.permissionSubmit($scope.permissionList).then(function (response) {
                $.toaster({ title: 'Message', priority: 'success', message: "Permission save successfully." });
            },
         function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };




        $scope.change = function () {



            permissionService.getPermissions($scope.model.roleId).then(function (response) {

                $scope.permissionList = response.data[1];

                

            },
             function (err) {
                  $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });


        };


    }
]);
