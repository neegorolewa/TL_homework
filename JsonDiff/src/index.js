import { ui } from "./ui.js";
import { auth } from "./auth.js";
import { jsonComparator } from "./json-comparator.js";

const elements = {
    jsonForm: document.querySelector(`.main-form`),
    textareaOld: document.querySelector(`#oldJson`),
    textareaNew: document.querySelector(`#newJson`),
    resultBlock: document.querySelector(`.result`),
    jsonButton: document.querySelector(`.main-form button`),
    jsonErrorOld: document.querySelector(`#error-jsonOld`),
    jsonErrorNew: document.querySelector(`#error-jsonNew`),

    loginForm: document.querySelector(`#login-form`),
}

function init() {
    auth.init();
    ui.updateUI();
    jsonComparator.init();
    setupEventListener();
    ui.showPromoPage();
}


function setupEventListener() {
    ui.elements.logo.addEventListener(`click`, (e) => {
        e.preventDefault();
        ui.showPromoPage();
    })

    ui.elements.authLink.addEventListener(`click`, (e) => {
        e.preventDefault();
        if (auth.state.isAuthenticated) {
            auth.logout();
        } else {
            ui.showLoginPage();
        }
        ui.updateUI();
    })

    ui.elements.startLink.addEventListener(`click`, (e) => {
        e.preventDefault();
        ui.showFormPage();
    })

    elements.loginForm.addEventListener(`submit`, (e) => {
        e.preventDefault();
        const userName = ui.elements.loginInput.value.trim();

        if (!userName) {
            ui.elements.loginError.classList.remove(`hidden`);
            return;
        }

        auth.login(userName);
        ui.elements.loginInput.value = '';
        ui.elements.loginError.classList.add(`hidden`);
        ui.updateUI();
        ui.showPromoPage();
    });

    elements.jsonForm.addEventListener(`submit`, jsonComparator.handleJsonSubmit);
}

document.addEventListener(`DOMContentLoaded`, init);