'use strict';
angular.module('naut').controller('costcentreController', ['$scope', '$location', '$modal', '$window', 'costcentreService', 'localStorageService', 'ngAuthSettings', 'modalService',

    function ($scope, $location, $modal, $window, costcentreService, localStorageService, ngAuthSettings, modalService) {

        $scope.model = {
            id: "0",
            costCentreName: "",
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


        if ($location.path() == "/app/costcentres") {

            costcentreService.getCostCentres($scope.model.clientId).then(function (response) {

                $scope.costcentreList = response.data;


            },
             function (err) {
                  $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

        }

        
        if ($location.path() == "/app/costcentre" || $location.path() == "/app/editcostcentre") {

            var id = $location.search().id;

            if (id != 0) {


                

                costcentreService.getCostCentreById(id, $scope.model.clientId).then(function (response) {

                    $scope.model.costCentreName = response.data.costCentreName;
                    $scope.model.id = response.data.id;

                },
             function (err) {
                  $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
            }



        }


        $scope.cancel = function () {

            window.location.href = '#/app/costcentres';
        },


        $scope.costcentreSubmit = function (isValid) {
            if (!isValid) {
                return false;
            }

            costcentreService.costcentreSubmit($scope.model).then(function (response) {
                window.location.href = '#/app/costcentres';
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

                costcentreService.delete(id, $scope.model.clientId).then(function (response) {
                    $scope.costcentreList = response.data;
                },
         function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });



            });






        };





    }
]);
