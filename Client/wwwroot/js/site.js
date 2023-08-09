// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*function changeText() {
    document.getElementById('text').innerHTML = "WOW berhasil diubah";
}

let query1 = document.getElementById('color');
let query2 = document.getElementById('warna');

query2.addEventListener('click', () => {
    if (query1.classList.contains("blue")) {
        query1.classList.remove("blue")
    } else {
        query1.classList.add("blue")
    }
})

let query3 = document.getElementById('text');
query3.addEventListener('mouseover', () => {
    query3.style.backgroundColor = "pink";
})
query3.addEventListener('mouseout', () => {
    query3.style.backgroundColor = "white";
})*/

//-------------------------------------------------------------------------------
//bikin sebuah looping ke animals, 2 fungsi :
//fungsi 1: jika species nya 'cat' maka ambil lalu pindahkan ke variabel OnlyCat
//fungsi 2: jika species nya 'fish' maka ganti class -> menjadi 'non-mamalia'
//-------------------------------------------------------------------------------

/*const animals = [
    { name: "dory", species: "fish", class: { name: "vertebrata" } },
    { name: "tom", species: "cat", class: { name: "mamalia" } },
    { name: "nemo", species: "fish", class: { name: "vertebrata" } },
    { name: "umar", species: "cat", class: { name: "mamalia" } },
    { name: "gary", species: "fish", class: { name: "human" } },
];*/


// filter dan mapping
// Fungsi 1: Mengambil semua data 'cat' dan memindahkannya ke variabel OnlyCat
/*
function getOnlyCats(animals) {
    const onlyCat = animals.filter(animal => animal.species === 'cat');
    return onlyCat;
}

// Fungsi 2: Mengganti class menjadi 'non-mamalia' untuk semua data 'fish'
function changeFishClassToNonMamalia(animals) {
    const modifiedAnimals = animals.map(animal => {
        if (animal.species === 'fish') {
            animal.class.name = 'non-mamalia';
        }
        return animal;
    });
    return modifiedAnimals;
}

// Memanggil kedua fungsi dan menampilkan hasilnya
const onlyCat = getOnlyCats(animals);
const modifiedAnimals = changeFishClassToNonMamalia(animals);

console.log('Only Cats:', onlyCat);
console.log('Modified Animals:', modifiedAnimals);
*/

/*// pake perulangan
function filterCat(animals) {
    const onlyCat = [];
    animals.forEach(animal => {
        if (animal.species == 'cat') {
            onlyCat.push(animal);
        }
    });
    return onlyCat;
}

function changeClass(animals) {
    animals.forEach((animal) => {
        if (animal.species == "fish") {
            animal.class.name = "non-mamalia"
        }
    });
    return animals;
}

// output
const hanyaCat = filterCat(animals);
console.log("Only cats : ", hanyaCat);

const ubahClass = changeClass(animals);
console.log("Modified class : ", ubahClass);*/

// dark mode
function toggleDarkMode() {
    const body = document.body;
    body.classList.toggle("dark-mode");

    // Periksa apakah mode gelap aktif atau tidak
    const isDarkMode = body.classList.contains("dark-mode");

    // Ubah simbol sesuai status dark mode
    const darkModeSymbol = document.getElementById("dark-mode-symbol");
    darkModeSymbol.innerHTML = isDarkMode ? "&#127769;" : "&#9728;";

    // Terapkan gaya tabel berdasarkan mode gelap
    const tableRows = document.querySelectorAll("table tbody tr");
    tableRows.forEach((row) => {
        if (isDarkMode) {
            row.classList.add("dark-mode");
        } else {
            row.classList.remove("dark-mode");
        }
    });

    // Terapkan kelas dark-mode pada elemen modal
    const modal = document.getElementById("exampleModal");
    if (isDarkMode) {
        modal.classList.add("dark-mode");
    } else {
        modal.classList.remove("dark-mode");
    }
}

// Deteksi preferensi dark mode oleh browser dan aktifkan jika diinginkan
if (window.matchMedia && window.matchMedia("(prefers-color-scheme: dark)").matches) {
    toggleDarkMode();
}

