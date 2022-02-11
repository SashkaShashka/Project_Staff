import api from "/utils/api.js";
import { createRublesFormat } from "/utils/format.js";
import "/components/alert/alert.js";
import { createDateFormat, setAlert, setSubmitting } from "/utils/forms.js";

function getStaffRequest(id) {
    return api.get("Staff/" + id);
}

function getPositionsRequest() {
    return api.get("Positions");
}
function putUpdatePositionRequest(positions) {
    return api.put("Staff/" + idPos + "/UpdatePosition", positions);
}
const form = document.getElementById("form-positions");
const table = document.querySelector("#table-positions");
const templateTable = document.querySelector("#row-table");
const priceFormat = createRublesFormat(true);
const idPos = +new URLSearchParams(location.search).get("serviceNumber");
const positions = {
    ServiceNumber: idPos,
    posts: [],
    addRemove(cardPoss, cardBet) {
        if (cardPoss.checked) {
            if (cardBet.value != "") {
                var post = {};
                post.position = +cardPoss.name;
                post.bet = +cardBet.value;
                this.posts.push(post);
            } else {
                cardPoss.checked = false;
            }
        } else {
            this.posts = this.posts.filter((post) => post.position != cardPoss.name);
        }
        console.log(this.posts);
    },
    addNewPosts(posts) {
        for (let post of posts) {
            var _post = {};
            _post.position = post.position.id;
            _post.bet = post.bet;
            this.posts.push(_post);
        }
    },
    updatePost(cardPoss, cardBet) {
        if (cardPoss.checked) {
            this.posts = this.posts.filter((post) => post.position != cardPoss.name);
            if (cardBet.value != "") {
                var post = {};
                post.position = +cardPoss.name;
                post.bet = +cardBet.value;
                this.posts.push(post);
            } else {
                cardPoss.checked = false;
            }
        }
    },
};

form.addEventListener("submit", (evt) => {
    evt.preventDefault();
    const form = evt.target;
    if (form.submitting) return;

    setSubmitting(form, true);
    setAlert(form, "");
    putUpdatePositionRequest(positions)
        .then(() => {
            setAlert(form, "Должности успешно отредактированы.", "success");
            setTimeout(() => (location = "../index.html"));
        })
        .catch(() => setAlert(form, "Не удалось редактировать должности."))
        .finally(() => setSubmitting(form, false));
});

fillAll();

async function fillAll() {
    table.innerHTML = '<div class="text-center my-3"><span class="spinner-border text-primary"></span></div>';
    try {
        const staffId = new URLSearchParams(location.search).get("serviceNumber");
        const staff = await getStaffRequest(staffId);
        table.innerHTML = "";
        positions.addNewPosts(staff.posts);
        const staffFIO = document.querySelector('[name="FIO"]');
        const staffBirthday = document.querySelector('[name="birthday"]');
        const staffUserName = document.querySelector('[name="user"]');
        staffFIO.innerHTML = staff.surName + " " + staff.firstName + " " + staff.middleName;
        staffBirthday.innerHTML = "Дата рождения: " + createDateFormat(staff.birthDay);
        if (staff.user != "") {
            staffUserName.innerHTML = "Связанный пользователь: " + staff.user;
        } else {
            staffUserName.innerHTML = "Связанный пользователь не задан";
        }
        let setUsePositions = new Set();
        let bets = [];
        setUsePositions.add(2);
        for (let post of staff.posts) {
            setUsePositions.add(post.position.id);
            bets.push(post.bet);
        }
        console.log(setUsePositions);
        const listPositions = await getPositionsRequest();
        for (let position of listPositions) {
            table.append(createPositionCard(position, setUsePositions, bets));
        }
    } catch (err) {
        console.error(err);
        const alert = document.createElement("alert-message");
        alert.innerText = "Не удалось загрузить список сотрудников";
        table.replaceChildren(alert);
    }
}
function createPositionCard(position, setUsePositions, bets) {
    const card = templateTable.content.cloneNode(true);
    const cardCheckbox = card.querySelector('[name="checkbox"]');
    const cardPosition = card.querySelector('[name="position"]');
    const cardDivision = card.querySelector('[name="division"]');
    const cardBet = card.querySelector('[name="bet"]');
    console.log(setUsePositions);
    if (cardCheckbox) {
        cardCheckbox.name = position.id;
        if (setUsePositions.has(position.id)) cardCheckbox.checked = true;
    }
    cardCheckbox.onclick = () => {
        positions.addRemove(cardCheckbox, cardBet);
    };
    if (cardPosition) {
        cardPosition.innerHTML = position.title;
    }
    if (cardDivision) {
        cardDivision.innerHTML = position.division;
    }
    if (cardCheckbox && cardCheckbox.checked == true) {
        cardBet.value = bets.shift();
    }
    cardBet.onchange = () => {
        positions.updatePost(cardCheckbox, cardBet);
    };
    return card;
}
