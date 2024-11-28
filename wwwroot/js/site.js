 

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
// create and edit view
//************************************************************************** */
 

var filesInput = document.getElementById('files');
var imageContainer = document.getElementById('imageContainer');

// Funkcja do zapisywania przesłanych plików w sessionStorage
function saveFiles() {
    var files = filesInput.files;

    if (files.length > 0) {
        var filesArray = Array.from(files);
        var filesData = filesArray.map(file => {
            return {
                file: file,
                name: file.name,
                type: file.type,
                size: file.size,
                url: URL.createObjectURL(file)  // Zapisz URL obiektu
            };
        });

        sessionStorage.setItem('uploadedFiles', JSON.stringify(filesData));

        // Wyświetlenie wszystkich zdjęć
        displayPhotos(filesData);
    }
};


// Funkcja pobiera dane z sesji do zmiennej po załadowaniu okna
window.onload = function () {
    sessionStorage.removeItem('uploadedFiles');
    var uploadedFilesData = sessionStorage.getItem('uploadedFiles');
    if (uploadedFilesData) {

        var uploadedFiles = JSON.parse(uploadedFilesData); // zamienia tekst na obiekt

        // Utworzenie nowej listy plików
        var newFilesList = new DataTransfer();

        uploadedFiles.forEach(file => {
            // Utworzenie nowego pliku i dodanie go do listy plików
            var newFile = new File([], file.name, { type: file.type, size: file.size });
            newFilesList.items.add(newFile);
        });
        // Przypisanie nowej listy plików do pola input
        filesInput.files = newFilesList.files;

        // Wyświetlenie wszystkich zdjęć
        displayPhotos(uploadedFiles);
    }
};

function displayPhotos(source) {
    imageContainer.replaceChildren();
    for (let i = 0; i < source.length; i++) {
        let img = document.createElement('img');
        img.src = source[i].url;
        img.style.width = '150px';
        img.style.height = '120px';
        img.style.margin = '2px';

        let name = document.createElement('span');
        name.innerText = source[i].name;

        imageContainer.append(img);
        imageContainer.append(name);
    }
}




//************************************************************************** */
// Skrypt dla kontrollera Movies. Odpowiadają za zapisywanie danych w sesjach
// oraz wczytywaniem danych tekstowych oraz html do tagu textarea.
// Wystepują w akcji Create oraz Edit
//************************************************************************** */



let editor = document.getElementById('editor');
function saveToSession() {
    // czyszczenie sesji
    removeSessionTextValue();

    let textValue = editor.value;
    sessionStorage.setItem('textValue', textValue);
}

// pobranie danych z sesji i osadzenie ich w edytorze html
function getInnerHtmlFromSession() {
    let textValue = sessionStorage.getItem('textValue');
    if (textValue) {
        editor.value = textValue;
    }
}
getInnerHtmlFromSession();





//************************************************************************** */
//
//************************************************************************** */



// czyście globalnie sesje
function clearSessions() {
    sessionStorage.clear();
}


// czyści wybrane sesje po zapisaniu rekordu
function removeItemUploadedFiles() {
    sessionStorage.removeItem('uploadedFiles');
}


function removeSessionPageIndex() {
    sessionStorage.removeItem('pageIndex');
}


function removeSessionTextValue() {
    sessionStorage.removeItem('textValue');
}