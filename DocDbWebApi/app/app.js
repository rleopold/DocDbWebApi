'use strict';

var app = angular.module('DocDbWebApi', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.constant('ngSettings', {
    apiServiceBaseUri: '',
});

app.config(['$routeProvider', function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "home",
        templateUrl: "/app/partials/home.html"
    });

    $routeProvider.when("/login", {
        controller: "login",
        controllerAs: "vm",
        templateUrl: "/app/partials/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signup",
        controllerAs: "vm",
        templateUrl: "/app/partials/signup.html"
    });

    $routeProvider.when("/people", {
        controller: "people",
        controllerAs: "vm",
        templateUrl: "/app/partials/people.html"
    });


    $routeProvider.otherwise({ redirectTo: '/home' });
}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
