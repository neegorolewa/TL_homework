import { JsonDiff } from "./json-diff.js";

const elements = {
    jsonForm: document.querySelector('.main-form'),
    textareaOld: document.querySelector('#oldJson'),
    textareaNew: document.querySelector('#newJson'),
    resultBlock: document.querySelector('.result'),
    jsonButton: document.querySelector(`.main-form button`),

    logo: document.querySelector(`.logo`),
    authLink: document.querySelector(`#auth-link`),
    userGreeting: document.querySelector(`#user-greeting`),
    usernameSpan: document.querySelector(`#username`),
    startLink: document.querySelector(`#start-link`),
    promoPage: document.querySelector(`#promo-page`),
    formPage: document.querySelector(`#form-page`),

    loginForm: document.querySelector(`#login-form`),
    loginInput: document.querySelector(`#login-input`),
    loginError: document.querySelector(`#login-error`),
    loginButton: document.querySelector(`#login-form button`),
    loginPage: document.querySelector(`#login-page`),
}

const state = {
    isAuthenticated: false,
    currentUser: null,
}

function init() {
    checkAuthStatus();
    setupEventListener();
    updateUI();
    showPromoPage();
}

function checkAuthStatus() {
    const savedUser = localStorage.getItem(`jsonDiffUser`);
    if (savedUser) {
        state.isAuthenticated = true;
        state.currentUser = savedUser;
    }
}

function updateUI() {
    if (state.isAuthenticated) {
        elements.authLink.textContent = "Log out";
        elements.userGreeting.classList.remove(`hidden`);
        elements.usernameSpan.textContent = state.currentUser;
        elements.startLink.classList.remove(`hidden`);
    } else {
        elements.authLink.textContent = "Log in";
        elements.userGreeting.classList.add(`hidden`);
        elements.startLink.classList.add(`hidden`);
        showLoginPage();
    }
}


function setupEventListener() {
    elements.logo.addEventListener(`click`, (e) => {
        e.preventDefault();
        showPromoPage();
    })

    elements.authLink.addEventListener(`click`, (e) => {
        e.preventDefault();
        if (state.isAuthenticated) {
            logout();
        } else {
            showLoginPage();
        }
    })

    elements.startLink.addEventListener(`click`, (e) => {
        e.preventDefault();
        showFormPage();
    })

    elements.jsonForm.addEventListener(`submit`, handleJsonSubmit);

    elements.loginForm.addEventListener(`submit`, handleLoginSubmit);
}

function handleLoginSubmit(e) {
    e.preventDefault();

    const userName = elements.loginInput.value.trim();

    if (!userName) {
        elements.loginError.classList.remove(`hidden`);
        return;
    }

    state.isAuthenticated = true;
    state.currentUser = userName;
    localStorage.setItem(`jsonDiffUser`, userName);

    elements.loginInput.value = '';
    elements.loginError.classList.add(`hidden`);

    updateUI();
    showPromoPage();
}

async function handleJsonSubmit(e) {
    e.preventDefault();

    const defaultButtonHtml = elements.jsonButton.innerHTML;
    elements.jsonButton.innerHTML = `Loading...`;
    elements.jsonButton.disabled = true;

    const oldValue = JSON.parse(elements.textareaOld.value);
    const newValue = JSON.parse(elements.textareaNew.value);

    const result = await JsonDiff.create(oldValue, newValue);
    elements.jsonButton.innerHTML = defaultButtonHtml;
    const jsonResult = JSON.stringify(result, undefined, 2);

    elements.resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
    const classList = elements.resultBlock.classList;

    const resultVisibleClass = 'result__visible';

    if (!classList.contains(resultVisibleClass)) {
        classList.add(resultVisibleClass)
    }
}

function logout() {
    state.isAuthenticated = false;
    state.currentUser = null;
    localStorage.removeItem(`jsonDiffUser`);
    updateUI();
}

function showPromoPage() {
    elements.promoPage.classList.remove(`hidden`);
    elements.formPage.classList.add(`hidden`);
    elements.loginPage.classList.add(`hidden`);
}

function showFormPage() {
    elements.formPage.classList.remove(`hidden`);
    elements.promoPage.classList.add(`hidden`);
    elements.loginPage.classList.add(`hidden`);
}

function showLoginPage() {
    elements.promoPage.classList.add(`hidden`);
    elements.formPage.classList.add(`hidden`);
    elements.loginPage.classList.remove(`hidden`)
    elements.log.focus();
}

document.addEventListener(`DOMContentLoaded`, init);