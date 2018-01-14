(function (angular) {
    'use strict';

    function selectCompoenentController() {
        var ctrl = this;

        // Properties
        ctrl.inputId = '';
        ctrl.label = '';
        ctrl.selectedItem = '';
        ctrl.inputType = 'text';

        // Functions
        ctrl.valueChanged = valueChanged;

        function valueChanged() {
            ctrl.onValueChange({ propertyName: ctrl.propertyName, newValue: ctrl.selectedItem.Value });
        }
    }

    var selectComponent = {
        bindings: {
            inputId: '@',
            label: '@',
            items: '=',
            propertyName: '@',
            onValueChange: '&'
        },
        templateUrl: '/AngularJs/Templates/as.select.html',
        controller: [selectCompoenentController]
    };

    angular
        .module('AppliedSystems')
        .component('asSelect', selectComponent);

})(window.angular);