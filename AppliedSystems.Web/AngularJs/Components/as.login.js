(function (angular) {
    'use strict';

    function loginComponentController($log) {
        var ctrl = this;

        // Properties
        ctrl.username = '';
        ctrl.password = '';
        ctrl.error = '';
        ctrl.returnUrl = '';
        ctrl.registrationUrl = '';

        // Functions
        ctrl.login = login;
        ctrl.updateValue = updateValue;

        function login() {

            if (!ctrl.username || !ctrl.password) {
                ctrl.error = 'Invalid Credentials';
                return;
            }

            ctrl.onLogin({ username: ctrl.username, password: ctrl.password, returnUrl: ctrl.returnUrl });
        }

        function updateValue(propertyName, newValue) {
            ctrl[propertyName] = newValue;
        }
    }

    var loginComponent = {
        bindings: {
            username: '@?',
            password: '@?',
            returnUrl: '@?',
            registrationUrl: '@?',
            error: '@?',
            onLogin: '&'
        },
        templateUrl: '/AngularJs/Templates/as.login.html',
        controller: ['$log', loginComponentController]
    };

    angular
        .module('AppliedSystems')
        .component('asLogin', loginComponent);

})(window.angular);