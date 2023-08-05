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
    darkModeSymbol.innerHTML = isDarkMode ? "&#9790;" : "&#9728;";

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
    console.log(hasil)
    let pok = "";
    $.each(hasil.results, (indeks, karakter) => {
        pok += `
            <tr>
                <td>${indeks + 1}</td>
                <td>${karakter.name}</td>
                <td>
                    <button onclick="detailPok('${karakter.url}')" type="button" class="btn btn-info" data-bs-toggle="modal" data-bs-target="#exampleModal">
                    Detail
                    </button>
                </td>
            </tr>
            `;
    });
    $("#apiPok").html(pok);
});

function detailPok(stringURL){
    $.ajax({
        url: stringURL,
        success: (hasil) => {
            const typesList = hasil.types.map((type) => {
                return `<span class="badge bg-warning">${type.type.name}</span>`;
            });
            $('#type').html(`Types: ${typesList.join(' ')}`);

            $('#img').attr('src', hasil.sprites.other['dream_world'].front_default);
            $('#name').text(hasil.name)
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
                return `<li>${stat.stat.name}: ${stat.base_stat}
                            <div class="progress mt-1">
                                <div class="progress-bar bg-danger progress-bar-striped progress-bar-animated" style="width: ${statPercentage}%;"></div>
                            </div>
                        </li>`;
            }).join('');
            $('#stats-list').html(statsHTML);

            // Calculate and display overall stats progress
            const totalStats = hasil.stats.reduce((total, stat) => total + Math.min(stat.base_stat, 100), 0);
            const overallStatsPercentage = (totalStats / (100 * 6)) * 100;
            $('#stats-progress').attr('style', `width: ${overallStatsPercentage}%`);
            $('.progress-text').text(`Total Stats ${overallStatsPercentage.toFixed(2)}%`);

            // Update base experience progress bar
            //const baseExperiencePercentage = (hasil.base_experience);
            //$('#experience-progress').attr('style', `width: ${baseExperiencePercentage}%`);
            //$('#experience-progress').attr('aria-valuenow', baseExperiencePercentage);
            //$('.progress-text').text(`${baseExperiencePercentage.toFixed(2)}%`);
        }
    });
}
