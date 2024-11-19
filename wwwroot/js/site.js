 

// rozwijane menu
function initializeDropdownMenu() {
    var dropdowns = document.querySelectorAll('.dropdown');
    dropdowns.forEach(function (dropdown) {
        var dropbtn = dropdown.querySelector('.dropbtn');
        var content = dropdown.querySelector('.dropdown-content');

        dropbtn.addEventListener('click', function () {
            closeAllDropdowns();
            content.classList.toggle('show');
        });

        window.addEventListener('click', function (event) {
            if (!event.target.matches('.dropbtn')) {
                if (content.classList.contains('show')) {
                    content.classList.remove('show');
                }
            }
        });
    }); 
    function closeAllDropdowns() {
        dropdowns.forEach(function (otherDropdown) {
            var otherContent = otherDropdown.querySelector('.dropdown-content');
            if (otherContent.classList.contains('show')) {
                otherContent.classList.remove('show');
            }
        });
    }
}
document.addEventListener('DOMContentLoaded', function () {
    initializeDropdownMenu();
});




//************************************************************************** */
// Funkcje wykorzystywane podczas uploadingu zdjęcia
// create view
//************************************************************************** */
 



// czyście globalnie sesje
function clearSessions() {
    sessionStorage.clear();
}


// czyści wybrane sesje po zapisaniu rekordu
function clearSession() {
    sessionStorage.removeItem('uploadPhoto');
    sessionStorage.removeItem('uploadedFilesBase64');
}


function removeSessionPageIndex() {
    sessionStorage.removeItem('pageIndex');
}


function test() {
    alert('test');
}