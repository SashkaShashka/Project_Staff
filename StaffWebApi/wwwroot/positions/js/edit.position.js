import api from "/utils/api.js";
import "/components/form.field/form.field.js";
import { setLoading, setAlert, setSubmitting } from "/utils/forms.js";

function getPositionsRequest(id) {
  return api.get("Positions/" + id);
}

function updatePositionsRequest(position) {
  return api.put("Positions/" + position.id, position);
}

const form = document.getElementById("form-position");
formLoad(form);

form.addEventListener("submit", (evt) => {
  evt.preventDefault();
  const form = evt.target;
  if (form.submitting) return;

  setSubmitting(form, true);
  setAlert(form, "");
  updatePositionsRequest({
    id: +new URLSearchParams(location.search).get("id"),
    title: form["title"].value,
    division: form["division"].value,
    salary: +form["salary"].value,
  })
    .then(() => {
      setAlert(form, "Должность успешно изменена", "success");
      setTimeout(() => (location = "/positions/index.html"));
    })
    .catch(() => setAlert(form, "Не удалось изменить должность"))
    .finally(() => setSubmitting(form, false));
});

function formLoad(form) {
  const position = new URLSearchParams(location.search).get("id");
  setLoading(form, true);
  setAlert(form, "");
  getPositionsRequest(position)
    .then((position) => {
      console.log(position);
      form["title"].value = position.title;
      form["division"].value = position.division;
      form["salary"].value = position.salary;
    })
    .catch(() =>
      setAlert(form, `Не удалось загрузить данные о должности с id: ${id}`)
    )
    .finally(() => setLoading(form, false));
}
