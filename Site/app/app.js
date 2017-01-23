'use strict';


// Declare app level module which depends on filters, and services
angular.module('myApp', [
   'ngMaterial',
  'ngRoute',
  'myApp.filters',
  'myApp.services',
  'myApp.directives',
  'myApp.controllers',
]).
config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/view1', { templateUrl: 'partials/partial1.html', controller: 'view1Controller' });
    $routeProvider.when('/view2', { templateUrl: 'partials/partial2.html', controller: 'MyCtrl2' });
    $routeProvider.when('/ExamEdit', { templateUrl: 'partials/ExamEdit.html', controller: 'ExamEditController' });

    $routeProvider.otherwise({ redirectTo: '/view1' });
}]);
