import { JsonDiff } from "./json-diff.js";

const form = document.querySelector('.main-form');
const textareaOld = document.querySelector('#oldJson');
const textareaNew = document.querySelector('#newJson');
const resultBlock = document.querySelector('.result');
const button = document.querySelector(`.main-form button`);


form.addEventListener('submit', async (event) => {
    event.preventDefault();
    const defaultButtonHtml = button.innerHTML;
    button.innerHTML = `Loading...`;
    button.disabled = true;

    const oldValue = JSON.parse(textareaOld.value);
    const newValue = JSON.parse(textareaNew.value);

    const result = await JsonDiff.create(oldValue, newValue);
    button.innerHTML = defaultButtonHtml;
    const jsonResult = JSON.stringify(result, undefined, 2);

    resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
    const classList = resultBlock.classList;

    const resultVisibleClass = 'result__visible';

    if (!classList.contains(resultVisibleClass)){
        classList.add(resultVisibleClass)
    }
});