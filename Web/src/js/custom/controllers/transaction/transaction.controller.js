'use strict';
angular.module('naut').controller('caseTransactionController', ['$scope', '$location', '$modal', '$window', 'caseTransactionService', 'localStorageService', 'ngAuthSettings', 'modalService', 'caseMasterService',

    function ($scope, $location, $modal, $window, caseTransactionService, localStorageService, ngAuthSettings, modalService, caseMasterService) {
        $scope.$broadcast("remarksList");
        $scope.sysModel = {
            createdBy: ''
        };
        
        $scope.viewPerm = {

            isAmountShow: false,
            isDateShow: true,
            isPhotoTakenShow: false
        };

        $scope.caseModel = {
            cif: '',
            bankName: '',
            customerName: '',
            outstanding: '',
            accountNo: '',
            
            
        };



        $scope.modelFunc = function () {

            $scope.model = {

                id: '',
                clientId: '',
                userId: '',
                caseId: '',
                bankId: '',
                customerId: '',
                callDateTime: '',
                code1: '',
                code2: '',
                tracingDetail: '',
                remarks: '',
                issuedVisit: '',
                visitDoneNotDone: '',
                emailNoticeServed: '',
                noticeServedOnHCAddress: '',
                paymentCollectedDate: '',
                amountCollected: '',
                CreatedBy: '',
                amountExDate: '',
                photoTaken: '',
                accountNo:''
            };

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                $scope.model.clientId = authData.clientId;
                $scope.sysModel.CreatedBy = authData.userName;
            }

        };

        $scope.modelFunc();

        $scope.searchFunc = function () {
            $scope.search = {

                id: '',
                bankId: '',
                customerId: '',
                bankName: '',
                customerName: '',
                currency: '',
                dpd: '',
                writeoffBkt: '',
                writeoffAmount: '',
                writeoffDate: '',
                securityChequeNumber: '',
                secuirtyChequeDate: '',
                supplementaryCardNumber: '',
                supplementaryCardHolderName: '',
                selationwithPrimaryCard: '',
                billingCycle: '',
                valueonTOS: '',
                valueonPOS: '',
                creditLimitCardsSanctionAmountLoan: '',
                lastPurchasingAmount: '',
                lastPurchasingDate: '',
                lastPaymentAmount: '',
                lastPaymentDate: '',
                overdueAmount: '',
                statusofPoliceCase: '',
                policeStation: '',
                allocationMonth: '',
                docsStatus: '',
                allocationStatus: '',
                closingDate: '',
                closedBy: '',
                date: ''
            };

            $scope.transection = {
                callDate: '',
                callTime: '',
                caseDetail: ''

            };

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                $scope.search.clientId = authData.clientId;
                $scope.sysModel.CreatedBy = authData.userName;
            }

        };
        $scope.searchFunc();




        $scope.permissionAccessLevel = {
            isAdd: false,
            isEdit: false,
            isDelete: false,
        };


        var permLevel = localStorageService.get('permissionData');
        if (permLevel) {


            $scope.permissionAccessLevel = permLevel;
        }





        //caseTransactionService.getCountries().then(function (response) {

        //    $scope.countryList = response.data;

        //},
        //       function (err) {
        //           $.toaster({ title: 'Error', priority: 'danger', message: err.data });
        //       });






        if ($location.path() == "/app/transactions") {

            caseTransactionService.getcaseTransactions($scope.model.clientId).then(function (response) {

                $scope.caseTransactionList = response.data;


            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });

        }


        if ($location.path() == "/app/transaction") {



            var cifNo = $location.search().cifNo;
                       
            $scope.cifNo = cifNo;
            
            if (cifNo != 0) {

                
                caseTransactionService.customerInfoByCifNo($scope.caseModel.accountNo, $scope.cifNo, $scope.model.clientId).then(function (response) {

                    $scope.accountList = response.data.table0;
                    
                    $scope.caseModel = response.data.table1[0];




                    caseTransactionService.getcaseTransactionByCaseId($scope.caseModel.accountNo, $scope.model.clientId).then(function (response) {


                        $scope.transactionList = response.data.table0;
                        $scope.code1List = response.data.table1;
                        $scope.code2List = response.data.table2;
                        $scope.remarksList = response.data.table3;

                    },
      function (err) {
          $.toaster({ title: 'Error', priority: 'danger', message: err.data });
      });
                    
                    

                },
                    function (err) {
                        $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                    });


             




            }



        }


        $scope.accountChange = function () {

            
            

            caseTransactionService.customerInfoByCifNo($scope.caseModel.accountNo, $scope.cifNo, $scope.model.clientId).then(function (response) {

                

                $scope.caseModel = response.data.table1[0];



                caseTransactionService.getcaseTransactionByCaseId($scope.caseModel.accountNo, $scope.model.clientId).then(function (response) {


                    $scope.transactionList = response.data.table0;
                    $scope.code1List = response.data.table1;
                    $scope.code2List = response.data.table2;
                    $scope.remarksList = response.data.table3;

                },
                function (err) {
                    $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                });


            },
                function (err) {
                    $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                });

          


        };

        $scope.loadPrevRemarksAndtracedDetails = function () {
            // trace detail start
            //var html = "";
            //for (var i = 0; i < $scope.remarksList.length; i++) {

            //    html += "";
            //    html += '<div">';
            //    html += '<p><h5>Call Date time:  ' + $scope.remarksList[i].callDate + ' ' + $scope.remarksList[i].calltime + ' : ' + $scope.remarksList[i].code1 + ': ' + $scope.remarksList[i].tracingDetail + '</h5></p></div>';
            //}

            //$("#remarksContainerTracingDetail").html(html);
            //var el = document.querySelector('#remarksContainerTracingDetail');
            //el.scrollTop = document.getElementById("remarksContainerTracingDetail").scrollHeight;
            //// trace detail end



            //// Remarks start
            //html = "";
            //for (var i = 0; i < $scope.remarksList.length; i++) {

            //    html += "";
            //    html += '<div">';
            //    html += '<p><h5>Call Date time:  ' + $scope.remarksList[i].callDate + ' ' + $scope.remarksList[i].calltime + ' : ' + $scope.remarksList[i].code1 + ': ' + $scope.remarksList[i].remarks + '</h5></p></div>';
            //}

            //$("#remarksContainer").html(html);
            //var el = document.querySelector('#remarksContainer');
            //el.scrollTop = document.getElementById("remarksContainer").scrollHeight;
            //// Remarks end

            

        }





        $scope.prevtracingDetail = function () {
           
           $('#traceDetailsModal').modal('toggle');
            
        };


        $scope.prevRemarks = function () {

            $('#myModal').modal('toggle');


        };

        $scope.code1Change = function () {



            if ($scope.model.code1 == "Paid" || $scope.model.code1 == "PTP") {
                $scope.viewPerm.isAmountShow = true;

                if ($scope.model.code1 == "Paid") {
                    $scope.viewPerm.isDateShow = true;
                }
                else if ($scope.model.code1 == "PTP") {
                    $scope.viewPerm.isDateShow = false;
                }


            }
            else {
                $scope.viewPerm.isAmountShow = false;
            }





        };





        $scope.cancel = function () {

            window.location.href = '#/app/transaction';
        };



        $scope.reset = function () {

            $scope.modelFunc();
            $scope.searchFunc();

        };






        $scope.pdf = function () {

            caseTransactionService.getPdfReport($scope.search).then(function (response) {

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

            caseTransactionService.getExcelReport($scope.search).then(function (response) {

                $scope.caseTransactionList = response.data;


            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });


        };




        $scope.caseTransactionSubmit = function (isValid) {

            //if (!isValid) {
            //    return false;
            //}


            $scope.model.CreatedBy = $scope.sysModel.CreatedBy;
            


            $scope.model.accountNo = $scope.caseModel.accountNo;

            caseTransactionService.caseTransactionSubmit($scope.model).then(function (response) {
                //window.location.href = '#/app/caseTransactions';
                $.toaster({ title: 'Info', priority: 'success', message: "Record saved successfully." });
                $scope.modelFunc();
                //$scope.searchFunc();

                caseTransactionService.getcaseTransactionByCaseId($scope.caseModel.accountNo, $scope.model.clientId).then(function (response) {


                    $scope.transactionList = response.data.table0;
                    $scope.code1List = response.data.table1;
                    $scope.code2List = response.data.table2;
                    $scope.remarksList = response.data.table3;

                    $scope.loadPrevRemarksAndtracedDetails();
                },
           function (err) {
               $.toaster({ title: 'Error', priority: 'danger', message: err.data });
           });









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

                caseTransactionService.delete(id, $scope.model.clientId).then(function (response) {
                    $scope.caseTransactionList = response.data;
                },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });



            });






        };

        $scope.issuedVisitChange = function ()
        {
            console.log('test');
            console.log($scope.model.issuedVisit);

            if ($scope.model.issuedVisit == 'Yes') {
                $scope.viewPerm.isPhotoTakenShow = true;
            } else {

                $scope.viewPerm.isPhotoTakenShow = false;
            }

        };





    }
]);
