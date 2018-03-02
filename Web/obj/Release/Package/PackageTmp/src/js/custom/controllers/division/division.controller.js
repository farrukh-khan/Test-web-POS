'use strict';
angular.module('naut').controller('divisionController', ['$scope', '$location', '$modal', '$window', 'divisionService', 'localStorageService', 'ngAuthSettings', 'modalService',

    function ($scope, $location, $modal, $window, divisionService, localStorageService, ngAuthSettings, modalService) {

        $scope.model = {
            id: "0",
            divisionName: "",
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


        if ($location.path() == "/app/divisions") {

            divisionService.getDivisions($scope.model.clientId).then(function (response) {

                $scope.divisionList = response.data;


            },
             function (err) {
                  $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

        }
        
        if ($location.path() == "/app/division" || $location.path() == "/app/editdivision") {

            var id = $location.search().id;

            if (id != 0) {

                divisionService.getDivisionById(id, $scope.model.clientId).then(function (response) {

                    $scope.model.divisionName = response.data.divisionName;
                    $scope.model.id = response.data.id;

                },
             function (err) {
                  $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
            }



        }


        $scope.cancel = function () {

            window.location.href = '#/app/divisions';
        },


        $scope.divisionSubmit = function (isValid) {
            if (!isValid) {
                return false;
            }

            divisionService.divisionSubmit($scope.model).then(function (response) {
                window.location.href = '#/app/divisions';
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

                divisionService.delete(id, $scope.model.clientId).then(function (response) {
                    $scope.divisionList = response.data;
                },
             function (err) {
                  $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

            });



        };


    }
]);
