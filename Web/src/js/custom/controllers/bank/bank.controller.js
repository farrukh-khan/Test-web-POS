'use strict';
angular.module('naut').controller('bankController', ['$scope', '$location', '$modal', '$window', 'bankService', 'localStorageService', 'ngAuthSettings', 'modalService', '$timeout',

    function ($scope, $location, $modal, $window, bankService, localStorageService, ngAuthSettings, modalService, $timeout) {

        $scope.model = {
            id: "0",
            name: "",
            clientId: 0,
            country: '',
            code: '',
            address: '',
            contactPerson: '',
            contactNo: '',
            emailAddress: '',
            CreatedBy: '',
            pageSize: 10,
            page: 1,

        };


        $scope.searchModel = function () {
            $scope.search = {

                name: "",
                clientId: 0,
                country: '',
                code: '',
                address: '',
                contactPerson: '',
                emailAddress: '',
                pageSize: 10,
                page: 1,
            };


            var authData = localStorageService.get('authorizationData');
            if (authData) {
                $scope.search.clientId = authData.clientId;
            }



        };

        $scope.searchModel();


        $scope.sysModel = {
            CreatedBy: ''
        };

        $scope.system = {
            alertMsg: '',
            startRowNumber: 1,
            endRowNumber: 10
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
            $scope.search.clientId = authData.clientId;
            $scope.sysModel.CreatedBy = authData.userName;
        }



        $scope.getDataList = function (page) {

            $scope.search.page = page;


            bankService.getFilterBanks($scope.search).then(function (response) {

                $scope.bankList = response.data[0];
                $scope.model.total = parseInt(response.data[1]);
                $scope.model.totalPage = parseInt(response.data[1] / $scope.search.pageSize);

            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });


        };


        if ($location.path() == "/app/banks") {

            $scope.getDataList(1);
        }


        if ($location.path() == "/app/bank" || $location.path() == "/app/editbank") {



            var id = $location.search().id;

            if (id != 0) {


                bankService.getbankById(id, $scope.model.clientId).then(function (response) {

                    $scope.model = response.data;

                },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });


            }



        }


        $scope.cancel = function () {

            window.location.href = '#/app/banks';
        };



        $scope.reset = function () {

            $scope.searchModel();
            $scope.getDataList(1);

        };





        $scope.filter = function () {

            $scope.getDataList($scope.search.page);

        };

        $scope.pdf = function () {

            bankService.getPdfReport($scope.search).then(function (response) {

                var tabWindowId = window.open('about:blank', '_blank');
                var blob = new Blob([response.data], { type: "application/pdf" });
                var objectUrl = URL.createObjectURL(blob);
                tabWindowId.location.href = objectUrl;


            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

        };


        $scope.excel = function () {

            bankService.getExcelReport($scope.search).then(function (response) {

                if ($timeout) {
                    var tabWindowId = window.open('about:blank', '_blank');
                    var blob = new Blob([response.data], { type: "application/vnd.ms-excel" });
                    var objectUrl = URL.createObjectURL(blob);
                    tabWindowId.location.href = objectUrl;
                }

            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });


        };


        $scope.bankSubmit = function (isValid) {
            if (!isValid) {
                return false;
            }

            $scope.model.CreatedBy = $scope.sysModel.CreatedBy;
            bankService.bankSubmit($scope.model).then(function (response) {
                window.location.href = '#/app/banks';
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

                $scope.model.CreatedBy = $scope.sysModel.CreatedBy;

                bankService.delete(id, $scope.model.clientId).then(function (response) {
                    $scope.bankList = response.data;
                },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });



            });



        };


        $scope.pagingAction = function (page) {

            $scope.system.startRowNumber = (page - 1) * 10,
        $scope.system.endRowNumber = ((page - 1) * 10) + 10;

            $scope.getDataList(page);
        };


    }
]);
