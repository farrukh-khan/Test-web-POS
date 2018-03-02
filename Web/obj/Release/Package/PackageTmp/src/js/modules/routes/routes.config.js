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
        $urlRouterProvider.otherwise('/login');

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
                  permission: function (authorizationService, $route) {
                      return authorizationService.permissionCheck("dashboard");
                      

                  }
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
            .state('app.division', {
                url: '/division',
                templateUrl: Route.base('Division/division.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("division");
                    }
                }
            })
            .state('app.editdivision', {
                url: '/editdivision',
                templateUrl: Route.base('Division/division.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("editdivision");
                    }
                }
            })

             .state('app.divisions', {
                 url: '/divisions',
                 templateUrl: Route.base('Division/list.division.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {
                         return authorizationService.permissionCheck("divisions");
                     }
                 }
             })


             .state('app.department', {
                 url: '/department',
                 templateUrl: Route.base('Department/department.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {
                         return authorizationService.permissionCheck("department");
                     }
                 }
             })
            .state('app.editdepartment', {
                url: '/editdepartment',
                templateUrl: Route.base('Department/department.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("editdepartment");
                    }
                }
            })

             .state('app.departments', {
                 url: '/departments',
                 templateUrl: Route.base('Department/list.department.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("departments");
                     }
                 }
             })

        .state('app.costcentre', {
            url: '/costcentre',
            templateUrl: Route.base('CostCentre/costcentre.html'),
            resolve: {
                assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                permission: function (authorizationService, $route) {

                    return authorizationService.permissionCheck("costcentre");
                }
            }

        })

            .state('app.editcostcentre', {
                url: '/editcostcentre',
                templateUrl: Route.base('CostCentre/costcentre.html'),
                resolve: {
                    assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                    permission: function (authorizationService, $route) {
                        return authorizationService.permissionCheck("editcostcentre");
                    }
                }
            })


             .state('app.costcentres', {
                 url: '/costcentres',
                 templateUrl: Route.base('CostCentre/list.costcentre.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("costcentres");
                     }
                 }


             })


               .state('app.extension', {
                   url: '/extension',
                   templateUrl: Route.base('Extension/extension.html'),
                   resolve: {
                       assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                       permission: function (authorizationService, $route) {

                           return authorizationService.permissionCheck("extension");
                       }
                   }

               })

             .state('app.extensions', {
                 url: '/extensions',
                 templateUrl: Route.base('Extension/list.extension.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("extensions");
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

             .state('app.products', {
                 url: '/products',
                 templateUrl: Route.base('Product/product.html'),
                 resolve: {
                     assets: Route.require('ui.select', 'ngTable', 'ngTableExport'),
                     permission: function (authorizationService, $route) {

                         return authorizationService.permissionCheck("products");
                     }
                 }

             });





    }


})();


