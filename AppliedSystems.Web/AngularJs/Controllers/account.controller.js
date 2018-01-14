(function () {
    'use strict';

    angular
        .module('AppliedSystems')
        .controller('AccountController', ['$scope', '$log', '$http', '$window', accountController]);

    function accountController($scope, $log, $http, $window) {
        // Properties
        $scope.errormessage = '';

        // Functions
        $scope.login = login;
        $scope.register = registerUser;

        function login(username, password, returnUrl) {
            var requestUrl = '/Login';

            var requestBody = {
                email: username,
                password: password
            };

            $http({
                method: 'POST',
                url: requestUrl,
                params: { returnUrl: returnUrl },
                data: JSON.stringify(requestBody)
            }).then(
                function(response) {
                    successfulLogin(response, username, returnUrl);
                },
                function(response) {
                    unsuccessfulLogin();
                });
        }

        function successfulLogin(response, username, returnUrl) {
            $log.info('Logged in user with username ' + username);
            $window.location.href = returnUrl;
        }

        function unsuccessfulLogin() {
            $scope.errormessage = 'Something went wrong trying to log you in';
        }

        function registerUser(userDetails, returnUrl) {
            var requestUrl = '/Account/Register';

            $http({
                method: 'POST',
                url: requestUrl,
                data: JSON.stringify(userDetails)
            }).then(function(response) {
                    successfulRegistration(response, returnUrl);
                },
                function(response) {
                    unsuccessfulRegistration();
                });
        }

        function successfulRegistration(response, returnUrl) {
            $log.info('Successfully registered user');
            $window.location.href = returnUrl;
        }

        function unsuccessfulRegistration() {
            $scope.errorMessage = 'Something went wrong trying to register you';
        }
    }
})();