'use strict';
app.controller('people', ['peopleService', function (peopleService) {

    var vm = this; //this is our view model
    vm.people = [];
    vm.person = {};

    //populate the array
    peopleService.getPeople().success(function (data) {
        vm.people = data;
    });

    vm.insertOrUpdate = function (person) {
        if (person.id == null) {
            peopleService.insertPerson(person).success(function (data) {
                vm.person.id = data.id;
                vm.people.push(vm.person);
                vm.person = {};
                vm.showAdd = false;
            });
        }
        else
        {
            peopleService.updatePerson(person).success(function (data) {
                vm.person = {};
                vm.showAdd = false;
            });
        }
    }

    vm.update = function (idx) {
        vm.person = vm.people[idx];
        vm.showAdd = true;
    }

    vm.delete = function (idx) {
        var delPerson = vm.people[idx];
        peopleService.deletePerson(delPerson).success(function (data) {
            vm.people.splice(idx, 1);
        });
    }

    vm.search = function (lastName) {
        peopleService.getPeopleByLastName(lastName).success(function (data) {
            vm.people = data;
        });
    }

    vm.clickAdd = function () {
        vm.person = {};
        vm.showAdd = !vm.showAdd;
    }

    vm.showAdd = false;

}])