'use strict';
angular.module('naut').controller('customerController', ['$scope', '$location', '$modal', '$window', 'customerService', 'localStorageService', 'ngAuthSettings', 'modalService', '$timeout',

    function ($scope, $location, $modal, $window, customerService, localStorageService, ngAuthSettings, modalService, $timeout) {

        $scope.model = {
            id: '0',
            cifBaseCIDRIMNo: '',
            loadCard: '',
            product: '',
            name: '',
            fatheName: '',
            motherName: '',
            gender: '',
            dob: '',
            age: '',
            martialST: '',
            hcResidenceAddress: '',
            zipCode: '',
            city: '',
            state: '',
            residenceAddress: '',
            residenceContactNumber: '',
            mobileContactNumber: '',
            employerName: '',
            employerCompanyAddress: '',
            positioninCompany: '',
            natureofBusiness: '',
            emailAddressCompany: '',
            refUAEName1: '',
            refUAEContactNumber1: '',
            refUAEName2: '',
            refUAEContactNumber2: '',
            cnicNo: '',
            passportNo: '',
            passportExpDate: '',
            hcResidenceContactNumbers: '',
            hcMobileContactNumbers: '',
            refHCContactNumbers: '',
            refHCAddress: '',
            emailAddress: '',
            clientId: '',

        };

        $scope.searchFunc = function () {
            $scope.search = {
                id: '0',
                cifBaseCIDRIMNo: '',
                loadCard: '',
                product: '',
                name: '',
                fatheName: '',
                motherName: '',
                gender: '',
                dob: '',
                age: '',
                martialST: '',
                hcResidenceAddress: '',
                zipCode: '',
                city: '',
                state: '',
                residenceAddress: '',
                residenceContactNumber: '',
                mobileContactNumber: '',
                employerName: '',
                employerCompanyAddress: '',
                positioninCompany: '',
                natureofBusiness: '',
                emailAddressCompany: '',
                refUAEName1: '',
                refUAEContactNumber1: '',
                refUAEName2: '',
                refUAEContactNumber2: '',
                cnicNo: '',
                passportNo: '',
                passportExpDate: '',
                hcResidenceContactNumbers: '',
                hcMobileContactNumbers: '',
                refHCContactNumbers: '',
                refHCAddress: '',
                emailAddress: '',
                clientId: '',
                pageSize: 10,
                page: 1

            };

            var authData = localStorageService.get('authorizationData');
            if (authData) {

                $scope.search.clientId = authData.clientId;

            }

        };



        $scope.system = {
            alertMsg: '',
            startRowNumber: 1,
            endRowNumber: 10
        };


        $scope.sysModel = {
            CreatedBy: ''
        };




        // real
        $scope.permissionAccessLevel = {
            isAdd: false,
            isEdit: false,
            isDelete: false,
        };



        $scope.searchFunc();

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


        $scope.filter = function () {

            $scope.getDataList(1);


        };


        $scope.getDataList = function (page) {

            $scope.search.page = page;

            customerService.getFilterCustomers($scope.search).then(function (response) {

                $scope.customerList = response.data[0];
                $scope.model.total = parseInt(response.data[1]);
                $scope.model.totalPage = parseInt(response.data[1] / $scope.search.pageSize);

            },
              function (err) {
              //    $.toaster({ title: 'Error', priority: 'danger', message: err.data });
              });

        }



        if ($location.path() == "/app/customers") {

            $scope.getDataList(1);



        }

        if ($location.path() == "/app/customer" || $location.path() == "/app/editcustomer") {

            var id = $location.search().id;

            if (id != 0) {

                customerService.getcustomerById(id, $scope.model.clientId).then(function (response) {

                    $scope.model.customerName = response.data.customerName;
                    $scope.model.id = response.data.id;


                    $scope.model = response.data;

                },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
            }



        }


        $scope.cancel = function () {

            window.location.href = '#/app/customers';
        },


        $scope.customerSubmit = function (isValid) {

            if (!isValid) {
                return false;
            }
            $scope.model.CreatedBy = $scope.sysModel.CreatedBy;
            customerService.customerSubmit($scope.model).then(function (response) {
                window.location.href = '#/app/customers';
            },
         function (err) {
            // $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };




        $scope.delete = function (id) {




            var modalOptions = {
                actionButtonText: 'OK',
                headerText: 'Confirm',
                bodyText: "Are you sure, you want to delete?"
            };

            modalService.showModal({}, modalOptions).then(function (result) {

                customerService.delete(id, $scope.model.clientId).then(function (response) {
                    $scope.customerList = response.data;
                },
             function (err) {
              //   $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

            });



        };

        $scope.reset = function () {

            $scope.searchFunc();

            $scope.getDataList(1);
        };


        $scope.pdf = function () {

            customerService.getPdfReport($scope.search).then(function (response) {

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

            customerService.getExcelReport($scope.search).then(function (response) {


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


        $scope.pagingAction = function (page) {


            $scope.system.startRowNumber = (page) * 10,
            $scope.system.endRowNumber = ((page) * 10) + 10;

            $scope.getDataList(page);
        };




    }
]);
