'use strict';
app.controller('login', ['$location', 'authService', function ($location, authService) {
    var vm = this;
    vm.loginData = {
        userName: "",
        password: "",
    };

    vm.message = "";

    vm.login = function () {

        authService.login(vm.loginData).then(function (response) {

            $location.path('/orders');

        },
         function (err) {
             vm.message = err.error_description;
         });
    };

}]);