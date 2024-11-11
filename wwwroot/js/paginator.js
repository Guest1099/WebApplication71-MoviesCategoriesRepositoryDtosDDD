

class Paginator {
    constructor(controller, modelPageIndex, sortowanieOptionSelectList) {

        initialize(controller, modelPageIndex);
    }


    function initialize(controller, modelPageIndex, sortowanieOptionSelectList) {

        // Inicjalizacja pól
        let q = '';
        let pageSize = 10;
        let pageIndex = 0;
        let sortowanieOption = '';
        let linkUrl = '';


        // pole wyszukiwania
        let searchInput = document.getElementById('searchInput');


        // SortowanieOption
        // pobieranie zaznaczonego elementu z listy SortowanieOptions i przekazywanie go do zmiennej, która jest przekazywana do atrybutu przycisku
        
        sortowanieOption = sortowanieOptionSelectList.value;
        sortowanieOptionSelectList.addEventListener('change', function () {
            sortowanieOption = this.value;
            alert('test');

            q = searchInput.value;
            pageIndex = parseInt(modelPageIndex);

            linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
            window.location.href = linkUrl;
        });

/*
        // PageSize
        // pobieranie zaznaczonego elementu z listy SortowanieOptions i przekazywanie go do zmiennej, która jest przekazywana do atrybutu przycisku
        let pageSizeSelectList = document.getElementById('pageSizeSelectList');
        pageSize = parseInt(pageSizeSelectList.value); // może mieć 5, 10, 15 lub 20
        pageSizeSelectList.addEventListener('change', function () {
            pageSize = parseInt(this.value);
            q = searchInput.value;
            pageIndex = parseInt(modelPageIndex);

            linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
            window.location.href = linkUrl;
        });


        let searchButton = document.getElementById('searchButton');
        searchButton.addEventListener('click', function (event) {
            event.preventDefault();
            pageIndex = '1'; // musi być ustawiona pierwsza pozycja
            q = searchInput.value.toString();
            q = q.trim();

            linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
            window.location.href = linkUrl;
        });*/
    }

}




/*

// Inicjalizacja pól
let q = '';
let pageSize = 10;
let pageIndex = 0;
let sortowanieOption = '';
let linkUrl = '';


// pole wyszukiwania
let searchInput = document.getElementById('searchInput');


// SortowanieOption
// pobieranie zaznaczonego elementu z listy SortowanieOptions i przekazywanie go do zmiennej, która jest przekazywana do atrybutu przycisku
let sortowanieOptionSelectList = document.getElementById('sortowanieOptionSelectList');
sortowanieOption = sortowanieOptionSelectList.value;
sortowanieOptionSelectList.addEventListener('change', function () {
    sortowanieOption = this.value;
    alert('test');

    q = searchInput.value;
    pageIndex = parseInt(modelPageIndex);

    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
    window.location.href = linkUrl;
});


// PageSize
// pobieranie zaznaczonego elementu z listy SortowanieOptions i przekazywanie go do zmiennej, która jest przekazywana do atrybutu przycisku
let pageSizeSelectList = document.getElementById('pageSizeSelectList');
pageSize = parseInt(pageSizeSelectList.value); // może mieć 5, 10, 15 lub 20
pageSizeSelectList.addEventListener('change', function () {
    pageSize = parseInt(this.value);
    q = searchInput.value;
    pageIndex = parseInt(modelPageIndex);

    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
    window.location.href = linkUrl;
});


let searchButton = document.getElementById('searchButton');
searchButton.addEventListener('click', function (event) {
    event.preventDefault();
    pageIndex = '1'; // musi być ustawiona pierwsza pozycja
    q = searchInput.value.toString();
    q = q.trim();

    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
    window.location.href = linkUrl;
});
    */