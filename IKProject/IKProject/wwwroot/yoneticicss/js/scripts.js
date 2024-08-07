document.addEventListener('DOMContentLoaded', function () {
    // Example of a simple JavaScript interaction for now

    // Add event listener for the logout button
    const logoutButton = document.querySelector('.logout-button');
    if (logoutButton) {
        logoutButton.addEventListener('click', function (event) {
            // Prevent the default action
            event.preventDefault();

            // Confirm the logout action
            if (confirm('Çýkýþ yapmak istediðinizden emin misiniz?')) {
                // Redirect to the logout URL
                window.location.href = logoutButton.href;
            }
        });
    }

    // Example of a simple theme toggle or other script can be added here
});