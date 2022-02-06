import api from "/utils/api.js";
import "/components/form.field/form.field.js";
import { setAlert, setSubmitting } from "/utils/forms.js";

function createStaffRequest(staff) {
  return api.post("Staff", staff);
}

const form = document.getElementById("form-staff");

form.addEventListener("submit", (evt) => {
  evt.preventDefault();
  const form = evt.target;
  if (form.submitting) return;

  setSubmitting(form, true);
  setAlert(form, "");
  createStaffRequest({
    surName: form["surName"].value,
    firstName: form["firstName"].value,
    middleName: form["middleName"].value,
    birthDay: form["birthDay"].value,
    User: form["userName"].value,
  })
    .then(() => {
      setAlert(form, "Сотрудник успешно создан.", "success");
      setTimeout(() => (location = "../index.html"));
    })
    .catch(() => setAlert(form, "Не удалось создать сотрудника."))
    .finally(() => setSubmitting(form, false));
});
