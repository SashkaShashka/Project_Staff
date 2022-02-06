import api from "/utils/api.js";
import "/components/form.field/form.field.js";
import { setAlert, setSubmitting } from "/utils/forms.js";

function createProductsRequest(product) {
  return api.post("Positions", product);
}

const form = document.getElementById("form-product");

form.addEventListener("submit", (evt) => {
  evt.preventDefault();
  const form = evt.target;
  if (form.submitting) return;

  setSubmitting(form, true);
  setAlert(form, "");
  createProductsRequest({
    title: form["title"].value,
    division: form["division"].value,
    salary: +form["salary"].value,
  })
    .then(() => {
      setAlert(form, "Дожность успешно создана.", "success");
      setTimeout(() => (location = "/positions/index.html"));
    })
    .catch(() => setAlert(form, "Не удалось создать товар."))
    .finally(() => setSubmitting(form, false));
});
