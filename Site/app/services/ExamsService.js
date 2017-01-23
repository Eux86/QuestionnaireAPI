
var app = angular.module('myApp.services');

app.service('examsService', [
    '$http', '$q', function ($http, $q) {

        var baseUrl = "/api/TestsRepository/";

        this.remove = function (examId) {
            var deferred = $q.defer();
            $http.get("/api/TestsRepository/RemoveExam?examId=" + examId).success(function (exam) {
                deferred.resolve(exam);
            }).error(function (exam, status, headers, config) {
                deferred.reject("Failed calling RemoveExam");
            });
            return deferred.promise;
        };

        this.getAllLimit = function (startIndex, numberOfItems) {
            var deferred = $q.defer();
            $http.get("/api/TestsRepository/GetAllLimit?startIndex=" + startIndex + "&number=" + numberOfItems).success(function (exam) {
                deferred.resolve(exam);
            }).error(function (exam, status, headers, config) {
                deferred.reject("Failed calling GetAllLimit");
            });
            return deferred.promise;
        };

        this.addExam = function (exam) {
            var deferred = $q.defer();
            var data = JSON.stringify(exam);
            var response = $http({
                method: "post",
                url: baseUrl + "AddExam",
                data: data,
                dataType: "json"
            }).success(function (sentenceId) {
                deferred.resolve(sentenceId);
            }).error(function (data, status, headers, config) {
                deferred.reject("Failed calling AddExam");
            });
            return deferred.promise;
        }


    }
]);



