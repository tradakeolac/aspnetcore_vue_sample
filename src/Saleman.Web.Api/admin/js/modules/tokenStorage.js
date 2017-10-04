'use strict';
var TOKEN_KEY = '__TOKEN__';

var TokenStorage = function () { };

TokenStorage.prototype.getToken = function () {
    var token = localStorage.getItem(TOKEN_KEY);

    return token ? JSON.parse(token) : null;
};


TokenStorage.prototype.storeToken = function (token) {
    localStorage.setItem(TOKEN_KEY, JSON.stringify(token));
}