// asynchronus javascript
// --Star Wars--
$.ajax({
    url: "https://swapi.dev/api/people/"
}).done((result) => {
    let temp = "";
    $.each(result.results, (index, character) => {
        temp += `
            <tr>
                <td>${index + 1}</td>
                <td>${character.name}</td>
                <td>${character.height}</td>
                <td>${character.mass}</td>
                <td>${character.gender}</td>
                <td>${character.birth_year}</td>
                <td>
                    <button onclick="detailSW('${character.url}')" type="button" class="btn btn-info" data-bs-toggle="modal" data-bs-target="#exampleModal">
                    Detail
                    </button>
                </td>
            </tr>
        `;
    });
    $("#listSW").html(temp);
});

/*function detailSW(stringURL) {
    $.ajax({
        url: stringURL
    }.done((result) => {

    })
    });
}*/

//------------------------------------------------------------------------------------------------------\\
// --Pokemon--
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((hasil) => {
    //console.log(hasil);
    let pok = "";
    $.each(hasil.results, (indeks, karakter) => {
        pok += `
            <tr>
                <td>${indeks + 1}</td>
                <td class="text-capitalize">${karakter.name}</td>
                <td>
                    <button onclick="detailPok('${karakter.url}')" type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal">
                    Detail
                    </button>
                </td>
            </tr>
            `;
    });
    $("#apiPok").html(pok);
});

const typeSymbols = {
    normal: '🔄',
    fighting: '🥊',
    flying: '🕊️',
    poison: '☠️',
    ground: '⛏️',
    rock: '🪨',
    bug: '🐛',
    ghost: '👻',
    steel: '🔧',
    fire: '🔥',
    water: '💧',
    grass: '🍃',
    electric: '⚡',
    psychic: '🔮',
    ice: '❄️',
    dragon: '🐉',
    dark: '🌑',
    fairy: '🧚',
    unknown: '❓',
    shadow: '🌑'
};
function detailPok(stringURL){
    $.ajax({
        url: stringURL,
        success: (hasil) => {
            const typesList = hasil.types.map((type) => {
                const typeName = type.type.name;
                const typeSymbol = typeSymbols[typeName] || ''; // Jika simbol tidak ditemukan, gunakan string kosong
                return `<span class="badge bg-warning">${typeSymbol} ${typeName}</span> ` ;
            });
            $('#type').html(`${typesList.join(' ')}`);

            $('#img').attr('src', hasil.sprites.other['dream_world'].front_default);
            //$('#img').attr('src', hasil.sprites.generation-v.black-white['animated'].front_default);
            $('#name').text(hasil.name);
            $('#weight').text(hasil.weight);
            $('#height').text(hasil.height);
            $('#experience-progress').text(hasil.base_experience);
          
            const abilitiesList = hasil.abilities.map((ability) => {
                return `<li>${ability.ability.name}</li>`;
            });
            $('#abilities').html(abilitiesList.join(''));
/*
            const stats = hasil.stats.map(stat => `<li>${stat.stat.name}: ${stat.base_stat}</li>`).join(' ');
            $('#statName').html(stats);*/

            // Calculate stats progress and display each stat with its base_stat
            const statsHTML = hasil.stats.map(stat => {
                const statPercentage = (Math.min(stat.base_stat, 100) / 100) * 100;
                return `<li>${stat.stat.name}
                            <div class="progress mt-1">
                                <div class="progress-bar bg-danger progress-bar-striped progress-bar-animated" style="width: ${statPercentage}%;">
                                <span>${stat.base_stat}%</span>
                                </div>
                            </div>
                             
                        </li>`;
            }).join('');
            $('#stats-list').html(statsHTML);

            // Calculate and display overall stats progress
            const totalStats = hasil.stats.reduce((total, stat) => total + Math.min(stat.base_stat, 100), 0);
            const overallStatsPercentage = (totalStats / (100 * 6)) * 100;
            $('#stats-progress').attr('style', `width: ${overallStatsPercentage}%`);
            $('.progress-text').text(`${overallStatsPercentage}%`);

        }
    });
}


