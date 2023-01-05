export function initialize() {
    let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    timeTrackerIndexedDb.onupgradeneeded = function () {
        let db = timeTrackerIndexedDb.result;
        db.createObjectStore("TimeEntries", { keyPath: "id", autoIncrement: true }).createIndex("date", "date", { unique: true });
        db.createObjectStore("WeekEntries", { keyPath: "id", autoIncrement: true }).createIndex("startDate", "startDate", { unique: true });
    }
}

let CURRENT_VERSION = 1;
let DATABASE_NAME = "TimeTracker";

export async function getByIndex(storeName, indexName, indexValue) {
    let request = new Promise((resolve) => {
        let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        timeTrackerIndexedDb.onsuccess = function () {
            let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readonly");
            let collection = transaction.objectStore(storeName);
            const myIndex = collection.index(indexName);
            let result = myIndex.get(indexValue);
            result.onsuccess = function () {
                resolve(result.result);
            }
        }
    });
    let result = await request;
    return result;
}

export async function get(storeName, id) {
    let request = new Promise((resolve) => {
        let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        timeTrackerIndexedDb.onsuccess = function () {
            let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readonly");
            let collection = transaction.objectStore(storeName);
            let result = collection.get(id);
            result.onsuccess = function () {
                resolve(result.result);
            }
        }
    });
    let result = await request;
    return result;
}

export async function getAll(storeName) {
    let request = new Promise((resolve) => {
        let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        timeTrackerIndexedDb.onsuccess = function () {
            let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readonly");
            let collection = transaction.objectStore(storeName);
            let result = collection.getAll();
            result.onsuccess = function () {
                resolve(result.result);
            }
        }
    });
    let result = await request;
    return result;
}

export function add(storeName, value) {
    let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    timeTrackerIndexedDb.onsuccess = function () {
        let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
        let collection = transaction.objectStore(storeName)
        let { id, ...newValue } = value;
        var addRequest = collection.add(newValue);
        addRequest.onerror = function (event) {
            console.log("Error! ", event)
        }
    }
}

export function put(storeName, value) {
    let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    timeTrackerIndexedDb.onsuccess = function () {
        let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
        let collection = transaction.objectStore(storeName)
        collection.put(value);
    }
}

export async function remove(storeName, id) {
    let request = new Promise((resolve) => {
        let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        timeTrackerIndexedDb.onsuccess = function () {
            let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
            let collection = transaction.objectStore(storeName);
            let result = collection.delete(id);
            result.onsuccess = function () {
                resolve(result.result);
            }
        }
    });
    let result = await request;
    return result;
}
