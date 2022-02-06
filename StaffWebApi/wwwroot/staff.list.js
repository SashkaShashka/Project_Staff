import api from "/utils/api.js";
import { createRublesFormat } from "/utils/format.js";
import "/components/alert/alert.js";
import { createDateFormat } from "/utils/format.js";

function getStaffRequest() {
  return api.get("Staff");
}

function deleteProductsRequest(id) {
  return api.delete("Staff/" + id);
}

const list = document.querySelector("#products-container");
const templateStaff = document.querySelector("#templateStaff");
const templatePosition = document.querySelector("#templatePosition");
const priceFormat = createRublesFormat(true);

fillStaff();

async function fillStaff() {
  list.innerHTML =
    '<div class="text-center my-3"><span class="spinner-border text-primary"></span></div>';
  try {
    const staffs = await getStaffRequest();
    list.innerHTML = "";

    for (let staff of staffs) {
      list.append(createStaffCard(staff));
    }
  } catch (err) {
    console.error(err);
    const alert = document.createElement("alert-message");
    alert.innerText = "Не удалось загрузить список товаров";
    list.replaceChildren(alert);
  }
}
function createStaffCard(staff) {
  console.log(staff.serviceNumber);
  const card = templateStaff.content.cloneNode(true);
  const cardId = card.querySelector('[name="id"]');
  const cardFio = card.querySelector('[name="FIO"]');
  const cardBirthday = card.querySelector('[name="birthday"]');
  const cardSalary = card.querySelector('[name="resultSalary"]');
  const cardEdit = card.querySelector('[name="edit"]');
  const cardAddPosotion = card.querySelector('[name="addPosition"]');
  const cardRemove = card.querySelector('[name="remove"]');
  const cardOpenWindow = card.querySelector('[name="openWindow"]');
  const cardWithOpenWindow = card.querySelector('[name="withOpenWindow"]');
  console.log(cardOpenWindow.href);
  cardOpenWindow.setAttribute("href", "#openWindow" + staff.serviceNumber);
  cardWithOpenWindow.id = "openWindow" + staff.serviceNumber;
  const listPos = card.querySelector("#pos-container");
  if (cardId) {
    cardId.innerHTML = staff.serviceNumber;
  }
  if (cardFio) {
    cardFio.innerHTML =
      staff.surName + " " + staff.firstName + " " + staff.middleName;
  }
  console.log(staff.birthDay);
  if (cardBirthday) {
    cardBirthday.innerHTML =
      "Дата рождения: " + createDateFormat(staff.birthDay);
  }
  if (cardSalary) {
    cardSalary.innerHTML =
      "Заработная плата: " + priceFormat.format(staff.salary);
  }
  for (let post of staff.posts) {
    listPos.append(createPosCard(post));
  }
  cardEdit.href = "./edit.html?id=" + staff.serviceNumber;
  cardRemove.onclick = () => {
    deleteProductsRequest(staff.serviceNumber)
      .then(() => fillProducts())
      .catch((err) => {
        console.error(err);
        const alert = document.createElement("alert-message");
        alert.innerText = `Не удалось удалить товар ${staff.serviceNumber}`;
        alert.scrollIntoView();
        list.after(alert);
      });
  };
  /*
  cardRemove.addEventListener("submit", () => {
    console.log(position.id);
    deleteProductsRequest(position.id)
      .then(() => fillProducts())
      .catch((err) => {
        console.error(err);
        const alert = document.createElement("alert-message");
        alert.innerText = `Не удалось удалить товар ${position.id} (${position.title})`;
        alert.scrollIntoView();
        list.after(alert);
      });
  });
*/
  return card;
}

function createPosCard(post) {
  const cardPos = templatePosition.content.cloneNode(true);
  const cardBet = cardPos.querySelector('[name="bet"]');
  const cardPosSalary = cardPos.querySelector('[name="salary"]');
  const cardTitle = cardPos.querySelector('[name="titlePos"]');
  const cardBetSalary = cardPos.querySelector('[name="betAndSalary"]');

  if (cardPosSalary) {
    cardPosSalary.innerHTML =
      "Оклад: " + priceFormat.format(post.position.salary);
  }
  if (cardTitle) {
    cardTitle.innerHTML = post.position.division;
  }

  if (cardBet) {
    cardBet.innerText = "Ставка: " + post.bet;
  }
  console.log(priceFormat.format(post.position.salary * post.bet));
  if (cardBetSalary) {
    cardBetSalary.innerText = priceFormat.format(
      post.position.salary * post.bet
    );
  }
  return cardPos;
}
