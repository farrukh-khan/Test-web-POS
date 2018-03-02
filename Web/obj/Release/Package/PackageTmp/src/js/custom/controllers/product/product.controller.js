'use strict';
angular.module('naut').controller('productController', ['$scope', '$location', '$modal', '$window', 'productService', 'localStorageService', 'ngAuthSettings', 'modalService', '$filter',

    function ($scope, $location, $modal, $window, productService, localStorageService, ngAuthSettings, modalService, $filter) {

        $scope.model = {
            selectedDDIList: [],
            selectedExtensionList: [],
            iCustomer: 0,
        };


        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $scope.model.iCustomer = authData.iCustomer;
        }


        productService.getProducts($scope.model.iCustomer).then(function (response) {

            $scope.productList = response.data.table0;
            $scope.ddlProductList = [];
            $scope.extensionProductList = [];

            var ddl = [];
            for (var i = 0; i < response.data.table1.length; i++) {

                ddl.push(response.data.table1[i].id);
            }

            var extensions = [];
            for (var i = 0; i < response.data.table2.length; i++) {

                extensions.push(response.data.table2[i].id);
            }

            for (var i = 0; i < $scope.productList.length; i++) {


                if (ddl.indexOf($scope.productList[i].id) != -1) {
                    $scope.model.selectedDDIList.push($scope.productList[i]);
                }
                



                if (extensions.indexOf($scope.productList[i].id) != -1) {
                    $scope.model.selectedExtensionList.push($scope.productList[i]);
                }

            }


        },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        $scope.productSubmit = function () {

            productService.productSubmit($scope.model).then(function (response) {
                $.toaster({ title: 'Message', priority: 'success', message: "Product save successfully." });
            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };





    }
]);
