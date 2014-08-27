'use strict';

//REST-ish service for getting our data in and out
app.service('peopleService', ['$http', function ($http) {

    var url = 'api/people'; //REST endpoint

    this.getPeople = function () {
        return $http.get(url);
    }

    this.getPerson = function (id) {
        return $http.get(url + '/' + id);
    }

    this.getPeopleByLastName = function (lastName) {
        return $http.get(url + '?lastName=' + lastName);
    }

    this.insertPerson = function (person) {
        return $http.post(url, person);
    }

    this.updatePerson = function (person) {
        return $http.put(url, person);
    }

    this.deletePerson = function (person) {
        return $http.delete(url + '/' + person.id);
    }
}]);