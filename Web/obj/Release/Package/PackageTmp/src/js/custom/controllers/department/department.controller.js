'use strict';
angular.module('naut').controller('departmentController', ['$scope', '$location', '$modal', '$window', 'departmentService', 'localStorageService', 'ngAuthSettings', 'modalService',

    function ($scope, $location, $modal, $window, departmentService, localStorageService, ngAuthSettings, modalService) {

        $scope.model = {
            id: "0",
            departmentName: "",
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


        if ($location.path() == "/app/departments") {

            departmentService.getDepartments($scope.model.clientId).then(function (response) {

                $scope.departmentList = response.data;


            },
             function (err) {
                  $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

        }
        

        if ($location.path() == "/app/department" || $location.path() == "/app/editdepartment") {

            var id = $location.search().id;

            if (id != 0) {

                departmentService.getDepartmentById(id, $scope.model.clientId).then(function (response) {

                    $scope.model.departmentName = response.data.departmentName;
                    $scope.model.id = response.data.id;

                },
             function (err) {
                  $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
            }



        }


        $scope.cancel = function () {

            window.location.href = '#/app/departments';
        },


        $scope.departmentSubmit = function (isValid) {

            if (!isValid) {
                return false;
            }

            departmentService.departmentSubmit($scope.model).then(function (response) {
                window.location.href = '#/app/departments';
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
                departmentService.delete(id, $scope.model.clientId).then(function (response) {
                    $scope.departmentList = response.data;
                },
         function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });
            });




        };





    }
]);
