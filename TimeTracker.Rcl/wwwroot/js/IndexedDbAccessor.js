export function initialize() {
    const timeTrackerIndexedDb = indexedDB.open(databaseName, currentVersion);
    timeTrackerIndexedDb.onupgradeneeded = function () {
        const db = timeTrackerIndexedDb.result;
        db.createObjectStore("WeekEntries", { keyPath: "id", autoIncrement: true }).createIndex("startDate", "startDate", { unique: true });
    };
}

let currentVersion = 1;
let databaseName = "TimeTracker";

export async function getByIndex(storeName, indexName, indexValue) {
    const request = new Promise((resolve) => {
        const timeTrackerIndexedDb = indexedDB.open(databaseName, currentVersion);
        timeTrackerIndexedDb.onsuccess = function () {
            const transaction = timeTrackerIndexedDb.result.transaction(storeName, "readonly");
            const collection = transaction.objectStore(storeName);
            const myIndex = collection.index(indexName);
            // ReSharper disable once DeclarationHides
            const result = myIndex.get(indexValue);
            result.onsuccess = function () {
                resolve(result.result);
            };
        };
    });
    const result = await request;
    return result;
}

export async function get(storeName, id) {
    const request = new Promise((resolve) => {
        const timeTrackerIndexedDb = indexedDB.open(databaseName, currentVersion);
        timeTrackerIndexedDb.onsuccess = function () {
            const transaction = timeTrackerIndexedDb.result.transaction(storeName, "readonly");
            const collection = transaction.objectStore(storeName);
            // ReSharper disable once DeclarationHides
            const result = collection.get(id);
            result.onsuccess = function () {
                resolve(result.result);
            };
        };
    });
    const result = await request;
    return result;
}

export async function getAll(storeName) {
    const request = new Promise((resolve) => {
        const timeTrackerIndexedDb = indexedDB.open(databaseName, currentVersion);
        timeTrackerIndexedDb.onsuccess = function () {
            const transaction = timeTrackerIndexedDb.result.transaction(storeName, "readonly");
            const collection = transaction.objectStore(storeName);
            // ReSharper disable once DeclarationHides
            const result = collection.getAll();
            result.onsuccess = function () {
                resolve(result.result);
            };
        };
    });
    const result = await request;
    return result;
}

export function add(storeName, value) {
    const timeTrackerIndexedDb = indexedDB.open(databaseName, currentVersion);
    timeTrackerIndexedDb.onsuccess = function () {
        const transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
        const collection = transaction.objectStore(storeName);
        // ReSharper disable once UnusedLocals
        const { id, ...newValue } = value;
        const addRequest = collection.add(newValue);
        addRequest.onerror = function (event) {
            console.log("Error! ", event);
        };
    };
}

export function put(storeName, value) {
    const timeTrackerIndexedDb = indexedDB.open(databaseName, currentVersion);
    timeTrackerIndexedDb.onsuccess = function () {
        const transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
        const collection = transaction.objectStore(storeName);
        collection.put(value);
    };
}

export async function remove(storeName, id) {
    const request = new Promise((resolve) => {
        const timeTrackerIndexedDb = indexedDB.open(databaseName, currentVersion);
        timeTrackerIndexedDb.onsuccess = function () {
            const transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
            const collection = transaction.objectStore(storeName);
            // ReSharper disable once DeclarationHides
            const result = collection.delete(id);
            result.onsuccess = function () {
                resolve(result.result);
            };
        };
    });
    const result = await request;
    return result;
}

export async function clear(storeName) {
    const request = new Promise((resolve) => {
        const timeTrackerIndexedDb = indexedDB.open(databaseName, currentVersion);
        timeTrackerIndexedDb.onsuccess = function () {
            const transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
            const collection = transaction.objectStore(storeName);
            // ReSharper disable once DeclarationHides
            const result = collection.clear();
            result.onsuccess = function () {
                resolve(result.result);
            };
        };
    });
    const result = await request;
    return result;
}
