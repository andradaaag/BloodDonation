// Initialize Firebase
var config = {
    apiKey: "AIzaSyC3qNG0NkgojCMCJvRHYNtUdfhvvTB9uFk",
    authDomain: "blooddonation-d1f79.firebaseapp.com",
    databaseURL: "https://blooddonation-d1f79.firebaseio.com",
    projectId: "blooddonation-d1f79",
    storageBucket: "blooddonation-d1f79.appspot.com",
    messagingSenderId: "671278196930"
};
firebase.initializeApp(config);

function register() {
    var email = document.getElementById("email").value;
    var password = document.getElementById('password').value;

    alert(email + " " + password);

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
