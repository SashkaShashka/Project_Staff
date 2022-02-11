import "/components/auth.header/auth.header.js";
import api from "/utils/api.js";

function getRolesRequest() {
    return api.get("account/roles");
}

export class SuperAuthHeader extends HTMLElement {
    constructor() {
        super();
        const header = document.createElement("header");
        header.classList.add("navbar", "navbar-expand-lg", "navbar-dark", "bg-primary");
        const head = document.createElement("div");
        head.classList.add("container", "flex-grow-5");
        const title = document.createElement("div");
        title.classList.add("navbar-brand");
        title.setAttribute("id", "titlePage");
        title.innerText = "Что-то";
        const nav = document.createElement("nav");
        const ul = document.createElement("ul");
        ul.classList.add("navbar-nav", "me-auto", "mx-sm-3");
        ul.setAttribute("id", "headNav");
        nav.append(ul);
        head.append(title);
        head.append(nav);
        var auth = document.createElement("auth-header");
        head.append(auth);
        header.append(head);
        this.append(header);
    }

    connectedCallback() {
        this.render();
    }
    render() {
        getRolesRequest().then((roles) => {
            const role = roles[0];
            const li = document.createElement("li");
            li.className = "nav-item";
            const a = document.createElement("a");
            a.classList.add("nav-link");
            li.append(a);

            const page = location.pathname.substring(1, location.pathname.indexOf("/", 1));

            const li1 = li.cloneNode(true);
            li1.firstChild.innerText = "Сотрудники";
            li1.firstChild.setAttribute("href", "/index.html");
            if (page === "staff" || page === "/") {
                li1.firstChild.classList.add("active");
            }

            const headNav = document.querySelector("#headNav");
            headNav.append(li1);

            if (role !== "Employee") {
                const li2 = li.cloneNode(true);
                li2.firstChild.innerText = "Должности";
                li2.firstChild.setAttribute("href", "/positions/index.html");
                if (page === "positions") {
                    li2.firstChild.classList.add("active");
                }
                headNav.append(li2);
                if (role === "Admin") {
                    const li3 = li.cloneNode(true);
                    li3.firstChild.innerText = "Пользователи";
                    li3.firstChild.setAttribute("href", "/users/index.html");
                    if (page === "users") {
                        li3.firstChild.classList.add("active");
                    }
                    headNav.append(li3);
                }
            }
        });
    }

    static get observedAttributes() {
        return ["value"];
    }

    set value(_value) {
        this.setAttribute("value", _value);
    }
    attributeChangedCallback(name, oldValue, newValue) {
        if (oldValue == newValue) return;
        switch (name) {
            case "value":
                const form = this.querySelector("#titlePage");
                console.log(form);
                form.innerHTML = newValue;
                break;
            default:
                this.input.setAttribute(name, newValue);
                break;
        }
    }
}

customElements.define("sauth-header", SuperAuthHeader);
