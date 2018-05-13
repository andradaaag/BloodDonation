// Initialize Firebase
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
        alert(errorMessage + "---" + errorCode);
    });
}

firebase.auth().onAuthStateChanged(
function login2(user) {
    var uid;
    if (user) {
        uid= user.uid;
        $('#UID').val(uid);
        //document.getElementById('UID').value = uid;
        
        alert("Logged in as " + firebase.auth().currentUser.email + " uid: " + uid);
    } else {
        alert("Not logged in");
    }

    var xhttp = new XMLHttpRequest();
    
    xhttp.open("POST", "login/test", true);
    xhttp.setRequestHeader("Content-type", "application/text");
    xhttp.onreadystatechange = function () {
        if ( this.readyState == 4 &&this.status == 200) {
           
           cFunction(this);
        }
    };
    xhttp.send(uid);
});

function cFunction(a) {
    console.log(a);
    switch (a.responseText) {
        case "admin":
            Redirect("/login/authenticated");
            break;
        case "donor":
            Redirect("/login/donor");
            break;
        case "doctor":
            Redirect("/login/doctor");
            break;
        case "personnel":
            Redirect("/login/personnel");
            break;

    }

    //TODO: redirect to proper role page
    console.log(a);
}

function Redirect(link) {
    console.log(window.location);
    window.location.replace(link);
}

firebase.auth().signOut().then(function () {
    alert("Signed out");
}).catch(function (error) {
    var errorCode = error.code;
    var errorMessage = error.message;
    // ...
    alert(errorMessage + "---" + errorCode);
});