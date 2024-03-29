﻿// Initialize Firebase
var config = {
    apiKey: "AIzaSyBX9u-1P99X08XHfL-rr3DxqJMCVnI4Vbw",
    authDomain: "blooddonation-bc0b9.firebaseapp.com",
    databaseURL: "https://blooddonation-bc0b9.firebaseio.com",
    projectId: "blooddonation-bc0b9",
    storageBucket: "blooddonation-bc0b9.appspot.com",
    messagingSenderId: "567938843952"
};
firebase.initializeApp(config);

function register() {
    var email = document.getElementById("Email").value;
    var password = document.getElementById('Password').value;

    alert(email + " " + password)

    firebase.auth().createUserWithEmailAndPassword(email, password).catch(function (error) {
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        // ...
        alert(errorMessage + "---" + errorCode);
    });
}

function login() {
    var email = document.getElementById("email").value;
    var password = document.getElementById('password').value;

    alert(email + " " + password);

    firebase.auth().signInWithEmailAndPassword(email, password).catch(function (error) {
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        // ...
    });
}

firebase.auth().onAuthStateChanged(function (user) {
    if (user) {
        alert("Logged in as " + firebase.auth().currentUser.email);
    } else {
        alert("Not logged in");
    }
});
