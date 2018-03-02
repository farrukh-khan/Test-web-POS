'use strict';
angular.module('naut').
    controller('productController',
            ['$scope', '$location', '$modal', '$window', 'localStorageService', 'ngAuthSettings', 'modalService', '$timeout', 'productService', 'categoryService',

    function ($scope, $location, $modal, $window, localStorageService, ngAuthSettings, modalService, $timeout, productService, categoryService) {



        // system setting start
        $scope.model = {
            id: 0, code: '', name: '', description: '', saleRate: 0, companyId: '', CreatedBy: '', categoryId: 0
        };


        $scope.searchModel = function () {
            $scope.search = {
                id: '0',
                name: "",
                companyId: 0,
                pageSize: 10,
                page: 1,
                totalPage: 0,
                categoryId: 0

            };
        };


        $scope.searchModel();
        $scope.system = {
            alertMsg: '',
            startRowNumber: 1,
            endRowNumber: 10
        };


        $scope.permissionAccessLevel = {
            isAdd: true,
            isEdit: true,
            isDelete: true,
        };


        var permLevel = localStorageService.get('permissionData');
        if (permLevel) {
            $scope.permissionAccessLevel = permLevel;
        }

        $scope.authData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {

                $scope.search.companyId = authData.clientId;
                $scope.model.companyId = authData.clientId;

                $scope.model.CreatedBy = authData.userName;
            }
        };

        $scope.authData();
        // system setting end



        if ($location.path() == "/app/product-list") {

            $scope.queryString = encodeString('0');


            $scope.getDataList = function (page) {

                $scope.search.page = page;


                productService.getProducts($scope.search).then(function (response) {

                    $scope.ProductList = response.data.table0;
                    $scope.search.totalPage = (parseInt(response.data.table1[0].totalRecord) / $scope.search.pageSize) * 10;
                },
                 function (err) {
                     $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                 });

            };

            $scope.edit = function (id) {

                var encode = encodeString(id);
                window.location.href = '#/app/edit-product?id=' + encode;
            };


            $scope.pagingAction = function (page) {

                $scope.system.startRowNumber = (page) * 10,
                $scope.system.endRowNumber = ((page) * 10) + 10;
                $scope.getDataList(page);
            };

            $scope.getDataList(1);
        }



        if ($location.path() == "/app/product" || $location.path() == "/app/edit-product") {


            $scope.getProduct = function () {

                $scope.cateSearch = {
                    id: '0',
                    Search: "",
                    companyId: $scope.model.companyId,
                    pageSize: -1
                };

                categoryService.getcategorys($scope.cateSearch).then(function (response) {

                    $scope.cateList = response.data.table0;

                    


                    var id = decodeString($location.search().id);

                    if (id != 0) {

                        $scope.search.id = id;
                        productService.getProducts($scope.search).then(function (response) {
                            $scope.model = response.data.table0[0];


                        },
                         function (err) {
                             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                         });

                    }


                },
 function (err) {
     $.toaster({ title: 'Error', priority: 'danger', message: err.data });
 });




            };
            $scope.cancel = function () {

                window.location.href = '#/app/product-list';
            };
            $scope.productSubmit = function (isValid) {


                if (!isValid) {
                    return false;
                }
                $scope.authData();
                productService.productSubmit($scope.model).then(function (response) {
                    window.location.href = '#/app/product-list';
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
                    productService.delete(id, $scope.model.clientId).then(function (response) {
                        $scope.ProductList = response.data;
                    },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
                });

            };
            $scope.reset = function () {

                //$scope.searchFunc();
                //productService.getProducts($scope.model.clientId).then(function (response) {

                //    $scope.ProductList = response.data;
                //},
                // function (err) {
                //     $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                // });

            };

            $scope.getProduct();

        }






    }
            ]);


