'use strict';
angular.module('naut').controller('customerAgentController', ['$scope', '$location', '$modal', '$window', 'customerAgentService', 'localStorageService', 'ngAuthSettings', 'modalService', 'authService', 'customerService',

    function ($scope, $location, $modal, $window, customerAgentService, localStorageService, ngAuthSettings, modalService, authService, customerService) {

        $scope.modelFunc = function () {
            $scope.model = {

                id: '0',
                clientId: '',
                custoemrId: '0',
                pageSize: 10,
                page: 0,
                customerName: '',
                cifNo:'',
                customerAgentDetail: []

            };
            var authData = localStorageService.get('authorizationData');
            if (authData) {

                $scope.model.clientId = authData.clientId;                

            }

        };

        $scope.modelFunc();


        $scope.sysModel = {
            CreatedBy: ''
        };


        $scope.system = {
            alertMsg: '',
            startRowNumber: 1,
            endRowNumber: 10
        };


        // real
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
            $scope.sysModel.CreatedBy = authData.userName;


        }



        $scope.getAllAgentCustomers = function (page) {

            $scope.model.page = page;
            customerAgentService.getcustomerAgents($scope.model).then(function (response) {

                
                $scope.customerAgentList = response.data[0];
                $scope.model.total = parseInt(response.data[1]);
                $scope.model.totalPage = parseInt(response.data[1] / $scope.model.pageSize);

            },
    function (err) {
        $.toaster({ title: 'Error', priority: 'danger', message: err.data });
    });


        };

        $scope.getAllAgentCustomers(1);

        authService.getUsers($scope.model.clientId).then(function (response) {

            $scope.userList = response.data;

        },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        $scope.filter = function () {
            $scope.getAllAgentCustomers(1);
        },



        $scope.cancel = function () {
            $scope.modelFunc();
            window.location.href = '#/app/customerAgents';
        },


        $scope.customerAgentSubmit = function () {
            
            $scope.model.CreatedBy = $scope.sysModel.CreatedBy;
            $scope.model.customerAgentDetail = $scope.customerAgentList;

            console.log($scope.model);
            customerAgentService.customerAgentSubmit($scope.model).then(function (response) {

                $scope.customerAgentList = response.data;

                $.toaster({ title: 'Info', priority: 'success', message: "Record Saved Successfully." });
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

                customerAgentService.delete(id, $scope.model.clientId).then(function (response) {
                    $scope.customerAgentList = response.data;
                },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

            });



        };

        $scope.reset = function () {
            $scope.modelFunc();
            $scope.getAllAgentCustomers(1);
           
        };



        $scope.pagingAction = function (page) {

            $scope.system.startRowNumber = (page - 1) * 10,
        $scope.system.endRowNumber = ((page - 1) * 10) + 10;

            $scope.getAllAgentCustomers(page);
        };



    }
]);
