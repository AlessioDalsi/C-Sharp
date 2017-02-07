/// <reference path="../lib/typings/angularjs/angular.d.ts" />
var myApp = angular.module("myApp", []);
var MyListController = (function () {
    function MyListController($http) {
        this.$http = $http;
    }
    MyListController.prototype.setCompleted = function (id) {
        this.$http.post("/MyList/SetCompleted/" + id, null)
            .then(function (data) {
            if (data.data)
                alert("Marcato come completato");
            else
                alert("Modifica non eseguita a causa di un errore");
        }, function (reason) {
            alert("E' avvenuto un errore");
        });
    };
    return MyListController;
}());
myApp.controller("myListController", ["$http", function ($http) { return new MyListController($http); }]);
