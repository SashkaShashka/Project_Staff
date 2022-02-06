import api from "/utils/api.js";
import "/components/form.field/form.field.js";
import { setLoading, setAlert, setSubmitting } from "/utils/forms.js";

function getStaffRequest(id) {
  return api.get("Staff/" + id);
}

function updateStaffRequest(staff) {
  return api.put("Staff/" + staff.serviceNumber, staff);
}

const form = document.getElementById("form-staff");
formLoad(form);

form.addEventListener("submit", (evt) => {
  evt.preventDefault();
  const form = evt.target;
  if (form.submitting) return;

  setSubmitting(form, true);
  setAlert(form, "");
  updateStaffRequest({
    serviceNumber: +new URLSearchParams(location.search).get("serviceNumber"),
    surName: form["surName"].value,
    firstName: form["firstName"].value,
    middleName: form["middleName"].value,
    birthDay: form["birthDay"].value,
    user: form["userName"].value,
  })
    .then(() => {
      setAlert(form, "Сотрудник успешно создан.", "success");
      setTimeout(() => (location = "../index.html"));
    })
    .catch(() => setAlert(form, "Не удалось создать сотрудника."))
    .finally(() => setSubmitting(form, false));
});

function formLoad(form) {
  const staff = new URLSearchParams(location.search).get("serviceNumber");
  setLoading(form, true);
  setAlert(form, "");
  getStaffRequest(staff)
    .then((staff) => {
      console.log(staff);
      form["surName"].value = staff.surName;
      form["firstName"].value = staff.firstName;
      form["middleName"].value = staff.middleName;
      form["birthDay"].value = staff.birthDay
        .substr(0, 10)
        .toLocaleString("ru-RU");
      form["userName"].value = staff.user;
    })
    .catch(() =>
      setAlert(
        form,
        `Не удалось загрузить данные о сотруднике ${serviceNumber}`
      )
    )
    .finally(() => setLoading(form, false));
}
