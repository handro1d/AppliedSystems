(function (angular) {
    'use strict';

    function dateComponentController() {
        var ctrl = this;

        // Properties
        ctrl.inputId = '';
        ctrl.day = '';
        ctrl.month = '';
        ctrl.year = '';
        ctrl.value = '';

        // Functions
        ctrl.valueChanged = valueChanged;

        function valueChanged() {
            var date;

            if (!ctrl.day || !ctrl.month || !ctrl.year) {
                date = null;
            } else {
                // Javascript months are 0 based, so minus 1 from provided month
                date = new Date(ctrl.year, (ctrl.month - 1), ctrl.day);
            }

            ctrl.onValueChange({ propertyName: ctrl.propertyName, newValue: date });
        }
    }

    var dateComponent = {
        bindings: {
            inputId: '@',
            value: '@',
            label: '@',
            propertyName: '@',
            onValueChange: '&'
        },
        templateUrl: '/AngularJs/Templates/as.date.html',
        controller: [dateComponentController]
    };

    angular
        .module('AppliedSystems')
        .component('asDateInput', dateComponent);

})(window.angular);