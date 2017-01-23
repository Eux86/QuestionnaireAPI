var app = angular.module('myApp.services');

app.service('dialogsService', [
    '$http', '$q', '$mdDialog', function ($http, $q, $mdDialog) {


        this.showAlert = function (title) {
            return $mdDialog.show(
                $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title(title)
                //.textContent(details)
                .ariaLabel(title)
                .ok('Ok')
            );
        }

        this.showNewSentenceDialog = function (event, defaultText, isQuestion) {
            var deferred = $q.defer();
            $mdDialog.show({
                controller: DialogController,
                controllerAs: 'ctrl',
                bindToController: true,
                templateUrl: 'App/Views/Dialogs/NewSentenceDialog.html',
                parent: angular.element(document.body),
                targetEvent: event,
                clickOutsideToClose: true,
                fullscreen: false,
                locals: {
                    text: defaultText,
                    isQuestion: isQuestion
                }
            })
                .then(function (sentence) {
                    deferred.resolve(sentence);
                }, function () {
                    deferred.reject("Dialog canceled");
                });
            return deferred.promise;
        }

        this.showConfirm = function (ev, title, message) {
            return $mdDialog.show($mdDialog.confirm()
                  .title(title)
                  //.textContent(message)
                  //.ariaLabel(title)
                  .targetEvent(ev)
                  .ok('yes')
                  .cancel('no')
                  );
        };

        this.showLoader = function (flag) {
            var loader = document.getElementById("loadingOverlay").style.visibility = flag ? 'visible' : 'hidden';
        }

    }
]);


function DialogController($scope, $mdDialog) {
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };
}

DialogController.$inject = ['$scope', "$mdDialog"];