//--------------------------------------------------------------------------------------
// --Employee--
$(document).ready(function () {
    let table = new DataTable('#tableEmployee', {
        ajax: {
            url: "https://localhost:7237/api/employees",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            //{ data: "guid" },
            {
                data: null,
                render: function (data, type, row, meta) {
                    // Use DataTable's rowIndex to display the sequence number
                    return meta.row + 1;
                }
            },
            { data: "nik" },
            { data: "firstname"},
            { data: "lastname" },
/*            {
                data: 'fullname',
                render: function (data, type, row) {
                    return row.firstname + ' ' + row.lastname;
                }
            },*/
            /*{
                data: "birtdate",
                render: function (data) {
                    return moment(data).format('DD-MM-YYYY');
                }
            },*/
            {
                data: 'gender',
                render: function (data, type, row) {
                    return data == 0 ? "Female" : "Male";
                }
            },
            {
                data: "hiringdate",
                render: function (data) {
                    return moment(data).format('DD-MM-YYYY');
                }
            },
            { data: "email" },
            { data: "phone" },
            {
                data: '',
                render: function (data, type, row) {
                    return `<button onclick="detailEmployee('${row.guid}')"
                            data-bs-toggle="modal" data-bs-target="#detailEmployee"
                            class="btn btn-warning"><i class="fas fa-eye"></i>
                            </button>

                            <button onclick="editEmployee('${row.guid}')"
                            data-bs-toggle="modal" data-bs-target="#editEmployee"
                            class="btn btn-primary"><i class="fas fa-edit"></i>
                            </button>

                            <button onclick="hapusEmployee('${row.guid}')"
                            data-bs-toggle="modal" data-bs-target="#hapusEmployee"
                            class="btn btn-danger"><i class="fas fa-trash-alt"></i>
                            </button>`;
                }
            }
        ],
        dom: 'Blfrtip',
        buttons: [
            {
                extend: 'colvis',
                title: 'Colvis',
                text: 'Column Visibility',
                className : 'btn btn-danger'
            },
            {
                extend: 'excelHtml5',
                title: 'Excel',
                text: 'Export to excel',
                /*Columns to export*/
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',
                title: 'PDF',
                text: 'Export to PDF',
                /*Columns to export*/
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'print',
                title: 'Print',
                text: 'Print Table',
                /*Columns to export*/
                exportOptions: {
                    columns: ':visible'
                }
            }
        ],
    });

});

function insertEmployee() {
    var emp = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    emp.firstname = $("#firstname").val();
    emp.lastname = $("#lastname").val();
    emp.birtdate = $("#birthdate").val();
    emp.gender = parseInt($("#gender").val());
    emp.hiringdate = $("#hiringdate").val();
    emp.email = $("#email").val();
    emp.phone = $("#phone").val();

    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7237/api/employees",
        type: "POST",
        data: JSON.stringify(emp), // Convert emp object to JSON string
        contentType: "application/json", // Set content type to JSON
        dataType: "json" // Expect JSON data in response
    }).done((result) => {
        // Success callback - show an alert or perform other actions
        alert("Employee added successfully!")
        location.reload();
    }).fail((error) => {
        // Error callback - show an alert or perform other actions
        alert("Failed to add employee. Please try again.");
    });
}


function editEmployee(guid) {
    var emp = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    emp.nik = $("editNik").val();
    emp.firstname = $("#editFirstname").val();
    emp.lastname = $("#editLastname").val();
    emp.birtdate = $("#editBirthdate").val();
    emp.gender = parseInt($("#editGender").val());
    emp.hiringdate = $("#editHiringdate").val();
    emp.email = $("#editEmail").val();
    emp.phone = $("#editPhone").val();

    $.ajax({
        url: "https://localhost:7237/api/employees/?guid=",
        type: 'PUT',  // Menggunakan metode PUT untuk mengedit data
        data: JSON.stringify(emp),  // Data yang ingin diedit
        success: function (response) {
            // Respons berhasil, lakukan sesuatu jika perlu
            console.log('Data karyawan berhasil diubah:', response);
        },
        error: function (xhr, status, error) {
            // Terjadi kesalahan saat mengirim permintaan
            console.error('Terjadi kesalahan:', error);
        }
    });
}

