import { JsonDiff } from "./json-diff.js";

export const jsonComparator = {
    elements: null,
    defaultButtonText: "Show difference",

    init() {
        this.elements = {
            jsonForm: document.querySelector(`.main-form`),
            textareaOld: document.querySelector(`#oldJson`),
            textareaNew: document.querySelector(`#newJson`),
            resultBlock: document.querySelector(`.result`),
            jsonButton: document.querySelector(`.main-form button`),
            jsonErrorOld: document.querySelector(`#error-jsonOld`),
            jsonErrorNew: document.querySelector(`#error-jsonNew`),
        };
        this.setupEventListeners();
    },

    setupEventListeners() {
        this.elements.jsonForm.addEventListener(`submit`, (e) => this.handleJsonSubmit(e));
    },

    isValidJSON(str) {
        try {
            JSON.parse(str);
            return true;
        } catch {
            return false;
        }
    },

    async handleJsonSubmit(e) {
        e.preventDefault();

        const oldJsonText = this.elements.textareaOld.value;

        const newJsonText = this.elements.textareaNew.value;

        let hasError = false;

        if (!this.isValidJSON(oldJsonText)) {
            this.elements.jsonErrorOld.classList.remove(`hidden`);
            hasError = true;
        }

        if (!this.isValidJSON(newJsonText)) {
            this.elements.jsonErrorNew.classList.remove(`hidden`);
            hasError = true;
        }

        if (hasError) {
            this.elements.jsonButton.textContent = this.defaultButtonText;
            this.elements.jsonButton.disabled = false;
            return;
        }

        const defaultButtonHtml = this.elements.jsonButton.innerHTML;
        this.elements.jsonButton.innerHTML = `Loading...`;
        this.elements.jsonButton.disabled = true;

        const oldValue = JSON.parse(oldJsonText);
        const newValue = JSON.parse(newJsonText);

        const result = await JsonDiff.create(oldValue, newValue);
        this.elements.jsonButton.innerHTML = defaultButtonHtml;
        const jsonResult = JSON.stringify(result, undefined, 2);

        this.elements.resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
        const classList = this.elements.resultBlock.classList;

        const resultVisibleClass = `result__visible`;

        if (!classList.contains(resultVisibleClass)) {
            classList.add(resultVisibleClass);
        }

        this.elements.jsonErrorOld.classList.add(`hidden`);
        this.elements.jsonErrorNew.classList.add(`hidden`);
        this.elements.jsonButton.disabled = false;
    }
};