//  .state('app.cards', {
//    url: '/cards',
//    templateUrl: Route.base('cards.html'),
//    resolve: {
//      assets: Route.require('flot-chart', 'flot-chart-plugins', 'ui.knob', 'loadGoogleMapsJS', function() {
//        return loadGoogleMaps();
//      }, 'ui.map')
//    }
//  })
//  .state('app.ui', {
//    url: '/ui',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//  })
//  .state('app.ui.buttons', {
//    url: '/buttons',
//    templateUrl: Route.base('ui.buttons.html')
//  })
//  .state('app.ui.notifications', {
//    url: '/notifications',
//    templateUrl: Route.base('ui.notifications.html'),
//    controller: 'NotificationController'
//  })
//  .state('app.ui.bootstrapui', {
//    url: '/bootstrapui',
//    templateUrl: Route.base('ui.bootstrap-ui.html')
//  })
//  .state('app.ui.panels', {
//    url: '/panels',
//    templateUrl: Route.base('ui.panels.html'),
//    resolve: {
//      assets: Route.require('jquery-ui')
//    }
//  })
//  .state('app.ui.navtree', {
//    url: '/navtree',
//    title: 'Nav Tree',
//    templateUrl: Route.base('ui.nav-tree.html'),
//    resolve: {
//      assets: Route.require('angularBootstrapNavTree')
//    }
//  })
//  .state('app.ui.nestable', {
//    url: '/nestable',
//    title: 'Nestable',
//    templateUrl: Route.base('ui.nestable.html'),
//    resolve: {
//      assets: Route.require('nestable')
//    }
//  })
//  .state('app.ui.sortable', {
//    url: '/sortable',
//    title: 'Sortable',
//    templateUrl: Route.base('ui.sortable.html'),
//    resolve: {
//      assets: Route.require('htmlSortable')
//    }
//  })
//  .state('app.ui.grid', {
//    url: '/grid',
//    templateUrl: Route.base('ui.grid.html')
//  })
//  .state('app.ui.grid-masonry', {
//    url: '/grid-masonry',
//    templateUrl: Route.base('ui.grid-masonry.html')
//  })
//  .state('app.ui.typo', {
//    url: '/typo',
//    templateUrl: Route.base('ui.typo.html')
//  })
//  .state('app.ui.palette', {
//    url: '/palette',
//    templateUrl: Route.base('ui.palette.html')
//  })
//  .state('app.ui.localization', {
//    url: '/localization',
//    title: 'Localization',
//    templateUrl: Route.base('ui.localization.html')
//  })
//  .state('app.maps', {
//    url: '/maps',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//  })
//  .state('app.maps.google', {
//    url: '/google',
//    templateUrl: Route.base('maps.google.html'),
//    resolve: {
//      assets: Route.require('loadGoogleMapsJS', function() {
//        return loadGoogleMaps();
//      }, 'ui.map')
//    }
//  })
//  .state('app.maps.vector', {
//    url: '/vector',
//    templateUrl: Route.base('maps.vector.html'),
//    resolve: {
//      assets: Route.require('vectormap', 'vectormap-maps')
//    }
//  })
//  .state('app.icons', {
//    url: '/icons',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//  })
//  .state('app.icons.feather', {
//    url: '/feather',
//    templateUrl: Route.base('icons.feather.html')
//  })
//  .state('app.icons.fa', {
//    url: '/fa',
//    templateUrl: Route.base('icons.fa.html')
//  })
//  .state('app.icons.weather', {
//    url: '/weather',
//    templateUrl: Route.base('icons.weather.html')
//  })
//  .state('app.icons.climacon', {
//    url: '/climacon',
//    templateUrl: Route.base('icons.climacon.html')
//  })
//  .state('app.forms', {
//    url: '/forms',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//  })
//  .state('app.forms.inputs', {
//    url: '/inputs',
//    templateUrl: Route.base('form.inputs.html'),
//    resolve: {
//      assets: Route.require('moment', 'textAngular', 'textAngularSetup')
//    }
//  })
//  .state('app.forms.validation', {
//    url: '/validation',
//    templateUrl: Route.base('form.validation.html')
//  })
//  .state('app.forms.wizard', {
//    url: '/wizard',
//    templateUrl: Route.base('form.wizard.html')
//  })
//  .state('app.forms.upload', {
//    url: '/upload',
//    title: 'Form upload',
//    templateUrl: Route.base('form.upload.html'),
//    resolve: {
//      assets: Route.require('angularFileUpload', 'filestyle')
//    }
//  })
//  .state('app.forms.xeditable', {
//    url: '/xeditable',
//    templateUrl: Route.base('form.xeditable.html'),
//    resolve: {
//      assets: Route.require('xeditable')
//    }
//  })
//  .state('app.forms.imagecrop', {
//    url: '/imagecrop',
//    templateUrl: Route.base('form.imagecrop.html'),
//    resolve: {
//      assets: Route.require('ngImgCrop', 'filestyle')
//    }
//  })
//  .state('app.forms.uiselect', {
//    url: '/uiselect',
//    templateUrl: Route.base('form.uiselect.html'),
//    controller: 'UISelectController',
//    resolve: {
//      assets: Route.require('ui.select')
//    }
//  })
//  .state('app.forms.slider', {
//    url: '/slider',
//    templateUrl: Route.base('form.slider.html'),
//    resolve: {
//      assets: Route.require('vr.directives.slider')
//    }
//  })

//  .state('app.tables', {
//    url: '/tables',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//  })
//  .state('app.tables.responsive', {
//    url: '/responsive',
//    templateUrl: Route.base('table.responsive.html')
//  })
//  .state('app.tables.ngtable', {
//    url: '/ngtable',
//    templateUrl: Route.base('table.ngtable.html'),
//    resolve: {
//      assets: Route.require('ngTable', 'ngTableExport')
//    }
//  })
//  .state('app.tables.xeditable', {
//    url: '/xeditable',
//    templateUrl: Route.base('table.xeditable.html'),
//    resolve: {
//      assets: Route.require('xeditable')
//    }
//  })
//  .state('app.extras', {
//    url: '/extras',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//  })
//  .state('app.extras.calendar', {
//    url: '/calendar',
//    templateUrl: Route.base('extras.calendar.html'),
//    resolve: {
//      assets: Route.require('jquery-ui', 'moment', 'ui.calendar', 'gcal')
//    }
//  })
//  .state('app.extras.tasks', {
//    url: '/tasks',
//    templateUrl: Route.base('extras.tasks.html'),
//    controller: 'TasksController as taskctrl'
//  })
//  .state('app.extras.invoice', {
//    url: '/invoice',
//    templateUrl: Route.base('extras.invoice.html')
//  })
//  .state('app.extras.search', {
//    url: '/search',
//    templateUrl: Route.base('extras.search.html'),
//    resolve: {
//      assets: Route.require('moment')
//    }
//  })
//  .state('app.extras.price', {
//    url: '/price',
//    templateUrl: Route.base('extras.price-table.html')
//  })
//  .state('app.extras.template', {
//    url: '/template',
//    templateUrl: Route.base('extras.template.html')
//  })
//  .state('app.extras.timeline', {
//    url: '/timeline',
//    templateUrl: Route.base('extras.timeline.html')
//  })
//  .state('app.extras.projects', {
//    url: '/projects',
//    templateUrl: Route.base('extras.projects.html')
//  })
//  .state('app.extras.gallery', {
//    url: '/gallery',
//    templateUrl: Route.base('extras.gallery.html'),
//    resolve: {
//      assets: Route.require('blueimp-gallery')
//    }
//  })
//  .state('app.extras.profile', {
//    url: '/profile',
//    templateUrl: Route.base('extras.profile.html'),
//    resolve: {
//      assets: Route.require('loadGoogleMapsJS', function() {
//        return loadGoogleMaps();
//      }, 'ui.map')
//    }
//  })
//// Mailbox
//.state('app-fh.mailbox', {
//  url: '/mailbox',
//  abstract: true,
//  templateUrl: Route.base('mailbox.html'),
//  resolve: {
//    assets: Route.require('moment')
//  }
//})
//  .state('app-fh.mailbox.folder', {
//    url: '/folder',
//    abstract: true
//  })
//  .state('app-fh.mailbox.folder.list', {
//    url: '/:folder',
//    views: {
//      'container@app-fh.mailbox': {
//        templateUrl: Route.base('mailbox.folder.html')
//      }
//    }
//  })
//  .state('app-fh.mailbox.folder.list.view', {
//    url: '/:id',
//    views: {
//      'mails@app-fh.mailbox.folder.list': {
//        templateUrl: Route.base('mailbox.view-mail.html')
//      }
//    },
//    resolve: {
//      assets: Route.require('textAngular', 'textAngularSetup')
//    }
//  })
//  .state('app-fh.mailbox.compose', {
//    url: '/compose',
//    views: {
//      'container@app-fh.mailbox': {
//        templateUrl: Route.base('mailbox.compose.html')
//      }
//    },
//    resolve: {
//      assets: Route.require('textAngular', 'textAngularSetup')
//    }
//  })
// Single Page Routes
//.state('page', {
//  url: '/page',
//  templateUrl: Route.base('page.html'),
//  resolve: {
//    assets: Route.require('icons', 'animate')
//  }
//})



