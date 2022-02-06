import api from "/utils/api.js";
import "/components/form.field/form.field.js";
import { setAlert, setSubmitting } from "/utils/forms.js";

function createPositionsRequest(position) {
  return api.post("Positions", position);
}

const form = document.getElementById("form-position");

form.addEventListener("submit", (evt) => {
  evt.preventDefault();
  const form = evt.target;
  if (form.submitting) return;

  setSubmitting(form, true);
  setAlert(form, "");
  createPositionsRequest({
    title: form["title"].value,
    division: form["division"].value,
    salary: +form["salary"].value,
  })
    .then(() => {
      setAlert(form, "Дожность успешно создана.", "success");
      setTimeout(() => (location = "/positions/index.html"));
    })
    .catch(() => setAlert(form, "Не удалось создать должность."))
    .finally(() => setSubmitting(form, false));
});
