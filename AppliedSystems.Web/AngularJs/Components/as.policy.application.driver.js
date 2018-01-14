(function (angular) {
    'use strict';

    function policyApplicationController($log) {
        var ctrl = this;

        // Properties
        ctrl.firstName = '';
        ctrl.surname = '';
        ctrl.occupationId = '';
        ctrl.dateOfBirth = '';

        // Functions
        ctrl.updateValue = updateValue;
        ctrl.displayAccordion = displayAccordion;
        ctrl.addClaim = addClaim;

        function updateValue(propertyName, newValue) {
            ctrl.driver[propertyName] = newValue;
        }

        function displayAccordion($event) {
            var clickedElement = $event.target;
            clickedElement.classList.toggle('active');

            var panel = clickedElement.nextElementSibling;
            if (panel.style.display === "block") {
                panel.style.display = "none";
            } else {
                panel.style.display = "block";
            }
        }

        function addClaim() {
            ctrl.driver.claims.push({dateOfClaim: ''});
        }
    }

    var policyApplicationDriverComponent = {
        bindings: {
            driverNumber: "=",
            driver: "=",
            occupations: "="
        },
        templateUrl: '/AngularJs/Templates/as.policy.application.driver.html',
        controller: ['$log', policyApplicationController]
    };

    angular
        .module('AppliedSystems')
        .component('asPolicyApplicationDriver', policyApplicationDriverComponent);

})(window.angular);