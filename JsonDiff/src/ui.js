import { auth } from "./auth.js";

export const ui = {
    elements: {
        logo: document.querySelector(`.logo`),
        authLink: document.querySelector(`#auth-link`),
        userGreeting: document.querySelector(`#user-greeting`),
        usernameSpan: document.querySelector(`#username`),
        startLink: document.querySelector(`#start-link`),
        promoPage: document.querySelector(`#promo-page`),
        formPage: document.querySelector(`#form-page`),
        loginPage: document.querySelector(`#login-page`),
        loginInput: document.querySelector(`#login-input`),
        loginError: document.querySelector(`#login-error`),
    },

    updateUI() {
        if (auth.state.isAuthenticated) {
            this.elements.authLink.textContent = "Log out";
            this.elements.userGreeting.classList.remove(`hidden`);
            this.elements.usernameSpan.textContent = auth.state.currentUser;
            this.elements.startLink.classList.remove(`hidden`);
        } else {
            this.elements.authLink.textContent = "Log in";
            this.elements.userGreeting.classList.add(`hidden`);
            this.elements.startLink.classList.add(`hidden`);
        }
    },

    showPromoPage() {
        this.elements.promoPage.classList.remove(`hidden`);
        this.elements.formPage.classList.add(`hidden`);
        this.elements.loginPage.classList.add(`hidden`);
    },

    showFormPage() {
        this.elements.formPage.classList.remove(`hidden`);
        this.elements.promoPage.classList.add(`hidden`);
        this.elements.loginPage.classList.add(`hidden`);
    },

    showLoginPage() {
        this.elements.promoPage.classList.add(`hidden`);
        this.elements.formPage.classList.add(`hidden`);
        this.elements.loginPage.classList.remove(`hidden`);
        this.elements.loginInput.focus();
    }
};