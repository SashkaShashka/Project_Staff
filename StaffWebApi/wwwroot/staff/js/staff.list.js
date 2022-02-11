import api from "/utils/api.js";
import { createRublesFormat } from "/utils/format.js";
import "/components/alert/alert.js";
import { createDateFormat } from "/utils/forms.js";

function getProfile() {
    return api.get("account");
}
function getStaffRequest(search, sortDate) {
    return api.get("Staff?" + "search=" + search + "&sortDate=" + sortDate);
}
function getStaffMiniRequest(search, sortDate) {
    return api.get("Staff/MiniList?" + "search=" + search + "&sortDate=" + sortDate);
}
function getStaffIDRequest(id) {
    return api.get("Staff/" + id);
}
function getRolesRequest() {
    return api.get("account/roles");
}

function deleteStaffRequest(id) {
    return api.delete("Staff/" + id);
}

function getTotalSalaryRequest() {
    return api.get("Staff/TotalSalary");
}

const list = document.querySelector("#staffs-container");
const fundSalary = document.querySelector('[name="fundSalary"]');
const templateStaff = document.querySelector("#templateStaff");
const templateStaffEmployee = document.querySelector("#templateStaffEmployee");

const templatePosition = document.querySelector("#templatePositionNotEmployee");
const templatePositionEmployee = document.querySelector("#templatePositionEmployee");

const sortDateButton = document.querySelector("#sortBirthday");
const searchStaffInput = document.querySelector("#searchStaffInput");
const searchStaffButton = document.querySelector("#searchStaff");
var sortDate = "asc";

const priceFormat = createRublesFormat(true);

const roles = await getRolesRequest();
const role = roles[0];
const userNameProfile = (await getProfile()).userName;

fillStaffForRole();

if (role !== "Admin") {
    document.querySelector('[name="addStaff"]').remove();
}

sortDateButton.onclick = () => {
    clickSortBirthday();
};

searchStaffButton.onclick = () => {
    fillStaffForRole();
};

async function fillStaffForRole() {
    if (role !== "Employee") {
        fillStaff();
    } else {
        fillStaffEmployee();
    }
}

async function clickSortBirthday() {
    updateSort();
    fillStaffForRole();
}

function updateSort() {
    if (sortDate === "asc") {
        sortDate = "desc";
        sortDateButton.innerHTML = '<i class="bi bi-sort-down-alt"></i> Сортировка по убыванию даты';
    } else {
        sortDate = "asc";
        sortDateButton.innerHTML = '<i class="bi bi-sort-up"></i> Сортировка по возрастанию даты';
    }
}

async function fillStaff() {
    list.innerHTML = '<div class="text-center my-3"><span class="spinner-border text-primary"></span></div>';
    try {
        const staffs = await getStaffRequest(searchStaffInput.value, sortDate);
        const totalSalary = await getTotalSalaryRequest();
        fundSalary.innerText = "Фонд заработной платы: " + priceFormat.format(totalSalary);

        list.innerHTML = "";

        for (let staff of staffs) {
            list.append(createStaffCard(staff));
        }
    } catch (err) {
        console.error(err);
        const alert = document.createElement("alert-message");
        alert.innerText = "Не удалось загрузить список сотрудников";
        list.replaceChildren(alert);
    }
}

async function fillStaffEmployee() {
    list.innerHTML = '<div class="text-center my-3"><span class="spinner-border text-primary"></span></div>';
    try {
        const staffs = await getStaffMiniRequest(searchStaffInput.value, sortDate);
        fundSalary.remove();

        list.innerHTML = "";

        for (let staff of staffs) {
            if (staff.user !== userNameProfile) {
                console.log("staff.user" + staff.user + "    " + "userNameProfile" + userNameProfile);
                list.append(createStaffCardEmployee(staff));
            } else {
                const cardStaff = createStaffCard(await getStaffIDRequest(staff.serviceNumber));
                const headCard = cardStaff.querySelector('[name="headCard"]');
                headCard.classList.remove("bg-white");
                headCard.classList.add("border-primary");
                list.prepend(cardStaff);
            }
        }
    } catch (err) {
        console.error(err);
        const alert = document.createElement("alert-message");
        alert.innerText = "Не удалось загрузить список сотрудников";
        list.replaceChildren(alert);
    }
}

