(function(angular) {
    'use strict';

    function inputComponentController() {
        var ctrl = this;

        // Properties
        ctrl.inputId = '';
        ctrl.label = '';
        ctrl.value = '';
        ctrl.inputType = 'text';

        // Functions
        ctrl.valueChanged = valueChanged;

        function valueChanged() {
            ctrl.onValueChange({ propertyName: ctrl.propertyName, newValue: ctrl.value });
        }
    }

    var inputComponent = {
        bindings: {
            inputId: '@',
            label: '@',
            value: '@',
            propertyName: '@',
            inputType: '@?',
            onValueChange: '&'
        },
        templateUrl: '/AngularJs/Templates/as.input.html',
        controller: [inputComponentController]
    };

    angular
        .module('AppliedSystems')
        .component('asInput', inputComponent);

})(window.angular);