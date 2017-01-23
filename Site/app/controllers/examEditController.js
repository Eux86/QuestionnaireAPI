var app = angular.module('myApp.controllers');
app.controller('ExamEditController', ['$scope', '$http', '$routeParams', '$window', '$filter', '$mdDialog', '$location', 'dialogsService', '$route', 'examsService',
    function ($scope, $http, $routeParams, $window, $filter, $mdDialog, $location, dialogsService, $route, examsService) {

        $scope.loading = 0;
        $scope.$watch('loading', function () {
            dialogsService.showLoader($scope.loading > 0);
        });


        $scope.exam = { Id: -1, Date: new Date(), Questions: [{ Id: -1, Answers: [{ Id: -1 }] }] };
        $scope.allSentences = [{}];
        $scope.answersToDelete = null;
        $scope.questionsToDelete = null;

        $scope.examId = $routeParams.examId;
        $scope.action = $routeParams.action;

        if ($scope.action == "edit") {
            loadExam($scope.examId);
        }
        if ($scope.action == "delete") {
            removeExam($scope.examId);
        }
        reloadSentences();

        // managing autocomplete
        $scope.querySentences = function (search) {
            var firstPass = $filter('filter')($scope.allSentences, search);
            return firstPass;
        };

        $scope.AddQuestion = function () {
            $scope.exam.Questions.push({ Id: -1, Answers: [{ Id: -1 }] });
        }
        $scope.AddAnswer = function (question) {
            question.Answers.push({ Answers: [{}] });
        }
        $scope.removeAnswer = function (question, $index) {
            if ($scope.answersToDelete == null) $scope.answersToDelete = [];
            $scope.answersToDelete.push(question.Answers[$index]);
            question.Answers.splice($index, 1);
        }
        $scope.removeQuestion = function ($index) {
            if ($scope.questionsToDelete == null) $scope.questionsToDelete = [];
            $scope.questionsToDelete.push($scope.exam.Questions[$index]);
            $scope.exam.Questions.splice($index, 1);
        }

        $scope.selected = function (answers, sentence) {
            if (sentence != null) {
                var lastAnswer = answers[answers.length - 1];
                // Add the last empty answer
                if (lastAnswer != null && lastAnswer.Sentence != null)
                    answers.push({ Id: -1 });
            }
        }

        $scope.questionSelected = function (questions, sentence) {
            if (sentence != null) {
                var lastQuestion = questions[questions.length - 1];
                // Add the last empty question
                if (lastQuestion != null && lastQuestion.Sentence != null)
                    questions.push({ Id: -1, Answers: [{ Id: -1 }] });
            }
        }

        $scope.questionSearchChanged = function (question, questionSearchText) {
            if (question.Sentence == null || question.Sentence.Id < 0) {
                if (questionSearchText.length > 0) {
                    question.Sentence = { Id: -1, Text: questionSearchText };

                    // Add the last empty question
                    var lastQuestion = $scope.exam.Questions[$scope.exam.Questions.length - 1];
                    if (lastQuestion != null && lastQuestion.Sentence != null)
                        $scope.exam.Questions.push({ Id: -1, Answers: [{ Id: -1 }] });
                }
            }
        }

        $scope.answerSearchChanged = function (answer, answers, answerSearchText) {
            if (answer.Sentence == null || answer.Sentence.Id < 0) {
                if (answerSearchText.length > 0) {
                    answer.Sentence = { Id: -1, Text: answerSearchText };

                    // Add the last empty answer
                    var lastAnswer = answers[answers.length - 1];
                    if (lastAnswer != null && lastAnswer.Sentence != null)
                        answers.push({ Id: -1 });
                }
            }
        }

        $scope.showNewSentenceDialog = function (event, defaultText, isQuestion) {
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
                    if (sentence != null && sentence.Text != null) {
                        $scope.createNewSentence(sentence.Text, sentence.RelatedTopics, sentence.IsQuestion);
                    }
                }, function () {
                    // nothing was added
                });
        }

        // Back-end communications
        function reloadSentences() {
            $scope.loading++;
            $http.get("/api/TestsRepository/GetAllSentences").success(function (data) {
                $scope.allSentences = data;
                $scope.loading--;
            });
        }

        function loadExam(examId) {
            $scope.loading++;
            $http.get("/api/TestsRepository/Test?testId=" + examId).success(function (data) {
                var temp = data;
                temp.Date = new Date(temp.Date);
                $scope.exam = temp;
                $scope.loading--;
            });
        }

        function removeExam(examId) {
            $scope.loading++;
            $http.get("/api/TestsRepository/RemoveExam?examId=" + examId).success(function (data) {
                $location.path('/ExamSimulator/');
                $scope.loading--;
            });
        }


        $scope.Save = function () {
            $scope.isSaving = true;
            $scope.loading++;
            var data = { exam: $scope.exam, answersToDelete: $scope.answersToDelete, questionsToDelete: $scope.questionsToDelete };

            examsService.addExam($scope.exam).then(function () {
                $scope.loading--;
                dialogsService.showAlert("Saved :)").then(
                    function () {
                        if ($scope.action == "edit") {
                            $route.reload();
                        } else {
                            $scope.go('/ExamEdit/edit/' + examId);
                        }
                        $scope.isSaving = false;
                    }
                );
                $scope.exam.Id = examId;
            }, function () {
                $scope.loading--;
                dialogsService.showAlert("There were problems saving the exam :(");
                $scope.isSaving = false;
            });


            //var response = $http({
            //    method: "post",
            //    url: "/Home/UpdateExam",
            //    data: JSON.stringify(data),
            //    dataType: "json"
            //}).success(function (examId) {
            //    $scope.loading--;
            //    dialogsService.showAlert("Saved :)").then(
            //        function () {
            //            if ($scope.action == "edit") {
            //                $route.reload();
            //            } else {
            //                $scope.go('/ExamEdit/edit/' + examId);
            //            }
            //            $scope.isSaving = false;
            //        }
            //    );
            //    $scope.exam.Id = examId;
            //}).error(function () {
            //    $scope.loading--;
            //    dialogsService.showAlert("There were problems saving the exam :(");
            //    $scope.isSaving = false;
            //});
        };

        $scope.createNewSentence = function (sentenceText, relatedTopics, isQuestion) {
            var data = { Text: sentenceText, RelatedTopics: relatedTopics, IsQuestion: isQuestion };
            var response = $http({
                method: "post",
                url: "/Home/CreateSentence",
                data: JSON.stringify(data),
                dataType: "json"
            }).success(function (data) {
                reloadSentences();
            });
        }

        // UTILS
        $scope.go = function (path) {
            $location.path(path);
        }

        //$scope.showAlert = function(title) {
        //    return $mdDialog.show(
        //        $mdDialog.alert()
        //        .parent(angular.element(document.querySelector('#popupContainer')))
        //        .clickOutsideToClose(true)
        //        .title(title)
        //        //.textContent(details)
        //        .ariaLabel(title)
        //        .ok('Ok')
        //    );
        //}

    }]);

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