function createStaffCardEmployee(staff) {
    console.log(staff.serviceNumber);
    const card = templateStaffEmployee.content.cloneNode(true);
    const cardId = card.querySelector('[name="id"]');
    const cardFio = card.querySelector('[name="FIO"]');
    const cardBirthday = card.querySelector('[name="birthday"]');

    const cardOpenWindow = card.querySelector('[name="openWindow"]');
    const cardWithOpenWindow = card.querySelector('[name="withOpenWindow"]');

    cardOpenWindow.setAttribute("href", "#openWindow" + staff.serviceNumber);
    cardWithOpenWindow.id = "openWindow" + staff.serviceNumber;
    const listPos = card.querySelector("#pos-container");

    cardId.innerHTML = "Табельный номер №" + staff.serviceNumber;
    cardFio.innerHTML = staff.surName + " " + staff.firstName + " " + staff.middleName;
    cardBirthday.innerHTML = "Дата рождения: " + createDateFormat(staff.birthDay);

    for (let post of staff.posts) {
        listPos.append(createPosCardEmployee(post));
    }
    return card;
}

function createStaffCard(staff) {
    console.log(staff.serviceNumber);
    const card = templateStaff.content.cloneNode(true);
    const cardId = card.querySelector('[name="id"]');
    const cardFio = card.querySelector('[name="FIO"]');
    const cardBirthday = card.querySelector('[name="birthday"]');
    const cardSalary = card.querySelector('[name="resultSalary"]');
    const cardUser = card.querySelector('[name="withUser"]');
    const cardEdit = card.querySelector('[name="edit"]');

    const cardOpenWindowDelete = card.querySelector('[name="openWindowDelete"]');
    const cardWindowOpenDelete = card.querySelector('[name="windowOpenDelete"]');

    const cardAddPosotion = card.querySelector('[name="addPosition"]');

    const cardRemove = card.querySelector('[name="remove"]');
    const cardOpenWindow = card.querySelector('[name="openWindow"]');
    const cardWithOpenWindow = card.querySelector('[name="withOpenWindow"]');

    cardOpenWindow.setAttribute("href", "#openWindow" + staff.serviceNumber);
    cardWithOpenWindow.id = "openWindow" + staff.serviceNumber;
    const listPos = card.querySelector("#pos-container");

    cardId.innerHTML = "Табельный номер №" + staff.serviceNumber;
    cardFio.innerHTML = staff.surName + " " + staff.firstName + " " + staff.middleName;
    cardBirthday.innerHTML = "Дата рождения: " + createDateFormat(staff.birthDay);

    for (let post of staff.posts) {
        listPos.append(createPosCard(post));
    }
    if (staff.user != "") {
        cardUser.innerHTML = "Связанный пользователь: " + staff.user;
    } else {
        cardUser.innerHTML = "Связанный пользователь не задан";
    }
    cardSalary.innerHTML = "Заработная плата: " + priceFormat.format(staff.salary);
    if (role === "Admin") {
        cardOpenWindowDelete.setAttribute("data-target", "#modal" + staff.serviceNumber);
        cardWindowOpenDelete.setAttribute("id", "modal" + staff.serviceNumber);
        cardEdit.href = "./staff/edit.html?serviceNumber=" + staff.serviceNumber;
        cardAddPosotion.href = "./staff/addPositions.html?serviceNumber=" + staff.serviceNumber;
        cardRemove.onclick = () => {
            deleteStaffRequest(staff.serviceNumber)
                .then(() => fillStaff())
                .catch((err) => {
                    console.error(err);
                    const alert = document.createElement("alert-message");
                    alert.innerText = `Не удалось удалить сотрудника ${staff.serviceNumber}`;
                    alert.scrollIntoView();
                    list.after(alert);
                });
        };
    } else {
        cardEdit.remove();
        cardAddPosotion.remove();
        cardOpenWindowDelete.remove();
    }

    return card;
}

function createPosCardEmployee(post) {
    const cardPos = templatePositionEmployee.content.cloneNode(true);
    const cardDivision = cardPos.querySelector('[name="division"]');
    const cardTitle = cardPos.querySelector('[name="titlePos"]');

    cardTitle.innerHTML = "Должность: " + post.title;
    cardDivision.innerHTML = "Подразделение: " + post.division;
    return cardPos;
}
function createPosCard(post) {
    const cardPos = templatePosition.content.cloneNode(true);
    const cardBet = cardPos.querySelector('[name="bet"]');
    const cardDivision = cardPos.querySelector('[name="division"]');
    const cardPosSalary = cardPos.querySelector('[name="salary"]');
    const cardTitle = cardPos.querySelector('[name="titlePos"]');
    const cardBetSalary = cardPos.querySelector('[name="betAndSalary"]');

    cardPosSalary.innerHTML = "Оклад: " + priceFormat.format(post.position.salary);
    cardTitle.innerHTML = "Должность: " + post.position.title;
    cardDivision.innerHTML = "Подразделение: " + post.position.division;
    cardBet.innerText = "Ставка: " + post.bet;
    cardBetSalary.innerText = "Заработная плата: " + priceFormat.format(post.position.salary * post.bet);

    return cardPos;
}
