//function toggleForm(formType) {
//    const loginForm = document.getElementById('login-form');
//    const signupForm = document.getElementById('signup-form');
//    const loginButton = document.getElementById('login-toggle');
//    const signupButton = document.getElementById('signup-toggle');

//    if (formType === 'login') {
//        loginForm.style.display = 'block';
//        signupForm.style.display = 'none';
//        loginButton.classList.add('active');
//        signupButton.classList.remove('active');
//    } else if (formType === 'signup') {
//        loginForm.style.display = 'none';
//        signupForm.style.display = 'block';
//        loginButton.classList.remove('active');
//        signupButton.classList.add('active');
//    }
//}

//// Sayfa yüklendiðinde varsayýlan olarak giriþ formunu göster
//document.addEventListener('DOMContentLoaded', function () {
//    toggleForm('login');
//});



const loginText = document.querySelector(".title-text .login");
const loginForm = document.querySelector("form.login");
const loginBtn = document.querySelector("label.login");
const signupBtn = document.querySelector("label.signup");
const signupLink = document.querySelector("form .signup-link a");
signupBtn.onclick = (() => {
    loginForm.style.marginLeft = "-50%";
    loginText.style.marginLeft = "-50%";
});
loginBtn.onclick = (() => {
    loginForm.style.marginLeft = "0%";
    loginText.style.marginLeft = "0%";
});
signupLink.onclick = (() => {
    signupBtn.click();
    return false;
});
