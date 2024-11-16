

class Paginator {
    constructor(controller, modelPageIndex, searchOptionSelectList, sortowanieOptionSelectList, pageSizeSelectList) {
        this.initialize(controller, modelPageIndex, searchOptionSelectList, sortowanieOptionSelectList, pageSizeSelectList);
    }

    initialize(controller, modelPageIndex, searchOptionSelectList, sortowanieOptionSelectList, pageSizeSelectList) {

        // Inicjalizacja pól
        let q = '';
        let pageSize = 10;
        let pageIndex = 0;
        let searchOption = '';
        let sortowanieOption = '';
        let linkUrl = '';


        // pole wyszukiwania
        let searchInput = document.getElementById('searchInput');


        // SortowanieOption
        // pobieranie zaznaczonego elementu z listy SortowanieOptions i przekazywanie go do zmiennej, która jest przekazywana do atrybutu przycisku
        if (searchOptionSelectList) {
            searchOption = searchOptionSelectList.value;
            searchOptionSelectList.addEventListener('change', function () {
                searchOption = this.value;
            });
        }




        // SortowanieOption
        // pobieranie zaznaczonego elementu z listy SortowanieOptions i przekazywanie go do zmiennej, która jest przekazywana do atrybutu przycisku
        sortowanieOption = sortowanieOptionSelectList.value;
        sortowanieOptionSelectList.addEventListener('change', function () {
            sortowanieOption = this.value;

            q = searchInput.value;
            pageIndex = parseInt(modelPageIndex);

            // tutaj jest obsługa kodu Users napisana pod kontroller Users tylko dlatego, że tam jest dodatkowy select list, którego trzeba obsłużyć, poniższy kod w warunku else jest przeznaczony dla pozostałych kontrollerów
            if (controller == 'Users') {
                if (q.length > 0) {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchOption=${searchOption}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
                }
                else {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchOption=${searchOption}&SortowanieOption=${sortowanieOption}`;
                }
            }
            else {
                if (q.length > 0) {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
                }
                else {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SortowanieOption=${sortowanieOption}`;
                }
            }
            window.location.href = linkUrl;
        });




        // PageSize
        // pobieranie zaznaczonego elementu z listy SortowanieOptions i przekazywanie go do zmiennej, która jest przekazywana do atrybutu przycisku
        pageSize = parseInt(pageSizeSelectList.value); // może mieć 5, 10, 15 lub 20
        pageSizeSelectList.addEventListener('change', function () {
            pageSize = parseInt(this.value);
            q = searchInput.value;
            pageIndex = parseInt(modelPageIndex);

            // tutaj jest obsługa kodu Users napisana pod kontroller Users tylko dlatego, że tam jest dodatkowy select list, którego trzeba obsłużyć, poniższy kod w warunku else jest przeznaczony dla pozostałych kontrollerów
            if (controller == 'Users') {
                if (q.length > 0) {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchOption=${searchOption}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
                }
                else {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchOption=${searchOption}&SortowanieOption=${sortowanieOption}`;
                }
            }
            else {
                if (q.length > 0) {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
                }
                else {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SortowanieOption=${sortowanieOption}`;
                }
            }
            window.location.href = linkUrl;
        });



/*
        searchButton.addEventListener('click', function (event) {
            event.preventDefault();
            pageIndex = '1'; // musi być ustawiona pierwsza pozycja
            q = searchInput.value.toString();
            q = q.trim();

            // tutaj jest obsługa kodu Users napisana pod kontroller Users tylko dlatego, że tam jest dodatkowy select list, którego trzeba obsłużyć, poniższy kod w warunku else jest przeznaczony dla pozostałych kontrollerów
            if (controller == 'Users') {
                if (q.length > 0) {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchOption=${searchOption}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
                }
                else {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchOption=${searchOption}&SortowanieOption=${sortowanieOption}`;
                }
            }
            else {
                if (q.length > 0) {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
                }
                else {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SortowanieOption=${sortowanieOption}`;
                }
            }
            window.location.href = linkUrl;
        });




        selectButton.addEventListener('click', function (event) {
            event.preventDefault();
            q = searchInput.value;
            pageIndex = parseInt(modelPageIndex);

            // tutaj jest obsługa kodu Users napisana pod kontroller Users tylko dlatego, że tam jest dodatkowy select list, którego trzeba obsłużyć, poniższy kod w warunku else jest przeznaczony dla pozostałych kontrollerów
            if (controller == 'Users') {
                if (q.length > 0) {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchOption=${searchOption}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
                }
                else {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchOption=${searchOption}&SortowanieOption=${sortowanieOption}`;
                }
            }
            else {
                if (q.length > 0) {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
                }
                else {
                    linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SortowanieOption=${sortowanieOption}`;
                }
            }
            window.location.href = linkUrl;
        });
*/
    }
}

