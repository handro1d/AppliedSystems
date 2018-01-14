(function (angular) {
    'use strict';

    function policyApplicationController($log) {
        var ctrl = this;

        // Properties
        ctrl.errorMessage = '';
        ctrl.startDate = '';
        ctrl.drivers = [];

        // Functions
        ctrl.apply = apply;
        ctrl.addDriver = addDriver;
        ctrl.updateValue = updateValue;

        function apply() {
            var policyDetails = {
                startDate: ctrl.startDate,
                drivers: ctrl.drivers
            };

            // TODO: Take return Url as a parameter? 
            ctrl.onApplication({ policyDetails: policyDetails, returnUrl: '/' });
        }

        function addDriver() {
            if (ctrl.drivers.length < 5) {
                // A driver can be added
                ctrl.drivers.push({
                    firstName: '',
                    surname: '',
                    dateOfBirth: '',
                    selectedOccupation: '',
                    claims: []
                });
                return;
            }

            // No additional drivers can be added
            ctrl.errorMessage = 'A maximum of 5 drivers can be added to a Policy';
        }

        function updateValue(propertyName, newValue) {
            ctrl[propertyName] = newValue;
        }
    }

    var policyApplicationComponent = {
        bindings: {
            errorMessage: '=',
            occupations: "=",
            onApplication: '&'
        },
        templateUrl: '/AngularJs/Templates/as.policy.application.html',
        controller: ['$log', policyApplicationController]
    };

    angular
        .module('AppliedSystems')
        .component('asPolicyApplication', policyApplicationComponent);

})(window.angular);