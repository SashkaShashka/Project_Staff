import api from "/utils/api.js";
import { createRublesFormat } from "/utils/format.js";
import "/components/alert/alert.js";

function getPositionsRequest(search, filterDivision) {
    return api.get("Positions?" + "search=" + search + "&filterDivision=" + filterDivision);
}
function getDivisionsRequest() {
    return api.get("Positions/Divisions");
}

function deletePositionsRequest(id) {
    return api.delete("Positions/" + id);
}
function getRolesRequest() {
    return api.get("account/roles");
}

const roles = await getRolesRequest();
const role = roles[0];

const list = document.querySelector("#position-container");
const template = document.querySelector("#position-template");
const addPosition = document.querySelector("#createPosition");
const priceFormat = createRublesFormat(true);
const filterDivision = document.querySelector("#filterDivision");
const searchPosition = document.querySelector("#searchPosition");
const selectPosition = document.querySelector("#selectPosition");
selectPosition.onclick = () => {
    fillPositions();
};
var divisions;

if (role !== "Admin") {
    addPosition.remove();
}
getAllDivisions();

async function getAllDivisions() {
    divisions = await getDivisionsRequest();
    filterDivision.innerHTML = "";
    const firstOprion = document.createElement("option");
    firstOprion.innerText = "Фильтр по подразделению";
    firstOprion.setAttribute("value", "");
    filterDivision.append(firstOprion);
    for (let division of divisions) {
        const divisionOption = document.createElement("option");
        divisionOption.setAttribute("value", division);
        divisionOption.innerText = division;
        filterDivision.append(divisionOption);
    }
}

fillPositions();

async function fillPositions() {
    list.innerHTML = '<div class="text-center my-3"><span class="spinner-border text-primary"></span></div>';
    try {
        const positions = await getPositionsRequest(searchPosition.value, filterDivision.value);
        list.innerHTML = "";

        for (let position of positions) {
            list.append(createPositionCard(position));
        }
    } catch (err) {
        console.error(err);
        const alert = document.createElement("alert-message");
        alert.innerText = "Не удалось загрузить список должностей";
        list.replaceChildren(alert);
    }
}
function createPositionCard(position) {
    console.log(position.id);
    const card = template.content.cloneNode(true);
    const cardId = card.querySelector('[name="id"]');
    const cardPosition = card.querySelector('[name="position"]');
    const cardDivision = card.querySelector('[name="division"]');
    const cardSalary = card.querySelector('[name="salary"]');
    const cardOpenWindowDelete = card.querySelector('[name="openWindowDelete"]');
    const cardWindowOpenDelete = card.querySelector('[name="windowOpenDelete"]');
    const cardEdit = card.querySelector('[name="edit"]');
    const cardRemove = card.querySelector('[name="remove"]');

    cardId.innerHTML = "ID: " + position.id;
    cardPosition.innerHTML = "Должность: " + position.title;
    cardDivision.innerHTML = "Поразделение: " + position.division;
    cardSalary.innerHTML = "Оклад: " + priceFormat.format(position.salary);
    if (role === "Admin") {
        cardOpenWindowDelete.setAttribute("data-target", "#modal" + position.id);
        cardWindowOpenDelete.setAttribute("id", "modal" + position.id);
        cardEdit.href = "./edit.html?id=" + position.id;
        cardRemove.onclick = () => {
            getAllDivisions();
            deletePositionsRequest(position.id)
                .then(() => fillPositions())
                .catch((err) => {
                    console.error(err);
                    const alert = document.createElement("alert-message");
                    alert.innerText = `Не удалось удалить должность "${position.title}" c id=${position.id}`;
                    alert.scrollIntoView();
                    list.after(alert);
                });
        };
    } else {
        cardEdit.remove();
        cardOpenWindowDelete.remove();
        cardWindowOpenDelete.remove();
    }
    return card;
}
