const create = async (oldObj, newObj) => {
    const keysOld = Object.keys(oldObj);
    const keysNew = Object.keys(newObj);
    const allKeys = [...new Set([...keysOld, ...keysNew]).values()];

    const getType = (newValue, oldValue) => {
        if (oldValue === undefined) {
            return 'new';
        }

        if (newValue === undefined) {
            return 'deleted';
        }

        return oldValue === newValue ? 'unchanged' : 'changed';
    }

    const resultArray = allKeys.map((key) => {
        const newValue = newObj[key];
        const oldValue = oldObj[key];

        return {
            type: getType(newValue, oldValue),
            oldValue,
            newValue,
            key
        };
    });

    const result = resultArray.reduce((accumulator, { key, ...item }) => ({
        ...accumulator,
        [key]: item
    }), {});

    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve(result);
        }, 1000);
    });
};

export const JsonDiff = { create };