// Layout dock
//.state('app-dock', {
//    url: '/dock',
//    abstract: true,
//    templateUrl: Route.base('app-dock.html'),
//    controller: function ($rootScope, $scope) {
//        $rootScope.app.layout.isDocked = true;
//        $scope.$on('$destroy', function () {
//            $rootScope.app.layout.isDocked = false;
//        });
//        // we can't use dropdown when material and docked
//        // main content overlaps dropdowns (forced for demo)
//        $rootScope.app.layout.isMaterial = false;
//    },
//    resolve: {
//        assets: Route.require('icons', 'screenfull', 'sparklines', 'slimscroll', 'toaster', 'animate')
//    }
//})
//.state('app-dock.dashboard', {
//    url: '/dashboard',
//    templateUrl: Route.base('dashboard.html'),
//    resolve: {
//        assets: Route.require('flot-chart', 'flot-chart-plugins', 'easypiechart')
//    }
//})
// Layout full height
//.state('app-fh', {
//    url: '/fh',
//    abstract: true,
//    templateUrl: Route.base('app-fh.html'),
//    resolve: {
//        assets: Route.require('icons', 'screenfull', 'sparklines', 'slimscroll', 'toaster', 'animate')
//    }

//})
//.state('app-fh.columns', {
//  url: '/columns',
//  templateUrl: Route.base('layout.columns.html')
//})
//.state('app-fh.chat', {
//  url: '/chat',
//  templateUrl: Route.base('extras.chat.html')
//});

//.state('app.maps', {
//    url: '/maps',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//})
//      .state('app.maps.google', {
//          url: '/google',
//          templateUrl: Route.base('maps.google.html'),
//          resolve: {
//              assets: Route.require('loadGoogleMapsJS', function () {
//                  return loadGoogleMaps();
//              }, 'ui.map')
//          }
//      })
//      .state('app.maps.vector', {
//          url: '/vector',
//          templateUrl: Route.base('maps.vector.html'),
//          resolve: {
//              assets: Route.require('vectormap', 'vectormap-maps')
//          }
//      });
//.state('app.charts', {
//    url: '/charts',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//})
// .state('app.charts.flot', {
//     url: '/flot',
//     templateUrl: Route.base('charts.flot.html'),
//     resolve: {
//         assets: Route.require('flot-chart', 'flot-chart-plugins')
//     }
// })
// .state('app.charts.pie', {
//     url: '/pie',
//     templateUrl: Route.base('charts.pie.html'),
//     resolve: {
//         assets: Route.require('ui.knob', 'easypiechart')
//     }
// })

//.state('app.forms', {
//    url: '/forms',
//    template: '<div ui-view ng-class="app.views.animation"></div>'
//})
//                 .state('app.forms.inputs', {
//                     url: '/inputs',
//                     templateUrl: Route.base('form.inputs.html'),
//                     resolve: {
//                         assets: Route.require('moment', 'textAngular', 'textAngularSetup')
//                     }
//                 })

//  .state('app.forms.uiselect', {
//      url: '/uiselect',
//      templateUrl: Route.base('form.uiselect.html'),
//      controller: 'UISelectController',
//      resolve: {
//          assets: Route.require('ui.select')
//      }
//  })