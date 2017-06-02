var AppAngular = angular.module('AngularApp', ['ngRoute', 'ui.bootstrap']);
AppAngular.controller('OwnersController', OwnersController);
AppAngular.controller('PetsController', PetsController);

var configFunction = function ($routeProvider, $httpProvider, $locationProvider) {
    $routeProvider
        .when('/owners', {
            templateUrl: 'Views/Owners.html',
            controller: 'OwnersController'
        })
        .when('/pets', {
            templateUrl: 'Views/Pets.html',
            controller: 'PetsController'
        })
        .when('/pets/:id', {
            templateUrl: 'Views/Pets.html',
            controller: 'PetsController'
        });

    $routeProvider.otherwise({ redirectTo: '/owners' });
    $locationProvider.hashPrefix('');
}
configFunction.$inject = ['$routeProvider', '$httpProvider', '$locationProvider'];

AppAngular.config(configFunction);