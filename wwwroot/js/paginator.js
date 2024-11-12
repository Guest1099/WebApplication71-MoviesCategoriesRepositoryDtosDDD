﻿


class Paginator {
    constructor(controller, modelPageIndex, sortowanieOptionSelectList, pageSizeSelectList, searchButton, selectButton) {
        this.initialize(controller, modelPageIndex, sortowanieOptionSelectList, pageSizeSelectList, searchButton, selectButton);
    }

    initialize(controller, modelPageIndex, sortowanieOptionSelectList, pageSizeSelectList, searchButton, selectButton) {

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

            q = searchInput.value;
            pageIndex = parseInt(modelPageIndex);

            if (q.length > 0) {
                linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
            }
            else {
                linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SortowanieOption=${sortowanieOption}`;
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

            if (q.length > 0) {
                linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
            }
            else {
                linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SortowanieOption=${sortowanieOption}`;
            }
            window.location.href = linkUrl;
        });


        searchButton.addEventListener('click', function (event) {
            event.preventDefault();
            pageIndex = '1'; // musi być ustawiona pierwsza pozycja
            q = searchInput.value.toString();
            q = q.trim();

            if (q.length > 0) {
                linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
            }
            else {
                linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SortowanieOption=${sortowanieOption}`;
            }
            window.location.href = linkUrl;
        });


        selectButton.addEventListener('click', function (event) {
            event.preventDefault();
            q = searchInput.value;
            pageIndex = parseInt(modelPageIndex);

            if (q.length > 0) {
                linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&q=${encodeURIComponent(q)}&SortowanieOption=${sortowanieOption}`;
            }
            else {
                linkUrl = `/${controller}/Index?PageSize=${pageSize}&PageIndex=${pageIndex}&SortowanieOption=${sortowanieOption}`;
            }
            window.location.href = linkUrl;
        });
    }
}

