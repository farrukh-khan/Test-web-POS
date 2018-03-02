'use strict';
angular.module('naut').controller('userController', ['$scope', '$location', '$modal', '$window', 'authService', 'roleService', 'localStorageService', 'ngAuthSettings', 'modalService',

    function ($scope, $location, $modal, $window, authService, roleService, localStorageService, ngAuthSettings, modalService) {

        $scope.model = {
            id: "0",
            firstName: "",
            lastName: "",
            roleName: "",
            roleId: "",
            isActive: true,
            email: "",
            password: "",
            confirmPassword: "",
            iCustomer: null,
            clientId: null,
            fkUser: 0,
            systemRole: '',
            userType: 3
        };

        $scope.system = {
            userType: 3,
            pageTitle: ''
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
            $scope.system.userType = authData.userType;
            $scope.model.userType = authData.userType;
            $scope.model.iCustomer = authData.iCustomer;
            $scope.model.clientId = authData.clientId;
            $scope.model.fkUser = authData.fkUser;
        }


        if ($scope.system.userType == 1) {
            $scope.system.systemRole = 'System Administrator';

            $scope.system.pageTitle = 'Customer';
        }
        else if ($scope.system.userType == 2) {
            $scope.system.systemRole = 'Administrator';
            $scope.system.pageTitle = 'User';
        }
        else {
            $scope.system.pageTitle = 'User';
            $scope.system.systemRole = 'User';
        }


        if ($location.path() == "/app/users") {

            authService.getUsers($scope.model.clientId).then(function (response) {

                $scope.userList = response.data;


            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

        }

        if ($location.path() == "/app/user" || $location.path() == "/app/edituser" || $location.path() == "/app/companyuser") {

            var id = $location.search().id;

            if (id != 0) {


                authService.getUsersById(id).then(function (response) {

                    $scope.model = response.data;
                    $scope.model.password = "                    ";
                    $scope.model.confirmPassword = "                    ";



                },
             function (err) {

                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

            }

            // get roles for user drop down

            var companyId = $location.search().companyid;

            if (companyId && $scope.model.userType == 1) {
                $scope.model.clientId = companyId;
            }

            roleService.getRoles($scope.model.clientId).then(function (response) {

                $scope.roleList = response.data;


            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });



        }
        else if ($location.path() == "/app/profile") {
            authService.getUsersById($scope.model.fkUser).then(function (response) {

                $scope.model = response.data;
                $scope.model.password = "                    ";
                $scope.model.confirmPassword = "                    ";


            },
         function (err) {

             $.toaster({ title: 'Error', priority: 'danger', message: err.error_description });
         });


            // get roles for user drop down

            roleService.getRoles($scope.model.clientId).then(function (response) {

                $scope.roleList = response.data;


            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });


        }




        // get Customers for user drop down

        if ($scope.system.userType == 1) {
            roleService.getCustomers().then(function (response) {

                $scope.cusList = response.data;

            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });





        }

        $scope.cancel = function () {
            if ($location.path() == "/app/profile") {
                window.location.href = '#/app/dashboard';
            }
            else if ($location.path() == "/app/companyuser") {
                var companyId = $location.search().companyid;
                window.location.href = '#/app/editcompany?id=' + companyId;
            }
            else {
                window.location.href = '#/app/users';
            }

        },


        $scope.userSubmit = function (isValid) {

            if (!isValid) {
                return false;
            }


            var companyId = $location.search().companyid;

            if ($scope.system.userType == 1) {

                if (companyId) {
                    $scope.model.clientId = companyId;
                }

                $scope.model.userType = 1;
            }
            else {
                $scope.model.clientId = authData.clientId;
            }

            authService.userSubmit($scope.model).then(function (response) {


                if ($location.path() == "/app/profile") {

                    $.toaster({ title: 'Message', priority: 'success', message: "Profile updated successfully." });
                }
                else if (companyId && $scope.model.userType == 1) {

                    window.location.href = '#/app/editcompany?id=' + $location.search().companyid;
                }
                else {
                    window.location.href = '#/app/users';
                }


            },
          function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.error_description });
          });

        };


        $scope.deleteUser = function (id) {

            var modalOptions = {
                actionButtonText: 'OK',
                headerText: 'Confirm',
                bodyText: "Are you sure, you want to delete?"
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                authService.deleteUser(id, $scope.model.clientId).then(function (response) {
                    $scope.userList = response.data;
                },
      function (err) {
          $.toaster({ title: 'Error', priority: 'danger', message: err.data });
      });
            });


        };





    }
]);
