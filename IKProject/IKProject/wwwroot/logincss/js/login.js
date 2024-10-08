//const loginText = document.querySelector(".title-text .login");
//const loginForm = document.querySelector("form.login");
//const loginBtn = document.querySelector("label.login");
//const signupBtn = document.querySelector("label.signup");
//const signupLink = document.querySelector("form .signup-link a");
//const registrationForm = document.querySelector("form.signup");

//// Form Ge�i�lerini Y�netme
//signupBtn.addEventListener('click', () => {
//    loginForm.style.marginLeft = "-50%";
//    loginText.style.marginLeft = "-50%";
//});

//loginBtn.addEventListener('click', () => {
//    loginForm.style.marginLeft = "0%";
//    loginText.style.marginLeft = "0%";
//});

//signupLink.addEventListener('click', (event) => {
//    event.preventDefault(); // Linkin varsay�lan davran���n� engelle
//    signupBtn.click(); // Kay�t butonuna t�kla
//});

//// Kay�t Formunu Y�netme
//if (registrationForm) {
//    registrationForm.addEventListener('submit', async (event) => {
//        event.preventDefault(); // Formun normal �ekilde g�nderilmesini engelle

//        const formData = new FormData(registrationForm);
//        const response = await fetch(registrationForm.action, {
//            method: 'POST',
//            body: new URLSearchParams(formData), // Form verilerini URLSearchParams olarak g�nder
//            headers: {
//                'Content-Type': 'application/x-www-form-urlencoded' // Form verilerini uygun �ekilde g�nder
//            }
//        });

//        if (response.ok) {
//            const result = await response.json();
//            alert(result.message || 'Kay�t ba�ar�l�!'); // API'den gelen mesaj� g�ster
//            registrationForm.reset(); // Formu s�f�rla
//            loginForm.style.marginLeft = "0%"; // Giri� formuna d�n
//            loginText.style.marginLeft = "0%"; // Ba�l�k metnine d�n
//        } else {
//            const error = await response.json();
//            alert(error.message || 'Kay�t s�ras�nda bir hata olu�tu.'); // API'den gelen hata mesaj�n� g�ster
//        }
//    });
//}





const loginText = document.querySelector(".title-text .login");
const loginForm = document.querySelector("form.login");
const signupForm = document.querySelector("form.signup");
const signupBtn = document.querySelector("label.signup");
const loginBtn = document.querySelector("label.login");
const signupLink = document.querySelector("form .signup-link a");

// Form Ge�i�lerini Y�netme
signupBtn.addEventListener('click', () => {
    loginForm.style.marginLeft = "-50%";
    loginText.style.marginLeft = "-50%";
});

loginBtn.addEventListener('click', () => {
    loginForm.style.marginLeft = "0%";
    loginText.style.marginLeft = "0%";
});

signupLink.addEventListener('click', (event) => {
    event.preventDefault(); // Linkin varsay�lan davran���n� engelle
    signupBtn.click(); // Kay�t butonuna t�kla
});

// Kay�t Formunu Y�netme
if (signupForm) {
    signupForm.addEventListener('submit', async (event) => {
        event.preventDefault(); // Formun normal �ekilde g�nderilmesini engelle

        const formData = new FormData(signupForm);
        const response = await fetch(signupForm.action, {
            method: 'POST',
            body: new URLSearchParams(formData),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });

        if (response.ok) {
            alert('Kay�t ba�ar�l�!'); // Ba�ar� mesaj�n� g�ster
            window.location.href = '/Account/Login'; // Ba�ar�yla y�nlendir
        } else {
            const error = await response.json();
            alert(error.message || 'Kay�t s�ras�nda bir hata olu�tu.'); // Hata mesaj�n� g�ster
        }
    });
}