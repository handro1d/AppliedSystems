(function () {
    'use strict';

    angular
        .module('AppliedSystems')
        .controller('PolicyController', ['$scope', '$log', '$http', '$window', '$sce', policyController]);

    function policyController($scope, $log, $http, $window, $sce) {
        // Properties
        $scope.errormessage = '';

        // Functions
        $scope.apply = applyForPolicy;

        function applyForPolicy(policyDetails, returnUrl) {
            var requestUrl = '/Policy/Apply';

            $http({
                method: 'POST',
                url: requestUrl,
                data: JSON.stringify(policyDetails)
            }).then(function(response) {
                    successfulPolicyCreation(response, returnUrl);
                },
                function(response) {
                    unsuccessfulPolicyCreation(response);
                });
        }

        function successfulPolicyCreation(response, returnUrl) {
            $log.info('Successfully created Policy');
            $window.location.href = returnUrl;
        }

        function unsuccessfulPolicyCreation(response) {
            $log.info(response);

            var errors = response.data.split(',');
            var errorString = '';

            $.each(unique(errors), function (i, value) {
                errorString = errorString + '<li>' + value + '</li>';
            });

            $scope.errormessage = $sce.trustAsHtml('Something went wrong trying during Policy Application.<br/><ul>' + errorString + '</ul>');
        }

        function unique(array) {
            return $.grep(array, function (el, index) {
                return index === $.inArray(el, array);
            });
        }
    }
})();