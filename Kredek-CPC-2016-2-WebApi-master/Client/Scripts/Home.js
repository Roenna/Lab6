function allStudentsToList() {
    $(document).ready(function () {
        // Wysyłanie tzw. AJAX request
        var answer = $.getJSON('http://localhost:56658/api/students')

        answer.done(function (data) {
            // Metoda 'done' jest wywoływana, gdy AJAX request zakończy się sukcesem
            // 'each' - funkcja, która dla każdego elementu z listy 'data' wywoła funkcję
            $.each(data, function (key, item) {
                // Żebył ładniej to wyglądało, to ja sobie stworzyłem funkcję listItem()
                $('#students').append(listItem(item));
            });
        });
    });
}

function listItem(item) {
    /* Ta funkcje zwraca string zawierający tag <li> (list item)
        A w tym tagu jest <a>, który tworzy hiperlink
        W tym konkretnym przypadku tworzy hiperlink to szczegółów konkretnego Studenta
    */
    href = '/Main/Details/' + item.Id;
    return '<li><a href="' + href + '">' + formatItem(item) + '</a> </li>';
}

function formatItem(item) {
    //Kolejne uproszczenie: funkcja która zwraca Imię Nazwisko ;-)
    return item.Name + ' ' + item.Surname;
}
function getDetails(id) {
    $(document).ready(function () {
        // Znów AJAX request
        $.getJSON('http://localhost:56658/api/students/' + id)
            .done(function (data) {
                /*
                Tutaj jest 'trick': nie tworzę obiektu 'answer', 
                tylko od razu na rzecz 'getJSON' wywołuję 'done'
                To rozwiązanie jest REKOMENDOWANE, ponieważ dzięki temu
                komputer nie musi wykonywać operacji przypisania
                Jedna operacja mniej.
                */
                $('#details').append(describeStudent(data));
            }).fail(function (jqXHR, textStatus, errorThrow) {
                /*
                .done się wywoła jak zapytanie się uda, jak nie, to wyświetli
                */
                console.log("dupa");
                console.log(textStatus);
            });
    });
}


function describeStudent(item) {
    // co oznaczaja klasy 'btn-...' poznacie w najbliższym tygodniu
    var name = '<div class="btn-primary">' + item.Name + '</div>'
    var surname = '<div class="btn-warning">' + item.Surname + '</div>'
    var email = '<div class="btn-success">' + item.Email + '</div>'
    var index = '<div class="btn-danger">' + item.Index + '</div>'
    var grades = '<ul class="btn-info">'
    // dodać  kolorowanie zależnie od oceny
    $.each(item.Grades, function (key, item) {
        grades += '<li class="btn btn-default">' + item.Value + '</li>'
    });
    grades += '</ul>'

    return name + surname + email + index + grades;
}
function addStudent() {
    $.ajax({
        url: 'http://localhost:56658/api/students',
        type: 'post',
        dataType: 'json',
        data: $("#newStudent").serializeArray(),
        success: function (data) {
            $('#students').append(listItem(data));
        }
    });
}
