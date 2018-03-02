'use strict';
angular.module('naut').
    controller('productGroupController',
            ['$scope', '$location', '$modal', '$window', 'localStorageService', 'ngAuthSettings', 'modalService', '$timeout', 'productGroupService', 'productService', '$filter', 'FileUploader',

    function ($scope, $location, $modal, $window, localStorageService, ngAuthSettings, modalService, $timeout, productGroupService, productService, $filter, FileUploader) {

        // system setting start
        $scope.model = {
            id: '', code: '', name: '', description: '', sRate: 0, companyId: '', CreatedBy: '', products: []
            , img: ''


        };

        $scope.searchModel = function () {
            $scope.search = {
                id: '0',
                Search: "",
                companyId: ''
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



        if ($location.path() == "/app/product-group-list") {

            $scope.queryString = encodeString('0');


            $scope.getDataList = function () {

                productGroupService.getProductGroups($scope.search).then(function (response) {
                    $scope.productGroupList = response.data;
                    //$scope.search.totalPage = (parseInt(response.data.table1[0].totalRecord) / $scope.search.pageSize) * 10;
                },
                 function (err) {
                     $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                 });

            };

            $scope.encode = function (id) {
                return encodeString(id);
            };

            $scope.searchFunc = function () {
                $scope.getDataList();
            };




            $scope.edit = function (id) {

                var encode = encodeString(id);
                window.location.href = '#/app/edit-product-group?id=' + encode;
            };



            $scope.getDataList();
        }



        if ($location.path() == "/app/product-group" || $location.path() == "/app/edit-product-group") {


            $scope.productList = [];

            $scope.multiple = {
                selectedTest: [],
            };




            $scope.getproductGroup = function () {


                $scope.productSearch = {
                    id: '0',
                    Search: "",
                    companyId: $scope.model.companyId,
                    pageSize: -1
                };

                productService.getProducts($scope.productSearch).then(function (response) {

                    $scope.productList = response.data.table0;

                    $scope.multiple.selectedTest = [];

                    var id = decodeString($location.search().id);


                    if (id != 0) {

                        $scope.search.id = id;

                        productGroupService.getProductGroups($scope.search).then(function (response) {


                            $scope.model.code = response.data[0].code;
                            $scope.model.description = response.data[0].description;

                            $scope.model.id = response.data[0].id;

                            $scope.model.name = response.data[0].name;

                            $scope.model.sRate = response.data[0].price;

                            for (var i = 0; i < response.data[0].products.length; i++) {

                                var filter = $filter('filter')($scope.productList, function (item) {
                                    return item.code == response.data[0].products[i].code;
                                });
                                $scope.multiple.selectedTest.push(filter[0]);
                            }


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

            $scope.testChange = function () {
                $scope.model.sRate = 0;
                for (var i = 0; i < $scope.multiple.selectedTest.length; i++) {
                    $scope.model.sRate += $scope.multiple.selectedTest[i].saleRate;
                }


            };


            $scope.cancel = function () {

                window.location.href = '#/app/product-group-list';
            };
            $scope.Submit = function (isValid) {


                if (!isValid) {
                    return false;
                }


                $scope.model.products = $scope.multiple.selectedTest;


                productGroupService.productGroupSubmit($scope.model).then(function (response) {

                    window.location.href = '#/app/product-group-list';
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
                    productGroupService.delete(id, $scope.model.clientId).then(function (response) {
                        $scope.productGroupList = response.data;
                    },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
                });

            };
            $scope.reset = function () {

                //$scope.searchFunc();
                //productGroupService.getproductGroups($scope.model.clientId).then(function (response) {

                //    $scope.productGroupList = response.data;
                //},
                // function (err) {
                //     $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                // });

            };

            $scope.getproductGroup();

        }


        $scope.setFile = function (element) {
            $scope.currentFile = element.files[0];
            var reader = new FileReader();

            reader.onload = function (event) {
                $scope.model.img = event.target.result
                $scope.$apply()
            }
            reader.readAsDataURL(element.files[0]);
        }




    }
            ]);


