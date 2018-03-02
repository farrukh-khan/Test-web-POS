'use strict';
angular.module('naut').controller('systemSettingController', ['$scope', '$location', '$modal', '$window', 'systemSettingService', 'localStorageService', 'ngAuthSettings', 'modalService',

    function ($scope, $location, $modal, $window, systemSettingService, localStorageService, ngAuthSettings, modalService) {

        $scope.model = {
            iCustomer: 0,
            answerTime: 0,
            localCode: 0
        };


        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $scope.model.iCustomer = authData.iCustomer;
        }


        



        systemSettingService.getSystemSettings($scope.model.iCustomer).then(function (response) {

            

            if (response.data != null)
            {
                $scope.model = response.data;

                console.log($scope.model);

            }
            

        },
         function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });







        $scope.systemSettingSubmit = function (isValid) {

            if (!isValid) {
                return false;
            }

            systemSettingService.systemSettingSubmit($scope.model).then(function (response) {
                $.toaster({ title: 'Message', priority: 'success', message: "Permission save successfully." });
            },
         function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };






    }
]);