function hapusEmployee(guid)
{
    if (confirm("Apakah Anda yakin ingin menghapus karyawan ini?"))
    {
        $.ajax({
            url: "https://localhost:7237/api/employees/?guid=" + guid,
            type: 'DELETE',  // Menggunakan metode DELETE untuk menghapus data
            success: function (response) {
                // Respons berhasil, lakukan sesuatu jika perlu
                console.log('Data karyawan berhasil dihapus:', response)
                location.reload()
            },
            error: function (xhr, status, error) {
                // Terjadi kesalahan saat mengirim permintaan
                console.log('Terjadi kesalahan:', error);
            }
        });
    }
}

function detailEmployees(guid) {
    $.ajax({
        url: "https://localhost:7237/api/employees/?guid=",
        type: "GET",
        dataType: "json",
        success: function (data) {
            // Jika data berhasil diambil dari server
            // Lakukan proses tampilan data di sini
            var employeeList = $("#tableEmployee"); // Ganti dengan id elemen HTML yang sesuai

            // Kosongkan daftar sebelum menambahkan data baru
            employeeList.empty();

            // Iterasi melalui setiap objek employee dalam data
            data.forEach(function (employee) {
                // Buat elemen baru untuk setiap employee
                var employeeItem = $("<li>").text("Name: " + employee.firstname + " " + employee.lastname);

                // Tambahkan elemen ke daftar
                employeeList.append(employeeItem);
            });
        },
        error: function (error) {
            // Jika terjadi error saat mengambil data dari server
            // Lakukan penanganan error di sini
            alert("Failed to fetch employee data. Please try again.");
        }
    });
}




//Create pie or douhnut chart

$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7237/api/employees"
    }).done(function (result) { // Perhatikan pemakaian fungsi anonim di sini
        let gender1 = 0;
        let gender2 = 0; 

        result.data.forEach(function (dataGender) {
            if (dataGender.gender === 0) {
                gender1++;
            } else if (dataGender.gender === 1) {
                gender2++;
            }
        });

        console.log("Total Female", gender1)
        console.log("Total Male", gender2)

        // Buat data baru untuk grafik pie berdasarkan data gender yang dihitung
        var genderPieData = {
            labels: ['Female', 'Male'],
            datasets: [
                {
                    data: [gender1, gender2],
                    backgroundColor: ['pink', 'blue'],
                }
            ]
        };

        // Buat grafik pie
        new Chart('pieChart', {
            type: 'pie',
            data: genderPieData
        });
    });
});

$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7237/api/employees"
    }).done(function (result) { // Perhatikan pemakaian fungsi anonim di sini
        let age10Up = 0;
        let age20Up = 0;

        const currentDate = new Date()
        result.data.forEach(function (dataUmur) {
            const birthDate = new Date(dataUmur.birtdate);
            const age = currentDate.getFullYear() - birthDate.getFullYear();

            if (age >= 10 && age <= 20) {
                age10Up++;
            } else {
                age20Up++;
            }
        });

        // Buat data baru untuk grafik pie berdasarkan data age yang dihitung
        var ageBarData = {
            labels: ['10-20 tahun', 'Diatas 20 tahun'],
            datasets: [
                {
                    data: [age10Up, age20Up],
                    backgroundColor: ['grey', 'aqua'],
                }
            ]
        };

        // Buat opsi untuk menampilkan label pada sumbu x
        var ageBarOptions = {
            maintainAspectRatio: false,
            responsive: true,
            scales: {
                x: [
                    {
                        ticks: {
                            display: true // Menampilkan label pada sumbu x
                        }
                    }
                ]
            }
        };

        // Buat grafik bar
        new Chart('barChart', {
            type: 'bar',
            data: ageBarData,
            option: ageBarOptions
        });
    });
});




