//const loginText = document.querySelector(".title-text .login");
//const loginForm = document.querySelector("form.login");
//const loginBtn = document.querySelector("label.login");
//const signupBtn = document.querySelector("label.signup");
//const signupLink = document.querySelector("form .signup-link a");
//signupBtn.onclick = (() => {
//    loginForm.style.marginLeft = "-50%";
//    loginText.style.marginLeft = "-50%";
//});
//loginBtn.onclick = (() => {
//    loginForm.style.marginLeft = "0%";
//    loginText.style.marginLeft = "0%";
//});
//signupLink.onclick = (() => {
//    signupBtn.click();
//    return false;
//});







//const loginText = document.querySelector(".title-text .login");
//const loginForm = document.querySelector("form.login");
//const loginBtn = document.querySelector("label.login");
//const signupBtn = document.querySelector("label.signup");
//const signupLink = document.querySelector("form .signup-link a");
//const registrationForm = document.querySelector("form.signup");

//// Form Geçiþlerini Yönetme
//signupBtn.onclick = () => {
//    loginForm.style.marginLeft = "-50%";
//    loginText.style.marginLeft = "-50%";
//};

//loginBtn.onclick = () => {
//    loginForm.style.marginLeft = "0%";
//    loginText.style.marginLeft = "0%";
//};

//signupLink.onclick = () => {
//    signupBtn.click();
//    return false;
//};

//// Kayýt Formunu Yönetme
//if (registrationForm) {
//    registrationForm.addEventListener('submit', async (event) => {
//        event.preventDefault(); // Formun normal þekilde gönderilmesini engelle

//        const formData = new FormData(registrationForm);
//        const response = await fetch(registrationForm.action, {
//            method: 'POST',
//            body: formData
//        });

//        if (response.ok) {
//            const result = await response.json();
//            alert('Kayýt baþarýlý!');
//            registrationForm.reset(); // Formu sýfýrla
//            loginForm.style.marginLeft = "0%"; // Giriþ formuna dön
//            loginText.style.marginLeft = "0%"; // Baþlýk metnine dön
//        } else {
//            alert('Kayýt sýrasýnda bir hata oluþtu.');
//        }
//    });
//}







const loginText = document.querySelector(".title-text .login");
const loginForm = document.querySelector("form.login");
const loginBtn = document.querySelector("label.login");
const signupBtn = document.querySelector("label.signup");
const signupLink = document.querySelector("form .signup-link a");
const registrationForm = document.querySelector("form.signup");

// Form Geçiþlerini Yönetme
signupBtn.addEventListener('click', () => {
    loginForm.style.marginLeft = "-50%";
    loginText.style.marginLeft = "-50%";
});

loginBtn.addEventListener('click', () => {
    loginForm.style.marginLeft = "0%";
    loginText.style.marginLeft = "0%";
});

signupLink.addEventListener('click', (event) => {
    event.preventDefault(); // Linkin varsayýlan davranýþýný engelle
    signupBtn.click(); // Kayýt butonuna týkla
});

// Kayýt Formunu Yönetme
if (registrationForm) {
    registrationForm.addEventListener('submit', async (event) => {
        event.preventDefault(); // Formun normal þekilde gönderilmesini engelle

        const formData = new FormData(registrationForm);
        const response = await fetch(registrationForm.action, {
            method: 'POST',
            body: new URLSearchParams(formData), // Form verilerini URLSearchParams olarak gönder
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded' // Form verilerini uygun þekilde gönder
            }
        });

        if (response.ok) {
            const result = await response.json();
            alert(result.message || 'Kayýt baþarýlý!'); // API'den gelen mesajý göster
            registrationForm.reset(); // Formu sýfýrla
            loginForm.style.marginLeft = "0%"; // Giriþ formuna dön
            loginText.style.marginLeft = "0%"; // Baþlýk metnine dön
        } else {
            const error = await response.json();
            alert(error.message || 'Kayýt sýrasýnda bir hata oluþtu.'); // API'den gelen hata mesajýný göster
        }
    });
}