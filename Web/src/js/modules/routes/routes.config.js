/**=========================================================
 * Module: RoutesConfig.js
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('naut')
        .config(routesConfig);

    routesConfig.$inject = ['$stateProvider', '$urlRouterProvider', 'RouteProvider'];
    function routesConfig($stateProvider, $urlRouterProvider, Route) {

        // Default route
        //$urlRouterProvider.otherwise('/login');

        $urlRouterProvider.otherwise('/app/dashboard');


        // Application Routes States
        $stateProvider
          .state('app', {
              url: '/app',
              abstract: true,
              templateUrl: Route.base('app.html'),
              resolve: {
                  _assets: Route.require('icons', 'screenfull', 'sparklines', 'slimscroll', 'toaster', 'animate')
              }

          })

             .state('error404', {
                 url: '/error404',
                 templateUrl: Route.base('Error/Error404.html')
             })

          .state('app.dashboard', {
              url: '/dashboard',
              templateUrl: Route.base('Dashboard/dashboard.html'),
              resolve: {
                  assets: Route.require('flot-chart', 'flot-chart-plugins', 'easypiechart'),
                  //permission: function (authorizationService, $route) {
                  //    return authorizationService.permissionCheck("dashboard");
                  //}
              }
          })
                .state('app.order', {
                    url: '/order',
                    templateUrl: Route.base('Order/order.html'),
                    resolve: {
                        assets: Route.require('ui.select')

                        //permission: function (authorizationService, $route) {
                        //    return authorizationService.permissionCheck("dashboard");
                        //}
                    }
                })
            .state('app.home', {
                url: '/home',
                templateUrl: Route.base('Dashboard/home.html'),
            })
         .state('login', {
             url: '/login',
             templateUrl: Route.base('account/login.html'),
             resolve: {
                 permission: function (authorizationService, $route) {
                     return authorizationService.permissionCheck("SKIP");
                 }
             }

         })



          .state('app.user', {
              url: '/user',
              templateUrl: Route.base('account/user.html'),
              resolve: {
                  permission: function (authorizationService, $route) {
                      return authorizationService.permissionCheck("user");
                  }
              }

          })


            .state('app.companies', {
                url: '/companies',
                templateUrl: Route.base('admin/list.company.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("companies");
                    }
                }

            })
            .state('app.company', {
                url: '/company',

                templateUrl: Route.base('admin/company.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("company");
                    }
                }

            })

            .state('app.editcompany', {
                url: '/editcompany',

                templateUrl: Route.base('admin/edit.company.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("editcompany");
                    }
                }
            })

            .state('app.companyrole', {
                url: '/companyrole',

                templateUrl: Route.base('admin/role.permission.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("companyrole");
                    }
                }
            })

            .state('app.companyuser', {
                url: '/companyuser',

                templateUrl: Route.base('account/user.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("companyuser");
                    }
                }
            })


            .state('app.edituser', {
                url: '/edituser',
                templateUrl: Route.base('account/user.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("edituser");
                    }
                }
            })

              .state('app.profile', {
                  url: '/profile',
                  templateUrl: Route.base('account/user.profile.html'),
                  resolve: {
                      permission: function (authorizationService, $route) {
                          return authorizationService.permissionCheck("SKIP");
                      }
                  }
              })
            .state('app.users', {
                url: '/users',
                templateUrl: Route.base('account/list.user.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("users");
                    }
                }
            })

             .state('app.role', {
                 url: '/role',
                 templateUrl: Route.base('account/role.html'),
                 resolve: {
                     permission: function (authorizationService, $route) {
                         return authorizationService.permissionCheck("role");
                     }
                 }
             })

            .state('app.editrole', {
                url: '/editrole',
                templateUrl: Route.base('account/role.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("editrole");
                    }
                }
            })

            .state('app.roles', {
                url: '/roles',
                templateUrl: Route.base('account/list.role.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("roles");
                    }
                }
            })
             .state('app.permission', {
                 url: '/permission',
                 templateUrl: Route.base('Permission/permission.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {
                         return authorizationService.permissionCheck("permission");
                     }
                 }

             })
              .state('recover', {
                  url: '/recover',
                  templateUrl: Route.base('account/recover.html'),
                  resolve: {
                      permission: function (authorizationService, $route) {
                          return authorizationService.permissionCheck("SKIP");
                      }
                  }

              })
          .state('lock', {
              url: '/lock',
              templateUrl: Route.base('account/lock.html'),
              resolve: {
                  permission: function (authorizationService, $route) {
                      return authorizationService.permissionCheck("SKIP");
                  }
              }
          })
            .state('app.reportcatalogue', {
                url: '/reportcatalogue',
                templateUrl: Route.base('report/reportCatalogue.html'),
                resolve: {
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("SKIP");
                    }
                }
            })
            .state('app.report', {
                url: '/report',
                templateUrl: Route.base('report/report.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport', 'flot-chart', 'flot-chart-plugins'),

                    permission: function (authorizationService, $route) {

                        return authorizationService.permissionCheck("report");
                    }
                }
            })
            .state('app.customer', {
                url: '/customer',
                templateUrl: Route.base('customer/customer.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("customer");
                    }
                }
            })
            .state('app.editcustomer', {
                url: '/editcustomer',
                templateUrl: Route.base('customer/customer.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("editcustomer");
                    }
                }
            })

             .state('app.customers', {
                 url: '/customers',
                 templateUrl: Route.base('customer/list.customer.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {
                         return authorizationService.permissionCheck("customers");
                     }
                 }
             })


             .state('app.case', {
                 url: '/case',
                 templateUrl: Route.base('claim/claim.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {
                         return authorizationService.permissionCheck("case");
                     }
                 }
             })
            .state('app.editcase', {
                url: '/editcase',
                templateUrl: Route.base('claim/claim.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("editcase");
                    }
                }
            })

             .state('app.cases', {
                 url: '/cases',
                 templateUrl: Route.base('claim/list.claim.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("cases");
                     }
                 }
             })

        .state('app.customer-cases', {
            url: '/customer-cases',
            templateUrl: Route.base('claim/list.customer.claim.html'),
            resolve: {
                assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                permission: function (authorizationService, $route) {

                    return authorizationService.permissionCheck("customer-cases");
                }
            }
        })



            .state('app.import', {
                url: '/import',
                templateUrl: Route.base('import/import.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {

                        return authorizationService.permissionCheck("import");
                    }
                }

            })


                  // category start
        .state('app.category', {
            url: '/category',
            templateUrl: Route.base('Category/Category.html'),
            resolve: {
                assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                permission: function (authorizationService, $route) {

                    return authorizationService.permissionCheck("category");
                }
            }

        })

            .state('app.edit-category', {
                url: '/edit-category',
                templateUrl: Route.base('Category/Category.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("edit-category");
                    }
                }
            })


             .state('app.category-list', {
                 url: '/category-list',
                 templateUrl: Route.base('Category/list.category.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("category-list");
                     }
                 }


             })

            // category End









            // test start
        .state('app.product', {
            url: '/product',
            templateUrl: Route.base('Product/product.html'),
            resolve: {
                assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                permission: function (authorizationService, $route) {

                    return authorizationService.permissionCheck("product");
                }
            }

        })

            .state('app.edit-product', {
                url: '/edit-product',
                templateUrl: Route.base('Product/product.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("edit-product");
                    }
                }
            })


             .state('app.product-list', {
                 url: '/product-list',
                 templateUrl: Route.base('Product/list.product.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("product-list");
                     }
                 }


             })

            // test End


          // test Group start
        .state('app.product-group', {
            url: '/product-group',
            templateUrl: Route.base('ProductGroup/product.group.html'),
            resolve: {
                assets: Route.require('ui.select', 'ngTable', 'ngTableExport', 'angularFileUpload', 'filestyle'),
                permission: function (authorizationService, $route) {

                    return authorizationService.permissionCheck("product-group");
                }
            }

        })

            .state('app.edit-product-group', {
                url: '/edit-product-group',
                templateUrl: Route.base('ProductGroup/product.group.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("edit-product-group");
                    }
                }
            })


             .state('app.product-group-list', {
                 url: '/product-group-list',
                 templateUrl: Route.base('ProductGroup/list.product.group.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("product-group-list");
                     }
                 }


             })

            // test Group End

            // pathology start

            .state('app.pathology-result', {
                url: '/pathology-result',
                templateUrl: Route.base('Pathology/Pathology-result.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("pathology-result");
                    }
                }
            })
            // pathology end

            .state('app.phlebotomy', {
                url: '/phlebotomy',
                templateUrl: Route.base('Phlebotomy/Phlebotomy.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("phlebotomy");
                    }
                }
            })















               .state('app.transaction', {
                   url: '/transaction',
                   templateUrl: Route.base('transaction/transaction.html'),
                   resolve: {
                       assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                       permission: function (authorizationService, $route) {

                           return authorizationService.permissionCheck("transaction");
                       }
                   }

               })

             .state('app.transactions', {
                 url: '/transactions',
                 templateUrl: Route.base('transaction/list.transaction.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("transactions");
                     }
                 }


             })
               .state('app.systemsettings', {
                   url: '/systemsettings',
                   templateUrl: Route.base('SystemSetting/systemSetting.html'),
                   resolve: {
                       assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                       permission: function (authorizationService, $route) {

                           return authorizationService.permissionCheck("systemsettings");
                       }
                   }


               })
         .state('app.logs', {
             url: '/logs',
             templateUrl: Route.base('log/list.log.html'),
             resolve: {
                 assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                 permission: function (authorizationService, $route) {
                     return authorizationService.permissionCheck("logs");
                 }
             }
         })

        .state('app.customeragent', {
            url: '/customeragent',
            templateUrl: Route.base('customerAgent/list.customerAgent.html'),
            resolve: {
                assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                permission: function (authorizationService, $route) {
                    return authorizationService.permissionCheck("customeragent");
                }
            }
        })




    }


})();

