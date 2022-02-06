import api from "/utils/api.js";
import { createRublesFormat } from "/utils/format.js";
import "/components/alert/alert.js";

function getPositionsRequest() {
  return api.get("Positions");
}

function deletePositionsRequest(id) {
  return api.delete("Positions/" + id);
}

const list = document.querySelector("#position-container");
const template = document.querySelector("#position-template");
const priceFormat = createRublesFormat(true);

fillPositions();

async function fillPositions() {
  list.innerHTML =
    '<div class="text-center my-3"><span class="spinner-border text-primary"></span></div>';
  try {
    const positions = await getPositionsRequest();
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
  if (cardId) {
    cardId.innerHTML = position.id;
  }
  if (cardPosition) {
    cardPosition.innerHTML = position.title;
  }
  if (cardDivision) {
    cardDivision.innerHTML = position.division;
  }
  if (cardSalary) {
    cardSalary.innerHTML = priceFormat.format(position.salary);
  }
  if (cardOpenWindowDelete) {
    cardOpenWindowDelete.setAttribute("data-target", "#modal" + position.id);
  }
  if (cardWindowOpenDelete) {
    cardWindowOpenDelete.setAttribute("id", "modal" + position.id);
  }
  cardEdit.href = "./edit.html?id=" + position.id;
  cardRemove.onclick = () => {
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
  return card;
}
