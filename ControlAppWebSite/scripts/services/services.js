controlApp.service('authenticate', function ($http) {
    return function (login, password, successCallBackFunction, errorCallBackFunction) {
        var serviceURL = 'http://localhost:64961/security/token';
        var data = "grant_type=password&username=" + login + "&password=" + password;
        $http.post(serviceURL, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
           .success(successCallBackFunction).error(errorCallBackFunction);
    }
})
.service('callPostApiJSON', function ($http, validToken) {
    return function (url, data, successCallBackFunction, errorCallBackFunction) {
        validToken();
        var serviceURL = 'http://localhost:64961/' + url;
        $http.post(serviceURL, JSON.stringify(data), { headers: { 'Authorization': 'Bearer ' + window.localStorage.getItem('controlAppToken'), 'Content-Type': 'application/json' } })
            .success(successCallBackFunction).error(errorCallBackFunction);
    }
})
.service('callPostApi', function ($http) {
    return function (url, data, successCallBackFunction, errorCallBackFunction) {
        var serviceURL = 'http://localhost:64961/' + url;
        $http.post(serviceURL + url, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .success(successCallBackFunction).error(errorCallBackFunction);
    }
})
.service('callGetApi', function ($http, validToken) {
    return function (url, successCallBackFunction, errorCallBackFunction) {
        validToken();
        var serviceURL = 'http://localhost:64961/' + url;
        $http.get(serviceURL, { headers: { 'Authorization': 'Bearer ' + window.localStorage.getItem('controlAppToken') } })
            .success(successCallBackFunction).error(errorCallBackFunction);
    }
})
.service('validToken', function () {
    return function () {
        if (window.localStorage.getItem('controlAppToken') != 'null' && window.localStorage.getItem('controlAppToken') != null && window.localStorage.getItem('controlAppToken') != undefined) {
            return window.localStorage.getItem('controlAppToken');
        }
        else {
            throw 'Token de acesso inválido. Efetue login novamente.';
        }
    }
})
.service('replaceAll', function () {
    return function (find, replace, str) {
        return str.replace(new RegExp(find, 'g'), replace);
    }
})
.service('clearText', function () {
    return function (texto) {
        texto = texto.replace(/^\s+|\s+$/g, "");//tira espaços do inicio e do fim
        texto = texto.replace(/\s{2,}/g, " ");//tira espaços duplicados

        replacements = {
            "[áãàäâª]": "a",
            "[éèëê]": "e",
            "[íìï]": "i",
            "[óòôõö]": "o",
            "[ùúûü]": "u",
            "[ç]": "c"
        };
        regex = {};
        for (key in replacements) {
            regex[key] = new RegExp(key, 'g');
        }

        for (key in replacements) {
            texto = texto.replace(regex[key], replacements[key]);//tira caracteres acentuados
        }
        texto = texto.replace(/[^A-Za-z0-9]/g, "");//tira caracteres especiais
        texto = texto.toLowerCase();//coloca em minusculo
        return texto;
    }
});