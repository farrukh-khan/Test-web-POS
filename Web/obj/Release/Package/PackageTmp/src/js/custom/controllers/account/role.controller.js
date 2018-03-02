'use strict';
angular.module('naut').controller('roleController', ['$scope', '$location', '$modal', '$window', 'roleService', 'localStorageService', 'ngAuthSettings', 'modalService',

    function ($scope, $location, $modal, $window, roleService, localStorageService, ngAuthSettings, modalService) {

        $scope.model = {
            id: "0",
            roleName: "",
            clientId: 0
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
            $scope.model.clientId = authData.clientId;
        }



        if ($location.path() == "/app/roles") {

            roleService.getRoles($scope.model.clientId).then(function (response) {
                $scope.roleList = response.data;
            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
        }

        if ($location.path() == "/app/role" || $location.path() == "/app/editrole") {

            var id = $location.search().id;

            if (id != 0) {

                roleService.getRoleById(id, $scope.model.clientId).then(function (response) {

                    $scope.model.roleName = response.data.roleName;
                    $scope.model.id = response.data.id;

                },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
            }

        }


        $scope.cancel = function () {

            window.location.href = '#/app/roles';
        },


        $scope.roleSubmit = function (isValid) {

            if (!isValid) {
                return false;
            }

            roleService.roleSubmit($scope.model).then(function (response) {



                window.location.href = '#/app/roles';
            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };




        $scope.delete = function (id) {


            var modalOptions = {
                actionButtonText: 'OK',
                headerText: 'Confirm',
                bodyText: "Are you sure, you want to delete?"
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                roleService.delete(id, $scope.model.clientId).then(function (response) {
                    $scope.roleList = response.data;
                },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });
            });




        };





    }
]);
