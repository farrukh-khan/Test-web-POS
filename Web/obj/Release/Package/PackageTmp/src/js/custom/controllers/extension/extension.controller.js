'use strict';
angular.module('naut').controller('extensionController', ['$scope', '$location', '$modal', '$window', 'extensionService', 'localStorageService', 'ngAuthSettings',

    function ($scope, $location, $modal, $window, extensionService, localStorageService, ngAuthSettings) {

        $scope.model = {
            extentionId: 0,
            id: "",
            name: "",
            divisionId: 0,
            departmentId: 0,
            costCentreId: 0,
            page: 1,
            pageSize: 10,
            total: 0,
            iCustomer: 0
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


        var iCustomer = 0;
        var clientId = 0;
        var authData = localStorageService.get('authorizationData');
        if (authData) {
           $scope.model.iCustomer = authData.iCustomer;
            clientId = authData.clientId;
        }


        if ($location.path() == "/app/extensions") {

            BindDataList($scope, extensionService);
        }

        if ($location.path() == "/app/extension") {

            var id = $location.search().id;

            if (id != 0) {

                extensionService.getextensionById(id, iCustomer, clientId).then(function (response) {



                    $scope.divisionList = response.data[1];
                    $scope.departmentList = response.data[2];
                    $scope.costCentreList = response.data[3];
                    $scope.model = response.data[0];

                },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
            }



        }


        //$scope.ascSort = function (index) {

        //    var rowName = "";
        //    rowName = $scope.getKeys($scope.reportDetailHeader);
        //    rowName = rowName[index].toString()
        //    $scope.reportDetailBody = $filter("orderBy")($scope.reportDetailBody, rowName)

        //}

        //$scope.descSort = function (index) {

        //    var rowName = "";
        //    rowName = $scope.getKeys($scope.reportDetailHeader);
        //    rowName = rowName[index].toString()
        //    $scope.reportDetailBody = $filter("orderBy")($scope.reportDetailBody, "-" + rowName)

        //};

        $scope.pagingAction = function (page) {

            $scope.model.page = page;

            BindDataList($scope, extensionService);

        };





        $scope.cancel = function () {

            window.location.href = '#/app/extensions';
        },


        $scope.extensionSubmit = function (isValid) {
            if (!isValid) {
                return false;
            }
            extensionService.extensionSubmit($scope.model).then(function (response) {
                window.location.href = '#/app/extensions';
            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };




        $scope.delete = function (id) {

            extensionService.delete(id).then(function (response) {
                $scope.extensionList = response.data;
            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };





    }




]);
function BindDataList($scope, extensionService) {

    extensionService.getextensions($scope.model).then(function (response) {


        $scope.extensionList = response.data.table0;
        $scope.model.total = response.data.table1[0]["totalRows"];
    },
            function (err) {
                $.toaster({ title: 'Error', priority: 'danger', message: err.data });
            });

}



