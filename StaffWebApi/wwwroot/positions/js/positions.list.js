import api from "/utils/api.js";
import { createRublesFormat } from "/utils/format.js";
import "/components/alert/alert.js";

function getProductsRequest() {
  return api.get("Positions");
}

function deleteProductsRequest(id) {
  return api.delete("Positions/" + id);
}

const list = document.querySelector("#products-container");
const template = document.querySelector("#products-template");
const priceFormat = createRublesFormat(true);

fillProducts();

async function fillProducts() {
  list.innerHTML =
    '<div class="text-center my-3"><span class="spinner-border text-primary"></span></div>';
  try {
    const products = await getProductsRequest();
    list.innerHTML = "";

    for (let product of products) {
      list.append(createProductCard(product));
    }
  } catch (err) {
    console.error(err);
    const alert = document.createElement("alert-message");
    alert.innerText = "Не удалось загрузить список товаров";
    list.replaceChildren(alert);
  }
}
function createProductCard(position) {
  console.log(position.id);
  const card = template.content.cloneNode(true);
  const cardId = card.querySelector('[name="id"]');
  const cardPosition = card.querySelector('[name="position"]');
  const cardDivision = card.querySelector('[name="division"]');
  const cardSalary = card.querySelector('[name="salary"]');
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
  cardEdit.href = "./edit.html?id=" + position.id;
  cardRemove.onclick = () => {
    deleteProductsRequest(position.id)
      .then(() => fillProducts())
      .catch((err) => {
        console.error(err);
        const alert = document.createElement("alert-message");
        alert.innerText = `Не удалось удалить товар ${position.id} (${position.id})`;
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
