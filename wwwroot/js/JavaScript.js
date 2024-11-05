




/*

 
        let searchInput = document.getElementById('searchInput');

        document.querySelectorAll('.paginator-item').forEach(link => {
            link.addEventListener('click', function () {

                let selectedValue = document.getElementById('selectList').value;
                let q = searchInput.value;

                // Tworzenie nowego URL z aktualnymi wartościami
                let newUrl = this.href + `?q=${encodeURIComponent(q)}&SortowanieOption=${encodeURIComponent(selectedValue)}`;

                window.location.href = newUrl; // Przejdź do nowego URL
            });
        });

        let selectList = document.getElementById('selectList');
        selectList.addEventListener('change', function () {
            let selectedValue = this.value;
            alert(selectedValue);
            document.querySelectorAll('.paginator-item').forEach(link => {
                link.setAttribute('asp-route-SortowanieOption', selectedValue);
                link.setAttribute('asp-route-q', searchInput.value); // poprawiono
            });
        }); 


 
    <script type="text/javascript">

        let selectList = document.getElementById('selectList');
        let selectListSelectedValue = selectList.value;
        selectList.addEventListener('change', function () {
            selectListSelectedValue = this.value;
        });

        function createButton() {
            let createElementA = document.createElement('a');

            createElementA.innerText = 'press button';
            createElementA.setAttribute('asp-action', 'Index');
            createElementA.setAttribute('asp-controller', 'Home');
            createElementA.id = 'aButton';
            createElementA.style.cursor = 'pointer';

            createElementA.addEventListener('click', function (event) {
                event.preventDefault();

                let searchInput = document.getElementById('searchInput');
                let searchInputValue = searchInput.value;

                createElementA.setAttribute('asp-route-q', searchInputValue);
                createElementA.setAttribute('asp-route-SortowanieOption', selectListSelectedValue);
                let linkHome = `/${this.getAttribute('asp-controller')}/${this.getAttribute('asp-action')}?q =${this.getAttribute('asp-route-q')}&& SortowanieOption =${this.getAttribute('asp-route-SortowanieOption')}`;

                this.setAttribute('href', linkHome);

                //this.setAttribute('href', '/Home/Index?TestA=AAAAAAAAA&TestB=BBBBBBBBB');
                window.location.href = this.href;
            });

            document.getElementById('container').appendChild(createElementA);
        }
        createButton();

    </script>*/




/*

 
        let searchInput = document.getElementById('searchInput');

        document.querySelectorAll('.paginator-item').forEach(link => {
            link.addEventListener('click', function () {

                let selectedValue = document.getElementById('selectList').value;
                let q = searchInput.value;

                // Tworzenie nowego URL z aktualnymi wartościami
                let newUrl = this.href + `?q=${encodeURIComponent(q)}&SortowanieOption=${encodeURIComponent(selectedValue)}`;

                window.location.href = newUrl; // Przejdź do nowego URL
            });
        });

        let selectList = document.getElementById('selectList');
        selectList.addEventListener('change', function () {
            let selectedValue = this.value;
            alert(selectedValue);
            document.querySelectorAll('.paginator-item').forEach(link => {
                link.setAttribute('asp-route-SortowanieOption', selectedValue);
                link.setAttribute('asp-route-q', searchInput.value); // poprawiono
            });
        }); 


 
    <script type="text/javascript">

        let selectList = document.getElementById('selectList');
        let selectListSelectedValue = selectList.value;
        selectList.addEventListener('change', function () {
            selectListSelectedValue = this.value;
        });

        function createButton() {
            let createElementA = document.createElement('a');

            createElementA.innerText = 'press button';
            createElementA.setAttribute('asp-action', 'Index');
            createElementA.setAttribute('asp-controller', 'Home');
            createElementA.id = 'aButton';
            createElementA.style.cursor = 'pointer';

            createElementA.addEventListener('click', function (event) {
                event.preventDefault();

                let searchInput = document.getElementById('searchInput');
                let searchInputValue = searchInput.value;

                createElementA.setAttribute('asp-route-q', searchInputValue);
                createElementA.setAttribute('asp-route-SortowanieOption', selectListSelectedValue);
                let linkHome = `/${this.getAttribute('asp-controller')}/${this.getAttribute('asp-action')}?q =${this.getAttribute('asp-route-q')}&& SortowanieOption =${this.getAttribute('asp-route-SortowanieOption')}`;

                this.setAttribute('href', linkHome);

                //this.setAttribute('href', '/Home/Index?TestA=AAAAAAAAA&TestB=BBBBBBBBB');
                window.location.href = this.href;
            });

            document.getElementById('container').appendChild(createElementA);
        }
        createButton();

    </script>*/