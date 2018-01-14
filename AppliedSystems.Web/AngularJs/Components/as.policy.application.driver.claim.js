(function (angular) {
    'use strict';

    function policyApplicationDriverClaimController($log) {
        var ctrl = this;

        // Properties
        ctrl.dateOfClaim = '';

        // Functions
        ctrl.updateValue = updateValue;

        function updateValue(propertyName, newValue) {
            ctrl.claim.dateOfClaim = newValue;
        }
    }

    var policyApplicationDriverClaimComponent = {
        bindings: {
            claimNumber: "=",
            claim: "="
        },
        templateUrl: '/AngularJs/Templates/as.policy.application.driver.claim.html',
        controller: ['$log', policyApplicationDriverClaimController]
    };

    angular
        .module('AppliedSystems')
        .component('asPolicyApplicationDriverClaim', policyApplicationDriverClaimComponent);

})(window.angular);