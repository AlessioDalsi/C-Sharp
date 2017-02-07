/// <reference path="../lib/typings/angularjs/angular.d.ts" />

var myApp = angular.module("myApp", []);

class MyListController implements ng.IController {

    constructor(private $http : ng.IHttpService) {

    }

    public setCompleted(id) {
        this.$http.post("/MyList/SetCompleted/" + id, null)
            .then((data) => {
                if (data.data)
                    alert("Marcato come completato")
                else
                    alert("Modifica non eseguita a causa di un errore");
            },
            (reason) => {
                alert("E' avvenuto un errore");
            });


    }


}



myApp.controller("myListController", ["$http", ($http) => new MyListController($http)])